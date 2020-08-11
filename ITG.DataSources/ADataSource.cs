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

        public abstract void Load();

        public abstract void Configure(DataSourceConfiguration config);

    }
}
