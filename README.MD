# ITG Technical test

Render a newsfeed, dynamically loading pages of articles - migrated from MVC4 to target dotNet core

## ITG.Models
*  _Configuration.DataSourceConfiguration_ - manages page size and connnect settings
*  _Entities.Article_ - represents an article
*  _Metadata.ArticleListMetaData_ - represents paging information

## ITG.Interfaces
*  _IDataSource_ defines the shape of a configurable paged DataSource

## ITG.DataSources
*  _ADataSource_ implements _IDataSource_ and acts as a template for concrete DataSource classes
*  _FileDataSource_ inherits from _ADataSource_ consumes a Json document, expects the connection settings to point to a file hosted in the site 

## ITG.Web
*  _Startup.cs_ - manages dependency injection for IDataSource
*  _wwwroot/js/newsfeed.js_ - runs the UX
*  _Controllers.ABaseDataSourceController_ - configures an IDataSourceInstance
*  _Controllers.NewsFeedController_ 
   +  **/Newsfeed/Index** - renders the newsfeed UI 
   +  **/NewsFeed/Page/{number}** - returns a Json array of articles

