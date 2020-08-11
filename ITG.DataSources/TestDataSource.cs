using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ITG.Models.Entities;
using Newtonsoft.Json;
using ITG.Models.MetaData;

namespace ITG.DataSources
{
    public class TestDataSource : ADataSource
    {
        //TODO move these into the config file
        private string _Path = String.Empty;

        private List<Article> _Data;

        public TestDataSource() : base()
        {
        }

        public override void Configure(Models.Configuration.DataSourceConfiguration config)
        {
            _Path = config.ConnectionSettings;
            
            _MetaData = new ArticleListMetaData { PageSize = config.PageSize };
        }

        public override List<Models.Entities.Article> GetPage(int pageNumber = 0)
        {
            if (_Data == default(List<Article>) || !_Data.Any())
            {
                Load();
            }

            return _Data.Skip(pageNumber * _MetaData.PageSize).Take(_MetaData.PageSize).ToList();
        }

        public override void Load()
        {
            if (!File.Exists(_Path))
            {
                // String interpolation was only introdced in C# 6.0
                throw new FileNotFoundException(string.Format("The data file was not found at :{0}", _Path));
            }

            using (StreamReader r = new StreamReader(_Path))
            {
                string json = r.ReadToEnd();
                _Data = JsonConvert.DeserializeObject<List<Article>>(json);
            }

            _MetaData.ArticleCount = _Data.Count();
        }
    }
}