using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using ViewModels.Carts;
using ViewModels.Pagination;
using ViewModels.Response;
using System.Linq.Dynamic.Core;
using System.Text;
using Gridify;
using Microsoft.AspNetCore.Http;
using ZedGraph;

namespace Application.System.Carts
{
    public class CartService : ICartService
    {
        private readonly BuildingConstructDbContext _context;

        public CartService(BuildingConstructDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<CartDTO>> Create(Guid userID, CreateCartRequest requests)
        {
            BaseResponse<CartDTO>? response = null;
            Cart cart;
            var existed = await _context.Carts
                .Include(x => x.Products)
                    .ThenInclude(x => x.ProductTypes)
                .Where(x => x.UserID.Equals(userID) && x.ProductID == requests.ProductID && x.TypeID == requests.TypeID).FirstOrDefaultAsync();

            if (existed != null)
            {

                if (existed.TypeID != null)
                {
                    var inStockQuanties = await _context.ProductTypes.Where(x => x.Id == existed.TypeID).FirstOrDefaultAsync();
                    var totalInCart = requests.Quantity + existed.Quantity;
                    if (totalInCart > inStockQuanties?.Quantity)
                    {
                        response = new BaseResponse<CartDTO>
                        {
                            Code = BaseCode.ERROR,
                            Message = "Sản phẩm không đủ số lượng",
                        };
                        return response;
                    }
                    else
                    {
                        var quantity = existed.Quantity;
                        quantity += requests.Quantity;

                        existed.Quantity = quantity;

                        _context.Carts.Update(existed);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    var quanties = existed.Products.UnitInStock;
                    var totalInCart = requests.Quantity + existed.Quantity;
                    if (totalInCart > quanties)
                    {
                        response = new BaseResponse<CartDTO>
                        {
                            Code = BaseCode.ERROR,
                            Message = "Sản phẩm không đủ số lượng",
                        };
                        return response;
                    }
                    else
                    {
                        var quantity = existed.Quantity;
                        quantity += requests.Quantity;

                        existed.Quantity = quantity;

                        _context.Carts.Update(existed);
                        await _context.SaveChangesAsync();
                    }
                }




                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                };
                return response;

            }

            if (requests.TypeID != null)
            {
                cart = new Cart()
                {
                    ProductID = requests.ProductID,
                    Quantity = requests.Quantity,
                    TypeID = requests.TypeID.Value,
                    UserID = userID
                };

            }
            else
            {
                cart = new Cart()
                {
                    ProductID = requests.ProductID,
                    Quantity = requests.Quantity,
                    UserID = userID
                };
            }


            await _context.Carts.AddAsync(cart);
            var rs = await _context.SaveChangesAsync();

            if (rs > 0)
            {
                var result = await _context.Carts
               .Include(x => x.Products)
                   .ThenInclude(x => x.MaterialStore)
                       .ThenInclude(x => x.User)
                .Include(x => x.ProductType)
               .Where(x => x.UserID.Equals(userID) && x.ProductID == requests.ProductID).FirstOrDefaultAsync();

                if (result != null)
                {
                    response = new()
                    {
                        Code = BaseCode.SUCCESS,
                        Message = BaseCode.SUCCESS_MESSAGE,
                        Data = MapToDTO(result)
                    };
                    return response;
                }
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

        public async Task<BasePagination<List<CartDTO>>> GetAll(Guid UserID, PaginationFilter filter)
        {
            BasePagination<List<CartDTO>> response;

            var orderBy = filter._orderBy.ToString();
            int totalRecord;
            orderBy = orderBy switch
            {
                "1" => "ascending",
                "-1" => "descending",
                _ => orderBy
            };

            if (string.IsNullOrEmpty(filter._sortBy))
            {
                filter._sortBy = "LastModifiedAt";
            }



            List<CartDTO> ls = new();


            var cart = await _context.Carts
                .Include(x => x.Products)
                    .ThenInclude(x => x.MaterialStore)
                        .ThenInclude(x => x.User)
                .Include(x => x.ProductType)
                    .ThenInclude(x => x.Color)
                 .Include(x => x.ProductType)
                    .ThenInclude(x => x.Size)
                 .Include(x => x.ProductType)
                    .ThenInclude(x => x.Other)
                .Where(x => x.UserID.Equals(UserID))
                .OrderBy(filter._sortBy + " " + orderBy)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            totalRecord = await _context.Carts.Where(x => x.UserID.Equals(UserID)).CountAsync();


            if (!cart.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Data = new(),
                    Message = BaseCode.NOTFOUND_MESSAGE
                };
                return response;
            }

            foreach (var item in cart)
            {
                ls.Add(MapToDTO(item));
            }

            double totalPages;

            totalPages = ((double)totalRecord / (double)filter.PageSize);

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            Pagination pagination = new()
            {
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalPages = roundedTotalPages,
                TotalRecords = totalRecord
            };

            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.SUCCESS_MESSAGE,
                Data = ls,
                Pagination = pagination
            };

            return response;

        }

        public async Task<BaseResponse<string>> Remove(Guid userID, List<RemoveCartRequest> requests)
        {
            BaseResponse<string> response;


            if (requests.Count == 0)
            {
                var existed = await _context.Carts.Where(x => x.UserID.Equals(userID)).ToListAsync();
                if (existed.Any())
                {
                    _context.RemoveRange(existed);
                    await _context.SaveChangesAsync();

                    response = new()
                    {
                        Code = BaseCode.SUCCESS,
                        Message = BaseCode.SUCCESS_MESSAGE
                    };
                    return response;


                }
                else
                {
                    response = new()
                    {
                        Code = BaseCode.ERROR,
                        Message = "THERE NOT THING TO REMOVE"
                    };
                    return response;
                }
            }
            else
            {
                foreach (var item in requests)
                {
                    var remove = _context.Carts.Where(x => x.UserID.Equals(userID) && x.Id == item.Id).FirstOrDefault();
                    if (remove != null)
                    {
                        _context.Carts.Remove(remove);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.SUCCESS_MESSAGE
            };
            return response;



        }

        public async Task<BaseResponse<List<CartDTO>>> Update(Guid userID, List<CreateCartRequest> requests)
        {
            BaseResponse<List<CartDTO>> response;
            List<CartDTO> ls = new();
            int rs = 0;

            IQueryable<Cart> query = _context.Carts.Where(x => x.UserID.Equals(userID));
            StringBuilder existed = new();

            var count = requests.Count;
            for (int i = 0; i < count; i++)
            {
                if (i == count - 1)
                {
                    existed.Append("ProductID=" + requests[i].ProductID);
                    break;
                }
                existed.Append("ProductID=" + requests[i].ProductID + "|");
            }
            query = query.ApplyFiltering(existed.ToString());

            if (existed != null)
            {
                _context.RemoveRange(query);
                await _context.SaveChangesAsync();
            }

            foreach (var item in requests)
            {
                var product = await _context.Products.Where(x => x.Id == item.ProductID).FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(item.TypeID.ToString()))
                {
                    var quanties = await _context.ProductTypes.Include(x => x.Products).Where(x => x.Id == item.TypeID).FirstOrDefaultAsync();
                    if (item.Quantity > quanties?.Quantity)
                    {
                        response = new()
                        {
                            Code = BaseCode.ERROR,
                            Message = quanties.Products.Name + "không có đủ số lượng"
                        };
                        return response;
                    }
                    else
                    {
                        var cart = new Cart()
                        {
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            TypeID = item.TypeID,
                            UserID = userID
                        };
                        await _context.Carts.AddAsync(cart);
                        rs = await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    var quanties = await _context.Products.Where(x => x.Id == item.ProductID).FirstOrDefaultAsync();
                    if (item.Quantity > quanties?.UnitInStock)
                    {
                        response = new()
                        {
                            Code = BaseCode.ERROR,
                            Message = quanties.Name + "không có đủ số lượng"
                        };
                        return response;
                    }
                    else
                    {
                        var cart = new Cart()
                        {
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            TypeID = item.TypeID,
                            UserID = userID
                        };
                        await _context.Carts.AddAsync(cart);
                        rs = await _context.SaveChangesAsync();
                    }
                }
            }

            if (rs > 0)
            {
                var cart = await _context.Carts
                     .Include(x => x.Products)
                            .ThenInclude(x => x.MaterialStore)
                                .ThenInclude(x => x.User)
                    .Include(x => x.ProductType)
                    .Where(x => x.UserID.Equals(userID))
                    .OrderBy("Id" + " " + "descending")
                    .ToListAsync();

                foreach (var item in cart)
                {
                    ls.Add(MapToDTO(item));
                }

                response = new()
                {
                    Data = ls,
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
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

        private CartDTO MapToDTO(Cart cart)
        {
            var listType = _context.ProductTypes
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Other)
                .Where(x => x.ProductID == cart.ProductID && x.Status == Status.SUCCESS).ToList();
            List<CartProductType> types = new();

            if (listType.Any())
            {
                foreach (var item in listType)
                {
                    CartProductType tmp = new()
                    {
                        Id = item.Id,
                        //TypeName = item.Name,
                        Quantity = item.Quantity,
                        Color = item.Color?.Name == "No Color" ? null : item.Color?.Name,
                        Size = item.Size?.Name == "No Size" ? null : item.Size?.Name,
                        Other = item.Other?.Name == "No Other" ? null : item.Other?.Name,
                        ColorID = item.ColorId == 1 ? null : item.ColorId,
                        SizeID = item.SizeID == 1 ? null : item.SizeID,
                        OtherID = item.OtherID == 1 ? null : item.OtherID,

                    };

                    if (tmp.OtherID == null)
                    {
                        tmp.Image = item.Color?.Image ?? null;
                    }
                    else
                    {
                        tmp.Image = item.Other?.Image ?? null;

                    }
                    types.Add(tmp);
                }
            }
            CartDTO dto = new()
            {
                Id = cart.Id,
                Image = cart.Products.Image,
                LastModifiedAt = cart.LastModifiedAt,
                MaterialStoreID = cart.Products.MaterialStoreID,
                MaterialStoreName = cart.Products.MaterialStore.User?.FirstName + " " + cart.Products.MaterialStore.User?.LastName,
                ProductID = cart.ProductID,
                ProductName = cart.Products.Name,
                Quantity = cart.Quantity,
                UnitInStock = cart.Products.UnitInStock,
                UnitPrice = cart.Products.UnitPrice,
                Unit = cart.Products.Unit,
                IsDisable = cart.Products.Status == false ? true : false,
                //TypeName = cart.ProductType?.Name != null ? cart.ProductType.Name : null,
                //Color = cart.ProductType?.Color?.Name != "No Color" ? cart.ProductType.Color.Name : null,
                //Size = cart.ProductType?.Size?.Name != "No Size" ? cart.ProductType.Size.Name : null,
                //Other = cart.ProductType?.Other?.Name != "No Other" ? cart.ProductType.Other.Name : null,
                TypeID = cart.ProductType?.Id != null ? cart.ProductType.Id : null,
                ProductType = listType.Any() ? types : null,
            };

            if (dto.TypeID != null)
            {
                if (dto.ProductType != null)
                {
                    var unitInStock = dto.ProductType.Where(x => x.Id == dto.TypeID).Select(x => x.Quantity).FirstOrDefault();

                    dto.UnitInStock = unitInStock;
                }

            }


            if (cart.ProductType?.Color?.Name != null)
            {
                dto.Color = cart.ProductType?.Color?.Name != "No Color" ? cart.ProductType?.Color.Name : null;
            }
            else
            {
                dto.Color = null;
            }
            if (cart.ProductType?.Size?.Name != null)
            {

                dto.Size = cart.ProductType?.Size?.Name != "No Size" ? cart.ProductType?.Size.Name : null;
            }
            else
            {
                dto.Size = null;
            }
            if (cart.ProductType?.Other?.Name != null)
            {

                dto.Other = cart.ProductType?.Other?.Name != "No Other" ? cart.ProductType?.Other.Name : null;
            }
            else
            {
                dto.Other = null;
            }
            if (cart.ProductType != null)
            {

                dto.Label = cart.ProductType.Label;
            }
            else
            {
                dto.Label = null;
            }






            return dto;
        }
    }
}
