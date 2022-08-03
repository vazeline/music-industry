using boilerplate.api.core.Clients;
using boilerplate.api.core.Helpers;
using boilerplate.api.core.Models;
using boilerplate.api.domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boilerplate.api.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase, IContactClient
    {
        private readonly IContactService _service;
        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet(RoutesHelper.Contact.Base)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<ContactReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<ContactReportModel>> GetEntries([FromQuery] EntriesQueryRequest request)
        {
            return await _service.GetEntries<ContactReportModel>(request);
        }

        [HttpPost(RoutesHelper.Contact.Base)]
        [ProducesResponseType(200, Type = typeof(CreateCommandResponse<int>))]
        [ProducesDefaultResponseType]
        public async Task<CreateCommandResponse<int>> CreateEntry([FromBody] CreateCommandRequest<ContactCreateModel> request)
        {
            return await _service.CreateEntry<ContactCreateModel, int>(request);
        }

        [HttpGet(RoutesHelper.Contact.RefEntities)]
        [ProducesResponseType(200, Type = typeof(EntriesQueryResponse<ReferenceReportModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntriesQueryResponse<ReferenceReportModel>> GetReferenceEntries([FromQuery] RefEntriesQueryRequest request)
        {
            return await _service.GetReferenceEntries(request);
        }

        [HttpGet(RoutesHelper.Contact.Id)]
        [ProducesResponseType(200, Type = typeof(EntryQueryResponse<ContactModel>))]
        [ProducesDefaultResponseType]
        public async Task<EntryQueryResponse<ContactModel>> GetEntry([FromRoute] int id)
        {
            return await _service.GetEntry(new EntryQueryRequest<int> { Id = id });
        }

        [HttpPut(RoutesHelper.Contact.Id)]
        [ProducesResponseType(200, Type = typeof(UpdateCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<UpdateCommandResponse> UpdateEntry([FromRoute] int id, [FromBody] UpdateCommandRequest<ContactUpdateModel> request)
        {
            return await _service.UpdateEntry(request);
        }

        [HttpDelete(RoutesHelper.Contact.Id)]
        [ProducesResponseType(200, Type = typeof(DeleteCommandResponse))]
        [ProducesDefaultResponseType]
        public async Task<DeleteCommandResponse> DeleteEntry([FromRoute] int id)
        {
            return await _service.DeleteEntry(new DeleteCommandRequest<int> { Id = id });
        }
    }
}
