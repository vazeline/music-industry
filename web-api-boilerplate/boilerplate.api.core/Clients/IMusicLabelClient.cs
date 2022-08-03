using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using Refit;
using System.Threading.Tasks;

namespace boilerplate.api.core.Clients
{
    public interface IMusicLabelClient
    {
        [Get(RoutesHelper.MusicLabel.Base)]
        Task<EntriesQueryResponse<MusicLabelReportModel>> GetEntries([Query] EntriesQueryRequest request);

        [Post(RoutesHelper.MusicLabel.Base)]
        Task<CreateCommandResponse<int>> CreateEntry([Body] CreateCommandRequest<MusicLabelCreateModel> request);

        [Get(RoutesHelper.MusicLabel.Id)]
        Task<EntryQueryResponse<MusicLabelReportModel>> GetEntry(int id);

        [Put(RoutesHelper.MusicLabel.Id)]
        Task<UpdateCommandResponse> UpdateEntry(int id, [Body] UpdateCommandRequest<MusicLabelUpdateModel> request);

        [Delete(RoutesHelper.MusicLabel.Id)]
        Task<DeleteCommandResponse> DeleteEntry(int id);
    }
}
