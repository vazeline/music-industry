using boilerplate.api.core.Models;

namespace boilerplate.ui.Models
{
    public class AppSettings
    {
        public ApiConfig ApiConfig { get; set; }
        public PagingAppSettings Paging { get; set; }
    }

    public class PagingAppSettings
    {
        public int DefaultPageLimit { get; set; } = 10;
        public int DefaultDropdownLimit { get; set; } = 20;
    }
}
