using boilerplate.api.core.Helpers;
using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models
{
    public record MusicianCreateModel
    {
        [Required]
        [StringLength(ValidationHelper.Musician.NameMaxLength)]
        public string Name { get; init; }
    }
}
