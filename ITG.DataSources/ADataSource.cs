using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITG.Interfaces;
using ITG.Models.Entities;
using ITG.Models.MetaData;
using ITG.Models.Configuration;

namespace ITG.DataSources
{
    public abstract class ADataSource : IDataSource
    {
        protected ArticleListMetaData _MetaData;

        public ArticleListMetaData MetaData
        {
            get { return _MetaData; }
        }

        public abstract List<Models.Entities.Article> GetPage(int pageNumber = 0);

        public abstract void Configure(DataSourceConfiguration config);

        // this is public, but not included in IDataSource - there may be odd cases
        // where a consumer needs to explicitly invoke Load, but it will be obvious
        // in the code where this is happening and should be discouraged
        public abstract void Load();
    }
}
