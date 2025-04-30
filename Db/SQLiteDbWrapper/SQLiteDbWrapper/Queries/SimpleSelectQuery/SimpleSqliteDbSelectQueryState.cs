namespace SqliteDbWrapper.Queries.SimpleSelectQuery
{
    /// <summary>
    /// Basic data object that aggregates the simple select query fields together.
    /// </summary>
    internal class SimpleSqliteDbSelectQueryState
    {
        public bool? IsDistinct { get; set; } = false;
        public int? Top { get; set; } = null;
        public string Fields { get; set; } = string.Empty;
        public string Where { get; set; } = string.Empty;
        public string GroupBy { get; set; } = string.Empty;
        public string OrderBy { get; set; } = string.Empty;
        public bool? IsAsc { get; set; } = false;
    }
}
