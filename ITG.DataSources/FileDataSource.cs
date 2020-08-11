using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ITG.Models.Entities;
using System.Text.Json;
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
                
                throw new FileNotFoundException($"The data file was not found at :{_Path}");
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

                // switched to using native dotnet core JsonSerializer
                _Data = JsonSerializer.Deserialize<List<Article>>(json);
            }

            _MetaData.ArticleCount = _Data.Count();
        }
    }
}