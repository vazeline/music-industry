using boilerplate.api.core.Models;
using System;

namespace boilerplate.ui.Models
{
    public class ServiceResult
    {
        public ServiceResult(BaseResponse apiResponse)
        {
            Status = new ServiceResultStatus
            {
                Success = apiResponse.Success,
                Code = apiResponse.Code,
                ErrorMessage = apiResponse.ErrorMessage
            };
        }

        public ServiceResult(ServiceResultStatus status)
        {
            Status = status;
        }

        public ServiceResultStatus Status { get; set; }

        public static ServiceResult CreateInstance(BaseResponse apiResponse)
        {
            return new ServiceResult(apiResponse);
        }

        public static ServiceResult CreateInstance(ServiceResultStatus status)
        {
            return new ServiceResult(status);
        }

        public static ServiceResult CreateErrorInstance(string errorMessage, ResponseCode code)
        {
            var apiResponse = new BaseResponse()
            {
                Success = false,
                ErrorMessage = errorMessage,
                Code = code
            };
            return new ServiceResult(apiResponse);
        }

        public static ServiceResult<K> CreateInstance<K>(BaseResponse apiResponse,
            K data, Paging paging = null)
        {
            return new ServiceResult<K>(apiResponse)
            {
                ViewModel = new ViewModel<K>
                {
                    Paging = paging,
                    Data = data
                }
            };
        }
        public static ServiceResult<K> CreateErrorInstance<K>(string errorMessage, ResponseCode code)
        {
            var apiResponse = new BaseResponse()
            {
                Success = false,
                ErrorMessage = errorMessage,
                Code = code
            };

            return new ServiceResult<K>(apiResponse)
            {
                ViewModel = new ViewModel<K>
                {
                    Paging = null,
                    Data = default
                }
            };
        }
    }
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(BaseResponse apiResponse) : base(apiResponse) { }
        public ViewModel<T> ViewModel { get; set; }
    }

    public class ViewModel<T>
    {
        public Paging Paging { get; set; }
        public T Data { get; set; }
    }

    public class Paging
    {
        public Paging(int totalCount, int offset, int limit, Func<int, int, string> getUrl)
        {
            TotalCount = totalCount;
            Offset = offset;
            Limit = limit;
            GetUrl = getUrl;
        }
        public int TotalCount { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Func<int, int, string> GetUrl { get; set; }
        public int CurrentPage => (Offset / Limit) + 1;
        public int Pages => (int)Math.Ceiling((float)TotalCount / Limit);

        public int CaclOffset(int page, int limit)
        {
            return (page - 1) * limit;
        }
    }

    public class PagingUrls
    {
        public int Number { get; set; }

        public string Url { get; set; }
    }

    public class ServiceResultStatus
    {
        public bool Success { get; set; }
        public ResponseCode Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}
