using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomManager _roomManager;

        public RoomController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _roomManager.GetAll());
        }
    }
}
