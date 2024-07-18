using Api.Data;
using Api.Dto;
using Api.Entity;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _dbContext;

        public RoomService(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        public async Task<BaseResponse<Guid>> CreateRoom(CreateRoom request)
        {


            if (request != null)
            {
                var existingRoom = await _dbContext.Rooms.FirstOrDefaultAsync(x =>
                        x.Id == request.Id);

                if (existingRoom != null)
                {
                    // Room already exists
                    return new BaseResponse<Guid>
                    {
                        Success = true,
                        Message = $"Room {request.RoomName} already exists.",
                        Hasherror = true
                    };
                }

                //create a new one
                var room = new Room
                {
                    RoomName = request.RoomName,
                    Description = request.Description,
                    RoomPrize = request.RoomPrize,
                    RoomType = request.RoomType,
                    MaxOccupancy = request.MaxOccupancy
                };

                await _dbContext.Rooms.AddAsync(room);
                _dbContext.SaveChanges();
            }
            return new BaseResponse<Guid>
            {
                Success = true,
                Message = $"Room {request.RoomName} Created Successfully"

            };



        }


        public async Task<BaseResponse<Guid>> DeleteRoomAsync(Guid Id)
        {
            try
            {
                var room = await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == Id);
                if (room == null)
                {
                    return new BaseResponse<Guid>
                    {
                        Success = false,
                        Message = $"Room with ID {Id} not found."
                    };
                }

                _dbContext.Rooms.Remove(room);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return new BaseResponse<Guid>
                    {
                        Success = true,
                        Message = $"Room with ID {Id} has been deleted successfully.",
                        Data = Id
                    };
                }
                else
                {
                    return new BaseResponse<Guid>
                    {
                        Success = false,
                        Message = $"Failed to delete room with ID {Id}. There was an error in the deletion process."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Guid>
                {
                    Success = false,
                    Message = $"An error occurred while deleting the room with ID {Id}: {ex.Message}"
                };
            }
        }


        public async Task<BaseResponse<IList<RoomDto>>> GetAllRoomsCreatedAsync()
        {
            var rooms = await _dbContext.Rooms
             .Select(x => new RoomDto()
             {
                 Id = x.Id,
                 RoomName = x.RoomName,
                 Description = x.Description,
                 RoomPrize = x.RoomPrize,
                 RoomType = x.RoomType,
                 MaxOccupancy = x.MaxOccupancy,
                 // Amenity = x.Amenity
             }).ToListAsync();


            return new BaseResponse<IList<RoomDto>>
            {
                Success = true,
                Message = "Rooms Succesfully Retrieved",
                Data = rooms
            };
        }


        public async Task<BaseResponse<RoomDto>> GetRoomsByIdAsync(Guid Id)
        {

            var rooms = await _dbContext.Rooms
             .Where(x => x.Id == Id)
             .Select(x => new RoomDto()
             {
                 Id = x.Id,
                 RoomName = x.RoomName,
                 Description = x.Description,
                 RoomPrize = x.RoomPrize,
                 RoomType = x.RoomType,
                 MaxOccupancy = x.MaxOccupancy,
             }).FirstOrDefaultAsync();
            if (rooms != null)
            {
                return new BaseResponse<RoomDto>
                {
                    Success = true,
                    Message = $"Room {Id} Retrieved succesfully",
                    Data = rooms

                };
            }
            else
            {
                return new BaseResponse<RoomDto>
                {
                    Success = false,
                    Message = $"Room {Id} Retrieval Failed"
                };
            }

        }



        public async Task<BaseResponse<RoomDto>> UpdateRoom(Guid Id, UpdateRoom request)
        {
            try
            {
                var room = _dbContext.Rooms.FirstOrDefault(x => x.Id == Id);
                if (room == null)
                {
                    return new BaseResponse<RoomDto>
                    {
                        Success = false,
                        Message = $"Room {request.RoomName} Update failed",
                        Hasherror = true
                    };

                }
                room.Description = request.Description;
                room.RoomPrize = request.RoomPrize;
                room.RoomType = request.RoomType;
                room.MaxOccupancy = request.MaxOccupancy;
                _dbContext.Rooms.Update(room);
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    return new BaseResponse<RoomDto>
                    {
                        Success = true,
                        Message = $"Room {request.Id} Updated Succesfully",
                    };

                }
                else
                {
                    return new BaseResponse<RoomDto>
                    {
                        Success = false,
                        Message = $"Room {request.Id} Update failed",
                        Hasherror = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<RoomDto>
                {
                    Success = false,
                    Message = $"Room {request.Id} Update failed",
                    Hasherror = true
                };
            }


        }

    }
}
