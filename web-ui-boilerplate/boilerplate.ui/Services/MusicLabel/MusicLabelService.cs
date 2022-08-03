using boilerplate.api.core.Clients;
using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.ui.Services
{
    public class MusicLabelService : IMusicLabelService
    {
        private readonly IMusicLabelClient _client;
        private readonly ILogger<MusicLabelService> _logger;
        public MusicLabelService(IMusicLabelClient client, ILogger<MusicLabelService> logger)
        {
            _client = client ?? ThrowHelper.NullArgument<IMusicLabelClient>();
            _logger = logger ?? ThrowHelper.NullArgument<ILogger<MusicLabelService>>();
        }

        public async Task<ServiceResult<MusicLabelGetEntriesViewModel>> GetEntries(int offset, int limit)
        {
            try
            {
                var response = await _client.GetEntries(new EntriesQueryRequest { Offset = offset, Limit = limit });
                return ServiceResult.CreateInstance(
                    response,
                    new MusicLabelGetEntriesViewModel
                    {
                        Entries = response.Data
                    },
                    new Paging(response.TotalCount, offset, limit, (o, l) => UIRoutesHelper.MusicLabel.GetEntries.GetUrl(o, l))
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<MusicLabelGetEntriesViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public ServiceResult<MusicLabelCreateEntryViewModel> GetCreateEntryViewModel()
        {
            return ServiceResult.CreateInstance(new BaseResponse { Success = true, Code = ResponseCode.Success }, new MusicLabelCreateEntryViewModel());
        }

        public async Task<ServiceResult> CreateEntry(MusicLabelCreateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.CreateEntry(new CreateCommandRequest<MusicLabelCreateModel> { Entry = model });
                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }

        }

        public async Task<ServiceResult<MusicLabelUpdateEntryViewModel>> GetUpdateEntryViewModel(int id)
        {
            try
            {
                var response = await _client.GetEntry(id);
                if (!response.Success)
                {
                    return ServiceResult.CreateErrorInstance<MusicLabelUpdateEntryViewModel>(response.ErrorMessage, response.Code);
                }

                return ServiceResult.CreateInstance(
                    response,
                    new MusicLabelUpdateEntryViewModel
                    {
                        Form = new MusicLabelUpdateEntryViewModel.FormModel
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
                return ServiceResult.CreateErrorInstance<MusicLabelUpdateEntryViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult> UpdateEntry(MusicLabelUpdateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.UpdateEntry(model.Id, new UpdateCommandRequest<MusicLabelUpdateModel> { Entry = model });
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
