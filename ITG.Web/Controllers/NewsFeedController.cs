using ITG.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ITG.Web.Controllers
{
    public class NewsFeedController : ABaseDataSourceController
    {


        public NewsFeedController(IDataSource dataSource, IConfiguration configuration) : base(dataSource, configuration)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Page(int id = 0)
        {
            var output = _DataSource.GetPage(id);

            return Json(output);
        }

    }
}
