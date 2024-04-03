using System.Net;

namespace InsuranceCalc.Utility
{
    public sealed class ExecutionStatus
    {
        public HttpStatusCode StatusCode { get; set; }

        public string? Message { get; set; } = null!;
        public object? Object { get; set; }

    }
}
