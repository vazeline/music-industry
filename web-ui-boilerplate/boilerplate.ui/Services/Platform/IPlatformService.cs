using boilerplate.ui.Models;
using System.Threading.Tasks;

namespace boilerplate.ui.Services
{
    public interface IPlatformService
    {
        Task<ServiceResult<PlatformGetEntriesViewModel>> GetEntries(int offset, int limit);
        ServiceResult<PlatformCreateEntryViewModel> GetCreateEntryViewModel();
        Task<ServiceResult> CreateEntry(PlatformCreateEntryViewModel.FormModel model);
        Task<ServiceResult<PlatformUpdateEntryViewModel>> GetUpdateEntryViewModel(int id);
        Task<ServiceResult> UpdateEntry(PlatformUpdateEntryViewModel.FormModel model);
        Task<ServiceResult> DeleteEntry(int id);
    }
}