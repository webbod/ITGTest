using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Day { get {  return Date.ToString("dd"); } }
        public string Month { get { return Date.ToString("MMM"); } }
        public string Year { get { return Date.ToString("yyyy"); } }
        public string FormattedDate
        {
            get { return Date.ToString("dd MMM yyyy"); }
        }
    }
}
