namespace Pizzaria.API.Domain.ResponseModel
{
    public class ErrorResponse
    {
        public string Status { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
    }

    public class ErrorResponse<T> : ErrorResponse
    {
        public T Request { get; set; }
    }
}
