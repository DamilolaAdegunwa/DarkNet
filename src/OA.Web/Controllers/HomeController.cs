using Dark.Web.Controllers;
using OA.Application.ShopMallApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IShopAppService _shopAppService;

        public HomeController(IShopAppService shopAppService)
        {
            _shopAppService = shopAppService;
        }
        // GET: Home
        public ActionResult Index()
        {
            _shopAppService.Show();
            return View();
        }
    }
}