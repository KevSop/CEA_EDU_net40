using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TestWebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CEAService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<CEAUserInfoEntity> GetUserList()
        {
            List<CEAUserInfoEntity> list = new List<CEAUserInfoEntity>();

            list.Add(new CEAUserInfoEntity() {
                Code = "7001",
                Name = "",   //教师7001_接口
                Type = "普通用户",
                Group = "Teacher",
                Company = "Company",
                PositionID = "1",
                PositionName = "教师",
                Valid = "T"
            });

            list.Add(new CEAUserInfoEntity()
            {
                Code = "7002",
                Name = "教师7002_接口",
                Type = "",   //普通用户
                Group = "Teacher",
                Company = "Company",
                PositionID = "1",
                PositionName = "教师",
                Valid = "T"
            });


            list.Add(new CEAUserInfoEntity()
            {
                Code = "8001",
                Name = "学生8001_接口",
                Type = "普通用户",
                Group = "Student",
                Company = "Company",
                PositionID = "1",
                PositionName = "学生",
                Valid = "T"
            });

            list.Add(new CEAUserInfoEntity()
            {
                Code = "8002",
                Name = "学生8002_接口",
                Type = "普通用户",
                Group = "Student",
                Company = "Company",
                PositionID = "1",
                PositionName = "学生",
                Valid = "T"
            });

            return list;
        }

        [WebMethod]
        public List<CEACurriculumInfoEntity> GetCurriculumList()
        {
            List<CEACurriculumInfoEntity> list = new List<CEACurriculumInfoEntity>();

            list.Add(new CEACurriculumInfoEntity()
            {
                Code = "101",
                Name = "课程101_接口",
                Type = "技能课程",
                Score = 10,
                Remark = "技能培训A",
                Valid = "T"
            });

            list.Add(new CEACurriculumInfoEntity()
            {
                Code = "102",
                Name = "课程102_接口",
                Type = "技能课程",
                Score = 10,
                Remark = "技能培训B",
                Valid = "T"
            });

            list.Add(new CEACurriculumInfoEntity()
            {
                Code = "103",
                Name = "课程103_接口",
                Type = "技能课程",
                Score = 10,
                Remark = "技能培训C",
                Valid = "T"
            });


            return list;
        }

        [WebMethod]
        public List<CEAClassInfoEntity> GetClassList()
        {
            List<CEAClassInfoEntity> list = new List<CEAClassInfoEntity>();

            list.Add(new CEAClassInfoEntity()
            {
                Code = "101",
                Name = "班级101_接口",
                Type = "空乘2016A",
                StartTime = DateTime.Parse("2016-05-01"),
                EndTime = DateTime.Parse("2016-08-01"),
                TeacherCode = "7001",
                Company = "Company",
                Department = "空乘部",
                Remark = "技能培训C",
                Valid = "T"
            });

            list.Add(new CEAClassInfoEntity()
            {
                Code = "102",
                Name = "班级102_接口",
                Type = "空乘2016A",
                StartTime = DateTime.Parse("2016-05-01"),
                EndTime = DateTime.Parse("2016-08-01"),
                TeacherCode = "7002",
                Company = "Company",
                Department = "空乘部",
                Remark = "技能培训C",
                Valid = "T"
            });

            list.Add(new CEAClassInfoEntity()
            {
                Code = "102",
                Name = "班级102_接口",
                Type = "空乘2016A",
                StartTime = DateTime.Parse("2016-05-01"),
                EndTime = DateTime.Parse("2016-08-01"),
                TeacherCode = "7002",
                Company = "Company",
                Department = "空乘部",
                Remark = "技能培训C",
                Valid = "T"
            });


            return list;
        }

        [WebMethod]
        public List<CEAClassRoomInfoEntity> GetClassRoomList()
        {
            List<CEAClassRoomInfoEntity> list = new List<CEAClassRoomInfoEntity>();

            list.Add(new CEAClassRoomInfoEntity()
            {
                Code = "101",
                Name = "A区101_接口",
                Type = "A区",
                Address = "园区A区101",
                SeatNum = 30,
                Remark = "教室101",
                Valid = "T"
            });

            list.Add(new CEAClassRoomInfoEntity()
            {
                Code = "102",
                Name = "A区102_接口",
                Type = "A区",
                Address = "园区A区102",
                SeatNum = 20,
                Remark = "教室102",
                Valid = "T"
            });

            list.Add(new CEAClassRoomInfoEntity()
            {
                Code = "203",
                Name = "B区203_接口",
                Type = "B区",
                Address = "园区B区203",
                SeatNum = 30,
                Remark = "教室203",
                Valid = "T"
            });

            return list;
        }

        [WebMethod]
        public List<CEAClassStudentMapEntity> GetClassStudentMapList()
        {
            List<CEAClassStudentMapEntity> list = new List<CEAClassStudentMapEntity>();

            list.Add(new CEAClassStudentMapEntity()
            {
                ClassCode = "8001",
                StudentCode = "102",
                Valid = "T"
            });

            list.Add(new CEAClassStudentMapEntity()
            {
                ClassCode = "8002",
                StudentCode = "102",
                Valid = "T"
            });

            return list;
        }

        [WebMethod]
        public List<CEAArrangeClassEntity> GetArrangeClassList()
        {
            List<CEAArrangeClassEntity> list = new List<CEAArrangeClassEntity>();

            list.Add(new CEAArrangeClassEntity()
            {
                CurriculumCode = "101",
                ClassCode = "8001",
                ClassRoomCode = "101",
                TeacherCode = "7001",
                StartTime = DateTime.Parse("2016-9-10 10:00:00"),
                EndTime = DateTime.Parse("2016-9-10 11:30:00"),
                Valid = "T"
            });

            list.Add(new CEAArrangeClassEntity()
            {
                CurriculumCode = "102",
                ClassCode = "8001",
                ClassRoomCode = "203",
                TeacherCode = "7001",
                StartTime = DateTime.Parse("2016-9-11 09:00:00"),
                EndTime = DateTime.Parse("2016-9-11 10:00:00"),
                Valid = "T"
            });

            list.Add(new CEAArrangeClassEntity()
            {
                CurriculumCode = "103",
                ClassCode = "8001",
                ClassRoomCode = "203",
                TeacherCode = "7002",
                StartTime = DateTime.Parse("2016-9-11 10:00:00"),
                EndTime = DateTime.Parse("2016-9-11 11:00:00"),
                Valid = "T"
            });

            return list;
        }

    }
}