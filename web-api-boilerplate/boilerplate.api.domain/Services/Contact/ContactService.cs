using AutoMapper;
using boilerplate.api.core.Models;
using boilerplate.api.data.Procedures;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public class ContactService : BaseService<IContactStore>, IContactService
    {
        public ContactService(IContactStore store, ILogger<ContactService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {

        }

        public async Task<EntryQueryResponse<ContactModel>> GetEntry(EntryQueryRequest<int> request)
        {
            if (request == null)
            {
                return new EntryQueryResponse<ContactModel>
                {
                    Success = false,
                    Code = ResponseCode.BadRequest,
                    ErrorMessage = "Request is null."
                };
            }

            try
            {
                return await Store.GetEntry(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return new EntryQueryResponse<ContactModel>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }

        public virtual async Task<UpdateCommandResponse> UpdateEntry(UpdateCommandRequest<ContactUpdateModel> request)
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

            var newRequest = new UpdateCommandRequest<ContactUpdateModel>
            {
                Entry = new ContactUpdateModel
                {
                    AddressLine1 = request.Entry.AddressLine1 ?? "",
                    AddressLine2 = request.Entry.AddressLine2 ?? "",
                    City = request.Entry.City ?? "",
                    Company = request.Entry.Company ?? "",
                    Email = request.Entry.Email ?? "",
                    Fax = request.Entry.Fax ?? "",
                    FirstName = request.Entry.FirstName ?? "",
                    Id = request.Entry.Id,
                    IsActive = request.Entry.IsActive,
                    LastName = request.Entry.LastName ?? "",
                    MusicianContacts = request.Entry.MusicianContacts,
                    MusicLabelContacts = request.Entry.MusicLabelContacts,
                    PhoneBusiness = request.Entry.PhoneBusiness ?? "",
                    PhoneCell = request.Entry.PhoneCell ?? "",
                    PlatformContacts = request.Entry.PlatformContacts,
                    State = request.Entry.State ?? "",
                    Title = request.Entry.Title ?? "",
                    Zip = request.Entry.Zip ?? "",
                }
            };

            try
            {
                var success = await Store.UpdateEntry(newRequest).ConfigureAwait(false);
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

        public async Task<EntriesQueryResponse<ReferenceReportModel>> GetReferenceEntries(RefEntriesQueryRequest request)
        {
            if (request == null)
            {
                return new EntriesQueryResponse<ReferenceReportModel>
                {
                    Success = false,
                    Code = ResponseCode.BadRequest,
                    ErrorMessage = "Request is null."
                };
            }

            try
            {
                return await Store.GetEntries<ReferenceReportModel, RefEntriesQueryRequest>(request, ContactGetRefEntriesProcedure.Name).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                return new EntriesQueryResponse<ReferenceReportModel>
                {
                    Success = false,
                    Code = ResponseCode.Error,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
