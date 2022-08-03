using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace boilerplate.api.data.Models
{
    public class MusicianContact : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int MusicianId { get; set; }
        public int ContactId { get; set; }

        public Contact Contact { get; set; }
        public Musician Musician { get; set; }
    }

    public static class MusicianContactExtension
    {
        public const string TABLE_NAME = "MusicianContacts";

        public static void DescribeMusicianContact(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicianContact>(c =>
            {
                c.Property(p => p.MusicianId).IsRequired(true);
                c.Property(p => p.ContactId).IsRequired(true);
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
            });

            modelBuilder.Entity<MusicianContact>().ToTable(TABLE_NAME);
        }
    }
}
