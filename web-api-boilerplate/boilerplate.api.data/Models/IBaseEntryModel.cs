using System;

namespace boilerplate.api.data.Models
{
    public interface IBaseEntryModel<T>
    {
        T Id { get; set; }
        DateTimeOffset DateCreated { get; set; }
    }
}
