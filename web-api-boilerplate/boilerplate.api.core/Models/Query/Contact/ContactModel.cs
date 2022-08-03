using System;
using System.Collections.Generic;

namespace boilerplate.api.core.Models
{
    public record ContactModel : ContactReportModel
    {
        public IEnumerable<ReferenceReportModel> MusicLabelContacts { get; set; }
        public IEnumerable<ReferenceReportModel> MusicianContacts { get; set; }
        public IEnumerable<ReferenceReportModel> PlatformContacts { get; set; }
    }
}
