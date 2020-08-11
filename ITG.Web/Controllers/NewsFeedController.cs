using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITG.Interfaces;
using ITG.Models.Entities;
using ITG.Models.MetaData;
using Newtonsoft.Json;
using ITG.Web.Models;

namespace ITG.Web.Controllers
{
    public class NewsFeedController : Controller
    {
        private IDataSource _DataSource;

        public NewsFeedController(IDataSource dataSource)
        {
            _DataSource = dataSource;
        }

        //
        // GET: /NewsFeed/

        public ActionResult Index()
        {
            var startingPage = 0;

            var model = new NewsFeedViewModel
            {
                CurrentPage = startingPage,
                MetaData = _DataSource.MetaData,
                Articles = _DataSource.GetPage(startingPage)
            };

            return View(model);
        }

        //
        // GET: /NewsFeed/Page/5

        public JsonResult Page(int id = 0)
        {
            IEnumerable<Article> output = null;

            if (id < _DataSource.MetaData.PageCount)
            {
                output = _DataSource.GetPage(id);
            }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /NewsFeed/MetaData

        public JsonResult MetaData()
        {
            return Json(_DataSource.MetaData, JsonRequestBehavior.AllowGet);
        }
    }
}
