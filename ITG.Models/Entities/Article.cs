﻿using System;

namespace ITG.Models.Entities
{
    public struct Article
    {
        public int ArticleId { get; set; }
        public string Headline { get; set; }
        public string Summary { get; set; }
        public string ImageUrl {get; set;}
        public string Body {get; set;}
        public DateTime Date { get; set;}

        public string Day => Date.ToString("dd"); 
        public string Month => Date.ToString("MMM");
        public string Year => Date.ToString("yyyy");
        public string FormattedDate => Date.ToString("dd MMM yyyy");

    }
}
