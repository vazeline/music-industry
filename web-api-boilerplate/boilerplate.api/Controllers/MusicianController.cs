using boilerplate.api.core.Clients;
using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boilerplate.api.Controllers
{
    [ApiController]
    public class MusicianController : ControllerBase, IMusicianClient
    {
        private readonly IMusicianService _service;
        public MusicianController(IMusicianService service)
        {
            _service = service;
        }

        [HttpGet(RoutesHelper.Musician.Base)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<MusicianReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<MusicianReportModel>> GetEntries([FromQuery] EntriesQueryRequest request)
        {
            return await _service.GetEntries<MusicianReportModel>(request);
        }

        [HttpPost(RoutesHelper.Musician.Base)]
        [ProducesResponseType(200, Type = typeof(CreateCommandResponse<int>))]
        [ProducesDefaultResponseType]
        public async Task<CreateCommandResponse<int>> CreateEntry([FromBody] CreateCommandRequest<MusicianCreateModel> request)
        {
            return await _service.CreateEntry<MusicianCreateModel, int>(request);
        }

        [HttpGet(RoutesHelper.Musician.Id)]
        [ProducesResponseType(200, Type = typeof(EntryQueryResponse<MusicianReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntryQueryResponse<MusicianReportModel>> GetEntry([FromRoute] int id)
        {
            return await _service.GetEntry<MusicianReportModel, int>(new EntryQueryRequest<int> { Id = id });
        }

        [HttpPut(RoutesHelper.Musician.Id)]
        [ProducesResponseType(200, Type = typeof(UpdateCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<UpdateCommandResponse> UpdateEntry([FromRoute] int id, [FromBody] UpdateCommandRequest<MusicianUpdateModel> request)
        {
            return await _service.UpdateEntry<MusicianUpdateModel, int>(request);
        }

        [HttpDelete(RoutesHelper.Musician.Id)]
        [ProducesResponseType(200, Type = typeof(DeleteCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<DeleteCommandResponse> DeleteEntry([FromRoute] int id)
        {
            return await _service.DeleteEntry(new DeleteCommandRequest<int> { Id = id });
        }
    }
}
