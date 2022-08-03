using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using System;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public interface IBaseStore
    {
        Task<EntriesQueryResponse<T>> GetEntries<T>(EntriesQueryRequest request);
        Task<EntryQueryResponse<T>> GetEntry<T, K>(EntryQueryRequest<K> request);
        Task<EntriesQueryResponse<T>> GetEntries<T, R>(R request, string procedureName) where R: BaseRequest;
        Task<K> CreateEntry<T, K>(CreateCommandRequest<T> request);
        Task<bool> UpdateEntry<T, K>(UpdateCommandRequest<T> request);
        Task<bool> DeleteEntry<T>(DeleteCommandRequest<T> request);
    }
}