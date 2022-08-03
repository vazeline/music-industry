using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models
{
    public class CreateCommandRequest<T> : BaseRequest
    {
        [Required]
        public T Entry { get; set; }
    }
}
