using AutoMapper;
using boilerplate.api.common.Models;
using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public class ContactStore : BaseStore, IContactStore
    {
        public ContactStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {

        }

        protected override Type DataModelType => typeof(Contact);
        protected override string EntriesProcedureName => ContactGetEntriesProcedure.Name;
        protected override string EntryProcedureName => ContactGetEntriesProcedure.Name;
        
        public async Task<EntryQueryResponse<ContactModel>> GetEntry(EntryQueryRequest<int> request)
        {
            using (var connection = new SqlConnection(ConnectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(ProcedureParams.Id, request.Id, DbType.Int32);
                parameter.Add(ProcedureParams.AllowDirtyRead, false, DbType.Boolean);

                using (var multi = await connection.QueryMultipleAsync(EntryProcedureName, param: parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    var cmodel = multi.Read<ContactModel>().FirstOrDefault();
                    cmodel.MusicianContacts = connection.Query<Musician>("select m.* from MusicianContacts " +
                        "join Musicians m on m.Id = MusicianId where ContactId=@Id", new { Id = request.Id })
                        .Select(m => new ReferenceReportModel { Id = m.Id, Name = m.Name, Type = ContactType.Platform }).ToArray();

                    cmodel.PlatformContacts = connection.Query<Platform>("select p.* from PlatformContacts " +
                        "join Platforms p on p.Id = PlatformId where ContactId=@Id", new { Id = request.Id })
                        .Select(p => new ReferenceReportModel { Id = p.Id, Name = p.Name, Type = ContactType.Platform }).ToArray();

                    cmodel.MusicLabelContacts = connection.Query<MusicLabel>("select ml.* from MusicLabelContacts " +
                        "join MusicLabels ml on ml.Id = MusicLabelId where ContactId=@Id", new { Id = request.Id })
                        .Select(ml => new ReferenceReportModel { Id=ml.Id, Name=ml.Name,Type=ContactType.MusicLabel }).ToArray();

                    return new EntryQueryResponse<ContactModel>
                    {
                        Data = cmodel,
                        Success = true,
                        Code = ResponseCode.Success
                    };
                }
            }
        }

        public async Task<bool> UpdateEntry(UpdateCommandRequest<ContactUpdateModel> request)
        {
            var mappedEntry = MapUpdateModel<ContactUpdateModel, int>(request) as Contact;
            
            var existingEntry = await Context.Contacts.FindAsync(mappedEntry.Id).ConfigureAwait(false) as Contact;
            if (existingEntry == null)
            {
                return false;
            }
            mappedEntry.DateCreated = existingEntry.DateCreated;
            Context.Entry(existingEntry).CurrentValues.SetValues(mappedEntry);
            await Context.Entry(existingEntry).Collection(x => x.PlatformContacts).LoadAsync();
            await Context.Entry(existingEntry).Collection(x => x.MusicianContacts).LoadAsync();
            await Context.Entry(existingEntry).Collection(x => x.MusicLabelContacts).LoadAsync();

            FillRelationCollection(mappedEntry.PlatformContacts, existingEntry.PlatformContacts, p=>p.PlatformId);

            FillRelationCollection(mappedEntry.MusicianContacts, existingEntry.MusicianContacts, m=>m.MusicianId);

            FillRelationCollection(mappedEntry.MusicLabelContacts, existingEntry.MusicLabelContacts, ml=>ml.MusicLabelId);

            await Context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        private static void FillRelationCollection<T>(ICollection<T> mappedCollection, ICollection<T> existingCollection, Func<T,int> getid)
            where T: IBaseEntryModel<int>
        {
            foreach (var p in existingCollection.ToArray())
            {
                if (mappedCollection.Any(x => getid(p) == x.Id))
                    continue;
                else existingCollection.Remove(p);
            }
            foreach (var p in mappedCollection)
            {
                if (existingCollection.Any(x => getid(p) == x.Id))
                    continue;
                else existingCollection.Add(p);
            }
        }
    }
}
