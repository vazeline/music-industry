using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace boilerplate.api.data.Models
{
    public class MusicLabel : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<MusicLabelContact> MusicLabelContacts { get; set; }
    }

    public static class MusicLabelExtension
    {
        public const string TABLE_NAME = "MusicLabels";

        public static void DescribeMusicLabel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicLabel>(c =>
            {
                c.Property(p => p.Name).IsRequired(true).HasMaxLength(ValidationHelper.MusicLabel.NameMaxLength).HasDefaultValue("");
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.HasMany(p => p.Contacts).WithMany(m => m.MusicLabels)
                    .UsingEntity<MusicLabelContact>(
                    "MusicLabelContacts",
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(t => t.MusicLabelContacts)
                    .HasForeignKey(pt => pt.ContactId),
                j => j
                    .HasOne(pt => pt.MusicLabel)
                    .WithMany(p => p.MusicLabelContacts)
                    .HasForeignKey(pt => pt.MusicLabelId),
                j =>
                {
                    j.Property(pt => pt.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                    j.HasKey(t => new { t.MusicLabelId, t.ContactId });
                });
            });

            modelBuilder.Entity<MusicLabel>().ToTable(TABLE_NAME);
        }
    }
}
