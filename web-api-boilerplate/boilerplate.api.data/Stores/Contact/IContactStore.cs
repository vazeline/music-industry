using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public interface IContactStore : IBaseStore
    {
        Task<EntryQueryResponse<ContactModel>> GetEntry(EntryQueryRequest<int> request);
        Task<bool> UpdateEntry(UpdateCommandRequest<ContactUpdateModel> request);
    }
}