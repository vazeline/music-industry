using boilerplate.api.core.Clients;
using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.ui.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactClient _client;
        private readonly ILogger<ContactService> _logger;
        private readonly PagingAppSettings pagingAppSettings;

        public ContactService(IContactClient client, ILogger<ContactService> logger, PagingAppSettings pagingAppSettings)
        {
            _client = client ?? ThrowHelper.NullArgument<IContactClient>();
            _logger = logger ?? ThrowHelper.NullArgument<ILogger<ContactService>>();
            this.pagingAppSettings = pagingAppSettings;
        }

        public async Task<ServiceResult<ContactGetEntriesViewModel>> GetEntries(int offset, int limit)
        {
            try
            {
                var response = await _client.GetEntries(new EntriesQueryRequest { Offset = offset, Limit = limit });
                return ServiceResult.CreateInstance(
                    response,
                    new ContactGetEntriesViewModel
                    {
                        Entries = response.Data
                    },
                    new Paging(response.TotalCount, offset, limit, (o, l) => UIRoutesHelper.Contact.GetEntries.GetUrl(o, l))
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<ContactGetEntriesViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        /// <summary>  
        /// Get country method.  
        /// </summary>  
        /// <returns>Return country for drop down list.</returns>  
        private async Task<IEnumerable<ReferenceReportModel>> GetContactItems()
        {
            // Initialization.  
            List<ReferenceReportModel> lstobj = null;

            try
            {
                // Loading.  
                lstobj = (await _client.GetReferenceEntries(new RefEntriesQueryRequest() { Limit =  pagingAppSettings.DefaultDropdownLimit })).Data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error while loading contact items: " + ex.Message);
                // Info  
                throw;
            }

            // info.  
            return lstobj;
        }

        public async Task<ServiceResult<ContactCreateEntryViewModel>> GetCreateEntryViewModel()
        {
            return ServiceResult.CreateInstance(new BaseResponse { Success = true, Code = ResponseCode.Success }, 
                new ContactCreateEntryViewModel() {  ContactItems = await GetContactItems() }
            );
        }


        public async Task<ServiceResult> CreateEntry(ContactCreateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.CreateEntry(new CreateCommandRequest<ContactCreateModel> { Entry = model });
                return ServiceResult.CreateInstance(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance(ex.Message, ResponseCode.Error);
            }

        }

        public async Task<ServiceResult<ContactUpdateEntryViewModel>> GetUpdateEntryViewModel(int id)
        {
            try
            {
                var response = await _client.GetEntry(id);
                if (!response.Success)
                {
                    return ServiceResult.CreateErrorInstance<ContactUpdateEntryViewModel>(response.ErrorMessage, response.Code);
                }

                var list = await GetContactItems();

                return ServiceResult.CreateInstance(response,
                    new ContactUpdateEntryViewModel()
                    {
                        Form = new ContactUpdateEntryViewModel.FormModel
                        {
                            Id = response.Data.Id,
                            FirstName = response.Data.FirstName,
                            LastName = response.Data.LastName,
                            AddressLine1 = response.Data.AddressLine1,
                            AddressLine2 = response.Data.AddressLine2,
                            City = response.Data.City,
                            Company = response.Data.Company,
                            Email = response.Data.Email,
                            Fax = response.Data.Fax,
                            IsActive = response.Data.IsActive,
                            PhoneBusiness = response.Data.PhoneBusiness,
                            PhoneCell = response.Data.PhoneCell,
                            State = response.Data.State,
                            Zip = response.Data.Zip,
                            Title = response.Data.Title,
                            Musicians = response.Data.MusicianContacts.Select(x => x.Id).Aggregate("", (x, y) => x + "," + y).TrimStart(','),
                            MusicLabels = response.Data.MusicLabelContacts.Select(x => x.Id).Aggregate("", (x, y) => x + "," + y).TrimStart(','),
                            Platforms = response.Data.PlatformContacts.Select(x => x.Id).Aggregate("", (x, y) => x + "," + y).TrimStart(','),
                        },
                        MusicianContacts = response.Data.MusicianContacts,
                        MusicLabelContacts = response.Data.MusicLabelContacts,
                        PlatformContacts = response.Data.PlatformContacts,
                        ContactItems = list,
                    }
                );

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.CreateErrorInstance<ContactUpdateEntryViewModel>(ex.Message, ResponseCode.Error);
            }
        }

        public async Task<ServiceResult> UpdateEntry(ContactUpdateEntryViewModel.FormModel model)
        {
            try
            {
                var response = await _client.UpdateEntry(model.Id, new UpdateCommandRequest<ContactUpdateModel> { Entry = model });
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
