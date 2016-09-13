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
    public class DictionaryController : BaseController
    {
     
        public ActionResult DicIndex()
        {
            return View();
        }

        public void GetAllDics()
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
            DictionaryManager dm = new DictionaryManager();
            List<DictionaryEntity> list = dm.GetSearch(searchKey, sort, order, offset, pageSize, out total);
            //List<DicViewModel> listView = new List<DicViewModel>();
            //foreach (var item in list)
            //{
            //    listView.Add(new DicViewModel { iId = item.iId, iKey = item.iKey, iValue = item.iValue, iType = item.iType, iUpdatedOn = item.iUpdatedOn.ToString("yyyyMMdd HH:mm") });
            //}

            //给分页实体赋值  
            PageModels<DictionaryEntity> model = new PageModels<DictionaryEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            HttpContext.Response.Write(jss.Serialize(model));
        }

        public string DicsSaveChanges(string jsonString, string action)
        {

            try
            {
                DictionaryEntity entity = JsonConvert.DeserializeObject<DictionaryEntity>(jsonString);
                DictionaryManager dm = new DictionaryManager();
                if (action == "add")
                {
                    entity.IsDisplay = "T";
                    entity.CreateBy = SessionHelper.CurrentUser.iUserName;
                    entity.UpdateBy = SessionHelper.CurrentUser.iUserName;

                    dm.Insert(entity);
                }
                else
                {
                    DictionaryEntity oldEntity = dm.GetDic(entity.ID);
                    entity.UpdateBy = SessionHelper.CurrentUser.iUserName;
                    entity.UpdateTime = DateTime.Now;
                    entity.CreateTime = oldEntity.CreateTime;
                    entity.CreateBy = oldEntity.CreateBy;

                    dm.Update(entity);
                }
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
