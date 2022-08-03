using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using static boilerplate.ui.Models.TableViewModel;

namespace boilerplate.ui.Models
{
    public class MusicLabelGetEntriesViewModel
    {
        public List<MusicLabelReportModel> Entries { get; set; } = new List<MusicLabelReportModel>();

        public TableViewModel GetTableViewModel(ViewDataDictionary ViewData)
        {
            return new TableViewModel
            {
                Name = (string)ViewData["Title"],
                CreateUrl = UIRoutesHelper.MusicLabel.CreateEntry.GetUrl,
                UpdateUrl = UIRoutesHelper.MusicLabel.UpdateEntry.GetUrl,
                DeleteUrl = UIRoutesHelper.MusicLabel.DeleteEntry.GetUrl,
                Items = Entries ?? new List<MusicLabelReportModel>(),
                Columns = new List<TableColumn>
                {
                    new TableColumn(nameof(MusicLabelReportModel.Id))
                    {
                        IsIdentifier = true
                    },
                    new TableColumn(nameof(MusicLabelReportModel.Name)),
                    new TableColumn(nameof(MusicLabelReportModel.DateCreated)),
                    new TableColumn(nameof(MusicLabelReportModel.DateModified)),
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
