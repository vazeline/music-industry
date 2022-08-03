using AutoMapper;
using boilerplate.api.common.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public abstract class BaseService<TStore> : IBaseService
        where TStore : class, IBaseStore
    {
        protected TStore Store { get; init; }
        protected ILogger<BaseService<TStore>> Logger { get; init; }
        protected readonly IMapper _mapper;

        public BaseService(TStore store, ILogger<BaseService<TStore>> logger, IMapper mapper)
        {
            Store = store ?? ThrowHelper.NullArgument<TStore>();
            Logger = logger ?? ThrowHelper.NullArgument<ILogger<BaseService<TStore>>>();
            _mapper = mapper ?? ThrowHelper.NullArgument<IMapper>();
        }

        public virtual async Task<EntriesQueryResponse<T>> GetEntries<T>(EntriesQueryRequest request)
        {
            if (request == null)
            {
                return new EntriesQueryResponse<T>
                {
                    Success = false,
                    Code = ResponseCode.BadRequest,
                    ErrorMessage = "Request is null."
                };
            }

            try
            {
                return await Store.GetEntries<T>(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return new EntriesQueryResponse<T>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }

        public virtual async Task<EntryQueryResponse<T>> GetEntry<T, K>(EntryQueryRequest<K> request)
        {
            if (request == null)
            {
                return new EntryQueryResponse<T>
                {
                    Success = false,
                    Code = ResponseCode.BadRequest,
                    ErrorMessage = "Request is null."
                };
            }

            try
            {
                return await Store.GetEntry<T, K>(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return new EntryQueryResponse<T>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }

        public virtual async Task<CreateCommandResponse<K>> CreateEntry<T, K>(CreateCommandRequest<T> request)
        {
            if (request == null)
            {
                return new CreateCommandResponse<K>
                {
                    Success = false,
                    Code = ResponseCode.BadRequest,
                    ErrorMessage = "Request is null."
                };
            }

            try
            {
                var id = await Store.CreateEntry<T, K>(request).ConfigureAwait(false);
                return new CreateCommandResponse<K>
                {
                    Id = id,
                    Success = true,
                    Code = ResponseCode.Success
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return new CreateCommandResponse<K>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }

        public virtual async Task<UpdateCommandResponse> UpdateEntry<T, K>(UpdateCommandRequest<T> request)
        {
            if (request == null)
            {
                return new UpdateCommandResponse
                {
                    Success = false,
                    Code = ResponseCode.BadRequest,
                    ErrorMessage = "Request is null."
                };
            }

            try
            {
                var success = await Store.UpdateEntry<T, K>(request).ConfigureAwait(false);
                if (!success)
                {
                    return new UpdateCommandResponse
                    {
                        Success = false,
                        Code = ResponseCode.NotFound
                    };
                }
                return new UpdateCommandResponse
                {
                    Success = true,
                    Code = ResponseCode.Success
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return new UpdateCommandResponse
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }

        public virtual async Task<DeleteCommandResponse> DeleteEntry<T>(DeleteCommandRequest<T> request)
        {
            if (request == null)
            {
                return new DeleteCommandResponse
                {
                    Success = false,
                    Code = ResponseCode.BadRequest,
                    ErrorMessage = "Request is null."
                };
            }

            try
            {
                var success = await Store.DeleteEntry(request).ConfigureAwait(false);
                if (!success)
                {
                    return new DeleteCommandResponse
                    {
                        Success = false,
                        Code = ResponseCode.NotFound
                    };
                }
                return new DeleteCommandResponse
                {
                    Success = true,
                    Code = ResponseCode.Success
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return new DeleteCommandResponse
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
