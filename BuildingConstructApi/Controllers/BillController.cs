using Application.System.Bill;
using Application.System.Notifies;
using BuildingConstructApi.Hubs;
using Data.DataContext;
using Data.Entities;
using Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ViewModels.BillModels;
using ViewModels.Notificate;
using ViewModels.Pagination;
using ViewModels.Response;

namespace BuildingConstructApi.Controllers
{
    [Route("api/billController")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BillController : ControllerBase
    {
        private readonly IBillServices _billServices;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly BuildingConstructDbContext _context;

        public BillController(IBillServices billServices, IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, BuildingConstructDbContext context)
        {
            _billServices = billServices;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _context = context;
        }

        [HttpPost("createBill")]
        public async Task<IActionResult> CreateBill([FromBody] List<BillDTO> request)
        {
            string? userID = User.FindFirst("UserID")?.Value;

            var rs = await _billServices.CreateBill(request);


                foreach (var item in request)
                {
                    var contractorID = await _context.Users.Where(x => x.Id.Equals(Guid.Parse(userID))).Select(x => x.ContractorId).FirstOrDefaultAsync();
                    var author = await _context.Users.Where(x => x.MaterialStoreID.Equals(item.StoreID)).FirstOrDefaultAsync();
                    var newestBill = await _context.Bills.Where(x => x.StoreID.Equals(item.StoreID) && x.ContractorId == contractorID).Select(x => x.Id).FirstOrDefaultAsync();

                    var store = await _context.Users.Where(x => x.MaterialStoreID == item.StoreID).FirstOrDefaultAsync();

                    NotificateAuthor notiAuthor = new()
                    {
                        Avatar = author.Avatar,
                        FirstName = "Bạn",
                        LastName = "có",
                    };

                    NotificationModels noti = new()
                    {
                        LastModifiedAt = DateTime.Now,
                        CreateBy = Guid.Parse(userID),
                        NavigateId = newestBill,
                        UserId = store.Id,
                        Message = NotificationMessage.CREATE_BILL,
                        NotificationType = NotificationType.BILL_NOTIFICATION,
                        Author = notiAuthor,
                    };

                    var check = await _userConnectionManager.SaveNotification(noti);
                    var connections = _userConnectionManager.GetUserConnections(store.Id.ToString());
                    if (connections != null && connections.Count > 0)
                    {
                        foreach (var connectionId in connections)
                        {

                            if (check != null)
                            {
                                noti.Id = check.Data.Id;
                                await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", noti);

                            }
                        }
                    }
                }
            return Ok(rs);
        }
        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllBill([FromBody] PaginationFilter request)
        {
            var validFilter = new PaginationFilter();

            if (request.FilterRequest == null)
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            }
            else
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy, request.FilterRequest);

            }

            var result = await _billServices.GetAllBill(request);
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetBillDetail([FromRoute] int id)
        //{
        //    var rs = await _billServices.GetDetail(id);

        //    return Ok(rs);
        //}

