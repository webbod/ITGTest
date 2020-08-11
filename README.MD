# ITG Technical test

Render a newsfeed, dynamically loading pages of articles

## ITG.Models
_Configuration.DataSourceConfiguration_ - manages page size and connnect settings
_Entities.Article_ - represents an article
_Metadata.ArticleListMetaData_ - represents paging information

## ITG.Interfaces
_IDataSource_ defines the shape of a configurable paged DataSource

## ITG.DataSources
_ADataSource_ implements _IDataSource_ and acts as a template for concrete DataSource classes
_TestDataSource_ consumes a Json document, expects the connection settings to point to a file hosted in the site 

## ITG.Web
_Bootstrapper.cs_ - manages dependency injection for IDataSource
_Controllers.ABaseDataSourceController_ - configures an IDataSourceInstance
_Controllers.NewsFeedController_ - /Newsfeed/Index - renders UI, /NewsFeed/Page/{number} - returns a Json array of articles
_Scripts/app.js_ - runs the UX
