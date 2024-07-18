namespace Api.Dto
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool Hasherror { get; set; } = false;
        public T Data { get; set; }
    }
}
