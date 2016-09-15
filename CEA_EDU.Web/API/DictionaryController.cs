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
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace CEA_EDU.Web.API
{
    public class DictionaryController : ApiController
    {
        public string GetDic()
        {
            SysDicManager dm = new SysDicManager();
            return new JavaScriptSerializer().Serialize(dm.GetDic(0));
        }
     
        public SysDicEntity GetDicByID(int ID)
        {
            SysDicManager dm = new SysDicManager();
            return dm.GetDic(ID);
        }

        public SysDicEntity GetDicByCode(string code)
        {
            SysDicManager dm = new SysDicManager();
            return dm.GetDicByCode(code);
        }

        public string GetDicByName(string name)
        {
            SysDicManager dm = new SysDicManager();
            return new JavaScriptSerializer().Serialize(dm.GetDicByName(name));
        }

        public string GetAllDics(string order, string sort, string searchKey, int offset, int pageSize)
        {
            int total = 0;
            SysDicManager dm = new SysDicManager();
            List<SysDicEntity> list = dm.GetSearch(searchKey, sort, order, offset, pageSize, out total);

            //给分页实体赋值  
            PageModels<SysDicEntity> model = new PageModels<SysDicEntity>();
            model.total = total;
            if (total % pageSize == 0)
                model.page = total / pageSize;
            else
                model.page = (total / pageSize) + 1;

            model.rows = list;

            //将查询结果返回  
            return new JavaScriptSerializer().Serialize(model);
        }

        public string PostDic(SysDicEntity entity)
        {
            try
            {
                //DictionaryViewModel model = JsonConvert.DeserializeObject<DictionaryViewModel>(jsonString.ToString());

                if (entity == null)
                {
                    return "error";
                }

                SysDicManager dm = new SysDicManager();

                entity.IsDisplay = "T";
                entity.CreateTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;

                dm.Insert(entity);
              
                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string PutDic(SysDicEntity entity)
        {
            try
            {
                //DictionaryViewModel model = JsonConvert.DeserializeObject<DictionaryViewModel>(jsonString.ToString());

                if (entity == null)
                {
                    return "error";
                }

                SysDicManager dm = new SysDicManager();

                entity.IsDisplay = "T";
                entity.CreateTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;

                dm.Update(entity);

                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteDic(int id)
        {
            try
            {
                //DictionaryViewModel model = JsonConvert.DeserializeObject<DictionaryViewModel>(jsonString.ToString());

                SysDicManager dm = new SysDicManager();

                SysDicEntity entity = dm.GetDic(id);

                entity.Valid = "F";
                entity.CreateTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;

                dm.Update(entity);

                return "success";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
