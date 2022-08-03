using boilerplate.api.core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace boilerplate.ui.Models
{
    public class ContactCreateEntryViewModel : ContactEntryViewModelBase<ContactCreateEntryViewModel.FormModel>
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Form.MusicLabels) &&
                string.IsNullOrEmpty(Form.Platforms) &&
                string.IsNullOrEmpty(Form.Musicians))
            {
                yield return new ValidationResult($"At least one contact should be added.");
            }
            if (!string.IsNullOrWhiteSpace(Form.Musicians) && Form.Musicians.Split(",").Any(x => !int.TryParse(x, out _)))
                yield return new ValidationResult("Musicians contacts collection has wrong format.");
            if (!string.IsNullOrWhiteSpace(Form.Platforms) && Form.Platforms.Split(",").Any(x => !int.TryParse(x, out _)))
                yield return new ValidationResult("Platforms contacts collection has wrong format.");
            if (!string.IsNullOrWhiteSpace(Form.MusicLabels) && Form.MusicLabels.Split(",").Any(x => !int.TryParse(x, out _)))
                yield return new ValidationResult("MusicLabels contacts collection has wrong format.");

            foreach (var item in base.Validate(validationContext))
                yield return item;
        }
        public record FormModel : ContactCreateModel
        {
            public string MusicLabels { get; set; }
            public string Platforms { get; set; }
            public string Musicians { get; set; }
        }
    }
    
}
