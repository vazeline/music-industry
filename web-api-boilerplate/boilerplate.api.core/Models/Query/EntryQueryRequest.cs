namespace boilerplate.api.core.Models
{
    public class EntryQueryRequest<T> : BaseRequest
    {
        public T Id { get; set; }
    }
}
