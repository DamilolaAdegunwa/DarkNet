using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MVCDemo
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //加载jquery及easyui
            bundles.Add(new ScriptBundle("~/EasyUI/js")
                .Include("~/EasyUI/jquery.min.js")
                .Include("~/EasyUI/jquery.easyui.min.js")
                .Include("~/EasyUI/jquery.easyui.extend.min.js")
                );


            bundles.Add(new StyleBundle("~/EasyUI/themes/css")
                .Include("~/EasyUI/themes/icon.css")
                .Include("~/EasyUI/themes/color.css")
                .Include("~/EasyUI/themes/metro/easyui.css")
                );


            bundles.Add(new ScriptBundle("~/Scripts/js").Include("~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/CSS/css")
                .Include("~/CSS/main.css")
                .Include("~/CSS/easyui.extend.css"));

            //启用压缩合并
            BundleTable.EnableOptimizations = true;
        }


    }
}