using boilerplate.api.core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace boilerplate.api.data.Models
{
    public class PlatformContact : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int PlatformId { get; set; }
        public int ContactId { get; set; }

        public Contact Contact { get; set; }
        public Platform Platform { get; set; } 
    }

    public static class PlatformContactExtension
    {
        public const string TABLE_NAME = "PlatformContacts";

        public static void DescribePlatformContact(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlatformContact>(c =>
            {
                c.Property(p => p.PlatformId).IsRequired(true);
                c.Property(p => p.ContactId).IsRequired(true);
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
            });

            modelBuilder.Entity<PlatformContact>().ToTable(TABLE_NAME);
        }
    }
}
