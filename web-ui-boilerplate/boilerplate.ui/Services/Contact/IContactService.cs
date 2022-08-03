using boilerplate.ui.Models;
using System.Threading.Tasks;

namespace boilerplate.ui.Services
{
    public interface IContactService
    {
        Task<ServiceResult<ContactGetEntriesViewModel>> GetEntries(int offset, int limit);
        Task<ServiceResult<ContactCreateEntryViewModel>> GetCreateEntryViewModel();
        Task<ServiceResult> CreateEntry(ContactCreateEntryViewModel.FormModel model);
        Task<ServiceResult<ContactUpdateEntryViewModel>> GetUpdateEntryViewModel(int id);
        Task<ServiceResult> UpdateEntry(ContactUpdateEntryViewModel.FormModel model);
        Task<ServiceResult> DeleteEntry(int id);
    }
}