using boilerplate.api.core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace boilerplate.ui.Models
{
    public class ContactEntryViewModelBase<FM> : IValidatableObject
        where FM : ContactModelBase
    {
        public FM Form { get; set; }
        public IEnumerable<ReferenceReportModel> ContactItems { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Form.FirstName))
                yield return new ValidationResult("Contact’s First name is required field.");
            if (string.IsNullOrEmpty(Form.LastName))
                yield return new ValidationResult("Contact’s Last name is required field.");

            if (string.IsNullOrEmpty(Form.Email) &&
                string.IsNullOrEmpty(Form.PhoneCell) &&
                string.IsNullOrEmpty(Form.Fax) &&
                string.IsNullOrEmpty(Form.AddressLine1) &&
                string.IsNullOrEmpty(Form.AddressLine2)) 
                yield return new ValidationResult("At least one of the following must be specified when creating a contact: Email, Mobile, Office, Fax.");
        }
    }
}
