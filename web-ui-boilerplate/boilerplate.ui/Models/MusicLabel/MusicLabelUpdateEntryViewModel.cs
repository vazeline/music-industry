using boilerplate.api.core.Models;

namespace boilerplate.ui.Models
{
    public class MusicLabelUpdateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : MusicLabelUpdateModel
        {

        }
    }
}
