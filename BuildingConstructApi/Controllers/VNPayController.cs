   using Application.System.VnPay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Payment;

namespace BuildingConstructApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class VNPayController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public VNPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Ok(url);
        }
        [HttpGet]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
           
            return Ok(response);
        }
        [HttpPost("StorePayment")]
        public async Task<IActionResult> StorePayment(PaymentRequestDTO response)
        {
            var rs =await _vnPayService.StoreDB(response);
            return Ok(rs);
        }
    }
}
