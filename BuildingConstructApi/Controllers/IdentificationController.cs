using Application.System.Identification;
using Application.System.Notifies;
using BuildingConstructApi.Hubs;
using Data.Enum;
using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Text;
using ViewModels.Identification;
using ViewModels.Notificate;
using ViewModels.Pagination;

namespace BuildingConstructApi.Controllers
{
    [Route("api/identification")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class IdentificationController : ControllerBase
    {
        private readonly IIdentificationService _identificationService;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;

        public IdentificationController(IIdentificationService identificationService, IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager)
        {
            _identificationService = identificationService;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
        }
        [HttpPost("getAll")]
        public async Task<IActionResult> GetAll([FromBody] PaginationFilter request)
        {

            var result = await _identificationService.GetAll(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {

            var result = await _identificationService.GetDetail(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIndetificationRequest requests)
        {
            var userID = User.FindFirst("UserID").Value;

            if (userID == null)
            {
                return BadRequest();
            }
            var result = await _identificationService.Create(Guid.Parse(userID), requests);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Status status)
        {

            var result = await _identificationService.Update(id, status);
            var connections = _userConnectionManager.GetUserConnections(result.Data.Id);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {

                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("UpdateProfile",result.Data);

                }
            }
            return Ok(result);
        }



        [HttpPost("test")]
        public async Task<IActionResult> Test([FromBody] DetectFaceRequest request)
        {
            Mat front;
            //var test= new MemoryStream(Encoding.UTF8.GetBytes(request.Image));
            //using (var memoryStream = new MemoryStream())
            //{
            //    await test.CopyToAsync(memoryStream);
            //    using (var img = Image.FromStream(test))
            //    {
            //       

            //    }
            //}

            byte[] bytes = Convert.FromBase64String(request.Image);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                front = GetMatFromSDImage(image);
                return Ok(_identificationService.DetectFace(front));
            }

        }

        private Mat GetMatFromSDImage(System.Drawing.Image image)
        {
            int stride = 0;
            Bitmap bmp = new Bitmap(image);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
            if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                stride = bmp.Width * 4;
            }
            else
            {
                stride = bmp.Width * 3;
            }

            Image<Bgra, byte> cvImage = new Image<Bgra, byte>(bmp.Width, bmp.Height, stride, (IntPtr)bmpData.Scan0);

            bmp.UnlockBits(bmpData);

            return cvImage.Mat;
        }
    }
}
