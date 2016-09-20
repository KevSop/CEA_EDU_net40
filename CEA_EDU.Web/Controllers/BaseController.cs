﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CEA_EDU.Domain;
using CEA_EDU.Web.Utils;

namespace CEA_EDU.Web.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判定登录
            if (SessionHelper.CurrentUser == null)
            {
                filterContext.Result = RedirectToRoute(new { Controller = "Account", Action = "Login", returnUrl = Request.Url.ToString() });
            }
            ViewBag.CurrentPageRights = "unknow";
            if (!"Home".Equals(filterContext.RequestContext.RouteData.Values["controller"].ToString()))
            {   
		//判定访问权限
                string url = Request.Url.LocalPath.TrimStart('/');
                if (Request.ApplicationPath != "/")
                {
                     url = Request.Url.LocalPath.Replace(Request.ApplicationPath, "").TrimStart('/');
                }
                string result = HasVisitRights(url);
                if (result == "deny")
                    filterContext.Result = RedirectToRoute(new { Controller = "Home", Action = "Index" });
                else
                {
                    ViewBag.CurrentPageRights = result;
                }

            }

            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());
            log.Error(filterContext.Exception);
        }

        private string HasVisitRights(string url)
        {
            if (SessionHelper.CurrentUser == null)
            {
                return "deny";
            }
            
            if (SessionHelper.CurrentUser.Type == "超级管理员")
                return "w";

            string sql = "SELECT top 1 [MenuRights] FROM [SysUserMenu] a inner join [SysMenu] b on a.MenuId = b.guid and a.UserCode = '{0}' and b.Url = '{1}'";
            DataSet ds = DbHelperSQL.Query(string.Format(sql, SessionHelper.CurrentUser.Code, url));

            if (ds.Tables[0].Rows.Count == 0)
                return "deny";
            else return ds.Tables[0].Rows[0][0].ToString();
        }
    }
}
