using boilerplate.api.core.Clients;
using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.ui.Services
{
    public class MusicianService : IMusicianService
    {
        private readonly IMusicianClient _client;
        private readonly ILogger<MusicianService> _logger;
        public MusicianService(IMusicianClient client, ILogger<MusicianService> logger)
        {
            _client = client ?? ThrowHelper.NullArgument<IMusicianClient>();
            _logger = logger ?? ThrowHelper.NullArgument<ILogger<MusicianService>>();
        }

        public async Task<ServiceResult<MusicianGetEntriesViewModel>> GetEntries(int offset, int limit)
        {
            try
            {
                var response = await _client.GetEntries(new EntriesQueryRequest { Offset = offset, Limit = limit });
                return ServiceResult.CreateInstance(
                    response,
                    new MusicianGetEntriesViewModel
                    {
                        Entries = response.Data
                    },
                    new Paging(response.TotalCount, offset, limit, (o, l) => UIRoutesHelper.Musician.GetEntries.GetUrl(o, l))
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<MusicianGetEntriesViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public ServiceResult<MusicianCreateEntryViewModel> GetCreateEntryViewModel()
        {
            return ServiceResult.CreateInstance(new BaseResponse { Success = true, Code = ResponseCode.Success }, new MusicianCreateEntryViewModel());
        }

        public async Task<ServiceResult> CreateEntry(MusicianCreateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.CreateEntry(new CreateCommandRequest<MusicianCreateModel> { Entry = model });
                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }
            
        }

        public async Task<ServiceResult<MusicianUpdateEntryViewModel>> GetUpdateEntryViewModel(int id)
        {
            try
            {
                var response = await _client.GetEntry(id);
                if (!response.Success)
                {
                    return ServiceResult.CreateErrorInstance<MusicianUpdateEntryViewModel>(response.ErrorMessage, response.Code);
                }

                return ServiceResult.CreateInstance(
                    response,
                    new MusicianUpdateEntryViewModel
                    {
                        Form = new MusicianUpdateEntryViewModel.FormModel
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
                return ServiceResult.CreateErrorInstance<MusicianUpdateEntryViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult> UpdateEntry(MusicianUpdateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.UpdateEntry(model.Id, new UpdateCommandRequest<MusicianUpdateModel> { Entry = model });
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
