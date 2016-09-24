using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CEA_EDU.Domain.Manager;
using CEA_EDU.Domain.Entity;

namespace WindowsFormsDataInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("用户");
            comboBox1.Items.Add("课程");
            comboBox1.Items.Add("教室");
            comboBox1.Items.Add("班级");
            comboBox1.Items.Add("班级学生");
            comboBox1.Items.Add("排课");

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SOAP 请求响应方式
            //string errormsg = "";
            //if (WSHelper.CreateWebServiceDll(out errormsg))
            //{
            //    dataGridView1.DataSource = WSHelper.GetResponse(EMethod.GetClassList);
            //}

            //WSHelper.CreateWebServiceCS(out errormsg)
            CEAWebService client = new CEAWebService();

           // WindowsFormsDataInterface.ServiceReference1.CEAServiceSoapClient client = new WindowsFormsDataInterface.ServiceReference1.CEAServiceSoapClient();

            switch (comboBox1.SelectedItem.ToString())
            {
                case "用户":
                    dataGridView1.DataSource = client.GetUserList();
                    break;
                case "课程":
                    dataGridView1.DataSource = client.GetCurriculumList();
                    break;
                case "教室":
                    dataGridView1.DataSource = client.GetClassRoomList();
                    break;
                case "班级":
                    dataGridView1.DataSource = client.GetClassList();
                    break;
                case "班级学生":
                    dataGridView1.DataSource = client.GetClassStudentMapList();
                    break;
                case "排课":
                    dataGridView1.DataSource = client.GetArrangeClassList();
                    break;

            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> errorMsgList = new List<string>();
            int totalCount = 0;
            int successCount = 0;

            switch (comboBox1.SelectedItem.ToString())
            {
                case "用户":
                    UserDataSyncHandler(out errorMsgList, out totalCount, out successCount);
                    break;
                case "课程":
                    CurriculumDataSyncHandler(out errorMsgList, out totalCount, out successCount);
                    break;
                case "教室":
                    ClassRoomDataSyncHandler(out errorMsgList, out totalCount, out successCount);
                    break;
                case "班级":
                    ClassDataSyncHandler(out errorMsgList, out totalCount, out successCount);
                    break;
                case "班级学生":
                    ClassStudentMapDataSyncHandler(out errorMsgList, out totalCount, out successCount);
                    break;
                case "排课":
                    ArrangeClassDataSyncHandler(out errorMsgList, out totalCount, out successCount);
                    break;

            }
            if (errorMsgList != null && errorMsgList.Count > 0)
            {
                StringBuilder sbError = new StringBuilder();
                errorMsgList.ForEach(r => sbError.AppendLine(r));
                MessageBox.Show(string.Format("同步失败 总数{0}条 失败{1}条,错误信息:\r\n", totalCount, successCount) + sbError.ToString());
            }
            else
            {
                MessageBox.Show("同步完成");
            }
        }

        public bool UserDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            try
            {
                UserInfoManager manager = new UserInfoManager();
                CEAWebService client = new CEAWebService();

                CEAUserInfoEntity[] ceaUsers = client.GetUserList();

                if (ceaUsers != null && ceaUsers.Length > 0)
                {
                    totalCount = ceaUsers.Length;

                    foreach (var ceaUser in ceaUsers)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(ceaUser.Code))
                            {
                                throw new Exception("账号不能为空");
                            }

                            if (string.IsNullOrWhiteSpace(ceaUser.Name) || string.IsNullOrWhiteSpace(ceaUser.Type))
                            {
                                throw new Exception(string.Format("账号：{0} 用户名称，用户类型不能为空", ceaUser.Code));
                            }

                            UserInfoEntity entity = manager.GetUserByCode(ceaUser.Code);
                            if (entity != null)
                            {
                                entity.Code = ceaUser.Code;
                                entity.Name = ceaUser.Name;
                                entity.Type = ceaUser.Type;
                                entity.Group = ceaUser.Group;
                                entity.Company = ceaUser.Company;
                                entity.PositionID = ceaUser.PositionID;
                                entity.PositionName = ceaUser.PositionName;
                                entity.Sex = ceaUser.Sex;
                                entity.Birthday = ceaUser.Birthday;
                                entity.Email = ceaUser.Email;
                                entity.Phone = ceaUser.Phone;
                                entity.Address = ceaUser.Address;
                                entity.IdentityCard = ceaUser.IdentityCard;
                                entity.Valid = ceaUser.Valid;

                                manager.Update(entity);
                            }
                            else
                            {
                                entity = new UserInfoEntity();

                                entity.Code = ceaUser.Code;
                                entity.Name = ceaUser.Name;
                                entity.Password = "111";
                                entity.Type = ceaUser.Type;
                                entity.Group = ceaUser.Group;
                                entity.Company = ceaUser.Company;
                                entity.PositionID = ceaUser.PositionID;
                                entity.PositionName = ceaUser.PositionName;
                                entity.Sex = ceaUser.Sex;
                                entity.Birthday = ceaUser.Birthday;
                                entity.Email = ceaUser.Email;
                                entity.Phone = ceaUser.Phone;
                                entity.Address = ceaUser.Address;
                                entity.IdentityCard = ceaUser.IdentityCard;
                                entity.Valid = ceaUser.Valid;

                                manager.Insert(entity);

                            }

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            errorMsgList.Add(ex.Message);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMsgList.Add(ex.Message);
                return false;
            }

        }

        public bool CurriculumDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            try
            {
                CurriculumInfoManager manager = new CurriculumInfoManager();
                CEAWebService client = new CEAWebService();

                CEACurriculumInfoEntity[] ceaEntityList = client.GetCurriculumList();

                if (ceaEntityList != null && ceaEntityList.Length > 0)
                {
                    totalCount = ceaEntityList.Length;

                    foreach (var ceaEntity in ceaEntityList)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(ceaEntity.Code))
                            {
                                throw new Exception("课程编号不能为空");
                            }

                            if (string.IsNullOrWhiteSpace(ceaEntity.Name))
                            {
                                throw new Exception(string.Format("课程编号：{0} 课程名称不能为空", ceaEntity.Code));
                            }

                            CurriculumInfoEntity entity = manager.GetCurriculumInfoByCode(ceaEntity.Code);
                            if (entity == null)
                            {
                                entity = new CurriculumInfoEntity();
                            }

                            entity.Code = ceaEntity.Code;
                            entity.Name = ceaEntity.Name;
                            entity.Type = ceaEntity.Type;
                            entity.Score = ceaEntity.Score;
                            entity.StartTime = ceaEntity.StartTime;
                            entity.EndTime = ceaEntity.EndTime;
                            entity.Remark = ceaEntity.Remark;
                            entity.Valid = ceaEntity.Valid;

                            if (entity.CurriculumID > 0)
                            {
                                manager.Update(entity);
                            }
                            else
                            {
                                manager.Insert(entity);
                            }

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            errorMsgList.Add(ex.Message);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMsgList.Add(ex.Message);
                return false;
            }
        }

        public bool ClassRoomDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            try
            {
                ClassRoomInfoManager manager = new ClassRoomInfoManager();
                CEAWebService client = new CEAWebService();

                CEAClassRoomInfoEntity[] ceaEntityList = client.GetClassRoomList();

                if (ceaEntityList != null && ceaEntityList.Length > 0)
                {
                    totalCount = ceaEntityList.Length;

                    foreach (var ceaEntity in ceaEntityList)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(ceaEntity.Code))
                            {
                                throw new Exception("教室编号不能为空");
                            }

                            if (string.IsNullOrWhiteSpace(ceaEntity.Name))
                            {
                                throw new Exception(string.Format("教室编号：{0} 教室名称不能为空", ceaEntity.Code));
                            }

                            ClassRoomInfoEntity entity = manager.GetClassRoomInfoByCode(ceaEntity.Code);
                            if (entity == null)
                            {
                                entity = new ClassRoomInfoEntity();
                            }

                            entity.Code = ceaEntity.Code;
                            entity.Name = ceaEntity.Name;
                            entity.Type = ceaEntity.Type;
                            entity.Address = ceaEntity.Address;
                            entity.SeatNum = ceaEntity.SeatNum;
                            entity.Remark = ceaEntity.Remark;
                            entity.Valid = ceaEntity.Valid;

                            if (entity.ClassRoomID > 0)
                            {
                                manager.Update(entity);
                            }
                            else
                            {
                                manager.Insert(entity);
                            }

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            errorMsgList.Add(ex.Message);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMsgList.Add(ex.Message);
                return false;
            }
        }

        public bool ClassDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            try
            {
                ClassInfoManager manager = new ClassInfoManager();
                CEAWebService client = new CEAWebService();

                CEAClassInfoEntity[] ceaEntityList = client.GetClassList();

                if (ceaEntityList != null && ceaEntityList.Length > 0)
                {
                    totalCount = ceaEntityList.Length;

                    foreach (var ceaEntity in ceaEntityList)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(ceaEntity.Code))
                            {
                                throw new Exception("班级编号不能为空");
                            }

                            if (string.IsNullOrWhiteSpace(ceaEntity.Name))
                            {
                                throw new Exception(string.Format("班级编号：{0} 班级名称不能为空", ceaEntity.Code));
                            }

                            ClassInfoEntity entity = manager.GetClassInfoByCode(ceaEntity.Code);
                            if (entity == null)
                            {
                                entity = new ClassInfoEntity();
                            }

                            UserInfoEntity user = new UserInfoManager().GetUserByCode(ceaEntity.TeacherCode);

                            entity.Code = ceaEntity.Code;
                            entity.Name = ceaEntity.Name;
                            entity.Type = ceaEntity.Type;
                            entity.StartTime = ceaEntity.StartTime;
                            entity.EndTime = ceaEntity.EndTime;
                            entity.TeacherID = user != null ? user.ID : 0;
                            entity.Company = ceaEntity.Company;
                            entity.Department = ceaEntity.Department;
                            entity.Remark = ceaEntity.Remark;
                            entity.Valid = ceaEntity.Valid;

                            if (entity.ClassID > 0)
                            {
                                manager.Update(entity);
                            }
                            else
                            {
                                manager.Insert(entity);
                            }

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            errorMsgList.Add(ex.Message);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMsgList.Add(ex.Message);
                return false;
            }
        }

        public bool ClassStudentMapDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            try
            {
                ClassStudentMapManager manager = new ClassStudentMapManager();
                CEAWebService client = new CEAWebService();

                CEAClassStudentMapEntity[] ceaEntityList = client.GetClassStudentMapList();

                if (ceaEntityList != null && ceaEntityList.Length > 0)
                {
                    totalCount = ceaEntityList.Length;

                    foreach (var ceaEntity in ceaEntityList)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(ceaEntity.ClassCode))
                            {
                                throw new Exception("班级编号不能为空");
                            }

                            if (string.IsNullOrWhiteSpace(ceaEntity.StudentCode))
                            {
                                throw new Exception(string.Format("班级编号：{0} 学员编号不能为空", ceaEntity.StudentCode));
                            }

                            UserInfoEntity userEntity = new UserInfoManager().GetUserByCode(ceaEntity.StudentCode);
                            ClassInfoEntity classEntity = new ClassInfoManager().GetClassInfoByCode(ceaEntity.ClassCode);

                            if (classEntity == null)
                            {
                                throw new Exception("班级不存在");
                            }


                            if(userEntity == null)
                            {
                                throw new Exception("学员不存在");
                            }

                            ClassStudentMapEntity entity = manager.GetClassStudentMapByKeys(classEntity.ClassID, userEntity.ID);
                            if (entity == null)
                            {
                                entity = new ClassStudentMapEntity();
                            }

                            entity.ClassID = classEntity.ClassID;
                            entity.StudentID = userEntity.ID;
                            entity.Valid = ceaEntity.Valid;

                            if (entity.ID > 0)
                            {
                                manager.Update(entity);
                            }
                            else
                            {
                                manager.Insert(entity);
                            }

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            errorMsgList.Add(ex.Message);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMsgList.Add(ex.Message);
                return false;
            }
        }

        public bool ArrangeClassDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            try
            {
                ArrangeClassManager manager = new ArrangeClassManager();
                CEAWebService client = new CEAWebService();

                CEAArrangeClassEntity[] ceaEntityList = client.GetArrangeClassList();

                if (ceaEntityList != null && ceaEntityList.Length > 0)
                {
                    totalCount = ceaEntityList.Length;

                    foreach (var ceaEntity in ceaEntityList)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(ceaEntity.CurriculumCode))
                            {
                                throw new Exception("课程编号不能为空");
                            }

                            if (string.IsNullOrWhiteSpace(ceaEntity.ClassCode))
                            {
                                throw new Exception(string.Format("班级编号不能为空"));
                            }

                            if (string.IsNullOrWhiteSpace(ceaEntity.ClassRoomCode))
                            {
                                throw new Exception(string.Format("教师编号不能为空"));
                            }

                            if (string.IsNullOrWhiteSpace(ceaEntity.TeacherCode))
                            {
                                throw new Exception(string.Format("教师编号不能为空"));
                            }

                            CurriculumInfoEntity curriculumEntity = new CurriculumInfoManager().GetCurriculumInfoByCode(ceaEntity.CurriculumCode);
                            ClassInfoEntity classEntity = new ClassInfoManager().GetClassInfoByCode(ceaEntity.ClassCode);
                            ClassRoomInfoEntity classRoomEntity = new ClassRoomInfoManager().GetClassRoomInfoByCode(ceaEntity.ClassRoomCode);
                            UserInfoEntity userEntity = new UserInfoManager().GetUserByCode(ceaEntity.TeacherCode);

                            if (curriculumEntity == null)
                            {
                                throw new Exception("课程不存在");
                            }

                            if (classEntity == null)
                            {
                                throw new Exception("班级不存在");
                            }

                            if (classRoomEntity == null)
                            {
                                throw new Exception("教室不存在");
                            }

                            if (userEntity == null)
                            {
                                throw new Exception("教师不存在");
                            }


                            ArrangeClassEntity entity = manager.GetArrangeClassByKeys(curriculumEntity.CurriculumID, classEntity.ClassID, classRoomEntity.ClassRoomID, userEntity.ID);
                            if (entity == null)
                            {
                                entity = new ArrangeClassEntity();
                            }

                            entity.CurriculumID = curriculumEntity.CurriculumID;
                            entity.ClassID = classEntity.ClassID;
                            entity.ClassRoomID = classRoomEntity.ClassRoomID;
                            entity.TeacherID = userEntity.ID;
                            entity.StartTime = ceaEntity.StartTime;
                            entity.EndTime = ceaEntity.EndTime;
                            entity.BespeakCount = ceaEntity.BespeakCount;
                            entity.AttendCount = ceaEntity.AttendCount;
                            entity.PassedCount = ceaEntity.PassedCount;
                            entity.Remark = ceaEntity.Remark;
                            entity.Valid = ceaEntity.Valid;

                            if (entity.ClassRoomID > 0)
                            {
                                manager.Update(entity);
                            }
                            else
                            {
                                manager.Insert(entity);
                            }

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            errorMsgList.Add(ex.Message);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMsgList.Add(ex.Message);
                return false;
            }
        }
    }
}
