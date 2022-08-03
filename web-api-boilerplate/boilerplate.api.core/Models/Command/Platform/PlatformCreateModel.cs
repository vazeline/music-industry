using boilerplate.api.core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models
{
    public record PlatformCreateModel
    {
        [Required]
        [StringLength(ValidationHelper.Platform.NameMaxLength)]
        public string Name { get; init; }
    }
}
