using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            Service1 client = new Service1();

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
    }
}
