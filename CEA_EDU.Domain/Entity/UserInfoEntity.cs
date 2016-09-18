using System;
using System.Collections.Generic;
using System.Text;

namespace CEA_EDU.Domain.Entity
{
    [Table("UserInfo")]
    public class UserInfoEntity
    {
        #region Constructor
        public UserInfoEntity() { }

        public UserInfoEntity(Int32 id, String code, String name, String password, String type, String group, String company, String department, String positionID, String positionName, String sex, DateTime? birthday, String email, String phone, String address, String identityCard, String valid, DateTime createTime, String createBy, DateTime updateTime, String updateBy)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.password = password;
            this.type = type;
            this.group = group;
            this.company = company;
            this.department = department;
            this.positionID = positionID;
            this.positionName = positionName;
            this.sex = sex;
            this.birthday = birthday;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.identityCard = identityCard;
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
        private String password;

        public String Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        private String type;

        public String Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private String group;

        public String Group
        {
            get { return this.group; }
            set { this.group = value; }
        }
        private String company;

        public String Company
        {
            get { return this.company; }
            set { this.company = value; }
        }
        private String department;

        public String Department
        {
            get { return this.department; }
            set { this.department = value; }
        }
        private String positionID;

        public String PositionID
        {
            get { return this.positionID; }
            set { this.positionID = value; }
        }
        private String positionName;

        public String PositionName
        {
            get { return this.positionName; }
            set { this.positionName = value; }
        }
        private String sex;

        public String Sex
        {
            get { return this.sex; }
            set { this.sex = value; }
        }
        private DateTime? birthday;

        public DateTime? Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; }
        }
        private String email;

        public String Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        private String phone;

        public String Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        private String address;

        public String Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
        private String identityCard;

        public String IdentityCard
        {
            get { return this.identityCard; }
            set { this.identityCard = value; }
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
            if (this.Code != null && 20 < this.Code.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Code should not be greater then 20!");
            }
            if (this.Name != null && 50 < this.Name.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Name should not be greater then 50!");
            }
            if (this.Password != null && 50 < this.Password.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Password should not be greater then 50!");
            }
            if (this.Type != null && 20 < this.Type.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Type should not be greater then 20!");
            }
            if (this.Group != null && 20 < this.Group.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Group should not be greater then 20!");
            }
            if (this.Company != null && 50 < this.Company.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Company should not be greater then 50!");
            }
            if (this.Department != null && 50 < this.Department.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Department should not be greater then 50!");
            }
            if (this.PositionID != null && 50 < this.PositionID.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of PositionID should not be greater then 50!");
            }
            if (this.PositionName != null && 50 < this.PositionName.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of PositionName should not be greater then 50!");
            }
            if (this.Sex != null && 2 < this.Sex.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Sex should not be greater then 2!");
            }
            if (this.Email != null && 50 < this.Email.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Email should not be greater then 50!");
            }
            if (this.Phone != null && 50 < this.Phone.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Phone should not be greater then 50!");
            }
            if (this.Address != null && 100 < this.Address.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of Address should not be greater then 100!");
            }
            if (this.IdentityCard != null && 20 < this.IdentityCard.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of IdentityCard should not be greater then 20!");
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
            if (this.CreateTime == null)
            {
                validatorResult = false;
                this.ErrorList.Add("The CreateTime should not be empty!");
            }
            if (this.CreateBy != null && 20 < this.CreateBy.Length)
            {
                validatorResult = false;
                this.ErrorList.Add("The length of CreateBy should not be greater then 20!");
            }
            if (this.UpdateTime == null)
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
