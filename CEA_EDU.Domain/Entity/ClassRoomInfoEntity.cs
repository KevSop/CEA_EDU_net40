using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Table("ClassRoomInfo")]
    public class ClassRoomInfoEntity
    {
        #region Constructor
        public ClassRoomInfoEntity() { }

        public ClassRoomInfoEntity(Int32 classRoomID,String code,String name,String address,String type,Int32? seatNum,String remark,String valid,DateTime createTime,String createBy,DateTime updateTime,String updateBy)
        {
            this.classRoomID = classRoomID;
            this.code = code;
            this.name = name;
            this.address = address;
            this.type = type;
            this.seatNum = seatNum;
            this.remark = remark;
            this.valid = valid;
            this.createTime = createTime;
            this.createBy = createBy;
            this.updateTime = updateTime;
            this.updateBy = updateBy;
        }
        #endregion

        #region Attributes
        private Int32 classRoomID;

        public Int32 ClassRoomID
        {
            get { return this.classRoomID; }
            set { this.classRoomID = value; }
        }
        private String code;

        public String Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        private String name;

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private String address;

        public String Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
        private String type;

        public String Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private Int32? seatNum;

        public Int32? SeatNum
        {
            get { return this.seatNum; }
            set { this.seatNum = value; }
        }
        private String remark;

        public String Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
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
            if (string.IsNullOrEmpty(this.Code))
            {
                validatorResult = false;
                this.ErrorList.Add("The Code should not be empty!");
            }
            if (this.Code != null && 50 < this.Code.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Code should not be greater then 50!");
            }
            if (string.IsNullOrEmpty(this.Name))
            {
                validatorResult = false;
                this.ErrorList.Add("The Name should not be empty!");
            }
            if (this.Name != null && 100 < this.Name.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Name should not be greater then 100!");
            }
            if (this.Address != null && 100 < this.Address.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Address should not be greater then 100!");
            }
            if (this.Type != null && 20 < this.Type.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Type should not be greater then 20!");
            }
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
