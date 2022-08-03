using boilerplate.api.core.Clients;
using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boilerplate.api.Controllers
{
    [ApiController]
    public class PlatformController : ControllerBase, IPlatformClient
    {
        private readonly IPlatformService _service;
        public PlatformController(IPlatformService service)
        {
            _service = service;
        }

        [HttpGet(RoutesHelper.Platform.Base)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<PlatformReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<PlatformReportModel>> GetEntries([FromQuery] EntriesQueryRequest request)
        {
            return await _service.GetEntries<PlatformReportModel>(request);
        }

        [HttpPost(RoutesHelper.Platform.Base)]
        [ProducesResponseType(200, Type = typeof(CreateCommandResponse<int>))]
        [ProducesDefaultResponseType]
        public async Task<CreateCommandResponse<int>> CreateEntry([FromBody] CreateCommandRequest<PlatformCreateModel> request)
        {
            return await _service.CreateEntry<PlatformCreateModel, int>(request);
        }

        [HttpGet(RoutesHelper.Platform.Id)]
        [ProducesResponseType(200, Type = typeof(EntryQueryResponse<PlatformReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntryQueryResponse<PlatformReportModel>> GetEntry([FromRoute] int id)
        {
            return await _service.GetEntry<PlatformReportModel, int>(new EntryQueryRequest<int> { Id = id });
        }

        [HttpPut(RoutesHelper.Platform.Id)]
        [ProducesResponseType(200, Type = typeof(UpdateCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<UpdateCommandResponse> UpdateEntry([FromRoute] int id, [FromBody] UpdateCommandRequest<PlatformUpdateModel> request)
        {
            return await _service.UpdateEntry<PlatformUpdateModel, int>(request);
        }

        [HttpDelete(RoutesHelper.Platform.Id)]
        [ProducesResponseType(200, Type = typeof(DeleteCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<DeleteCommandResponse> DeleteEntry([FromRoute] int id)
        {
            return await _service.DeleteEntry(new DeleteCommandRequest<int> { Id = id });
        }
    }
}
