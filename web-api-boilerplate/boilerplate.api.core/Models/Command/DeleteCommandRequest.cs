namespace boilerplate.api.core.Models
{
    public class DeleteCommandRequest<T> : BaseRequest
    {
        public T Id { get; set; }
    }
}
