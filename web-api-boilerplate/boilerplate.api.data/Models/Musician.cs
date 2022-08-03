using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace boilerplate.api.data.Models
{
    public class Musician : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }

        public ICollection<Contact> Contacts { get; set; }
        public ICollection<MusicianContact> MusicianContacts { get; set; }
    }

    public static class MusicianExtension
    {
        public const string TABLE_NAME = "Musicians";

        public static void DescribeMusician(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musician>(c =>
            {
                c.Property(p => p.Name).IsRequired(true).HasMaxLength(ValidationHelper.Musician.NameMaxLength).HasDefaultValue("");
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.HasMany(p => p.Contacts).WithMany(m => m.Musicians)
                    .UsingEntity<MusicianContact>("MusicianContacts",
                j => j
                    .HasOne(pt => pt.Contact)
                    .WithMany(t => t.MusicianContacts)
                    .HasForeignKey(pt => pt.ContactId),
                j => j
                    .HasOne(pt => pt.Musician)
                    .WithMany(p => p.MusicianContacts)
                    .HasForeignKey(pt => pt.MusicianId),
                j =>
                {
                    j.Property(pt => pt.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                    j.HasKey(t => new { t.MusicianId, t.ContactId });
                });
            });

            modelBuilder.Entity<Musician>().ToTable(TABLE_NAME);
        }
    }
}
