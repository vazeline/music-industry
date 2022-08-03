using System;
using System.Collections.Generic;
using System.Data;

namespace boilerplate.api.data.Stores.Base
{
    internal static class DbTypeMap
    {
        private static readonly Dictionary<Type, DbType> instance;

        public static Dictionary<Type, DbType> Instance => instance;

        static DbTypeMap()
        {
            instance = new Dictionary<Type, DbType>();
            instance[typeof(byte)] = DbType.Byte;
            instance[typeof(sbyte)] = DbType.SByte;
            instance[typeof(short)] = DbType.Int16;
            instance[typeof(ushort)] = DbType.UInt16;
            instance[typeof(int)] = DbType.Int32;
            instance[typeof(uint)] = DbType.UInt32;
            instance[typeof(long)] = DbType.Int64;
            instance[typeof(ulong)] = DbType.UInt64;
            instance[typeof(float)] = DbType.Single;
            instance[typeof(double)] = DbType.Double;
            instance[typeof(decimal)] = DbType.Decimal;
            instance[typeof(bool)] = DbType.Boolean;
            instance[typeof(string)] = DbType.String;
            instance[typeof(char)] = DbType.StringFixedLength;
            instance[typeof(Guid)] = DbType.Guid;
            instance[typeof(DateTime)] = DbType.DateTime;
            instance[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            instance[typeof(byte[])] = DbType.Binary;
            instance[typeof(byte?)] = DbType.Byte;
            instance[typeof(sbyte?)] = DbType.SByte;
            instance[typeof(short?)] = DbType.Int16;
            instance[typeof(ushort?)] = DbType.UInt16;
            instance[typeof(int?)] = DbType.Int32;
            instance[typeof(uint?)] = DbType.UInt32;
            instance[typeof(long?)] = DbType.Int64;
            instance[typeof(ulong?)] = DbType.UInt64;
            instance[typeof(float?)] = DbType.Single;
            instance[typeof(double?)] = DbType.Double;
            instance[typeof(decimal?)] = DbType.Decimal;
            instance[typeof(bool?)] = DbType.Boolean;
            instance[typeof(char?)] = DbType.StringFixedLength;
            instance[typeof(Guid?)] = DbType.Guid;
            instance[typeof(DateTime?)] = DbType.DateTime;
            instance[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;

        }
    }
}
