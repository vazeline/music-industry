using boilerplate.api.core.Models;
using System.Threading.Tasks;

namespace boilerplate.api.domain.Services
{
    public interface IBaseService
    {
        Task<EntriesQueryResponse<T>> GetEntries<T>(EntriesQueryRequest request);
        Task<EntryQueryResponse<T>> GetEntry<T, K>(EntryQueryRequest<K> request);
        Task<CreateCommandResponse<K>> CreateEntry<T, K>(CreateCommandRequest<T> request);
        Task<UpdateCommandResponse> UpdateEntry<T, K>(UpdateCommandRequest<T> request);
        Task<DeleteCommandResponse> DeleteEntry<T>(DeleteCommandRequest<T> request);
    }
}