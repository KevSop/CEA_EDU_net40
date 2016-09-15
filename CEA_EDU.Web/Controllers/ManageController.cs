using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CEA_EDU.Domain;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Domain.Manager;
using CEA_EDU.Web.Models;
using CEA_EDU.Web.Utils;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CEA_EDU.Web.Controllers
{
    public class ManageController : BaseController
    {
        public ActionResult UserIndex()
        {
            ViewBag.Companies = new SysDicManager().GetDicByParentCode("Company");

            var contents = GetStandardMenuTree(false);
            ViewBag.treeNodes = JsonConvert.SerializeObject(contents);

            return View();
        }

        public ActionResult DicIndex()
        {
            return View();
        }

        public ActionResult PeopleIndex()
        {
            return View();
        }

        public ActionResult MenuIndex()
        {
            var contents = GetStandardMenuTree(true);
            ViewBag.treeNodes = JsonConvert.SerializeObject(contents);
            return View();
        }

        #region 用户方法
        public void GetAllUsers()
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //请求中携带的条件  
            string order = HttpContext.Request.Params["order"];
            string sort = HttpContext.Request.Params["sort"];
            string searchKey = HttpContext.Request.Params["search"];
            int offset = Convert.ToInt32(HttpContext.Request.Params["offset"]);  //0
            int pageSize = Convert.ToInt32(HttpContext.Request.Params["limit"]);

            int total = 0;
            UserManager um = new UserManager();
            List<UserInfoEntity> list = um.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            List<UserViewModel> listView = new List<UserViewModel>();
            foreach (var item in list)
            {
                listView.Add(new UserViewModel { 
                    ID = item.ID,
                    Code = item.Code, 
                    Name = item.Name, 
                    Type = item.Type, 
                    Group = item.Group, 
                    Company = item.Company, 
                    Department = item.Department, 
                    PositionID = item.PositionID, 
                    PositionName = item.PositionName, 
                    Sex = item.Sex,
                    Birthday = item.Birthday == null ? "" : ((DateTime)item.Birthday).ToString("yyyy-MM-dd"), 
                    Email = item.Email, 
                    Phone = item.Phone, 
                    Address = item.Address, 
                    UpdateTime = item.UpdateTime
                });
            }

            //给分页实体赋值  
            PageModels<UserViewModel> model = new PageModels<UserViewModel>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = listView;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public string UsersSaveChanges(string jsonString, string action)
        {

            try
            {
                UserInfoEntity ue = JsonConvert.DeserializeObject<UserInfoEntity>(jsonString);
                UserManager pm = new UserManager();
                if (action == "add")
                {
                    pm.Insert(ue);
                }
                else
                {
                    UserInfoEntity ueOld = pm.GetUser(ue.Code);
                    ueOld.Name = ue.Name;
                    ueOld.Type = ue.Type;
                    ueOld.Company = ue.Company;

                    ueOld.UpdateBy = SessionHelper.CurrentUser.Code;

                    pm.Update(ueOld);
                }
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        #endregion

        #region 公用方法
        public ActionResult ChangeCompany(string newCompany)
        {
            if (SessionHelper.CurrentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                LoginUserViewModel userinfo = SessionHelper.CurrentUser;
                userinfo.Company = newCompany;
                Session[SessionHelper.CurrentUserKey] = userinfo;
                return Content("success");
            }
        }

        private void WriteDataTableToServer(DataTable dt, string targettable, string connectstr)
        {

            using (SqlConnection conn = new SqlConnection(connectstr))
            {
                conn.Open();
                using (SqlBulkCopy sqlbulk = new SqlBulkCopy(conn))
                {

                    sqlbulk.DestinationTableName = targettable;
                    sqlbulk.NotifyAfter = dt.Rows.Count;
                    for (int i = 0; i < dt.Columns.Count; ++i)
                    {
                        SqlBulkCopyColumnMapping map = new SqlBulkCopyColumnMapping(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        sqlbulk.ColumnMappings.Add(map);

                    }
                    sqlbulk.WriteToServer(dt);

                }

            }

        }
        #endregion

        #region 菜单方法
        public string SaveItemTree(string treeJson)
        {
            try
            {
                List<ItemContent2> contents = JsonConvert.DeserializeObject<List<ItemContent2>>(treeJson);
                List<ItemContent> contents2 = RecursiveFun(contents[0]);
                if (contents2.Count() > 0)
                {
                    string[] nameUrl = contents2[0].name.Split('$');
                    StringBuilder sourceTableSb = new StringBuilder("MERGE INTO [SysMenu] AS T USING( SELECT ");
                    sourceTableSb.Append("'" + contents2[0].id + "' as GUID, ");
                    sourceTableSb.Append("'" + contents2[0].pId + "' as PARENTID, ");
                    sourceTableSb.Append("'" + nameUrl[0].Replace("'", "''") + "' as NAME, ");
                    sourceTableSb.Append("'" + nameUrl[1] + "' as URL ");

                    for (int i = 1; i < contents2.Count; i++)
                    {
                        nameUrl = contents2[i].name.Split('$');
                        sourceTableSb.Append(" union all select ");
                        sourceTableSb.Append("'" + contents2[i].id + "', ");
                        sourceTableSb.Append("'" + contents2[i].pId + "', ");
                        sourceTableSb.Append("'" + nameUrl[0].Replace("'", "''") + "', ");
                        sourceTableSb.Append("'" + nameUrl[1] + "' ");
                    }
                    sourceTableSb.Append(") AS S ON T.GUID = S.GUID WHEN MATCHED THEN UPDATE SET T.PARENTID=S.PARENTID, T.NAME=S.NAME, T.IUrl = S.URL WHEN NOT MATCHED THEN INSERT(GUID,PARENTID,NAME,URL) VALUES(S.GUID,S.PARENTID,S.NAME,S.URL);");

                    DbHelperSQL.ExecuteSql(sourceTableSb.ToString());

                }
                /* 样例
       MERGE INTO [TestDb].[dbo].[GUIDTable] AS T
       USING 
     ( select '1' as iguid, 'test1'as idata
     union all select '1375' , 'test1375'
     union all select '2' , 'test2'
     ) 
     AS S
     ON T.iguid=S.iguid
     WHEN MATCHED 
     THEN UPDATE SET T.idata=S.idata , t.iguid=s.iguid
     WHEN NOT MATCHED 
     THEN INSERT(iguid)  VALUES(S.iguid);
     */
                return "success";

            }
            catch (Exception e)
            {
                return "fail" + e.ToString();
            }

        }

        private List<ItemContent> RecursiveFun(ItemContent2 items)
        {
            List<ItemContent> resultTemp = new List<ItemContent>();
            resultTemp.Add(new ItemContent { id = items.id, pId = items.pId, name = items.name });

            if (items.children == null || items.children.Count() == 0)
                return resultTemp;
            else
            {
                foreach (var item in items.children)
                {
                    resultTemp.AddRange(RecursiveFun(item));
                }
                return resultTemp;
            }
        }

        public JsonResult GetUserMenuTree(string userCode)
        {
            try
            {
                string querySql = "select MenuId+'|'+MenuRights from sysUserMenu where UserCode = '{0}'";
                DataSet ds = DbHelperSQL.Query(string.Format(querySql, userCode));
                List<string> contents = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    contents.Add(dr[0].ToString());
                }
                return new JsonResult { Data = new { success = true, data = contents }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, data = "", msg = ex.ToString() }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        public string UserMenuSaveChanges(string jsonString, string userid, string companyid)
        {
            try
            {
                List<string> menuIds = JsonConvert.DeserializeObject<List<string>>(jsonString);

                DataTable dt = new DataTable("sysUserMenu");
                dt.Columns.Add("UserCode");
                dt.Columns.Add("MenuId");
                dt.Columns.Add("MenuRights");
                foreach (string menuid in menuIds)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow[0] = userid;
                    dataRow[1] = menuid.Split('|')[0];
                    dataRow[2] = menuid.Split('|')[1];
                    dt.Rows.Add(dataRow);
                }
                string deleteSql = "delete from [sysUserMenu] where UserCode = '{0}'";
                DbHelperSQL.ExecuteSql(string.Format(deleteSql, userid, companyid));
                WriteDataTableToServer(dt, "sysUserMenu", ConfigurationManager.ConnectionStrings["ECAEDUConnectionString"].ConnectionString);
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        private List<ItemContent> GetStandardMenuTree(bool widthUrl = true)
        {
            string querySql = "select guid, parentid, name, Url from  [sysMenu] ";
            DataSet ds = DbHelperSQL.Query(querySql);
            List<ItemContent> contents = new List<ItemContent>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (widthUrl)
                {
                    contents.Add(new ItemContent { id = dr[0].ToString(), pId = dr[1].ToString(), name = dr[2].ToString() + "$" + ((dr[3] != null && !string.IsNullOrEmpty(dr[3].ToString())) ? dr[3].ToString() : ""), open = true });

                }
                else
                {
                    contents.Add(new ItemContent { id = dr[0].ToString(), pId = dr[1].ToString(), name = dr[2].ToString(), open = true });

                }
            }
            return contents;
        }

        public string GetMenuHtml(string rootUrl)
        {
            if (SessionHelper.CurrentUser.Type == "超级管理员")
                return "";


            string querySql = "select guid, parentid, name, Url from  [sysMenu] a inner join [sysUserMenu] b on a.guid = b.menuid and b.UserCode='" + SessionHelper.CurrentUser.Code + "'";
            if (SessionHelper.CurrentUser.Type == "超级管理员")
            {
                querySql = "select guid, parentid, name, Url from  [sysMenu]";
            }
            DataSet ds = DbHelperSQL.Query(querySql);
            List<ItemContent> contents = new List<ItemContent>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                contents.Add(new ItemContent { id = dr[0].ToString(), pId = dr[1].ToString(), name = dr[2].ToString() + "$" + ((dr[3] != null && !string.IsNullOrEmpty(dr[3].ToString())) ? dr[3].ToString() : "") });
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in contents.Where(i => i.pId == "0"))
            {
                sb.AppendLine(string.Format("<li class=\"sub-menu\"> \n <a href=\"javascript:;\"> \n <i class=\"icon-laptop\"></i> \n <span>{0}</span> \n </a>", item.name.Split('$')[0]));

                foreach (var menu in contents.Where(i => i.pId == item.id))
                {
                    sb.AppendLine(string.Format("<ul class=\"sub\"> \n <li><a href=\"{1}\">{0}</a></li> \n </ul>", menu.name.Split('$')[0], rootUrl + "/" + menu.name.Split('$')[1]));
                }
                sb.AppendLine("</li>");
            }
            return sb.ToString();
        }
        #endregion
        
    }
}
