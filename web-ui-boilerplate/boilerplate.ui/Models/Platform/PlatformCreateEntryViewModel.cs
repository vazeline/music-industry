using boilerplate.api.core.Models;

namespace boilerplate.ui.Models
{
    public class PlatformCreateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : PlatformCreateModel
        {

        }
    }
}
