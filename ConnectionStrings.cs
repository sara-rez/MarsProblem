using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MarsProblem
{
    public class ConnectionStrings
    {
        public string ConnectionStringMarsEnabled { get; set; } = string.Empty;
        public string ConnectionStringMarsDisabled { get; set; } = string.Empty;
    }
}
