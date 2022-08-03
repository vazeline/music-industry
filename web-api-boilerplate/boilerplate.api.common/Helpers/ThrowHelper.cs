using System;

namespace boilerplate.api.common.Helpers
{
    public static class ThrowHelper
    {
        public static T NullArgument<T>()
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }
    }
}
