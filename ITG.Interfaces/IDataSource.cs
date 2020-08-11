using System.Collections.Generic;
using ITG.Models.Entities;
using ITG.Models.MetaData;
using ITG.Models.Configuration;

namespace ITG.Interfaces
{
    public interface IDataSource
    {
        /// <summary>
        /// Describes the datasource
        /// </summary>
        ArticleListMetaData MetaData { get; }

        /// <summary>
        /// Pages through the dataset
        /// </summary>
        /// <param name="pageNumber">requested page</param>
        /// <returns>a page worth of records</returns>
        List<Article> GetPage(int pageNumber = 0);

        /// <summary>
        /// Configures the datasource
        /// </summary>
        /// <param name="config">configuration data</param>
        void Configure(DataSourceConfiguration config);
    }
}
