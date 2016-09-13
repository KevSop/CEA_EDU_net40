using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CEA_EDU.Domain.Entity;
using CEA_EDU.Common.Extend;

namespace CEA_EDU.Domain.Manager
{
    /// <summary>
    /// 类型管理类
    /// </summary>
    public class DictionaryManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(DictionaryEntity entity)
        {
            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<DictionaryEntity>(session.Connection, entity, session.Transaction);
                session.Commit();
            }
            catch (System.Exception)
            {
                session.Rollback();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }

        public void Update(DictionaryEntity entity)
        {
            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<DictionaryEntity>(session.Connection, entity, session.Transaction);
                session.Commit();
            }
            catch (System.Exception)
            {
                session.Rollback();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }

        public DictionaryEntity GetDic(int id)
        {
            string sql = @"select * from Dictionary where valid = 'T' and id=@id ";
            return Repository.Query<DictionaryEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public DictionaryEntity GetDicByCode(string code)
        {
            string sql = @"select * from Dictionary where valid = 'T' and code=@code ";
            return Repository.Query<DictionaryEntity>(sql, new { code = code }).FirstOrDefault();
        }

        public List<DictionaryEntity> GetDicByName(string name)
        {
            string sql = @"select * from Dictionary where valid = 'T' and name='%'+ @name + '%' ";
            return Repository.Query<DictionaryEntity>(sql, new { name = name }).ToList();
        }

        public List<DictionaryEntity> GetDicByType(string type)
        {
            string sql = @"select * from Dictionary where valid = 'T' and type=@type";
            return Repository.Query<DictionaryEntity>(sql, new { type = type }).ToList();
        }

        public List<DictionaryEntity> GetSearch(string keyString, string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from Dictionary where valid = 'T'  and code like '%{0}%' ", keyString);
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<DictionaryEntity> list = new List<DictionaryEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                DictionaryEntity entity = new DictionaryEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.Code = Ext.ToString(dr["code"]);
                entity.Name = Ext.ToString(dr["name"]);
                entity.ParentCode = Ext.ToString(dr["parentCode"]);
                entity.Type = Ext.ToString(dr["type"]);
                entity.Value = Ext.ToString(dr["value"]);
                entity.SortValue = Ext.ToInt(dr["sortValue"]);
                entity.Description = Ext.ToString(dr["description"]);
                entity.IsDisplay = Ext.ToString(dr["isDisplay"]);
                entity.CreateTime = Ext.ToDate(dr["createTime"]);
                entity.CreateBy = Ext.ToString(dr["createBy"]);
                entity.UpdateTime = Ext.ToDate(dr["updateTime"]);
                entity.UpdateBy = Ext.ToString(dr["updateBy"]);

                list.Add(entity);
            }

            return list;

        }
    }
}