using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace boilerplate.api.data.Models
{
    public class MusicLabelContact : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int MusicLabelId { get; set; }
        public int ContactId { get; set; }

        public Contact Contact { get; set; }
        public MusicLabel MusicLabel { get; set; }
    }

    public static class MusicLabelContactExtension
    {
        public const string TABLE_NAME = "MusicLabelContacts";

        public static void DescribeMusicLabelContact(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MusicLabelContact>(c =>
            {
                c.Property(p => p.MusicLabelId).IsRequired(true);
                c.Property(p => p.ContactId).IsRequired(true);
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
            });

            modelBuilder.Entity<MusicLabelContact>().ToTable(TABLE_NAME);
        }
    }
}
