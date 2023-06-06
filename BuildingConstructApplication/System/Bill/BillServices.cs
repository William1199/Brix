using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;
using ViewModels.BillModels;
using ViewModels.Carts;
using ViewModels.Pagination;
using ViewModels.Response;
using ZedGraph;

namespace Application.System.Bill
{
    public class BillServices : IBillServices
    {
        private readonly BuildingConstructDbContext _context;
        private IHttpContextAccessor _accessor;
        public BillServices(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<BaseResponse<string>> CreateBill(List<BillDTO> requests)
        {
            bool flag = false;
            BaseResponse<string> response = new();
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var usID = identifierClaim.Value;
            var contracID = _context.Users.Where(x => x.Id.ToString().Equals(usID)).FirstOrDefault().ContractorId;


            //Checking quantities for product before create bill 

            foreach (var item in requests)
            {
                foreach (var billdetail in item.ProductBillDetail)
                {
                    var checkingProduct = _context.Products.FirstOrDefault(x => x.Id == billdetail.ProductId);

                    if (billdetail.TypeID == null)
                    {
                        if (checkingProduct != null)
                        {
                            if (billdetail.Quantity > checkingProduct.UnitInStock)
                            {
                                response.Data = checkingProduct.Id.ToString();
                                response.Code = BaseCode.ERROR;
                                response.Message = checkingProduct.Name+" Không đủ số lượng";
                                return response;
                            }
                        }
                    }
                    else
                    {
                        var checkingProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == billdetail.TypeID);

                        if (checkingProductType != null)
                        {
                            if (billdetail.Quantity > checkingProductType.Quantity)
                            {
                                response.Data = checkingProduct.Id.ToString();
                                response.Code = BaseCode.ERROR;
                                response.Message = checkingProduct.Name + " Không đủ số lượng";
                                return response;
                            }
                        }
                    }
                }
            }
            foreach (var r in requests)
            {
                var bill = new Data.Entities.Bill();
                bill.Note = r.Notes;
                bill.Status = r.Status;
                bill.StartDate = DateTime.Now;
                bill.EndDate = r.EndDate;
                bill.TotalPrice = r.TotalPrice;
                bill.ContractorId = contracID;
                bill.StoreID = r.StoreID;
                bill.CreateBy = Guid.Parse(usID);
                bill.LastModifiedAt = DateTime.Now;
                await _context.AddAsync(bill);
                var rs = await _context.SaveChangesAsync();
                if (rs > 0)
                {

                    foreach (var item in r.ProductBillDetail)
                    {
                        var billDetail = new BillDetail();
                        billDetail.BillID = bill.Id;
                        billDetail.ProductID = item.ProductId;
                        billDetail.ProductTypeId = item.TypeID;
                        billDetail.Quantity = item.Quantity;
                        billDetail.Price = item.Price;

                        _context.BillDetails.Add(billDetail);
                        var checkout = _context.SaveChanges();
                        if (checkout > 0)
                        {
                            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                            product.UnitInStock = product.UnitInStock - item.Quantity;
                            var pType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Id == item.TypeID);
                            if (pType != null)
                            {
                                pType.Quantity -= item.Quantity;
                                product.SoldQuantities += item.Quantity;
                            }

                            await _context.SaveChangesAsync();
                        }
                    }
                }
                flag = true;
            }

            IQueryable<Cart> query = _context.Carts.Where(x => x.UserID.ToString().Equals(usID.ToString()));
            StringBuilder existed = new();
            var count = requests.Count;

            List<Cart> ls = new();
            if (flag == true)
            {
                foreach (var item in requests)
                {
                    foreach (var pro in item.ProductBillDetail)
                    {
                        var product = _context.Carts.Where(x => x.ProductID == pro.ProductId && x.UserID.ToString().Equals(usID.ToString()) && x.TypeID == pro.TypeID).FirstOrDefault();
                        if (product != null)
                        {
                        ls.Add(product);

                        }
                    }
                }

                if (ls.Any())
                {
                    _context.RemoveRange(ls);
                    await _context.SaveChangesAsync();

                }
                response.Data = null;
                response.Code = BaseCode.SUCCESS;
                response.Message = "Đặt hàng thành công";
            }
            else
            {
                response.Data = null;
                response.Code = BaseCode.ERROR;
                response.Message = "Đặt hàng thất bại";
            }
            return response;
        }

