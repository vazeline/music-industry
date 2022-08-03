using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using static boilerplate.ui.Models.TableViewModel;

namespace boilerplate.ui.Models
{
    public class MusicianGetEntriesViewModel
    {
        public List<MusicianReportModel> Entries { get; set; } = new List<MusicianReportModel>();

        public TableViewModel GetTableViewModel(ViewDataDictionary ViewData)
        {
            return new TableViewModel
            {
                Name = (string)ViewData["Title"],
                CreateUrl = UIRoutesHelper.Musician.CreateEntry.GetUrl,
                UpdateUrl = UIRoutesHelper.Musician.UpdateEntry.GetUrl,
                DeleteUrl = UIRoutesHelper.Musician.DeleteEntry.GetUrl,
                Items = Entries ?? new List<MusicianReportModel>(),
                Columns = new List<TableColumn>
                {
                    new TableColumn(nameof(MusicianReportModel.Id))
                    {
                        IsIdentifier = true
                    },
                    new TableColumn(nameof(MusicianReportModel.Name)),
                    new TableColumn(nameof(MusicianReportModel.DateCreated)),
                    new TableColumn(nameof(MusicianReportModel.DateModified)),
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
