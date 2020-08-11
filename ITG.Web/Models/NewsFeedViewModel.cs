using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITG.Models.MetaData;
using ITG.Models.Entities;

namespace ITG.Web.Models
{
    public class NewsFeedViewModel
    {
        public ArticleListMetaData MetaData { get; set; }
        public int CurrentPage { get; set; }
    }
}