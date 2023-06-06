using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Gridify;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Text;
using ViewModels.Categories;
using ViewModels.MaterialStore;
using ViewModels.Pagination;
using ViewModels.Response;

namespace Application.System.MaterialStores
{
    public class MaterialStoreService : IMaterialStoreService
    {
        private readonly BuildingConstructDbContext _context;
        private IHttpContextAccessor _accessor;
        public MaterialStoreService(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<bool> CreateProduct(ProductDTO request)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var userID = identifierClaim.Value.ToString();
            var roles = _accessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value.ToString();
            if (roles.ToLower().Equals(("store")))
            {
                return false;
            }
            var storeID = await _context.Users.Where(x => x.Id.ToString().Equals(userID)).Select(x => x.MaterialStoreID).FirstOrDefaultAsync();

            Products products = new()
            {
                Name = request.Name,
                Description = request.Description,
                Brand = request.Brand,
                UnitPrice = request.UnitPrice,
                UnitInStock = request.UnitInStock,
                Image = request.Image,
                SoldQuantities = 0,
                MaterialStoreID = storeID,
                Unit = request.Unit,
                CreateBy = Guid.Parse(userID),
                Status = true
            };

            await _context.Products.AddAsync(products);
            await _context.SaveChangesAsync();


            if (request.ProductTypes != null)
            {
                List<ProductType> list = new();
                List<Color> colorExisted = new();
                List<ProductSize> sizeExisted = new();


                foreach (var item in request.ProductTypes)
                {
                    var productType = new ProductType();
                    productType.ProductID = products.Id;
                    productType.Quantity = item.Quantity;
                    productType.Status = Status.SUCCESS;

                    if (!string.IsNullOrEmpty(item.Label))
                    {
                        productType.Label = item.Label;
                    }

                    if (!string.IsNullOrEmpty(item.Color))
                    {
                        var inColorList = colorExisted.Where(x => x.Name.Equals(item.Color)).FirstOrDefault();

                        if (inColorList != null)
                        {
                            productType.ColorId = inColorList.Id;
                        }
                        else
                        {

                            Color tmpColor = new()
                            {
                                Name = item.Color,
                                Image = item.Image,
                            };

                            await _context.Colors.AddAsync(tmpColor);
                            await _context.SaveChangesAsync();

                            colorExisted.Add(tmpColor);
                            productType.ColorId = tmpColor.Id;

                        }

                    }
                    else
                    {
                        productType.ColorId = 1;
                    }


                    if (!string.IsNullOrEmpty(item.Size))
                    {
                        var inSizeList = sizeExisted.Where(x => x.Name.Equals(item.Size)).FirstOrDefault();

                        if (inSizeList != null)
                        {
                            productType.SizeID = inSizeList.Id;
                        }
                        else
                        {

                            ProductSize tmpSize = new()
                            {
                                Name = item.Size,
                            };

                            await _context.Sizes.AddAsync(tmpSize);
                            await _context.SaveChangesAsync();
                            sizeExisted.Add(tmpSize);
                            productType.SizeID = tmpSize.Id;
                        }



                    }
                    else
                    {
                        productType.SizeID = 1;
                    }


                    if (!string.IsNullOrEmpty(item.Other))
                    {
                        Other tmpOther = new()
                        {
                            Name = item.Other,
                            Image = item.Image
                        };

                        await _context.Others.AddAsync(tmpOther);
                        await _context.SaveChangesAsync();
                        productType.OtherID = tmpOther.Id;
                    }
                    else
                    {
                        productType.OtherID = 1;
                    }

                    list.Add(productType);

                }
                await _context.AddRangeAsync(list);
                var rs = await _context.SaveChangesAsync();
                if (rs < 0)
                {
                    _context.Remove(products);
                    return false;
                }
            }
            if (request.Categories != null)
            {
                var productcate = new ProductCategories();

                foreach (var item in request.Categories)
                {
                    var check = _context.Categories.Where(x => x.ID == item.CategoryID).SingleOrDefault();
                    if (check == null)
                    {
                        _context.Products.Remove(products);
                        await _context.SaveChangesAsync();
                        return false;
                    }
                    var id = products.Id;
                    productcate.ProductID = id;
                    productcate.CategoriesID = item.CategoryID;
                    productcate.Name = item.Name;
                    await _context.AddAsync(productcate);
                    var rs = await _context.SaveChangesAsync();
                    if (rs < 0)
                    {
                        _context.Products.Remove(products);
                        await _context.SaveChangesAsync();
                        return false;
                    }

                }

            }
            return true;
        }

