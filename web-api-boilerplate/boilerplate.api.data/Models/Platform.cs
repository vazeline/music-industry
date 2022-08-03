using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace boilerplate.api.data.Models
{
    public class Platform : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<PlatformContact> PlatformContacts { get; set; }
    }

    public static class PlatformExtension
    {
        public const string TABLE_NAME = "Platforms";

        public static void DescribePlatform(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>(c =>
            {
                c.Property(p => p.Name).IsRequired(true).HasMaxLength(ValidationHelper.Platform.NameMaxLength).HasDefaultValue("");
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.HasMany(p => p.Contacts).WithMany(m => m.Platforms)
                    .UsingEntity<PlatformContact>(
                    "PlatformContacts",
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(t => t.PlatformContacts)
                    .HasForeignKey(pt => pt.ContactId),
                j => j
                    .HasOne(pt => pt.Platform)
                    .WithMany(p => p.PlatformContacts)
                    .HasForeignKey(pt => pt.PlatformId),
                j =>
                {
                    j.Property(pt => pt.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                    j.HasKey(t => new { t.PlatformId, t.ContactId });
                });
            });

            modelBuilder.Entity<Platform>().ToTable(TABLE_NAME);
        }
    }
}
