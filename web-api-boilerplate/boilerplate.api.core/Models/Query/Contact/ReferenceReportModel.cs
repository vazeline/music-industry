using System;

namespace boilerplate.api.core.Models
{
    public record ReferenceReportModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ContactType Type { get; init; }
    }
}
