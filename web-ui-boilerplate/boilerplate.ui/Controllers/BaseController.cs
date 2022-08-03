using boilerplate.ui.Helpers;
using boilerplate.ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace boilerplate.ui.Controllers
{
    public abstract class BaseController : Controller
    {
        protected abstract string MainRoute();
        protected IActionResult GetResult<T>(ServiceResult<T> result, bool isMainAction)
        {
            if (!result.Status.Success)
            {
                TempData["Error"] = result.Status.ErrorMessage ?? result.Status.Code.ToString();
                if (isMainAction)
                {
                    return LocalRedirect(UIRoutesHelper.Home.Main.GetUrl());
                }
                else
                {
                    return LocalRedirect(MainRoute());
                }
            }
            else
            {
                return View(result.ViewModel);
            }
        }

        protected IActionResult GetRedirectResult(ServiceResult result)
        {
            if (!result.Status.Success)
            {
                TempData["Error"] = result.Status.ErrorMessage ?? result.Status.Code.ToString();
            }
            return LocalRedirect(MainRoute());
        }
    }
}
