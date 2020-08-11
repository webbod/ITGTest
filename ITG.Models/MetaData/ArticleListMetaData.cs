using System;

namespace ITG.Models.MetaData
{
    public struct ArticleListMetaData
    {
        public int ArticleCount { get; set; }
        public int PageSize { get; set; }

        public int PageCount => (int)Math.Ceiling((double)(ArticleCount / PageSize));  
    }
}
