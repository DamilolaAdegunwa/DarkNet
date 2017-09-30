using Dark.Web.Controllers;
using OA.Application.ShopMallApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dark.Core.Authorization;
using System.Threading.Tasks;
using Dark.Web.Models;
using System.Reflection;
using Dark.Web.Authorization;

namespace OA.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}