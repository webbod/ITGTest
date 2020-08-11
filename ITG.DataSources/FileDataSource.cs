using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ITG.Models.Entities;
using Newtonsoft.Json;
using ITG.Models.MetaData;
using ITG.Models.Configuration;

namespace ITG.DataSources
{
    public class FileDataSource : ADataSource
    {
        private string _Path = String.Empty;

        private List<Article> _Data;

        public FileDataSource() : base()
        {
        }

        private void ConfigurePath(DataSourceConfiguration config)
        {
            // ensure the path to the file is in the correct format
            var path = config.ConnectionSettings.Replace('/', '\\');
            path = path.Substring(0, 1) == "~" ? path.Substring(1) : path;
            path = path.Substring(0, 1) == "\\" ? path.Substring(1) : path;

            _Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            if (!File.Exists(_Path))
            {
                // String interpolation was only introdced in C# 6.0
                throw new FileNotFoundException(string.Format("The data file was not found at :{0}", _Path));
            }
        }

        public override void Configure(DataSourceConfiguration config)
        {
            ConfigurePath(config);
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
            using (StreamReader r = new StreamReader(_Path))
            {
                string json = r.ReadToEnd();
                _Data = JsonConvert.DeserializeObject<List<Article>>(json);
            }

            _MetaData.ArticleCount = _Data.Count();
        }
    }
}