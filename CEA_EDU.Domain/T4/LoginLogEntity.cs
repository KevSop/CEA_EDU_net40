using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Serializable]
    [Table("LoginLog")]
    public class LoginLogEntity
    {
        #region Constructor
        public LoginLogEntity() { }

        public LoginLogEntity(Int32 iD,String type,Int32 loginID,String loginType,String name,String action,DateTime timeRecord,String remark,String loginIP,String loginIP2,DateTime createTime,DateTime updateTime)
        {
            this.iD = iD;
            this.type = type;
            this.loginID = loginID;
            this.loginType = loginType;
            this.name = name;
            this.action = action;
            this.timeRecord = timeRecord;
            this.remark = remark;
            this.loginIP = loginIP;
            this.loginIP2 = loginIP2;
            this.createTime = createTime;
            this.updateTime = updateTime;
        }
        #endregion

        #region Attributes
        private Int32 iD;

        public Int32 ID
        {
            get { return this.iD; }
            set { this.iD = value; }
        }
        private String type;

        public String Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private Int32 loginID;

        public Int32 LoginID
        {
            get { return this.loginID; }
            set { this.loginID = value; }
        }
        private String loginType;

        public String LoginType
        {
            get { return this.loginType; }
            set { this.loginType = value; }
        }
        private String name;

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private String action;

        public String Action
        {
            get { return this.action; }
            set { this.action = value; }
        }
        private DateTime? timeRecord;

        public DateTime? TimeRecord
        {
            get { return this.timeRecord; }
            set { this.timeRecord = value; }
        }
        private String remark;

        public String Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }
        private String loginIP;

        public String LoginIP
        {
            get { return this.loginIP; }
            set { this.loginIP = value; }
        }
        private String loginIP2;

        public String LoginIP2
        {
            get { return this.loginIP2; }
            set { this.loginIP2 = value; }
        }
        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return this.createTime; }
            set { this.createTime = value; }
        }
        private DateTime updateTime;

        public DateTime UpdateTime
        {
            get { return this.updateTime; }
            set { this.updateTime = value; }
        }
        #endregion

        #region Validator
        public List<string> ErrorList = new List<string>();
        private bool Validator()
        {    
            bool validatorResult = true;
            if (string.IsNullOrEmpty(this.Type))
            {
                validatorResult = false;
                this.ErrorList.Add("The Type should not be empty!");
            }
            if (this.Type != null && 100 < this.Type.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Type should not be greater then 100!");
            }
            if (string.IsNullOrEmpty(this.LoginType))
            {
                validatorResult = false;
                this.ErrorList.Add("The LoginType should not be empty!");
            }
            if (this.LoginType != null && 20 < this.LoginType.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of LoginType should not be greater then 20!");
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                validatorResult = false;
                this.ErrorList.Add("The Name should not be empty!");
            }
            if (this.Name != null && 255 < this.Name.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Name should not be greater then 255!");
            }
            if (this.Action != null && 100 < this.Action.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Action should not be greater then 100!");
            }
            if (this.Remark != null && 500 < this.Remark.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Remark should not be greater then 500!");
            }
            if (string.IsNullOrEmpty(this.LoginIP))
            {
                validatorResult = false;
                this.ErrorList.Add("The LoginIP should not be empty!");
            }
            if (this.LoginIP != null && 255 < this.LoginIP.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of LoginIP should not be greater then 255!");
            }
            if (string.IsNullOrEmpty(this.LoginIP2))
            {
                validatorResult = false;
                this.ErrorList.Add("The LoginIP2 should not be empty!");
            }
            if (this.LoginIP2 != null && 255 < this.LoginIP2.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of LoginIP2 should not be greater then 255!");
            }
            if (this.CreateTime==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The CreateTime should not be empty!");
            }
            if (this.UpdateTime==null)
            {
                validatorResult = false;
                this.ErrorList.Add("The UpdateTime should not be empty!");
            }
            return validatorResult;
        }    
        #endregion
    }
}
