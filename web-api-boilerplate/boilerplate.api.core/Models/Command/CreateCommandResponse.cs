namespace boilerplate.api.core.Models
{
    public class CreateCommandResponse<T> : BaseResponse
    {
        public T Id { get; set; }
    }
}
