using boilerplate.api.core.Models;

namespace boilerplate.ui.Models
{
    public class MusicLabelCreateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : MusicLabelCreateModel
        {

        }
    }
}
