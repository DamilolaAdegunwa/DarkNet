﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dark.Core.Authorization;
using Dark.Web.Authorization;
using Dark.Web.Controllers;
using Dark.Web.Models;
using OA.Application.ShopMallApp;

namespace OA.Web.Controllers
{
    [Skip]
    public class LoginController : BaseController
    {
        private readonly ILoginManager loginManager;
        //private IShopAppService shopAppService;
        public LoginController(ILoginManager _loginManager)
        {
            loginManager = _loginManager;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // Post: Login
        public async Task<ActionResult> LoginAsync(LoginModel loginModel)
        {
           

            return await ResultAsync(await loginManager.LoginAsync(loginModel));
        }
    }
}