using System.Collections.Generic;

namespace boilerplate.api.core.Models
{
    public class EntriesQueryResponse<T>: BaseResponse
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
