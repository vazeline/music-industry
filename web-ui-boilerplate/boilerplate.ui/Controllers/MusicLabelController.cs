using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using boilerplate.ui.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boilerplate.ui.Controllers
{
    public class MusicLabelController : BaseController
    {
        private readonly IMusicLabelService _service;
        private readonly PagingAppSettings _pagingAppSettings;

        protected override string MainRoute() => UIRoutesHelper.MusicLabel.GetEntries.GetUrl();

        public MusicLabelController(IMusicLabelService service, PagingAppSettings pagingAppSettings)
        {
            _service = service ?? ThrowHelper.NullArgument<IMusicLabelService>();
            _pagingAppSettings = pagingAppSettings ?? ThrowHelper.NullArgument<PagingAppSettings>();
        }

        [HttpGet(UIRoutesHelper.MusicLabel.GetEntries.PATH)]
        public async Task<IActionResult> GetEntries(int offset = 0, int? limit = null)
        {
            var result = await _service.GetEntries(offset, limit ?? _pagingAppSettings.DefaultPageLimit);
            return GetResult(result, true);
        }

        [HttpGet(UIRoutesHelper.MusicLabel.CreateEntry.PATH)]
        public IActionResult CreateEntry()
        {
            var result = _service.GetCreateEntryViewModel();
            return GetResult(result, false);
        }

        [HttpPost(UIRoutesHelper.MusicLabel.CreateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEntry(ViewModel<MusicLabelCreateEntryViewModel> model)
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

        [HttpGet(UIRoutesHelper.MusicLabel.UpdateEntry.PATH)]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id)
        {
            var result = await _service.GetUpdateEntryViewModel(id);
            return GetResult(result, false);
        }

        [HttpPost(UIRoutesHelper.MusicLabel.UpdateEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEntry([FromRoute] int id, ViewModel<MusicLabelUpdateEntryViewModel> model)
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

        [HttpPost(UIRoutesHelper.MusicLabel.DeleteEntry.PATH)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEntry([FromRoute] int id)
        {
            var result = await _service.DeleteEntry(id);
            return GetRedirectResult(result);
        }
    }
}
