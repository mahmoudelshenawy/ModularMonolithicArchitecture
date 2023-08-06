namespace Shared.Models.DockerEnviroments
{
    public static class DockerEnvironments
    {
        public static string SqlServerDB_HOST = Environment.GetEnvironmentVariable("DB_HOST") ?? string.Empty;
        public static string SqlServerDB_NAME = Environment.GetEnvironmentVariable("DB_NAME") ?? string.Empty;
        public static string SqlServerDB_SA_PASSWORD = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? string.Empty;
        public static string RABBITMQ_HOST_URL = Environment.GetEnvironmentVariable("RABBITMQ_HOST_URL") ?? string.Empty;
        public static string RABBITMQ_USERNAME = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? string.Empty;
        public static string RABBITMQ_PASSWORD = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? string.Empty;
    }
}
