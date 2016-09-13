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
using System.Web.Http;

namespace CEA_EDU.Web.Controllers
{
    public class DictionaryServiceController : ApiController
    {

        public string GetDic()
        {
            DictionaryManager dm = new DictionaryManager();
            return new JavaScriptSerializer().Serialize(dm.GetDic(2));
        }
     
        public DictionaryEntity GetDicByID(int ID)
        {
            DictionaryManager dm = new DictionaryManager();
            return dm.GetDic(ID);
        }

        public DictionaryEntity GetDicByCode(string code)
        {
            DictionaryManager dm = new DictionaryManager();
            return dm.GetDicByCode(code);
        }

        public string GetAllDics(string order, string sort, string searchKey, int offset, int pageSize)
        {
            //用于序列化实体类的对象  
            JavaScriptSerializer jss = new JavaScriptSerializer();

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
            return jss.Serialize(model);
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
