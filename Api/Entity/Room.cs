namespace Api.Entity
{
    public class Room
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public int MaxOccupancy { get; set; }
        public decimal RoomPrize { get; set; }
        public string Description { get; set; }
    }
}
