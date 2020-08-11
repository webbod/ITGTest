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
        protected List<Article> _Data;

        public ADataSource()
        {

        }

        public virtual List<Models.Entities.Article> GetPage(int pageNumber = 0)
        {
            // returning an IEnumerable rather than an IQueryable breaks the link between the 
            // returned data and the underlying dataset - influenced by the Repository pattern
            return _Data.Skip(pageNumber * _MetaData.PageSize).Take(_MetaData.PageSize).ToList();
        }

        public Models.MetaData.ArticleListMetaData MetaData
        {
            get { return _MetaData; }
        }

        public abstract void Load();

        public abstract void Configure(DataSourceConfiguration config);

    }
}
