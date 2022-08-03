namespace boilerplate.ui.Helpers
{
    public static class UIRoutesHelper
    {
        public static class Home
        {
            public static class Main
            {
                public const string PATH = "/";
                public static string GetUrl() => $"{PATH}";
            }
        }

        public static class Musician
        {
            public static class GetEntries
            {
                public const string PATH = "musicians";
                public static string GetUrl(int? offset = null, int? limit = null)
                    => $"/{PATH}/{string.Empty.AddPaging(offset, limit)}";
            }

            public static class CreateEntry
            {
                public const string PATH = "musicians/create";
                public static string GetUrl() => $"/{PATH}/";
            }

            public static class UpdateEntry
            {
                public const string PATH = "musicians/{id}/update";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }

            public static class DeleteEntry
            {
                public const string PATH = "musicians/{id}/delete";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }
        }

        public static class MusicLabel
        {
            public static class GetEntries
            {
                public const string PATH = "musicLabels";
                public static string GetUrl(int? offset = null, int? limit = null)
                    => $"/{PATH}/{string.Empty.AddPaging(offset, limit)}";
            }

            public static class CreateEntry
            {
                public const string PATH = "musicLabels/create";
                public static string GetUrl() => $"/{PATH}/";
            }

            public static class UpdateEntry
            {
                public const string PATH = "musicLabels/{id}/update";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }

            public static class DeleteEntry
            {
                public const string PATH = "musicLabels/{id}/delete";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }
        }

        public static class Platform
        {
            public static class GetEntries
            {
                public const string PATH = "platforms";
                public static string GetUrl(int? offset = null, int? limit = null)
                    => $"/{PATH}/{string.Empty.AddPaging(offset, limit)}";
            }

            public static class CreateEntry
            {
                public const string PATH = "platforms/create";
                public static string GetUrl() => $"/{PATH}/";
            }

            public static class UpdateEntry
            {
                public const string PATH = "platforms/{id}/update";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }

            public static class DeleteEntry
            {
                public const string PATH = "platforms/{id}/delete";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }
        }

        public static class Contact
        {
            public static class GetEntries
            {
                public const string PATH = "contacts";
                public static string GetUrl(int? offset = null, int? limit = null)
                    => $"/{PATH}/{string.Empty.AddPaging(offset, limit)}";
            }

            public static class CreateEntry
            {
                public const string PATH = "contacts/create";
                public static string GetUrl() => $"/{PATH}/";
            }

            public static class UpdateEntry
            {
                public const string PATH = "contacts/{id}/update";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }

            public static class DeleteEntry
            {
                public const string PATH = "contacts/{id}/delete";
                public static string GetUrl(object id) => $"/{PATH.Replace("{id}", $"{id}")}/";
            }
        }


        private static string AddPaging(this string queryString, int? offset = null, int? limit = null)
        {
            if (offset >= 0)
            {
                queryString = queryString.SetQueryString("offset", offset);

                if (limit > 0)
                {
                    queryString = queryString.SetQueryString("limit", limit);
                }
            }

            return queryString;
        }


        private static string SetQueryString(this string queryString, string name, object value)
        {
            return value == null
                ? queryString
                : $"{(string.IsNullOrWhiteSpace(queryString) ? "?" : $"{queryString}&")}{name}={value}";
        }
    }
}