        public async Task<BasePagination<List<BillDTO>>> GetAllBill(PaginationFilter filter)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var usID = identifierClaim.Value;
            var storeID = _context.Users.Where(x => x.Id.ToString().Equals(usID)).FirstOrDefault().MaterialStoreID;
            var contracID = _context.Users.Where(x => x.Id.ToString().Equals(usID)).FirstOrDefault().ContractorId;
            BasePagination<List<BillDTO>> response;
            var orderBy = filter._orderBy.ToString();
            int totalRecord;
            bool flag = false;

            List<Data.Entities.Bill>? data;
            var query = _context.Bills;

            if (string.IsNullOrEmpty(filter._sortBy))
            {
                filter._sortBy = "Id";
            }
            orderBy = orderBy switch
            {
                "1" => "ascending",
                "-1" => "descending",
                _ => orderBy
            };
            if (filter.FilterRequest != null && filter.FilterRequest.Status.HasValue)
            {
                data = await query
                    .Include(x => x.BillDetails)
                 .OrderBy(filter._sortBy + " " + orderBy)
                 .Skip((filter.PageNumber - 1) * filter.PageSize)
                 .Take(filter.PageSize)
                 .Where(x => x.Status == filter.FilterRequest.Status)
                 .ToListAsync();
            }
            else
            {
                data = await query.Include(x => x.BillDetails)
                        .OrderBy(filter._sortBy + " " + orderBy)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
            }

            if (storeID != null)
            {
                data = data.Where(x => x.StoreID == storeID).ToList();
            }
            if (contracID != null)
            {
                data = data.Where(x => x.ContractorId == contracID).ToList();

            }

            var totalRecords = await _context.Bills.CountAsync();

