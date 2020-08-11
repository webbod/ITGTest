using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Models.MetaData
{
    public struct ArticleListMetaData
    {
        public int ArticleCount { get; set; }
        public int PageSize { get; set; }
        public int PageCount
        {
            // PageCount is never going to be a large number
            // Cieling has different implementations for decimals and doubles
            get { return (int)Math.Ceiling((double)(ArticleCount / PageSize)); } 
        }
    }
}
