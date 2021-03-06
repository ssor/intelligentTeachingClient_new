﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using intelligentMiddleWare;
using Nexus.Windows.Forms;

namespace Carbinet
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();

            PieChart1.ItemStyle.SurfaceAlphaTransparency = 0.95F;
            PieChart1.FocusedItemStyle.SurfaceAlphaTransparency = 1F;
            PieChart1.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
            PieChart1.Inclination = 1.047F;
            PieChart1.AutoSizePie = true;
            PieChart1.Thickness = 30;
            PieChart1.Rotation = 0.1396263F;
            PieChart1.ShowEdges = false;

            PieChart1.Items.Add(new PieChartItem(10, Color.BurlyWood, "Tan", "Tan tool tip", 0));
            PieChart1.Items.Add(new PieChartItem(10, Color.Gold, "Gold", "Gold tool tip", 0));
            PieChart1.Items.Add(new PieChartItem(20, Color.Chocolate, "Brown", "Brown tool tip", 30));
            PieChart1.Items.Add(new PieChartItem(10, Color.DarkRed, "Red", "Red tool tip", 0));


        }
        #region 实时互动
        void test_实时互动1()
        {
            //接收到一个标签数据
            MiddleWareCore.set_mode(MiddleWareMode.实时互动);
            string cmd1 = "[select,master_node,equip000001,stu000001,01,A]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd1));
            IntelligentEvent evt1 = MiddleWareCore.get_a_event();
            if (evt1.epcID == "stu000001"
                && evt1.localDeviceID == "master_node"
                && evt1.remoteDeviceID == "equip000001"
                && evt1.questionID == "01"
                && evt1.questionValue == "A"
                && evt1.event_unit_list[0] == IntelligentEventUnit.new_epc)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
            //找到该学生的信息，包括学生姓名、位置、选择
            //查找学生信息
            Person person = MemoryTable.getPersonByEpc(evt1.epcID);
            if (person.id_num == "CE4D9397871")
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        void test_实时互动2()
        {
            //接收到同一个标签在另一个设备上
            MiddleWareCore.set_mode(MiddleWareMode.实时互动);
            string cmd2_1 = "[select,master_node,subnode1,rfid01,01,A]";
            string cmd2_2 = "[select,master_node,subnode2,rfid01,01,A]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd2_2));
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd2_1));
            IntelligentEvent evt2 = MiddleWareCore.get_a_event();
            if (evt2.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        void test_实时互动3()
        {
            //标签重复
            MiddleWareCore.set_mode(MiddleWareMode.实时互动);
            string cmd3_1 = "[select,master_node,subnode2,rfid01,01,A]";
            string cmd3_2 = "[select,master_node,subnode2,rfid01,01,A]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd3_1));
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd3_2));
            IntelligentEvent evt3 = MiddleWareCore.get_a_event();
            if (evt3.event_unit_list.IndexOf(IntelligentEventUnit.repeat_epc) >= 0)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        void test_实时互动4()
        {
            //标签重复
            MiddleWareCore.set_mode(MiddleWareMode.实时互动);
            string cmd3_1 = "[select,master_node,subnode2,rfid01,01,A]";
            string cmd3_2 = "[select,master_node,subnode2,rfid01,01,B]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd3_1));
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd3_2));
            IntelligentEvent evt3 = MiddleWareCore.get_a_event();
            if (evt3.event_unit_list.IndexOf(IntelligentEventUnit.change_answer) >= 0)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        void test_实时互动5()
        {
            //接收到一个标签数据
            MiddleWareCore.set_mode(MiddleWareMode.实时互动);
            string cmd1 = "[select,master_node,subnode1,,01,A]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd1));
            IntelligentEvent evt1 = MiddleWareCore.get_a_event();
            if (evt1 == null || evt1.name == IntelligentEvent.event_empty)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            MemoryTable.initializeTabes();
            this.test_实时互动1();
            this.test_实时互动2();
            this.test_实时互动3();
            this.test_实时互动4();
            this.test_实时互动5();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.test_考勤1();
            this.test_考勤2();
            this.test_考勤3();
        }
        #region
        private void test_考勤1()
        {
            MiddleWareCore.set_mode(MiddleWareMode.考勤);
            string cmd1 = "[select,master_node,subnode1,rfid01,01,A]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd1));
            IntelligentEvent evt1 = MiddleWareCore.get_a_event();
            if (evt1.epcID == "rfid01"
                && evt1.localDeviceID == "master_node"
                && evt1.remoteDeviceID == "subnode1"
                && evt1.questionID == "01"
                && evt1.questionValue == "A"
                && evt1.event_unit_list[0] == IntelligentEventUnit.new_epc)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        void test_考勤2()
        {
            //接收到同一个标签在另一个设备上
            MiddleWareCore.set_mode(MiddleWareMode.考勤);
            string cmd2_1 = "[select,master_node,subnode1,rfid01,01,A]";
            string cmd2_2 = "[select,master_node,subnode2,rfid01,01,A]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd2_2));
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd2_1));
            IntelligentEvent evt2 = MiddleWareCore.get_a_event();
            if (evt2.event_unit_list.IndexOf(IntelligentEventUnit.epc_on_another_device) >= 0)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        void test_考勤3()
        {
            //标签重复
            MiddleWareCore.set_mode(MiddleWareMode.考勤);
            string cmd3_1 = "[select,master_node,subnode2,rfid01,01,A]";
            string cmd3_2 = "[select,master_node,subnode2,rfid01,01,A]";
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd3_1));
            MiddleWareCore.Set_new_data(ProtocolHelper.getProtocolHelper(cmd3_2));
            IntelligentEvent evt3 = MiddleWareCore.get_a_event();
            if (evt3.event_unit_list.IndexOf(IntelligentEventUnit.repeat_epc) >= 0)
            {
                Debug.WriteLine("True");
            }
            else
            {
                Debug.WriteLine("False");
            }
        }
        #endregion
    }
}
