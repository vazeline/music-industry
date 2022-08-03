using boilerplate.api.core.Models;
using boilerplate.ui.Helpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using static boilerplate.ui.Models.TableViewModel;

namespace boilerplate.ui.Models
{
    public class ContactGetEntriesViewModel
    {
        public List<ContactReportModel> Entries { get; set; } = new List<ContactReportModel>();

        public TableViewModel GetTableViewModel(ViewDataDictionary ViewData)
        {
            return new TableViewModel
            {
                Name = (string)ViewData["Title"],
                CreateUrl = UIRoutesHelper.Contact.CreateEntry.GetUrl,
                UpdateUrl = UIRoutesHelper.Contact.UpdateEntry.GetUrl,
                DeleteUrl = UIRoutesHelper.Contact.DeleteEntry.GetUrl,
                Items = Entries ?? new List<ContactReportModel>(),
                Columns = new List<TableColumn>
                {
                    new TableColumn(nameof(ContactReportModel.Id))
                    {
                        IsIdentifier = true
                    },
                    new TableColumn(nameof(ContactReportModel.FirstName)),
                    new TableColumn(nameof(ContactReportModel.LastName)),
                    new TableColumn(nameof(ContactReportModel.Email)),
                    new TableColumn(nameof(ContactReportModel.PhoneCell)),
                    new TableColumn(nameof(ContactReportModel.PhoneBusiness)),
                    new TableColumn(nameof(ContactReportModel.IsActive),"Active"),
                    new TableColumn("MusicLabelCount", "Labels"),
                    new TableColumn("MusicianCount", "Musicians"),
                    new TableColumn("PlatformCount", "Platforms"),
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
