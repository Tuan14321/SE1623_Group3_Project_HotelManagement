using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel_Management_API.Models;
using Hotel_Management_API.DTO;

namespace Hotel_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly DataHotelContext _context;

        public RoomsController(DataHotelContext context)
        {
            _context = context;
        }

        [HttpGet("GetRoom")]
        public IActionResult GetRooms()
        {
            var rooms = _context.Rooms
                .Include(x => x.Type)
                .Include(x => x.Floor)
                .Include(x => x.Status)
                .ToList();

            // Ánh xạ các phòng và các thuộc tính liên quan vào một danh sách mới
            var roomList = rooms.Select(room => new RoomDTO
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                TypeName = room.Type?.Name,
                StatusName = room.Status?.StatusName,
                FloorName = room.Floor?.FloorName
            }).ToList();

            return Ok(roomList);
        }

        // GET: api/Rooms/5
        [HttpGet("GetRoom/{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _context.Rooms
                .Include(x => x.Type)
                .Include(x => x.Floor)
                .Include(x => x.Status)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }

            //var roomInfo = new RoomDTO
            //{
            //    RoomId = room.RoomId,
            //    RoomName = room.RoomName,
            //    TypeId = room.Type.Name,
            //    StatusId = room.Status.StatusName,
            //    FloorId = room.Floor.FloorName
            //};
            var roomDTO = new RoomDTO
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                TypeName = room.Type?.Name,
                StatusName = room.Status?.StatusName,
                FloorName = room.Floor?.FloorName
            };

            return Ok(roomDTO);

            
        }

        // POST: api/Rooms
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDTORequest room)
        {
            

            _context.Rooms.Add(new Room
            {
                RoomId = room.RoomId,
                RoomName = room.RoomName,
                TypeId = room.TypeId,
                StatusId = room.StatusId,
                FloorId = room.FloorId

            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.RoomId }, room);
        }

        // PUT: api/Rooms/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomDTORequest room)
        {
            

            var existingRoom = await _context.Rooms.FindAsync(id);

            if (existingRoom == null)
            {
                return NotFound();
            }

            // Cập nhật thuộc tính của phòng từ room
            existingRoom.RoomName = room.RoomName;
            existingRoom.TypeId = room.TypeId;
            existingRoom.StatusId = room.StatusId;
            existingRoom.FloorId = room.FloorId;

            _context.Entry(existingRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Rooms/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
