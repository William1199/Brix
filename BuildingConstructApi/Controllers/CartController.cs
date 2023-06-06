using Application.System.Carts;
using Application.System.Commitments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Carts;
using ViewModels.Pagination;


namespace BuildingConstructApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter request)
        {
            var validFilter = new PaginationFilter(request.PageNumber, request.PageSize, request._sortBy, request._orderBy);
            string? userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _cartService.GetAll(Guid.Parse(userID),request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCartRequest requests)
        {
            string? userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _cartService.Create(Guid.Parse(userID),requests);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] List<CreateCartRequest> requests)
        {
            string? userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _cartService.Update(Guid.Parse(userID), requests);
            return Ok(result);
        }

        [HttpPut("delete")]
        public async Task<IActionResult> Delete([FromBody] List<RemoveCartRequest> requests)
        {
            string? userID = User.FindFirst("UserID")?.Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _cartService.Remove(Guid.Parse(userID), requests);
            return Ok(result);
        }
    }
}
