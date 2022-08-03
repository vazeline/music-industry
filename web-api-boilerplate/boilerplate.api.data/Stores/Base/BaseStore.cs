using AutoMapper;
using boilerplate.api.common.Helpers;
using boilerplate.api.common.Models;
using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using boilerplate.api.data.Stores.Base;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public abstract class BaseStore : IBaseStore
    {
        private readonly ConnectionStrings _connectionStrings;
        protected readonly IMapper _mapper;

        protected abstract Type DataModelType { get; }
        protected abstract string EntriesProcedureName { get; }
        protected abstract string EntryProcedureName { get; }
        protected ConnectionStrings ConnectionStrings { get => _connectionStrings; }
        protected readonly ApplicationDbContext Context;

        public BaseStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
        {
            _connectionStrings = connectionStrings ?? ThrowHelper.NullArgument<ConnectionStrings>();
            Context = context ?? ThrowHelper.NullArgument<ApplicationDbContext>();
            _mapper = mapper ?? ThrowHelper.NullArgument<IMapper>();
        }

        public virtual async Task<EntriesQueryResponse<T>> GetEntries<T>(EntriesQueryRequest request)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(ProcedureParams.AllowDirtyRead, true, DbType.Boolean);
                parameter.Add(ProcedureParams.Offset, request.Offset, DbType.Int32);
                parameter.Add(ProcedureParams.Limit, request.Limit, DbType.Int32);

                using (var multi = await connection.QueryMultipleAsync(EntriesProcedureName, param: parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new EntriesQueryResponse<T>
                    {
                        Data = multi.Read<T>().ToList(),
                        TotalCount = multi.Read<int>().FirstOrDefault(),
                        Success = true,
                        Code = ResponseCode.Success
                    };
                }
            }
        }

        public virtual async Task<EntriesQueryResponse<T>> GetEntries<T,R>(R request, string procedureName) where R: BaseRequest
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(ProcedureParams.AllowDirtyRead, true, DbType.Boolean);
                foreach (var p in request.GetType().GetProperties())
                    parameter.Add(GetParameterName(p.Name), p.GetValue(request), DbTypeMap.Instance[p.PropertyType]);
                
                using (var multi = await connection.QueryMultipleAsync(procedureName, param: parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new EntriesQueryResponse<T>
                    {
                        Data = multi.Read<T>().ToList(),
                        Success = true,
                        Code = ResponseCode.Success
                    };
                }
            }
        }

        private string GetParameterName(string name)
        {
            return $"@{name.ToLower()}";
        }

        public virtual async Task<EntryQueryResponse<T>> GetEntry<T, K>(EntryQueryRequest<K> request)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                var idType = typeof(K) == typeof(int) ? DbType.Int32 : DbType.Guid;
                parameter.Add(ProcedureParams.Id, request.Id, idType);
                parameter.Add(ProcedureParams.AllowDirtyRead, false, DbType.Boolean);

                using (var multi = await connection.QueryMultipleAsync(EntryProcedureName, param: parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new EntryQueryResponse<T>
                    {
                        Data = multi.Read<T>().FirstOrDefault(),
                        Success = true,
                        Code = ResponseCode.Success
                    };
                }
            }
        }

        public virtual async Task<K> CreateEntry<T, K>(CreateCommandRequest<T> request)
        {
            var mappedEntry = MapCreateModel<T, K>(request);
            Context.Add(mappedEntry);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return mappedEntry.Id;
        }

        public virtual async Task<bool> UpdateEntry<T, K>(UpdateCommandRequest<T> request)
        {
            var mappedEntry = MapUpdateModel<T, K>(request);
            var existingEntry = await Context.FindAsync(DataModelType, mappedEntry.Id).ConfigureAwait(false);
            if (existingEntry == null)
            {
                return false;
            }
            mappedEntry.DateCreated = ((IBaseEntryModel<K>)existingEntry).DateCreated;
            Context.Entry(existingEntry).CurrentValues.SetValues(mappedEntry);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public virtual async Task<bool> DeleteEntry<T>(DeleteCommandRequest<T> request)
        {
            var existingEntry = await Context.FindAsync(DataModelType, request.Id).ConfigureAwait(false);
            if (existingEntry == null)
            {
                return false;
            }
            Context.Remove(existingEntry);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        protected IBaseEntryModel<K> MapCreateModel<T, K>(CreateCommandRequest<T> request)
        {
            return (IBaseEntryModel<K>)_mapper.Map(request.Entry, request.Entry.GetType(), DataModelType);
        }

        protected IBaseEntryModel<K> MapUpdateModel<T, K>(UpdateCommandRequest<T> request)
        {
            return (IBaseEntryModel<K>)_mapper.Map(request.Entry, request.Entry.GetType(), DataModelType);
        }
    }
}
