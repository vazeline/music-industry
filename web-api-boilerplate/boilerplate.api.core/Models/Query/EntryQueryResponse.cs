namespace boilerplate.api.core.Models
{
    public class EntryQueryResponse<T>: BaseResponse
    {
        public T Data { get; set; }
    }
}
