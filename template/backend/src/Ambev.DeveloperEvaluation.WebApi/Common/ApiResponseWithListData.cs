namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public class ApiResponseWithListData<T> : ApiResponse
    {
        public List<T> Data { get; set; } = new();
    }
}