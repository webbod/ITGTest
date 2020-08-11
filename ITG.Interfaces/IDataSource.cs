using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITG.Models.Entities;
using ITG.Models.MetaData;

namespace ITG.Interfaces
{
    public interface IDataSource
    {
        ArticleListMetaData MetaData { get; }
        List<Article> GetPage(int pageNumber = 0);
    }
}
