using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using boilerplate.ui.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.ui.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactService _service;
        private readonly PagingAppSettings _pagingAppSettings;

        protected override string MainRoute() => UIRoutesHelper.Contact.GetEntries.GetUrl();

        public ContactController(IContactService service, PagingAppSettings pagingAppSettings)
        {
            _service = service ?? ThrowHelper.NullArgument<IContactService>();
            _pagingAppSettings = pagingAppSettings ?? ThrowHelper.NullArgument<PagingAppSettings>();
        }

        [HttpGet(UIRoutesHelper.Contact.GetEntries.PATH)]
        public async Task<IActionResult> GetEntries(int offset = 0, int? limit = null)
        {
            var result = await _service.GetEntries(offset, limit ?? _pagingAppSettings.DefaultDropdownLimit);
            return GetResult(result, true);
        }

        [HttpGet(UIRoutesHelper.Contact.CreateEntry.PATH)]
        public async Task<IActionResult> CreateEntry()
        {
            return GetResult(await _service.GetCreateEntryViewModel(), false);
        }

        [HttpPost(UIRoutesHelper.Contact.CreateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEntry(ViewModel<ContactCreateEntryViewModel> model)
        {
            if (model?.Data?.Form == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                model.Data.Form.PlatformContacts = ParseContacts(model.Data.Form.Platforms);
                model.Data.Form.MusicianContacts = ParseContacts(model.Data.Form.Musicians);
                model.Data.Form.MusicLabelContacts = ParseContacts(model.Data.Form.MusicLabels);

                var result = await _service.CreateEntry(model.Data.Form);
                if (result.Status.Success)
                {
                    return GetRedirectResult(result);
                }
            }

            var newResult = await _service.GetCreateEntryViewModel();
            newResult.ViewModel.Data.Form = model.Data.Form;
            return GetResult(newResult, false);
        }

        private static List<int> ParseContacts(string collection)
        {
            if(collection == null)
                return new List<int>();
            return collection.Split(",").Select(x => int.Parse(x)).ToList();
        }

        [HttpGet(UIRoutesHelper.Contact.UpdateEntry.PATH)]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id)
        {
            var result = await _service.GetUpdateEntryViewModel(id);
            return GetResult(result, false);
        }

		//TODO: передавать только список добавленные/удаленные id контактов- уточнить
        [HttpPost(UIRoutesHelper.Contact.UpdateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id, ViewModel<ContactUpdateEntryViewModel> model)
        {
            if (model?.Data?.Form == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                model.Data.Form.PlatformContacts = ParseContacts(model.Data.Form.Platforms);
                model.Data.Form.MusicianContacts = ParseContacts(model.Data.Form.Musicians);
                model.Data.Form.MusicLabelContacts = ParseContacts(model.Data.Form.MusicLabels);

                var result = await _service.UpdateEntry(model.Data.Form);
                if (result.Status.Success)
                {
                    return GetRedirectResult(result);
                }
            }

            var newResult = await _service.GetUpdateEntryViewModel(id);
            newResult.ViewModel.Data.Form = model.Data.Form;
            return GetResult(newResult, false);
        }

        [HttpPost(UIRoutesHelper.Contact.DeleteEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEntry([FromRoute] int id)
        {
            var result = await _service.DeleteEntry(id);
            return GetRedirectResult(result);
        }
    }
}