        public async Task<BasePagination<List<ProductStoreDTO>>> GetAllProductStore(PaginationFilter filter, bool isAll, int? storeID, string? keyword)
        {
            BasePagination<List<ProductStoreDTO>> response;
            var orderBy = filter._orderBy.ToString();

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

            IQueryable<Products> query = _context.Products.Include(x => x.ProductCategories).Include(x => x.MaterialStore).ThenInclude(x => x.User);


            var data = await query
                .AsNoTracking()
               .OrderBy(filter._sortBy + " " + orderBy)
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();
            if (isAll == false)
            {
                data = await query
                .AsNoTracking()
                .Where(x => x.MaterialStoreID == storeID && x.Status == true && x.Name.Contains(keyword))
               .OrderBy(filter._sortBy + " " + orderBy)
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();
            }
            else
            {

            }
            var totalRecords = await _context.Products.CountAsync();

            if (!data.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new List<ProductStoreDTO>(),
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
                    Data = await MapListDTO(data, isAll),
                    Pagination = pagination
                };
            }

            return response;
        }




        public async Task<List<ProductStoreDTO>> MapListDTO(List<Products> list, bool? isAll)
        {
            List<ProductStoreDTO> result = new();

            foreach (var item in list)
            {
                ProductStoreDTO dto = new();
                dto.Id = item.Id;
                dto.Name = item.Name;
                dto.Description = item.Description;
                dto.Brand = item.Brand;
                dto.UnitInStock = item.UnitInStock;
                dto.UnitPrice = item.UnitPrice;
                dto.SoldQuantities = item.SoldQuantities;
                dto.LastModifiedAt = item.LastModifiedAt;

                string img = item?.Image;
                if (img != null)
                {
                    string[] firstImg = img.Split(",");
                    dto.Image = firstImg[0].Trim();
                }
                else
                {
                    dto.Image = "";
                }
                dto.StoreName = item.MaterialStore?.User?.FirstName + item.MaterialStore?.User?.LastName;
                dto.StoreID = item.MaterialStoreID;
                dto.StoreImage = item.MaterialStore?.Image;
                dto.ProductCategories = await GetCategory(item.ProductCategories);
                dto.isAll = isAll;
                result.Add(dto);
            }
            return result;
        }

        public async Task<BasePagination<List<MaterialStoreDTO>>> GetList(PaginationFilter filter)
        {
            BasePagination<List<MaterialStoreDTO>> response;
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



            IQueryable<MaterialStore> query = _context.MaterialStores;
            StringBuilder placeSearch = new();

            if (filter.FilterRequest != null)
            {
                if (filter.FilterRequest.Places.Any())
                {
                    var count = filter.FilterRequest.Places.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (i == count - 1)
                        {
                            placeSearch.Append("Place=" + filter.FilterRequest.Places[i]);
                            break;
                        }
                        placeSearch.Append("Place=" + filter.FilterRequest.Places[i] + "|");
                    }
                    query = query.ApplyFiltering(placeSearch.ToString());
                }
            }

            var result = await query
                .Include(x => x.User)
               .OrderBy(filter._orderBy + " " + orderBy)
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();

            if (filter.FilterRequest != null)
            {
                totalRecord = result.Count;
            }
            else
            {
                totalRecord = await _context.MaterialStores.CountAsync();
            }

