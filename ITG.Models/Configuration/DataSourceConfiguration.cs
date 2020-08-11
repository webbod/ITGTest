namespace ITG.Models.Configuration
{
    public struct DataSourceConfiguration
    {
        /// <summary>
        /// contains enough information to connect to the datasource - it could be a blob of Json
        /// </summary>
        public string ConnectionSettings { get; set; }
        /// <summary>
        /// number of articles to return on a page
        /// </summary>
        public int PageSize { get; set; }
    }
}
