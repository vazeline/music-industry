namespace boilerplate.api.core.Models
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public ResponseCode Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}
