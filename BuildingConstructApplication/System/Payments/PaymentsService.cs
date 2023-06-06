using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pagination;
using ViewModels.Payment;
using ViewModels.Response;
using ViewModels.Users;

namespace Application.System.Payments
{
    public class PaymentsService : IPaymentsService
    {
        private readonly BuildingConstructDbContext _context;
        private IHttpContextAccessor _accessor;

        public PaymentsService(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<BaseResponse<RefundDTO>> CheckRefundPayment()
        {
            BaseResponse<RefundDTO> response = new();
            var userId = _accessor.HttpContext?.User?.FindFirst("UserID")?.Value.ToString();
            var query = await _context.Payments.Where(x => x.UserId.ToString().Equals(userId) && x.ExpireationDate.Month >= DateTime.Now.Month - 1).OrderByDescending(x => x.ExpireationDate).ToListAsync();
            var viewCheck = await _context.ContractorPosts.Where(x => x.CreateBy.ToString().Equals(userId)).ToListAsync();
            var n = await _context.Users.Where(x => x.BuilderId != null).CountAsync();
            var number = n * 0.2M;
            var flag = false;
            foreach (var item in viewCheck)
            {
                if (item.ViewCount < number)
                {
                    flag = true;
                }
            }
            if (flag == true)
            {
                var check = await _context.ContractorPosts.Include(x => x.AppliedPosts).Where(x => x.CreateBy.ToString().Equals(userId)).ToListAsync();
                foreach (var i in check)
                {
                    if (i.AppliedPosts.Count() < 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }
            if (flag == false)
            {
                if (query.Count == 0 || query.Where(x => x.IsRefund == true).Any() || query.Where(x => x.ExtendDate != null && x.ExtendDate.Value <= DateTime.Now).Any())
                {
                    response.Code = BaseCode.SUCCESS;
                    response.Message = BaseCode.SUCCESS_MESSAGE;
                    response.Data = null;
                }
                else
                {
                    if (query.First().ExpireationDate >= DateTime.Now)
                    {
                        response.Code = BaseCode.SUCCESS;
                        response.Message = BaseCode.SUCCESS_MESSAGE;
                        response.Data = new();
                        response.Data.IsOver = false;
                        response.Data.EndDate = query.First().ExpireationDate.ToString();
                    }
                    //else if (query.First().ExpireationDate.Month >= DateTime.Now.Month-1)
                    //{
                    //    response.Code = BaseCode.SUCCESS;
                    //    response.Message = BaseCode.SUCCESS_MESSAGE;
                    //    response.Data = null;
                    //}
                    else
                    {
                        query.First().ExtendDate = DateTime.Now.AddMonths(1);
                        _context.Payments.Update(query.First());
                        await _context.SaveChangesAsync();
                        response.Code = BaseCode.SUCCESS;
                        response.Message = BaseCode.SUCCESS_MESSAGE;
                        response.Data = new();
                        response.Data.IsOver = true;
                        response.Data.EndDate = query.First().ExpireationDate.ToString();
                    }
                }
            }
            else
            {
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Data = null;
            }
            return response;
        }

        public async Task<BasePagination<List<StoreOrderStatistic>>> GetTop5OrderStore()
        {
            var response = new BasePagination<List<StoreOrderStatistic>>();
            var query = await _context.Bills.Include(x => x.MaterialStore).GroupBy(x => x.StoreID).Select(gr => new
            {
                a = gr.Key,
                c = gr.ToList(),
                b = gr.Count(),
            }).OrderByDescending(x => x.b).Take(5).ToListAsync();



            if (query != null)
            {
                response.Data = new();

                foreach (var user in query)
                {
                    var us = _context.MaterialStores.Include(x=>x.User).Where(x => x.Id == user.a).FirstOrDefault();
                    var storeinfo = new StoreOrderStatistic();
                    storeinfo.Name=us.User?.FirstName+" "+us.User?.LastName;
                    storeinfo.Place = us.Place;
                    storeinfo.Experience = us.Experience;
                    storeinfo.Description = us.Description;
                    storeinfo.TaxCode = us.TaxCode;
                    storeinfo.Image = us.User.Avatar;
                    storeinfo.Website = us.Website;
                    storeinfo.Id = us.Id;
                    storeinfo.OrderCount = user.b;
                    response.Data.Add(storeinfo);

                }
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
            }
            else
            {
                response.Data = null;
                response.Code = BaseCode.ERROR;
                response.Message = BaseCode.ERROR_MESSAGE;

            }
            return response;
        }

        public async Task<BasePagination<List<UserPaymentDTO>>> GetTop5PaymentContractor()
        {
            var response = new BasePagination<List<UserPaymentDTO>>();
            var query = await _context.Payments.Include(x => x.Users).Where(x => x.Users.Contractor != null).GroupBy(x => x.UserId).Select(gr => new
            {
                a = gr.Key,
                c = gr.ToList(),
                b = gr.Count(),
            }).OrderByDescending(x => x.b).Take(5).ToListAsync();



            if (query != null)
            {
                response.Data = new();

                foreach (var user in query)
                {
                    var us = _context.Users.Include(x => x.Contractor).Where(x => x.Id.ToString().Equals(user.a.ToString()) && x.Contractor != null).FirstOrDefault();
                    var contractorInfo = new UserPaymentDTO();
                    contractorInfo.UserId = us.Id;
                    contractorInfo.Address = us.Address;
                    contractorInfo.Phone = us.PhoneNumber;
                    contractorInfo.Avatar = us.Avatar;
                    contractorInfo.Status = us.Status;
                    contractorInfo.Email = us.Email;
                    contractorInfo.Avatar = us.Avatar;
                    contractorInfo.FirstName = us.FirstName;
                    contractorInfo.LastName = us.LastName;
                    contractorInfo.Contractor = new();
                    contractorInfo.Contractor.Description = us.Contractor.Description;
                    contractorInfo.Contractor.Website = us.Contractor.Website;
                    contractorInfo.Contractor.CompanyName = us.Contractor.CompanyName;
                    contractorInfo.Contractor.Id = us.Contractor.Id;
                    contractorInfo.TotalMoney = (user.b * 200000).ToString();
                    response.Data.Add(contractorInfo);
                }
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
            }
            else
            {
                response.Data = null;
                response.Code = BaseCode.ERROR;
                response.Message = BaseCode.ERROR_MESSAGE;

            }
            return response;
        }

        public async Task<BasePagination<List<UserPaymentDTO>>> GetTop5PaymentStore()
        {
            var response = new BasePagination<List<UserPaymentDTO>>();
            var query = await _context.Payments.Include(x => x.Users).Where(x => x.Users.MaterialStore != null).GroupBy(x => x.UserId).Select(gr => new
            {
                a = gr.Key,
                c = gr.ToList(),
                b = gr.Count(),
            }).OrderByDescending(x => x.b).Take(5).ToListAsync();



            if (query != null)
            {
                response.Data = new();

                foreach (var user in query)
                {
                    var us = _context.Users.Include(x => x.MaterialStore).Where(x => x.Id.ToString().Equals(user.a.ToString()) && x.MaterialStore != null).FirstOrDefault();
                    var storeinfo = new UserPaymentDTO();
                    storeinfo.UserId = us.Id;
                    storeinfo.Address = us.Address;
                    storeinfo.Phone = us.PhoneNumber;
                    storeinfo.Avatar = us.Avatar;
                    storeinfo.Status = us.Status;
                    storeinfo.Email = us.Email;
                    storeinfo.Avatar = us.Avatar;
                    storeinfo.FirstName = us.FirstName;
                    storeinfo.LastName = us.LastName;
                    storeinfo.DetailMaterialStore = new();
                    storeinfo.DetailMaterialStore.Place = us.MaterialStore.Place;
                    storeinfo.DetailMaterialStore.Experience = us.MaterialStore.Experience;
                    storeinfo.DetailMaterialStore.Description = us.MaterialStore.Description;
                    storeinfo.DetailMaterialStore.TaxCode = us.MaterialStore.TaxCode;
                    storeinfo.DetailMaterialStore.Image = us.MaterialStore.Image;
                    storeinfo.DetailMaterialStore.Website = us.MaterialStore.Website;
                    storeinfo.DetailMaterialStore.Id = us.MaterialStore.Id;
                    storeinfo.TotalMoney = (user.b * 200000).ToString();
                    response.Data.Add(storeinfo);
                }
                response.Code = BaseCode.SUCCESS;
                response.Message = BaseCode.SUCCESS_MESSAGE;
            }
            else
            {
                response.Data = null;
                response.Code = BaseCode.ERROR;
                response.Message = BaseCode.ERROR_MESSAGE;

            }
            return response;
        }

        public async Task<BasePagination<List<PaymentDTO>>> PaymentList()
        {
            var response = new BasePagination<List<PaymentDTO>>();
            response.Data = new();
            double totalPages;
            var totalRecords = await _context.Payments.Include(x => x.Users).CountAsync();
            totalPages = totalRecords / (double)25;

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            Pagination pagination = new()
            {
                CurrentPage = 1,
                PageSize = 25,
                TotalPages = roundedTotalPages,
                TotalRecords = totalRecords
            };
            var query = await _context.Payments.Include(x => x.Users).ToListAsync();
            if (query.Any())
            {
                foreach (var item in query)
                {
                    var dto = new PaymentDTO();
                    dto.FullName = item.Users.LastName + " " + item.Users.FirstName;
                    dto.Phonenumber = item.Users.PhoneNumber;
                    dto.Price = item.Price;
                    dto.PaymentDate = item.PaymentDate;
                    dto.ExpireationDate = item.ExpireationDate;
                    dto.IsRefund = (bool)item.IsRefund;
                    dto.TransactionId = item.TransactionId;
                    dto.VnPayResponseCode = item.VnPayResponseCode;
                    dto.PaymentId = item.PaymentId;
                    dto.ExtendDate = item.ExtendDate;
                    response.Data.Add(dto);
                }
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Code = BaseCode.SUCCESS;
                response.Pagination = pagination;
            }
            else
            {
                response.Data = null;
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Code = BaseCode.SUCCESS;
            }
            return response;
        }

      

        public async Task<BaseResponse<bool>> UpdateIsRefund()
        {
            var userId = _accessor.HttpContext?.User.FindFirst("UserID")?.Value.ToString();
            var query = await _context.Payments.Where(x => x.UserId.ToString().Equals(userId)).OrderByDescending(x => x.ExpireationDate).ToListAsync();
            var response = new BaseResponse<bool>();
            query.First().IsRefund = true;
            query.First().ExtendDate = null;
            _context.Payments.Update(query.First());
            var rs = await _context.SaveChangesAsync();
            if (rs > 0)
            {
                response.Data = true;
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Code = BaseCode.SUCCESS;
            }
            else
            {
                response.Data = false;
                response.Message = BaseCode.ERROR_MESSAGE;
                response.Code = BaseCode.ERROR;
            }
            return response;
        }


        public async Task<BasePagination<List<PaymentDTO>>> PaymentListByUser(Guid userId)
        {
            var response = new BasePagination<List<PaymentDTO>>();
            response.Data = new();
            double totalPages;
            var totalRecords = await _context.Payments
                .Include(x => x.Users)
                .Where(x=>x.UserId.Equals(userId))
                .CountAsync();
            totalPages = totalRecords / (double)25;

            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            Pagination pagination = new()
            {
                CurrentPage = 1,
                PageSize = 25,
                TotalPages = roundedTotalPages,
                TotalRecords = totalRecords
            };
            var query = await _context.Payments
                .Include(x => x.Users)
                .Where(x => x.UserId.Equals(userId))
                .ToListAsync();
            if (query.Any())
            {
                foreach (var item in query)
                {
                    var dto = new PaymentDTO();
                    dto.FullName = item.Users.LastName + " " + item.Users.FirstName;
                    dto.Phonenumber = item.Users.PhoneNumber;
                    dto.Price = item.Price;
                    dto.PaymentDate = item.PaymentDate;
                    dto.ExpireationDate = item.ExpireationDate;
                    dto.IsRefund = (bool)item.IsRefund;
                    dto.TransactionId = item.TransactionId;
                    dto.VnPayResponseCode = item.VnPayResponseCode;
                    dto.PaymentId = item.PaymentId;
                    dto.ExtendDate = item.ExtendDate;
                    response.Data.Add(dto);
                }
                response.Message = BaseCode.SUCCESS_MESSAGE;
                response.Code = BaseCode.SUCCESS;
                response.Pagination = pagination;
            }
            else
            {
                response.Data = null;
                response.Message = BaseCode.ERROR_MESSAGE;
                response.Code = BaseCode.ERROR;
            }
            return response;
        }
    }
}