            if (!data.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new List<BillDTO>(),
                };
            }
            else
            {
                double totalPages;

                totalPages = totalRecords / (double)filter.PageSize;

                var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                Pagination pagination = new()
                {
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalPages = roundedTotalPages,
                    TotalRecords = totalRecords
                };

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = await MapListDTO(data, usID),
                    Pagination = pagination
                };
            }

            return response;
        }

        private async Task<List<BillDTO>> MapListDTO(List<Data.Entities.Bill> data, string userID)
        {
            List<BillDTO> rs = new();
            foreach (var item in data)
            {
                var store = await _context.Users.FirstOrDefaultAsync(x => x.MaterialStoreID == item.StoreID);



                BillDTO bill = new();
                bill.BillId = item.Id;
                bill.TotalPrice = item.TotalPrice;
                bill.StoreID = item.StoreID;
                bill.StoreName = store.FirstName + " " + store.LastName;
                bill.Avatar = store.Avatar;
                bill.Notes = item.Note;
                bill.Reason = item.Reason;
                bill.StartDate = item.StartDate;
                bill.EndDate = item.EndDate;
                bill.Status = item.Status;
                bill.UserID = userID;
                foreach (var i in item.BillDetails)
                {
                    bill.ProductBillDetailGet = MapProductDTO((int)i.BillID);

                }
                rs.Add(bill);
            }
            return rs;
        }

        public async Task<BaseResponse<BillDetailDTO>> GetDetail(int billID)
        {
            BaseResponse<BillDetailDTO> response;

            var rs = await _context.BillDetails
                .Include(x => x.Bills)
                    .ThenInclude(x => x.MaterialStore)
                    .ThenInclude(x => x.User)
                .Include(x => x.Bills)
                    .ThenInclude(x => x.Contractor)
                .Include(x => x.Products)
                .Where(x => x.BillID == billID)
                .ToListAsync();
            if (!rs.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Data = new(),
                    Message = BaseCode.NOTFOUND_MESSAGE
                };
                return response;
            }


            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.SUCCESS_MESSAGE,
                Data = await MapDetailDTO(rs),
            };

            return response;
        }




        public async Task<BaseResponse<SmallBillDetailDTO>> GetDetailBySmallBill(int billID)
        {
            BaseResponse<SmallBillDetailDTO> response;

            var result = await _context.Bills
                .Include(x => x.MaterialStore)
                    .ThenInclude(x => x.User)
                .Include(x=>x.Contractor)
                    .ThenInclude(x=>x.User)
                .Where(x => x.Id == billID).FirstOrDefaultAsync();

            if (result != null)
            {

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = MapSmallDetailDTOFirstType(result),
                };
            }
            else
            {
                response = new()
                {
                    Code = BaseCode.ERROR,
                    Message = BaseCode.ERROR_MESSAGE,
                };
            }
            return response;

        }

        public async Task<BillDetailDTO> MapDetailDTO(List<BillDetail> list)
        {
            List<ProductBillDetail> product = new();

            foreach (var item in list)
            {
                ProductBillDetail pro = new()
                {
                    Image = item.Products.Image,
                    ProductBrand = item.Products.Brand,
                    ProductDescription = item.Products.Description,
                    ProductName = item.Products.Name,
                    UnitPrice = item.Products.UnitPrice,
                    BillDetailQuantity = item.Quantity,
                    BillDetailTotalPrice = item.Price
                };
                product.Add(pro);
            }

            


            BigBillDetail bill = new()
            {
                ContractorId = list.First().Bills.ContractorId.Value,
                EndDate = list.First().Bills.EndDate,
                Id = list.First().Bills.Id,
                Note = list.First().Bills?.Note,
                PaymentDate = list.First().Bills.PaymentDate,
                StartDate = list.First().Bills.StartDate,
                Status = list.First().Bills.Status,
                StoreID = list.First().Bills.StoreID,
                //StoreName = list.First().Bills.MaterialStore.User.FirstName + " " + list.First().Bills.MaterialStore.User.LastName,
                TotalPrice = list.First().Bills.TotalPrice,
                //Type = list.First().Bills.Type

            };


            BillDetailDTO dto = new()
            {
                Bill = bill,
                Products = product,
                
            };
            return dto;
        }



        private SmallBillDetailDTO MapSmallDetailDTOFirstType(Data.Entities.Bill bill)
        {
            List<SmallBillDTO> smallDetails = new();

            BigBillDetail BillDTO = new()
            {
                ContractorId = bill.ContractorId.Value,
                EndDate = bill.EndDate,
                Id = bill.Id,
                Note = bill.Note,
                Reason = bill.Reason,
                PaymentDate = bill.PaymentDate,
                StartDate = bill.StartDate,
                Status = bill.Status,
                StoreID = bill.StoreID,
                TotalPrice = bill.TotalPrice,
                //Type = bill.Type,
                _lastModifiedAt = bill.LastModifiedAt

            };


            StoreDTO store = new()
            {
                Avatar = bill.MaterialStore?.User?.Avatar,
                Email = bill.MaterialStore?.User?.Email,
                Id = bill.StoreID,
                StoreName = bill.MaterialStore?.User?.FirstName + " " + bill.MaterialStore?.User?.LastName,
                UserId = bill.MaterialStore?.User?.Id
            };

            SmallBillDTO small = new()
            {
                Status = bill.Status,
                ProductBillDetail = MapProductDTO(bill.Id)
            };
            smallDetails.Add(small);

            BuyerInfo buyerInfo = new()
            {
                Address = bill.Contractor?.User?.Address,
                Name = bill.Contractor?.User?.FirstName + " " + bill.Contractor?.User?.LastName,
                Phone = bill.Contractor?.User?.PhoneNumber
            };

            SmallBillDetailDTO dto = new()
            {
                Bill = BillDTO,
                Details = smallDetails,
                Store = store,
                BuyerInfo = buyerInfo
            };
            return dto;
        }

        private List<ProductBillDetail> MapProductDTO(int id)
        {
            List<ProductBillDetail> list = new();
            int cartID = 0;
            var rs = _context.BillDetails
                .Include(x => x.Products)
                .Include(x => x.Bills)
                    .ThenInclude(x => x.Contractor)
                .Include(x => x.ProductTypes)
                    .ThenInclude(x => x.Color)
                 .Include(x => x.ProductTypes)
                    .ThenInclude(x => x.Size)
                 .Include(x => x.ProductTypes)
                    .ThenInclude(x => x.Other)
                .Where(x => x.BillID == id)
            .ToList();

            foreach (var item in rs)
            {
                List<CartProductType> types = new();
                var listType = _context.ProductTypes
                    .Include(x => x.Color)
                    .Include(x => x.Size)
                    .Include(x => x.Other)
                    .Where(x => x.ProductID == item.ProductID && x.Status == Status.SUCCESS).ToList();

                if (listType.Any())
                {
                    foreach (var type in listType)
                    {
                        CartProductType tmp = new()
                        {
                            Id = type.Id,
                            //TypeName = type.Name,
                            Quantity = type.Quantity,
                            Color = type.Color?.Name == "No Color" ? null : type.Color.Name,
                            Size = type.Size?.Name == "No Size" ? null : type.Size.Name,
                            Other = type.Other?.Name == "No Other" ? null : type.Other.Name,
                            ColorID = type.ColorId == 1 ? null : type.ColorId,
                            SizeID = type.SizeID == 1 ? null : type.SizeID,
                            OtherID = type.OtherID == 1 ? null : type.OtherID,
                            //OtherImage = type.Other.Image == null ? null : type.Other.Image,
                            //ColorImage = type.Color.Image == null ? null : type.Color.Image,


                        };

                        if (tmp.OtherID == null)
                        {
                            tmp.Image = type.Color.Image != null ? type.Color.Image : null;
                        }
                        else
                        {
                            tmp.Image = type.Other.Image != null ? type.Other.Image : null;

                        }

                        types.Add(tmp);
                    }
                }

                if (item.Bills.Status == Status.CANCEL)
                {
                    cartID = _context.Carts.Where(x => x.ProductID == item.ProductID && x.UserID == item.Bills.Contractor.CreateBy).Select(x => x.Id).FirstOrDefault();
                }


                ProductBillDetail pro = new()
                {
                    ProductId = item.ProductID,
                    Image = item.Products.Image,
                    ProductBrand = item.Products.Brand,
                    ProductDescription = item.Products.Description,
                    ProductName = item.Products.Name,
                    UnitPrice = item.Products.UnitPrice,
                    BillDetailQuantity = item.Quantity,
                    BillDetailTotalPrice = item.Price,
                    TypeId = item.ProductTypeId,
                    //TypeName = item.ProductTypes?.Name,
                    //ColorName= item.ProductTypes?.Color.Name,
                    //SizeName= item.ProductTypes?.Size.Name,
                    //OtherName= item.ProductTypes?.Other.Name,



                    Unit = item.Products.Unit,
                    ProductType = listType.Any() ? types : null,
                    CartId = item.Bills.Status == Status.CANCEL ? cartID : null
                };
                if (item.ProductTypes?.Color?.Name != null)
                {
                    pro.ColorName = item.ProductTypes?.Color?.Name != "No Color" ? item.ProductTypes.Color.Name : null;
                }
                else
                {
                    pro.ColorName = null;
                }
                if (item.ProductTypes?.Size?.Name != null)
                {

                    pro.SizeName = item.ProductTypes?.Size?.Name != "No Size" ? item.ProductTypes.Size.Name : null;
                }
                else
                {
                    pro.SizeName = null;
                }
                if (item.ProductTypes?.Other?.Name != null)
                {

                    pro.OtherName = item.ProductTypes?.Other?.Name != "No Other" ? item.ProductTypes.Other.Name : null;
                }
                else
                {
                    pro.OtherName = null;
                }

                if (item.ProductTypes != null)
                {

                    pro.Label = item.ProductTypes.Label;
                }
                else
                {
                    pro.Label = null;
                }


                list.Add(pro);
            }
            return list;
        }

        public async Task<BaseResponse<List<Cart>>> UpdateStatusBill(Status status, int billID, string message, Guid userID)
        {
            BaseResponse<List<Cart>> response;
            var bill = await _context.Bills.FirstOrDefaultAsync(x => x.Id == billID);
            List<Cart> ls = new();

            if (bill != null)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    bill.Reason = message;
                    if (status == Status.CANCEL)
                    {
                        var billDetail = await _context.BillDetails.Where(x => x.BillID == billID).ToListAsync();
                        foreach (var item in billDetail)
                        {

                            var product = _context.Products.FirstOrDefault(x => x.Id == item.ProductID);

                            Cart cart = new()
                            {
                                LastModifiedAt = bill.LastModifiedAt,
                                ProductID = item.ProductID.Value,
                                TypeID = item.ProductTypeId,
                                Quantity = item.Quantity,
                                UserID = userID,
                            };
                            ls.Add(cart);

                            if(item.ProductTypeId == null)
                            {
                                var unitInStockProduct = item.Quantity + product.UnitInStock;
                                var soldQuantities = product.SoldQuantities - item.Quantity;
                                product.UnitInStock = unitInStockProduct;
                                product.SoldQuantities = soldQuantities; 
                            
                            }
                            else
                            {
                                var type = _context.ProductTypes.Include(x=>x.Products).FirstOrDefault(x => x.Id == item.ProductTypeId);
                                if (type != null)
                                {
                                    var unitTypeInStock = item.Quantity + type.Quantity;
                                    var soldQuantities = type.Products.SoldQuantities - item.Quantity;
                                    product.SoldQuantities = soldQuantities;
                                    type.Quantity = unitTypeInStock;
                                }
                                _context.ProductTypes.Update(type);
                            }

                            _context.Products.Update(product);
                            _context.SaveChanges();


                        }
                        await _context.AddRangeAsync(ls);
                        await _context.SaveChangesAsync();
                    }
                }
                bill.Status = status;
                bill.LastModifiedAt = DateTime.Now;
                _context.Update(bill);
                await _context.SaveChangesAsync();

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = ls.Any() ? ls : new(),
                    NavigateId = billID
                };
                return response;
            }

            response = new()
            {
                Code = BaseCode.ERROR,
                Message = BaseCode.ERROR_MESSAGE
            };
            return response;

        }

        public async Task<BasePagination<List<ProductBillDetail>>> GetHistoryProductBill(Guid userID, PaginationFilter filter)
        {
            BasePagination<List<ProductBillDetail>> response;
            var orderBy = filter._orderBy.ToString();
            int totalRecords;
            orderBy = orderBy switch
            {
                "1" => "ascending",
                "-1" => "descending",
                _ => orderBy
            };
            if (string.IsNullOrEmpty(filter._sortBy))
            {
                filter._sortBy = "ProductId";
            }


            List<ProductBillDetail> ls = new();

            var user = await _context.Users.Include(x => x.Contractor).FirstOrDefaultAsync(x => x.Id.Equals(userID));

            if (user != null)
            {
                var bill = await _context.Bills.Where(x => x.ContractorId == user.ContractorId).ToListAsync();

                foreach (var item in bill)
                {
                    var tmpList = MapProductDTO(item.Id);
                    ls.AddRange(tmpList);
                }
                ls = ls.DistinctBy(x => x.ProductId).ToList();
                IQueryable<ProductBillDetail> query = ls.AsQueryable();

                var rs = query.OrderBy(filter._sortBy + " " + orderBy)
                     .Skip((filter.PageNumber - 1) * filter.PageSize)
                     .Take(filter.PageSize)
                     .ToList();

                totalRecords = rs.Count;


                double totalPages;

                totalPages = totalRecords / (double)filter.PageSize;

                var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
                Pagination pagination = new()
                {
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalPages = roundedTotalPages,
                    TotalRecords = totalRecords
                };

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Data = ls,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Pagination = pagination
                };
                return response;

            }
            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.NOTFOUND_MESSAGE
            };

            return response;

        }


    }


}
