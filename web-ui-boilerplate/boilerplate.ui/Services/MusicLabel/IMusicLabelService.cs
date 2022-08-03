using boilerplate.ui.Models;
using System.Threading.Tasks;

namespace boilerplate.ui.Services
{
    public interface IMusicLabelService
    {
        Task<ServiceResult<MusicLabelGetEntriesViewModel>> GetEntries(int offset, int limit);
        ServiceResult<MusicLabelCreateEntryViewModel> GetCreateEntryViewModel();
        Task<ServiceResult> CreateEntry(MusicLabelCreateEntryViewModel.FormModel model);
        Task<ServiceResult<MusicLabelUpdateEntryViewModel>> GetUpdateEntryViewModel(int id);
        Task<ServiceResult> UpdateEntry(MusicLabelUpdateEntryViewModel.FormModel model);
        Task<ServiceResult> DeleteEntry(int id);
    }
}