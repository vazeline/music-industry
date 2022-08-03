using Microsoft.EntityFrameworkCore;
using System;

namespace boilerplate.api.data.Models
{
    public class ProcedureVersion : IBaseEntryModel<int>
    {
        public int Id { get; set; }
        public string ProcedureName { get; set; }
        public int Version { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
    }

    public static class ProcedureVersionExtension
    {
        public const string TABLE_NAME = "ProcedureVersions";
        public static void DescribeProcedureVersion(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcedureVersion>(c =>
            {
                c.Property(p => p.ProcedureName).IsRequired(true);
                c.Property(p => p.DateCreated).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
                c.Property(p => p.DateModified).HasDefaultValueSql("(SYSDATETIMEOFFSET())");
            });
            modelBuilder.Entity<ProcedureVersion>().ToTable(TABLE_NAME);
        }
    }
}
