using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using Refit;
using System.Threading.Tasks;

namespace boilerplate.api.core.Clients
{
    public interface IMusicianClient
    {
        [Get(RoutesHelper.Musician.Base)]
        Task<EntriesQueryResponse<MusicianReportModel>> GetEntries([Query] EntriesQueryRequest request);

        [Post(RoutesHelper.Musician.Base)]
        Task<CreateCommandResponse<int>> CreateEntry([Body] CreateCommandRequest<MusicianCreateModel> request);

        [Get(RoutesHelper.Musician.Id)]
        Task<EntryQueryResponse<MusicianReportModel>> GetEntry(int id);

        [Put(RoutesHelper.Musician.Id)]
        Task<UpdateCommandResponse> UpdateEntry(int id, [Body] UpdateCommandRequest<MusicianUpdateModel> request);

        [Delete(RoutesHelper.Musician.Id)]
        Task<DeleteCommandResponse> DeleteEntry(int id);
    }
}
