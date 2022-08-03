using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace boilerplate.api.data.Models
{
    public class Contact : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhoneCell { get; set; }
        public string PhoneBusiness { get; set; }
        public string Fax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Musician> Musicians { get; set; }
        public ICollection<Platform> Platforms { get; set; }
        public ICollection<MusicLabel> MusicLabels { get; set; }
        public ICollection<MusicLabelContact> MusicLabelContacts { get; set; }
        public ICollection<PlatformContact> PlatformContacts { get; set; }
        public ICollection<MusicianContact> MusicianContacts { get; set; }

    }

    public static class ContactExtension
    {
        public const string TABLE_NAME = "Contact";

        public static void DescribeContact(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(c =>
            {
                c.Property(p => p.FirstName).IsRequired(true).HasMaxLength(ValidationHelper.Contact.NameMaxLength).HasDefaultValue("");
                c.Property(p => p.LastName).IsRequired(true).HasMaxLength(ValidationHelper.Contact.NameMaxLength).HasDefaultValue("");
                c.Property(p => p.Title).IsRequired(true).HasMaxLength(ValidationHelper.Contact.TitleMaxLength).HasDefaultValue("");
                c.Property(p => p.Company).IsRequired(true).HasMaxLength(ValidationHelper.Contact.CompanyMaxLength).HasDefaultValue("");
                c.Property(p => p.Email).IsRequired(true).HasMaxLength(ValidationHelper.Contact.EmailMaxLength).HasDefaultValue("");
                c.Property(p => p.PhoneCell).IsRequired(true).HasMaxLength(ValidationHelper.Contact.PhoneCellMaxLength).HasDefaultValue("");
                c.Property(p => p.PhoneBusiness).IsRequired(true).HasMaxLength(ValidationHelper.Contact.PhoneBusinessMaxLength).HasDefaultValue("");
                c.Property(p => p.Fax).IsRequired(true).HasMaxLength(ValidationHelper.Contact.FaxMaxLength).HasDefaultValue("");
                c.Property(p => p.AddressLine1).IsRequired(true).HasMaxLength(ValidationHelper.Contact.AddressLine1MaxLength).HasDefaultValue("");
                c.Property(p => p.AddressLine2).IsRequired(true).HasMaxLength(ValidationHelper.Contact.AddressLine2MaxLength).HasDefaultValue("");
                c.Property(p => p.City).IsRequired(true).HasMaxLength(ValidationHelper.Contact.CityMaxLength).HasDefaultValue("");
                c.Property(p => p.State).IsRequired(true).HasMaxLength(ValidationHelper.Contact.StateMaxLength).HasDefaultValue("");
                c.Property(p => p.Zip).IsRequired(true).HasMaxLength(ValidationHelper.Contact.ZipMaxLength).HasDefaultValue("");
                c.Property(p => p.IsActive);
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.HasMany(p => p.Musicians).WithMany(m => m.Contacts)
                    .UsingEntity<MusicianContact>(mc=>mc.ToTable("MusicianContacts"));
                c.HasMany(p => p.Platforms).WithMany(m => m.Contacts)
                    .UsingEntity<PlatformContact>(mc => mc.ToTable("PlatformContacts"));
                c.HasMany(p => p.MusicLabels).WithMany(m => m.Contacts)
                    .UsingEntity<MusicLabelContact>(mc => mc.ToTable("MusicLabelContacts"));
            });

            modelBuilder.Entity<Contact>().ToTable(TABLE_NAME);
        }
    }
}
