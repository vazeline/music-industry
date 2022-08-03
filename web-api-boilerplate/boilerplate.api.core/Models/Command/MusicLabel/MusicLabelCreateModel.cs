using boilerplate.api.core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models
{
    public record MusicLabelCreateModel
    {
        [Required]
        [StringLength(ValidationHelper.MusicLabel.NameMaxLength)]
        public string Name { get; init; }
    }
}
