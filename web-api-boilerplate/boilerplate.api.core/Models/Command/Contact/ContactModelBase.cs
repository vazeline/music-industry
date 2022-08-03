using boilerplate.api.core.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace boilerplate.api.core.Models
{
    public record ContactModelBase
    {
        [Required]
        [StringLength(ValidationHelper.Contact.NameMaxLength)]
        public string FirstName { get; init; }
        [Required]
        [StringLength(ValidationHelper.Contact.NameMaxLength)]
        public string LastName { get; init; }

        [StringLength(ValidationHelper.Contact.TitleMaxLength)]
        public string Title { get; init; }

        [StringLength(ValidationHelper.Contact.CompanyMaxLength)]
        public string Company { get; init; }

        [StringLength(ValidationHelper.Contact.EmailMaxLength)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; init; }

        [StringLength(ValidationHelper.Contact.PhoneCellMaxLength)]
        public string PhoneCell { get; init; }

        [StringLength(ValidationHelper.Contact.PhoneBusinessMaxLength)]
        public string PhoneBusiness { get; init; }

        [StringLength(ValidationHelper.Contact.FaxMaxLength)]
        public string Fax { get; init; }

        [StringLength(ValidationHelper.Contact.AddressLine1MaxLength)]
        public string AddressLine1 { get; init; }

        [StringLength(ValidationHelper.Contact.AddressLine2MaxLength)]
        public string AddressLine2 { get; init; }

        [StringLength(ValidationHelper.Contact.CityMaxLength)]
        public string City { get; init; }

        [StringLength(ValidationHelper.Contact.StateMaxLength)]
        public string State { get; init; }

        [StringLength(ValidationHelper.Contact.ZipMaxLength)]
        public string Zip { get; init; }

        public bool IsActive { get; init; }

        public IEnumerable<int> MusicLabelContacts { get; set; }
        public IEnumerable<int> PlatformContacts { get; set; }
        public IEnumerable<int> MusicianContacts { get; set; }
    }
}