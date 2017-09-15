using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace OA.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //加载jquery及easyui

            bundles.Add(new StyleBundle("~/lib/layui/css")
                .Include("~/lib/layui/css/layui.css"));

            bundles.Add(new ScriptBundle("~/lib/layui/js")
                .Include("~/lib/layui/layui.js")
                );


            //启用压缩合并
            BundleTable.EnableOptimizations = true;
        }


    }
}