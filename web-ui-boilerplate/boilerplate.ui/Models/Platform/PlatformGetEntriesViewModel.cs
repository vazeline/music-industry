using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using static boilerplate.ui.Models.TableViewModel;

namespace boilerplate.ui.Models
{
    public class PlatformGetEntriesViewModel
    {
        public List<PlatformReportModel> Entries { get; set; } = new List<PlatformReportModel>();

        public TableViewModel GetTableViewModel(ViewDataDictionary ViewData)
        {
            return new TableViewModel
            {
                Name = (string)ViewData["Title"],
                CreateUrl = UIRoutesHelper.Platform.CreateEntry.GetUrl,
                UpdateUrl = UIRoutesHelper.Platform.UpdateEntry.GetUrl,
                DeleteUrl = UIRoutesHelper.Platform.DeleteEntry.GetUrl,
                Items = Entries ?? new List<PlatformReportModel>(),
                Columns = new List<TableColumn>
                {
                    new TableColumn(nameof(PlatformReportModel.Id))
                    {
                        IsIdentifier = true
                    },
                    new TableColumn(nameof(PlatformReportModel.Name)),
                    new TableColumn(nameof(PlatformReportModel.DateCreated)),
                    new TableColumn(nameof(PlatformReportModel.DateModified)),
                    new TableColumn
                    {
                        IsEdit = true
                    },
                    new TableColumn
                    {
                        IsRemove = true
                    }
                }
            };
        }
    }
}
