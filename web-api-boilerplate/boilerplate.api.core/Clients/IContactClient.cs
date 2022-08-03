using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using Refit;
using System.Threading.Tasks;

namespace boilerplate.api.core.Clients
{
    public interface IContactClient
    {
        [Get(RoutesHelper.Contact.Base)]
        Task<EntriesQueryResponse<ContactReportModel>> GetEntries([Query] EntriesQueryRequest request);

        [Post(RoutesHelper.Contact.Base)]
        Task<CreateCommandResponse<int>> CreateEntry([Body] CreateCommandRequest<ContactCreateModel> request);

        [Get(RoutesHelper.Contact.RefEntities)]
        Task<EntriesQueryResponse<ReferenceReportModel>> GetReferenceEntries([Query] RefEntriesQueryRequest request);

        [Get(RoutesHelper.Contact.Id)]
        Task<EntryQueryResponse<ContactModel>> GetEntry(int id);

        [Put(RoutesHelper.Contact.Id)]
        Task<UpdateCommandResponse> UpdateEntry(int id, [Body] UpdateCommandRequest<ContactUpdateModel> request);

        [Delete(RoutesHelper.Contact.Id)]
        Task<DeleteCommandResponse> DeleteEntry(int id);
    }
}
