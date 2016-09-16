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
    /// 班级学生信息管理类
    /// </summary>
    public class ClassStudentMapManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(ClassStudentMapEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<ClassStudentMapEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(ClassStudentMapEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<ClassStudentMapEntity>(session.Connection, entity, session.Transaction);
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

        public ClassStudentMapEntity GetClassStudentMapByID(int id)
        {
            string sql = @"select * from ClassStudentMap where valid = 'T' and id=@id ";
            return Repository.Query<ClassStudentMapEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public List<ClassStudentMapEntity> GetClassStudentMapByClassID(int classID)
        {
            string sql = @"select * from ClassStudentMap where valid = 'T' and classID=@classID ";
            return Repository.Query<ClassStudentMapEntity>(sql, new { classID = classID }).ToList();
        }

        public List<ClassStudentMapEntity> GetClassStudentMapByStudentID(int studentID)
        {
            string sql = @"select * from ClassStudentMap where valid = 'T' and studentID=@studentID ";
            return Repository.Query<ClassStudentMapEntity>(sql, new { studentID = studentID }).ToList();
        }

        public List<ClassStudentMapEntity> GetSearch(string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from ClassStudentMap where valid = 'T'");
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<ClassStudentMapEntity> list = new List<ClassStudentMapEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                ClassStudentMapEntity entity = new ClassStudentMapEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.ClassID = Ext.ToInt(dr["ClassID"]);
                entity.StudentID = Ext.ToInt(dr["StudentID"]);
                entity.Valid = Ext.ToString(dr["Valid"]);
                entity.CreateTime = Ext.ToDate(dr["CreateTime"]);
                entity.CreateBy = Ext.ToString(dr["CreateBy"]);
                entity.UpdateTime = Ext.ToDate(dr["UpdateTime"]);
                entity.UpdateBy = Ext.ToString(dr["UpdateBy"]);

                list.Add(entity);
            }

            return list;

        }
    }
}