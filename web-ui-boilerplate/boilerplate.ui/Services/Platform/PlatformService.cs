using boilerplate.api.core.Clients;
using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.ui.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IPlatformClient _client;
        private readonly ILogger<PlatformService> _logger;
        public PlatformService(IPlatformClient client, ILogger<PlatformService> logger)
        {
            _client = client ?? ThrowHelper.NullArgument<IPlatformClient>();
            _logger = logger ?? ThrowHelper.NullArgument<ILogger<PlatformService>>();
        }

        public async Task<ServiceResult<PlatformGetEntriesViewModel>> GetEntries(int offset, int limit)
        {
            try
            {
                var response = await _client.GetEntries(new EntriesQueryRequest { Offset = offset, Limit = limit });
                return ServiceResult.CreateInstance(
                    response,
                    new PlatformGetEntriesViewModel
                    {
                        Entries = response.Data
                    },
                    new Paging(response.TotalCount, offset, limit, (o, l) => UIRoutesHelper.Platform.GetEntries.GetUrl(o, l))
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<PlatformGetEntriesViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public ServiceResult<PlatformCreateEntryViewModel> GetCreateEntryViewModel()
        {
            return ServiceResult.CreateInstance(new BaseResponse { Success = true, Code = ResponseCode.Success }, new PlatformCreateEntryViewModel());
        }

        public async Task<ServiceResult> CreateEntry(PlatformCreateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.CreateEntry(new CreateCommandRequest<PlatformCreateModel> { Entry = model });
                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }

        }

        public async Task<ServiceResult<PlatformUpdateEntryViewModel>> GetUpdateEntryViewModel(int id)
        {
            try
            {
                var response = await _client.GetEntry(id);
                if (!response.Success)
                {
                    return ServiceResult.CreateErrorInstance<PlatformUpdateEntryViewModel>(response.ErrorMessage, response.Code);
                }

                return ServiceResult.CreateInstance(
                    response,
                    new PlatformUpdateEntryViewModel
                    {
                        Form = new PlatformUpdateEntryViewModel.FormModel
                        {
                            Id = response.Data.Id,
                            Name = response.Data.Name
                        }
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<PlatformUpdateEntryViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult> UpdateEntry(PlatformUpdateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.UpdateEntry(model.Id, new UpdateCommandRequest<PlatformUpdateModel> { Entry = model });
                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult> DeleteEntry(int id)
        {
            try
            {
                var response = await _client.DeleteEntry(id);
                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }
        }
    }
}
