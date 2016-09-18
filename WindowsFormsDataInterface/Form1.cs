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
                UserManager manager = new UserManager();
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

            return true;
        }

        public bool ClassRoomDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            return true;
        }

        public bool ClassDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            return true;
        }

        public bool ClassStudentMapDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            return true;
        }

        public bool ArrangeClassDataSyncHandler(out List<string> errorMsgList, out int totalCount, out int successCount)
        {
            errorMsgList = new List<string>();
            totalCount = 0;
            successCount = 0;

            return true;
        }
    }
}
