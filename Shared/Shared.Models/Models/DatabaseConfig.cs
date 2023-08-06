namespace Shared.Models.Models
{
    public class DatabaseConfig
    {
        public DatabaseConfig(bool isContainerized, string defaultConnectionString,
            string dockerComposeConnectionString, List<KeyValuePair<string, string>> variables)
        {
            IsContainerized = isContainerized;
            Variables = variables;
            ConnectionString = SetConnectionString(defaultConnectionString, dockerComposeConnectionString);
        }

        public bool IsContainerized { get; set; }
        public List<KeyValuePair<string, string>> Variables { get; set; } = new();
        public string ConnectionString { get; set; }

        private string SetConnectionString(string defaultConnectionString, string dockerComposeConnectionString)
        {
            if (IsContainerized)
            {
                foreach (var list in Variables)
                {
                    dockerComposeConnectionString = dockerComposeConnectionString.Replace(list.Key, list.Value);
                }
                return dockerComposeConnectionString;
            }
            return defaultConnectionString;
        }
    }
}
