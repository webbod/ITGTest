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
        private int _PageSize = 0;

        public TestDataSource() : base()
        {
        }

        public override void Configure(Models.Configuration.DataSourceConfiguration config)
        {
            _Path = config.ConnectionSettings;
            _PageSize = config.PageSize;

            // this datasource is static and can be loaded immediately
            Load();
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

            _MetaData = new ArticleListMetaData { ArticleCount = _Data.Count(), PageSize = _PageSize };
        }
    }
}