            if (!result.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new()
                };
            }
            else
            {
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
                    Data = MapListDTO(result),
                    Pagination = pagination
                };
            }
            return response;
        }

        public async Task<BasePagination<List<MaterialStoreDTO>>> Search(PaginationFilter filter, string keyword)
        {
            BasePagination<List<MaterialStoreDTO>> response;

            var result = await _context.MaterialStores.Include(x => x.User).Where(x => x.User.LastName.Contains(keyword)).ToListAsync();
            var totalRecord = result.Count;

            if (!result.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new()
                };
            }
            else
            {
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
                    Data = MapListDTO(result),
                    Pagination = pagination
                };
            }
            return response;
        }

        private List<MaterialStoreDTO> MapListDTO(List<MaterialStore> list)
        {
            List<MaterialStoreDTO> result = new();

            foreach (var item in list)
            {

                MaterialStoreDTO dto = new()
                {

                    Avatar = item.User.Avatar,
                    FirstName = item.User.FirstName,
                    LastName = item.User.LastName,
                    Description = item.Description,
                    Id = item.Id,
                    Place = item.Place,
                    Experience = item.Experience,
                    Image = item.Image,
                    TaxCode = item.TaxCode,
                    Webstie = item.Website,
                    UserId = item.User.Id,

                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<BaseResponse<ProductDetailDTO>> GetProductDetail(int productId)
        {
            BaseResponse<ProductDetailDTO> response = new();
            var rs = await _context.Products
                .Include(x => x.MaterialStore)
                    .ThenInclude(x => x.User)
                .Include(x => x.ProductCategories)
                .Include(x => x.ProductTypes)
                .SingleOrDefaultAsync(x => x.Id == productId);
            if (rs == null)
            {

                response.Code = BaseCode.ERROR;
                response.Message = "Cannot find that Product";
                return response;
            }
            ProductDetailDTO productDetail = new();
            productDetail.Id = rs.Id;
            productDetail.Name = rs.Name;

            if (rs.Image != null)
            {
                string[] img = rs.Image.Split(",");
                productDetail.Image = new();
                foreach (var item in img)
                {

                    productDetail.Image.Add(item);
                }
            }
            productDetail.Description = rs.Description;
            productDetail.UnitInStock = rs.UnitInStock;
            productDetail.UnitPrice = rs.UnitPrice;
            productDetail.Unit = rs.Unit;
            productDetail.UserId = rs.MaterialStore.User.Id;
            productDetail.Brand = rs.Brand;
            productDetail.SoldQuantities = rs.SoldQuantities;
            productDetail.ProductType = await GetProductType(rs.ProductTypes);
            productDetail.Store = await GetStore((int)rs.MaterialStoreID);
            productDetail.ProductCategories = await GetCategory(rs.ProductCategories);
            productDetail.CreatedBy = rs.CreateBy;
            productDetail.Avatar = rs.MaterialStore.User.Avatar;
            response.Data = productDetail;
            response.Code = BaseCode.SUCCESS;
            response.Message = "SUCCESS";
            return response;
        }


        public async Task<MaterialStoreDTO> GetStore(int storeID)
        {
            var results = await _context.MaterialStores.Where(x => x.Id == storeID).SingleOrDefaultAsync();
            var user = await _context.Users.Where(x => x.MaterialStoreID == storeID).FirstOrDefaultAsync();
            var final = new MaterialStoreDTO();
            final.Id = storeID;
            final.FirstName = user.FirstName;
            final.LastName = user.LastName;
            final.Webstie = results.Website;
            final.Description = results.Description;
            final.Image = results.Image;
            final.Experience = results.Experience;
            final.TaxCode = results.TaxCode;
            final.Place = results.Place;
            return final;
        }

        public async Task<List<CategoryDTO>> GetCategory(List<ProductCategories> productCategories)
        {
            List<CategoryDTO> list = new();
            foreach (var c in productCategories)
            {
                var final = new CategoryDTO();
                final.Id = c.CategoriesID;
                final.CategoryName = c.Name;
                final.Name = c.Name;
                list.Add(final);
            }
            return list;
        }


        public async Task<List<ProductCategoryDTO>> GetProductCategoryName(List<ProductCategories> productCategories)
        {
            List<ProductCategoryDTO> list = new();
            foreach (var c in productCategories)
            {
                var results = await _context.ProductCategories.Where(x => x.ProductID == c.ProductID).SingleOrDefaultAsync();
                var final = new ProductCategoryDTO();
                final.Name = results.Name;
                list.Add(final);
            }
            return list;
        }



        public async Task<List<ProductTypeDTO>> GetProductType(List<ProductType> productType)
        {
            List<ProductTypeDTO> list = new();
            foreach (var c in productType)
            {
                var results = await _context.ProductTypes
                    .Include(x => x.Color)
                    .Include(x => x.Size)
                    .Include(x => x.Other)
                    .Where(x => x.Id == c.Id && x.Status == Status.SUCCESS)
                    .SingleOrDefaultAsync();
                var final = new ProductTypeDTO();
                final.Id = results.Id;
                //final.TypeName = results.Name;
                final.Quantity = results.Quantity;
                final.Label = results.Label;
                final.Other = results.Other?.Name == "No Other" ? null : results.Other.Name;
                final.Size = results.Size?.Name == "No Size" ? null : results.Size.Name;
                final.Color = results.Color?.Name == "No Color" ? null : results.Color.Name;
                final.ColorId = results.ColorId == 1 ? null : results.ColorId;
                final.SizeId = results.SizeID == 1 ? null : results.SizeID;
                final.OtherId = results.OtherID == 1 ? null : results.OtherID;

                if (final.OtherId == null)
                {
                    final.Image = results.Color.Image != null ? results.Color.Image : null;
                }
                else
                {
                    final.Image = results.Other.Image != null ? results.Other.Image : null;

                }


                //final.OtherImage=results.Other.Image ==null ? null : results.Other.Image;
                //final.ColorImage=results.Color.Image ==null ? null : results.Color.Image;
                list.Add(final);
            }
            return list;
        }

        public async Task<bool> DeleteProduct(int productID)
        {
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst(ClaimTypes.Role);
            var role = identifierClaim.Value.ToString();
            if (!role.Equals("Store"))
            {
                return false;
            }
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productID);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BaseResponse<ProductStoreDTO>> UpdateProduct(UpdateProductDTO request)
        {
            BaseResponse<ProductStoreDTO> response = new();

            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var userID = identifierClaim.Value.ToString();


            var storeID = await _context.Users.Where(x => x.Id.ToString().Equals(userID)).Select(x => x.MaterialStoreID).SingleOrDefaultAsync();

            var existedProduct = await _context.Products.Include("ProductCategories").Where(x => x.Id == request.productId && x.MaterialStoreID == storeID).FirstOrDefaultAsync();

            // If update => old product will change status == FALSE , add new product 

            if (existedProduct == null)
            {
                response.Message = BaseCode.NOTFOUND_MESSAGE;
                response.Code = BaseCode.ERROR;
                return response;
            }
            else
            {
                //Change status old product 

                existedProduct.Status = false;
                _context.Products.Update(existedProduct);
                await _context.SaveChangesAsync();

            }

            Products newUpdateProduct = new()
            {
                Name = request.Name,
                Unit = request.Unit,
                UnitPrice = request.UnitPrice,
                UnitInStock = request.UnitInStock,
                Description = request.Description,
                Brand = request.Brand,
                Image = request.Image,
                Status = true,
                MaterialStoreID = existedProduct.MaterialStoreID,
                CreateBy = Guid.Parse(userID),
                SoldQuantities = existedProduct.SoldQuantities,
            };

            await _context.AddAsync(newUpdateProduct);
            await _context.SaveChangesAsync();

            if (request.ProductTypes != null)
            {
                List<ProductType> list = new();
                List<Color> colorExisted = new();
                List<ProductSize> sizeExisted = new();

                foreach (var item in request.ProductTypes)
                {
                    var productType = new ProductType();
                    productType.ProductID = newUpdateProduct.Id;
                    productType.Quantity = item.Quantity;
                    productType.Status = Status.SUCCESS;

                    if (!string.IsNullOrEmpty(item.Label))
                    {
                        productType.Label = item.Label;
                    }

                    if (!string.IsNullOrEmpty(item.Color))
                    {
                        var inColorList = colorExisted.Where(x => x.Name.Equals(item.Color)).FirstOrDefault();

                        if (inColorList != null)
                        {
                            productType.ColorId = inColorList.Id;
                        }
                        else
                        {

                            Color tmpColor = new()
                            {
                                Name = item.Color,
                                Image = item.Image,
                            };

                            await _context.Colors.AddAsync(tmpColor);
                            await _context.SaveChangesAsync();

                            colorExisted.Add(tmpColor);
                            productType.ColorId = tmpColor.Id;

                        }

                    }
                    else
                    {
                        productType.ColorId = 1;
                    }


                    if (!string.IsNullOrEmpty(item.Size))
                    {
                        var inSizeList = sizeExisted.Where(x => x.Name.Equals(item.Size)).FirstOrDefault();

                        if (inSizeList != null)
                        {
                            productType.SizeID = inSizeList.Id;
                        }
                        else
                        {

                            ProductSize tmpSize = new()
                            {
                                Name = item.Size,
                            };

                            await _context.Sizes.AddAsync(tmpSize);
                            await _context.SaveChangesAsync();
                            sizeExisted.Add(tmpSize);
                            productType.SizeID = tmpSize.Id;
                        }



                    }
                    else
                    {
                        productType.SizeID = 1;
                    }


                    if (!string.IsNullOrEmpty(item.Other))
                    {
                        Other tmpOther = new()
                        {
                            Name = item.Other,
                            Image = item.Image
                        };

                        await _context.Others.AddAsync(tmpOther);
                        await _context.SaveChangesAsync();
                        productType.OtherID = tmpOther.Id;
                    }
                    else
                    {
                        productType.OtherID = 1;
                    }

                    list.Add(productType);

                }
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();


            }
            if (request.Categories != null)
            {

                foreach (var item in request.Categories)
                {
                    var productcate = new ProductCategories
                    {
                        ProductID = newUpdateProduct.Id,
                        CategoriesID = item.CategoryID,
                        Name = item.Name
                    };
                    await _context.AddAsync(productcate);
                    await _context.SaveChangesAsync();
                }

            }

            ProductStoreDTO newProduct = new()
            {
                Id = newUpdateProduct.Id
            };


            response = new()
            {
                Code = BaseCode.SUCCESS,
                Message = BaseCode.SUCCESS_MESSAGE,
                Data= newProduct

            };

            return response;
        }

        public async Task<BaseResponse<List<MaterialStoreStatisticDTO>>> GetBillStatistic()
        {
            var response = new BaseResponse<List<MaterialStoreStatisticDTO>>();
            response.Data = new List<MaterialStoreStatisticDTO>();
            for (int i = 1; i <= 12; i++)
            {
                var store = new MaterialStoreStatisticDTO();
                store.Month = i;
                store.BillCount = 0;
                response.Data.Add(store);
            }
            Claim identifierClaim = _accessor.HttpContext.User.FindFirst("UserID");
            var userID = identifierClaim.Value.ToString();
            var query = await _context.Bills.Where(x => x.CreateBy.ToString().Equals(userID)).GroupBy(x => x.LastModifiedAt.Month).Select(g => new
            {
                Month = g.Key,
                BillCount = g.Count(),
            }).ToListAsync();
            foreach (var i in query)
            {
                foreach (var item in response.Data.Where(x => x.Month == i.Month))
                {
                    item.Month = i.Month;
                    item.BillCount = i.BillCount;
                }
            }
            response.Code = BaseCode.SUCCESS;
            response.Message = BaseCode.SUCCESS_MESSAGE;
            return response;
        }

        public async Task<BasePagination<List<ProductStoreDTO>>> SearchProductInStore(PaginationFilter filter, int storeId, string keyword)
        {
            BasePagination<List<ProductStoreDTO>> response;
            var orderBy = filter._orderBy.ToString();

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

            IQueryable<Products> query = _context.Products.Include(x => x.ProductCategories).Include(x => x.MaterialStore).ThenInclude(x => x.User);


            var data = await query
                .AsNoTracking()
                .Where(x => x.MaterialStoreID == storeId && x.Name.Contains(keyword))
               .OrderBy(filter._sortBy + " " + orderBy)
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .ToListAsync();

            var totalRecords = await _context.Products.CountAsync();

            if (!data.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new List<ProductStoreDTO>(),
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
                    Data = await MapListDTO(data, false),
                    Pagination = pagination
                };
            }

            return response;


        }

        public async Task<BaseResponse<List<ProductStoreDTO>>> GetTop5SellProduct()
        {
            BaseResponse<List<ProductStoreDTO>> response;


            var data = await _context.BillDetails.Include(x => x.Products).ThenInclude(x => x.ProductCategories).Include(x => x.Bills).ThenInclude(x => x.MaterialStore)
                        .GroupBy(x => x.ProductID).Select(x => new
                        {
                            a = x.Key,
                            b = x.Sum(x => x.Quantity)
                        }).OrderByDescending(x => x.b).Take(5).ToListAsync();

            if (!data.Any())
            {
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.EMPTY_MESSAGE,
                    Data = new List<ProductStoreDTO>(),
                };
            }
            else
            {
                List<ProductStoreDTO> result = new();
                foreach (var item in data)
                {

                    var query = _context.Products.Include(x => x.BillDetails).Include(x => x.ProductCategories).Include(x => x.MaterialStore).Where(x => x.Id == item.a).FirstOrDefault();
                        ProductStoreDTO dto = new();
                        dto.Id = query.Id;
                        dto.Name = query.Name;
                        dto.Description = query.Description;
                        dto.Brand = query.Brand;
                        dto.UnitInStock = query.UnitInStock;
                        dto.UnitPrice = query.UnitPrice;
                        dto.SoldQuantities = query.SoldQuantities;
                        dto.LastModifiedAt = query.LastModifiedAt;

                        string img = query?.Image;
                        if (img != null)
                        {
                            string[] firstImg = img.Split(",");
                            dto.Image = firstImg[0].Trim();
                        }
                        else
                        {
                            dto.Image = "";
                        }
                        dto.StoreName = query.MaterialStore?.User?.FirstName + query.MaterialStore?.User?.LastName;
                        dto.StoreID = query.MaterialStoreID;
                        dto.StoreImage = query.MaterialStore?.Image;
                        dto.ProductCategories = await GetCategory(query.ProductCategories);
                        result.Add(dto);
                }
                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE,
                    Data = result
                };
            }

            return response;
        }
    }
}