        [HttpGet("small/{id}")]
        public async Task<IActionResult> GetSmallBill([FromRoute] int id)
        {
            var rs = await _billServices.GetDetailBySmallBill(id);

            return Ok(rs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBillStatus([FromRoute] int id, [FromBody] UpdateBillRequest request)
        {
            string? userID = User.FindFirst("UserID")?.Value;
            var rs = await _billServices.UpdateStatusBill(request.Status, id, request.Message, Guid.Parse(userID));

            NotificateAuthor notiAuthor = null;
            NotificationModels noti = null;
            string receivedId = string.Empty;


            //Xác nhận 
            if (request.Status == Status.ACCEPTED)
            {
                var author = await _context.Bills
                    .Include(x => x.MaterialStore)
                        .ThenInclude(x => x.User)
                    .Include(x => x.Contractor)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (author != null)
                {

                    notiAuthor = new()
                    {
                        Avatar = author.MaterialStore?.User?.Avatar,
                        FirstName = "Đơn hàng",
                        LastName = "của bạn",
                    };

                    noti = new()
                    {
                        LastModifiedAt = DateTime.Now,
                        CreateBy = author.MaterialStore.CreateBy,
                        NavigateId = rs.NavigateId,
                        UserId = author.Contractor.CreateBy,
                        Message = NotificationMessage.UPDATE_BILL_ACCEPTED,
                        NotificationType = NotificationType.BILL_NOTIFICATION,
                        Author = notiAuthor,
                    };

                    receivedId = author.Contractor.CreateBy.ToString();


                }
            }
            //Vận chuyển
            if (request.Status == Status.PAID)
            {
                var author = await _context.Bills
                    .Include(x => x.MaterialStore)
                        .ThenInclude(x => x.User)
                    .Include(x => x.Contractor)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (author != null)
                {

                    notiAuthor = new()
                    {
                        Avatar = author.MaterialStore?.User?.Avatar,
                        FirstName = author.MaterialStore?.User?.FirstName,
                        LastName = author.MaterialStore?.User?.LastName,
                    };

                    noti = new()
                    {
                        LastModifiedAt = DateTime.Now,
                        CreateBy = author.MaterialStore.CreateBy,
                        NavigateId = rs.NavigateId,
                        UserId = author.Contractor.CreateBy,
                        Message = NotificationMessage.UPDATE_BILL_DELIVERD,
                        NotificationType = NotificationType.BILL_NOTIFICATION,
                        Author = notiAuthor,
                    };

                    receivedId = author.Contractor.CreateBy.ToString();

                }
            }
            //Nhận hàng
            if (request.Status == Status.SUCCESS)
            {
                var author = await _context.Bills
                    .Include(x => x.MaterialStore)
                        .ThenInclude(x => x.User)
                    .Include(x => x.Contractor)
                        .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (author != null)
                {

                    notiAuthor = new()
                    {
                        Avatar = author.Contractor?.User?.Avatar,
                        FirstName = author.Contractor?.User?.FirstName,
                        LastName = author.Contractor?.User?.LastName,
                    };

                    noti = new()
                    {
                        LastModifiedAt = DateTime.Now,
                        CreateBy = author.Contractor.CreateBy,
                        NavigateId = rs.NavigateId,
                        UserId = author.MaterialStore.CreateBy,
                        Message = NotificationMessage.UPDATE_BILL_DELIVERD,
                        NotificationType = NotificationType.BILL_NOTIFICATION,
                        Author = notiAuthor,
                    };

                    receivedId = author.MaterialStore.CreateBy.ToString();

                }
            }
            //Hủy 
            if (request.Status == Status.CANCEL)
            {
                var author = await _context.Bills
                   .Include(x => x.MaterialStore)
                       .ThenInclude(x => x.User)
                   .Include(x => x.Contractor)
                       .ThenInclude(x => x.User)
                   .FirstOrDefaultAsync(x => x.Id == id);

                if (author != null)
                {

                    notiAuthor = new()
                    {
                        Avatar = author.Contractor?.User?.Avatar,
                        FirstName = author.Contractor?.User?.FirstName,
                        LastName = author.Contractor?.User?.LastName,
                    };

                    noti = new()
                    {
                        LastModifiedAt = DateTime.Now,
                        CreateBy = author.Contractor.CreateBy,
                        NavigateId = rs.NavigateId,
                        UserId = author.MaterialStore.CreateBy,
                        Message = NotificationMessage.UPDATE_BILL_CANCELED,
                        NotificationType = NotificationType.BILL_NOTIFICATION,
                        Author = notiAuthor,
                    };

                    receivedId = author.MaterialStore.CreateBy.ToString();
                }
            }


            var check = await _userConnectionManager.SaveNotification(noti);
            var connections = _userConnectionManager.GetUserConnections(receivedId);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {

                    if (check != null)
                    {
                        noti.Id = check.Data.Id;
                        await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", noti);

                    }
                }
            }



            return Ok(rs);
        }


        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryProductBill([FromQuery] PaginationFilter request)
        {
            var userID = User.FindFirst("UserID").Value;
            var validFilter = new PaginationFilter();


            if (userID == null)
            {
                return BadRequest();
            }

            if (request.FilterRequest == null)
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            }
            else
            {
                validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy, request.FilterRequest);

            }


            var rs = await _billServices.GetHistoryProductBill(Guid.Parse(userID), validFilter);

            return Ok(rs);
        }

    }
}
