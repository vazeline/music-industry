using boilerplate.api.core.Models;

namespace boilerplate.ui.Models
{
    public class MusicianUpdateEntryViewModel
    {
        public FormModel Form { get; set; }
        public record FormModel : MusicianUpdateModel
        {

        }
    }
}
