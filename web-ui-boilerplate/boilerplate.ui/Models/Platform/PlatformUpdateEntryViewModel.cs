using boilerplate.api.core.Models;

namespace boilerplate.ui.Models
{
    public class PlatformUpdateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : PlatformUpdateModel
        {

        }
    }
}
