using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using boilerplate.ui.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boilerplate.ui.Controllers
{
    public class PlatformController : BaseController
    {
        private readonly IPlatformService _service;
        private readonly PagingAppSettings _pagingAppSettings;

        protected override string MainRoute() => UIRoutesHelper.Platform.GetEntries.GetUrl();

        public PlatformController(IPlatformService service, PagingAppSettings pagingAppSettings)
        {
            _service = service ?? ThrowHelper.NullArgument<IPlatformService>();
            _pagingAppSettings = pagingAppSettings ?? ThrowHelper.NullArgument<PagingAppSettings>();
        }

        [HttpGet(UIRoutesHelper.Platform.GetEntries.PATH)]
        public async Task<IActionResult> GetEntries(int offset = 0, int? limit = null)
        {
            var result = await _service.GetEntries(offset, limit ?? _pagingAppSettings.DefaultPageLimit);
            return GetResult(result, true);
        }

        [HttpGet(UIRoutesHelper.Platform.CreateEntry.PATH)]
        public IActionResult CreateEntry()
        {
            var result = _service.GetCreateEntryViewModel();
            return GetResult(result, false);
        }

        [HttpPost(UIRoutesHelper.Platform.CreateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEntry(ViewModel<PlatformCreateEntryViewModel> model)
        {
            if (model?.Data?.Form == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var result = await _service.CreateEntry(model.Data.Form);
                if (result.Status.Success)
                {
                    return GetRedirectResult(result);
                }
            }

            var newResult = _service.GetCreateEntryViewModel();
            newResult.ViewModel.Data.Form = model.Data.Form;
            return GetResult(newResult, false);
        }

        [HttpGet(UIRoutesHelper.Platform.UpdateEntry.PATH)]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id)
        {
            var result = await _service.GetUpdateEntryViewModel(id);
            return GetResult(result, false);
        }

        [HttpPost(UIRoutesHelper.Platform.UpdateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id, ViewModel<PlatformUpdateEntryViewModel> model)
        {
            if (model?.Data?.Form == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
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

        [HttpPost(UIRoutesHelper.Platform.DeleteEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEntry([FromRoute] int id)
        {
            var result = await _service.DeleteEntry(id);
            return GetRedirectResult(result);
        }
    }
}
