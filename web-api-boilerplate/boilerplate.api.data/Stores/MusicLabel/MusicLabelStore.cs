using AutoMapper;
using boilerplate.api.common.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using System;

namespace boilerplate.api.data.Stores
{
    public class MusicLabelStore : BaseStore, IMusicLabelStore
    {
        public MusicLabelStore(ConnectionStrings connectionStrings,ApplicationDbContext context, IMapper mapper)
            : base(connectionStrings, context, mapper)
        {

        }

        protected override Type DataModelType => typeof(MusicLabel);
        protected override string EntriesProcedureName => MusicLabelGetEntriesProcedure.Name;
        protected override string EntryProcedureName => MusicLabelGetEntriesProcedure.Name; 
    }
}
