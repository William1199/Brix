using Application.System.Notifies;
using Application.System.Payments;
using BuildingConstructApi.Hubs;
using Data.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ViewModels.Pagination;

namespace BuildingConstructApi.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentsService _paymentService;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly BuildingConstructDbContext _context;

        public PaymentController(IPaymentsService paymentService, IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, BuildingConstructDbContext context)
        {
            _paymentService = paymentService;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _context = context;
        }
        [HttpPost("checkRefund")]
        public async Task<IActionResult> CheckPayment() {
            var result = await _paymentService.CheckRefundPayment();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> PaymentList()
        {
            var result = await _paymentService.PaymentList();
            return Ok(result);
        }
        [HttpPost("UpdateRefund")]
        public async Task<IActionResult> UpdateRefund()
        {
            var result = await _paymentService.UpdateIsRefund();
            return Ok(result);
        }
        [HttpGet("getTop5ContractorPayment")]
        public async Task<IActionResult> GetTop5PaymentContractor()
        {
            var result = await _paymentService.GetTop5PaymentContractor();
            return Ok(result);
        }
        [HttpGet("getTop5StorePayment")]
        public async Task<IActionResult> GetTop5PaymentStore()
        {
            var result = await _paymentService.GetTop5PaymentStore();
            return Ok(result);
        }
        [HttpGet("getTop5StoreOrder")]
        public async Task<IActionResult> GetTop5StoreOrder()
        {
            var result = await _paymentService.GetTop5OrderStore();
            return Ok(result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetPaymentListUser()
        {
            string? id = User.FindFirst("UserID")?.Value;

            if(id == null)
            {
                return Ok();
            }
            var result = await _paymentService.PaymentListByUser(Guid.Parse(id));
            return Ok(result);
        }
    }
}
