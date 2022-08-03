namespace boilerplate.api.core.Models
{
    public class RefEntriesQueryRequest: BaseRequest
    {
        public string Filter { get; set; }
        public int Limit { get; set; }
    }
}
