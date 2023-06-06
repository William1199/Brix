using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Pagination;
using ViewModels.Response;
using ViewModels.Notificate;

namespace Application.System.Notifies
{
    public class NotificationServices : INotificationServices
    {
        private readonly BuildingConstructDbContext _context;
        private IHttpContextAccessor _accessor;

        public NotificationServices(BuildingConstructDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public async Task<BasePagination<List<NotificationDTO>>> GetAllNotification(PaginationFilter filter, Guid UserId)
        {
            BasePagination<List<NotificationDTO>> response = new();
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

            IQueryable<Notification> query = _context.Notifcations;


            var data = await query
                .Include(x => x.User)
                .AsNoTracking()
                .Where(x => x.UserID.Equals(UserId))
                .OrderBy(filter._sortBy + " " + orderBy)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();


            var totalRecords = await _context.Notifcations.Where(x => x.UserID.Equals(UserId)).CountAsync();

            if (data == null)
            {

                response.Code = BaseCode.ERROR;
                response.Message = "Cannot find!!!";
                return response;
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
                    Data = await MapListDTO(data),
                    Pagination = pagination
                };

            }

            return response;
        }
        public async Task<List<NotificationDTO>> MapListDTO(List<Notification> list)
        {
            List<NotificationDTO> result = new();

            foreach (var item in list)
            {
                NotificationDTO dto = new();
                dto.Id = item.Id;
                dto.UserID = item.UserID;
                dto.Title = item.Title;
                dto.Type = item.Type;
                dto.Message = item.Message;
                dto.IsRead = item.IsRead;
                dto.CreateBy = item.CreateBy;
                dto.LastModifiedAt = item.LastModifiedAt;
                dto.NavigateId = item.NavigateId;
                var user = await _context.Users.FindAsync(item.CreateBy);
                if (user != null)
                {
                    dto.Author = new();
                    dto.Author.FirstName = user.FirstName;
                    dto.Author.LastName = user.LastName;
                    dto.Author.Avatar = user.Avatar;
                }

                result.Add(dto);
            }
            return result;
        }

        public async Task<BaseResponse<string>> UpdateIsRead(int Id)
        {
            BaseResponse<string> response;
            var noti = await _context.Notifcations.FirstOrDefaultAsync(x => x.Id == Id);

            if (noti != null)
            {
                noti.IsRead = true;
                _context.Update(noti);
                await _context.SaveChangesAsync();

                response = new()
                {
                    Code = BaseCode.SUCCESS,
                    Message = BaseCode.SUCCESS_MESSAGE
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
    }
}
