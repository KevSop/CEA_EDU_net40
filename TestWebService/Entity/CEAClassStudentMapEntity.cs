using System;
using System.Collections.Generic;
using System.Text;

namespace TestWebService
{
    public class CEAClassStudentMapEntity
    {
        #region Constructor
        public CEAClassStudentMapEntity() { }

        public CEAClassStudentMapEntity(Int32 id, String classCode, String studentCode, String valid, DateTime createTime, String createBy, DateTime updateTime, String updateBy)
        {
            this.id = id;
            this.classCode = classCode;
            this.studentCode = studentCode;
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
        private String classCode;

        public String ClassCode
        {
            get { return this.classCode; }
            set { this.classCode = value; }
        }
        private String studentCode;

        public String StudentCode
        {
            get { return this.studentCode; }
            set { this.studentCode = value; }
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
