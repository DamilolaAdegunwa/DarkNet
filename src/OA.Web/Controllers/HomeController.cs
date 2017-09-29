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
    [Skip]
    public class HomeController : BaseController
    {

        private LoginManager _loginManager;

        public HomeController(LoginManager loginManager)
        {
            _loginManager = loginManager;
            //_shopAppService = shopAppService;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<JsonResult> LoginAsync(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return RedirectAjaxData(AjaxResult.Fail("表单数据错误"));
            }
            return ToJSON(await _loginManager.LoginAsync(login));
        }
    }
}