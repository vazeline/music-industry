using AutoMapper;
using boilerplate.api.common.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using System;

namespace boilerplate.api.data.Stores
{
    public class MusicianStore: BaseStore, IMusicianStore
    {
        public MusicianStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {
            
        }

        protected override Type DataModelType => typeof(Musician);
        protected override string EntriesProcedureName => MusicianGetEntriesProcedure.Name;
        protected override string EntryProcedureName => MusicianGetEntriesProcedure.Name;
    }
}
