using Api.Dto;

namespace Api.Services
{
    public interface IRoomService
    {
        Task<BaseResponse<Guid>> CreateRoom(CreateRoom request);
        Task<BaseResponse<Guid>> DeleteRoomAsync(Guid Id);
        Task<BaseResponse<IList<RoomDto>>> GetAllRoomsCreatedAsync();
        Task<BaseResponse<RoomDto>> GetRoomsByIdAsync(Guid Id);
        Task<BaseResponse<RoomDto>> UpdateRoom(Guid Id, UpdateRoom request);
    }
}
