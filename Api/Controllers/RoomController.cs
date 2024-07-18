using Api.Dto;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("create-room")]
        public async Task<IActionResult> CreateRoom(CreateRoom request)
        {
            var room = await _roomService.CreateRoom(request);
            if (room.Success)
            {
                return Ok(room);
            }
            else
            {
                return BadRequest(room);
            }
        }


        [HttpPost("edit-room")]
        public async Task<IActionResult> EditRoom([FromBody] UpdateRoom request)
        {

            var room = await _roomService.UpdateRoom(request.Id, request);
            if (room.Success == false)
            {
                return Ok(room);
            }
            else
            {
                return BadRequest(room);
            }
        }

        [HttpDelete("delete-room")]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] Guid id)
        {
            var room = await _roomService.DeleteRoomAsync(id);
            if (room.Success == false)
            {
                return Ok(room);
            }
            return BadRequest(room);

        }
        [HttpGet("get-all-rooms-created")]
        public async Task<IActionResult> GetAllRoomsCreatedAsync()
        {
            var rooms = await _roomService.GetAllRoomsCreatedAsync();
            if (rooms.Success == false)
            {
                return Ok(rooms);
            }
            else
            {
                return BadRequest(rooms);
            }


        }

        [HttpGet("get-room-by-id/{id}")]
        public async Task<IActionResult> GetRoomsByIdAsync(Guid id)
        {

            var rooms = await _roomService.GetRoomsByIdAsync(id);
            if (rooms.Success == false)
            {
                return Ok(rooms);
            }
            else
            {
                return BadRequest(rooms);
            }

        }
    }
}
