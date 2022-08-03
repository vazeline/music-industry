using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public interface IContactService : IBaseService
    {
        Task<EntriesQueryResponse<ReferenceReportModel>> GetReferenceEntries(RefEntriesQueryRequest request);
        Task<EntryQueryResponse<ContactModel>> GetEntry(EntryQueryRequest<int> request);
        Task<UpdateCommandResponse> UpdateEntry(UpdateCommandRequest<ContactUpdateModel> request);
    }
}