

namespace TweetBook4.Contracts.vi
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public const string Base = Root + "/" + Version;
        public static class Employee
        {
            public const  string getAll = Base+"/employees";
            public const string Create = Base + "/employees";
            public const string Get = Base + "/employees/{id:int}";
            public const string Update = Base + "/employees/{id:int}";
            public const string Delete = Base + "/employees/{id:int}";
            public const string Search = Base + "/employees/search";
        }

        public static class Dept
        {
            public const string getAll = Base + "/departments";
            public const string Create = Base + "/departments";
            public const string Get = Base + "/departments/{id:int}";
            public const string Update = Base + "/departments/{id:int}";
            public const string Delete = Base + "/departments/{id:int}";
            public const string Search = Base + "/departments/search";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
        }
    }
}
