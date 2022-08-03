using boilerplate.api.core.Models;

namespace boilerplate.ui.Models
{
    public class MusicianCreateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : MusicianCreateModel
        {

        }
    }
}
