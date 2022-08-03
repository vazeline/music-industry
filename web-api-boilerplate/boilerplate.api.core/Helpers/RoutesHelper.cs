namespace boilerplate.api.core.Helpers
{
    public static class RoutesHelper
    {
        public static class Musician
        {
            public const string Base = "/api/musicians";
            public const string Id = Base + "/{id}";
        }

        public static class MusicLabel
        {
            public const string Base = "/api/musicLabels";
            public const string Id = Base + "/{id}";
        }

        public static class Platform
        {
            public const string Base = "/api/platforms";
            public const string Id = Base + "/{id}";
        }
        public static class Contact
        {
            public const string Base = "/api/contacts";
            public const string RefEntities = "/api/contacts/refentities";
            public const string Id = Base + "/{id}";
        }
    }
}
