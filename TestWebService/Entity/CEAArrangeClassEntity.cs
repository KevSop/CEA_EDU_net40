using System;
using System.Collections.Generic;
using System.Text;

namespace TestWebService
{
    public class CEAArrangeClassEntity
    {
        #region Constructor
        public CEAArrangeClassEntity() { }

        public CEAArrangeClassEntity(Int32 id, String curriculumCode, String classCode, String classRoomCode, String teacherCode, DateTime? startTime, DateTime? endTime, String remark, Int32? bespeakCount, Int32? attendCount, Int32? passedCount, String valid, DateTime createTime, String createBy, DateTime updateTime, String updateBy)
        {
            this.id = id;
            this.curriculumCode = curriculumCode;
            this.classCode = classCode;
            this.classRoomCode = classRoomCode;
            this.teacherCode = teacherCode;
            this.startTime = startTime;
            this.endTime = endTime;
            this.remark = remark;
            this.bespeakCount = bespeakCount;
            this.attendCount = attendCount;
            this.passedCount = passedCount;
            this.valid = valid;
            this.createTime = createTime;
            this.createBy = createBy;
            this.updateTime = updateTime;
            this.updateBy = updateBy;
        }
        #endregion

        #region Attributes
        private Int32 id;

        public Int32 ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private String curriculumCode;

        public String CurriculumCode
        {
            get { return this.curriculumCode; }
            set { this.curriculumCode = value; }
        }
        private String classCode;

        public String ClassCode
        {
            get { return this.classCode; }
            set { this.classCode = value; }
        }
        private String classRoomCode;

        public String ClassRoomCode
        {
            get { return this.classRoomCode; }
            set { this.classRoomCode = value; }
        }
        private String teacherCode;

        public String TeacherCode
        {
            get { return this.teacherCode; }
            set { this.teacherCode = value; }
        }
        private DateTime? startTime;

        public DateTime? StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }
        private DateTime? endTime;

        public DateTime? EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }
        private String remark;

        public String Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        private Int32? bespeakCount;

        public Int32? BespeakCount
        {
            get { return this.bespeakCount; }
            set { this.bespeakCount = value; }
        }
        private Int32? attendCount;

        public Int32? AttendCount
        {
            get { return this.attendCount; }
            set { this.attendCount = value; }
        }
        private Int32? passedCount;

        public Int32? PassedCount
        {
            get { return this.passedCount; }
            set { this.passedCount = value; }
        }
        private String valid;

        public String Valid
        {
            get { return this.valid; }
            set { this.valid = value; }
        }
        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }
        private String createBy;

        public String CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }
        private DateTime updateTime;

        public DateTime UpdateTime
        {
            get { return this.updateTime; }
            set { this.updateTime = value; }
        }
        private String updateBy;

        public String UpdateBy
        {
            get { return this.updateBy; }
            set { this.updateBy = value; }
        }
        #endregion

        #region Validator
        public List<string> ErrorList = new List<string>();
        private bool Validator()
        {    
            bool validatorResult = true;
            if (this.Remark != null && 500 < this.Remark.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Remark should not be greater then 500!");
            }
            if (string.IsNullOrEmpty(this.Valid))
            {
                validatorResult = false;
                this.ErrorList.Add("The Valid should not be empty!");
            }
            if (this.Valid != null && 1 < this.Valid.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Valid should not be greater then 1!");
            }
            if (this.CreateTime==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The CreateTime should not be empty!");
            }
            if (this.CreateBy != null && 20 < this.CreateBy.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of CreateBy should not be greater then 20!");
            }
            if (this.UpdateTime==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The UpdateTime should not be empty!");
            }
            if (this.UpdateBy != null && 20 < this.UpdateBy.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of UpdateBy should not be greater then 20!");
            }
            return validatorResult;
        }    
        #endregion
    }
}
