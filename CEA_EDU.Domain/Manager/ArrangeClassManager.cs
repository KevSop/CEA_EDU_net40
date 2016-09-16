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
    /// 班级排课管理类
    /// </summary>
    public class ArrangeClassManager : ManagerBase
    {
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Insert(ArrangeClassEntity entity)
        {
            entity.Valid = "T";
            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Insert<ArrangeClassEntity>(session.Connection, entity, session.Transaction);
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

        public void Update(ArrangeClassEntity entity)
        {
            entity.UpdateTime = DateTime.Now;

            IDbSession session = SessionFactory.CreateSession();
            try
            {
                session.BeginTrans();
                Repository.Update<ArrangeClassEntity>(session.Connection, entity, session.Transaction);
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

        public ArrangeClassEntity GetArrangeClassByID(int id)
        {
            string sql = @"select * from ArrangeClass where valid = 'T' and id=@id ";
            return Repository.Query<ArrangeClassEntity>(sql, new { id = id }).FirstOrDefault();
        }

        public List<ArrangeClassEntity> GetArrangeClassByCurriculumID(int curriculumID)
        {
            string sql = @"select * from ArrangeClass where valid = 'T' and curriculumID=@curriculumID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { curriculumID = curriculumID }).ToList();
        }

        public List<ArrangeClassEntity> GetArrangeClassByClassID(int classID)
        {
            string sql = @"select * from ArrangeClass where valid = 'T' and classID=@classID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { classID = classID }).ToList();
        }

        public List<ArrangeClassEntity> GetArrangeClassByClassRoomID(int classRoomID)
        {
            string sql = @"select * from ArrangeClass where valid = 'T' and classRoomID=@classRoomID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { classRoomID = classRoomID }).ToList();
        }

        public List<ArrangeClassEntity> GetArrangeClassByTeacherID(int teacherID)
        {
            string sql = @"select * from ArrangeClass where valid = 'T' and teacherID=@teacherID ";
            return Repository.Query<ArrangeClassEntity>(sql, new { teacherID = teacherID }).ToList();
        }

        public List<ArrangeClassEntity> GetSearch(string sort, string order, int offset, int pageSize, out int total)
        {
            int pageCount = 0;
            string querySql = string.Format("select * from ArrangeClass where valid = 'T'");
            DataTable dt = SplitPage.SqlSplitPage(querySql, string.Format("order by {0} {1}", sort, order), null, offset / pageSize, pageSize, out pageCount, out total);

            List<ArrangeClassEntity> list = new List<ArrangeClassEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                ArrangeClassEntity entity = new ArrangeClassEntity();

                entity.ID = Ext.ToInt(dr["ID"]);
                entity.CurriculumID = Ext.ToInt(dr["CurriculumID"]);
                entity.ClassID = Ext.ToInt(dr["ClassID"]);
                entity.ClassRoomID = Ext.ToInt(dr["ClassRoomID"]);
                entity.TeacherID = Ext.ToInt(dr["TeacherID"]);
                entity.StartTime = Ext.ToDateOrNull(dr["StartTime"]);
                entity.EndTime = Ext.ToDateOrNull(dr["EndTime"]);
                entity.Remark = Ext.ToString(dr["Remark"]);
                entity.BespeakCount = Ext.ToIntOrNull(dr["BespeakCount"]);
                entity.AttendCount = Ext.ToIntOrNull(dr["AttendCount"]);
                entity.PassedCount = Ext.ToIntOrNull(dr["PassedCount"]);
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