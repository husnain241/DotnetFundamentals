namespace Auth.DTOs
{
    public class ProductBenchmarkResultDto<T>
    {
        public string GeneratedSql { get; set; } = string.Empty;
        public long ExecutionTimeMilliseconds { get; set; }
        public int RecordCount { get; set; }
        public T Data { get; set; } = default!;
    }
}