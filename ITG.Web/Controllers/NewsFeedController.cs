using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ITG.Interfaces;

namespace ITG.Web.Controllers
{
    public class NewsFeedController : ABaseDataSourceController
    {


        public NewsFeedController(IDataSource dataSource) : base(dataSource)
        {
        }

        //
        // GET: /NewsFeed/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /NewsFeed/Page/5

        public JsonResult Page(int id = 0)
        {
            var output = _DataSource.GetPage(id);
            
            return Json(output, JsonRequestBehavior.AllowGet);
        }

    }
}
