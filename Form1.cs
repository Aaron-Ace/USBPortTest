using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;


namespace USBPortTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //添加拉鈕
            textBox1.ScrollBars = ScrollBars.Vertical;

            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            IniFile ini = new IniFile(str + "\\USBTest.ini");
            if (ini.Read("USB1") == "1") { checkBox1.Checked = true; } else { checkBox1.Checked = false; }
            if (ini.Read("USB2") == "1") { checkBox2.Checked = true; } else { checkBox2.Checked = false; }
            if (ini.Read("USB3") == "1") { checkBox3.Checked = true; } else { checkBox3.Checked = false; }
            if (ini.Read("USB4") == "1") { checkBox5.Checked = true; } else { checkBox5.Checked = false; }
            if (ini.Read("USB5") == "1") { checkBox4.Checked = true; } else { checkBox4.Checked = false; }
            if (ini.Read("USB6") == "1") { checkBox6.Checked = true; } else { checkBox6.Checked = false; }
            if (ini.Read("USB7") == "1") { checkBox7.Checked = true; } else { checkBox7.Checked = false; }
            if (ini.Read("USB8") == "1") { checkBox8.Checked = true; } else { checkBox8.Checked = false; }
            if (ini.Read("USB9") == "1") { checkBox9.Checked = true; } else { checkBox9.Checked = false; }
            if (ini.Read("USB10") == "1") { checkBox10.Checked = true; } else { checkBox10.Checked = false; }
            if (ini.Read("USB11") == "1") { checkBox11.Checked = true; } else { checkBox11.Checked = false; }
            if (ini.Read("USB12") == "1") { checkBox12.Checked = true; } else { checkBox12.Checked = false; }
            if (ini.Read("AUTO") == "1")
            {
                checkBox13.Checked = true;
                textBox1.AppendText("   [Auto Test]\r\n");
                button1_Click(sender, e);
            }

        }

        //SCAN Botton
        private void button2_Click(object sender, EventArgs e)
        {
            //清理全部，返回預設值
            CleanAll();

            //添加磁碟名
            DiskName();

            //計算有多少個USB Devices 
            textBox1.AppendText("Total Number of USB Key Devices: " + CountUSB.count + "\r\n");

            //如果count為0 則無USB Devices 
            if (CountUSB.count == 0)
            { textBox1.AppendText("None Devices Found" + "\r\n"); }
        }

        //Test Botton
        public void button1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("   [Test Start]");

            //PictureBox 返回預設
            CleanPicBox();

            CleanTextBox14to25();

            //添加磁碟名
            DiskName();
            CreateUSBFile_textBox_added();

            //logtestflag 返回預設值
            log_test.log_flag = 0;

            //Test whether checkBox check or not
            int[] CheckBoxBool = new int[13];
            if (checkBox1.Checked) { CheckBoxBool[1] = 1; } else { CheckBoxBool[1] = 0; }
            if (checkBox2.Checked) { CheckBoxBool[2] = 1; } else { CheckBoxBool[2] = 0; }
            if (checkBox3.Checked) { CheckBoxBool[3] = 1; } else { CheckBoxBool[3] = 0; }
            if (checkBox4.Checked) { CheckBoxBool[4] = 1; } else { CheckBoxBool[4] = 0; }
            if (checkBox5.Checked) { CheckBoxBool[5] = 1; } else { CheckBoxBool[5] = 0; }
            if (checkBox6.Checked) { CheckBoxBool[6] = 1; } else { CheckBoxBool[6] = 0; }
            if (checkBox7.Checked) { CheckBoxBool[7] = 1; } else { CheckBoxBool[7] = 0; }
            if (checkBox8.Checked) { CheckBoxBool[8] = 1; } else { CheckBoxBool[8] = 0; }
            if (checkBox9.Checked) { CheckBoxBool[9] = 1; } else { CheckBoxBool[9] = 0; }
            if (checkBox10.Checked) { CheckBoxBool[10] = 1; } else { CheckBoxBool[10] = 0; }
            if (checkBox11.Checked) { CheckBoxBool[11] = 1; } else { CheckBoxBool[11] = 0; }
            if (checkBox12.Checked) { CheckBoxBool[12] = 1; } else { CheckBoxBool[12] = 0; }

            //測試Error Code 是否返回0(正常)
            for (int i = 1; i <= 12; i++)
            {
                //如果CheckBox有被勾選 
                if (CheckBoxBool[i] == 1)
                {
                    //Test 預設值為錯誤
                    bool test = false;
                    string str;
                    switch (i)
                    {

                        case 1: str = textBox14.Text.ToString(); if (str == "0") { test = true; } break;
                        case 2: str = textBox15.Text.ToString(); if (str == "0") { test = true; } break;
                        case 3: str = textBox16.Text.ToString(); if (str == "0") { test = true; } break;
                        case 4: str = textBox17.Text.ToString(); if (str == "0") { test = true; } break;
                        case 5: str = textBox18.Text.ToString(); if (str == "0") { test = true; } break;
                        case 6: str = textBox19.Text.ToString(); if (str == "0") { test = true; } break;
                        case 7: str = textBox20.Text.ToString(); if (str == "0") { test = true; } break;
                        case 8: str = textBox21.Text.ToString(); if (str == "0") { test = true; } break;
                        case 9: str = textBox22.Text.ToString(); if (str == "0") { test = true; } break;
                        case 10: str = textBox23.Text.ToString(); if (str == "0") { test = true; } break;
                        case 11: str = textBox24.Text.ToString(); if (str == "0") { test = true; } break;
                        case 12: str = textBox25.Text.ToString(); if (str == "0") { test = true; } break;

                    }

                    //Test為是 更改圖片為成功
                    if (test == true)
                    {
                        switch (i)
                        {
                            case 1: pictureBox1.Image = Properties.Resources.bmp00006; break;
                            case 2: pictureBox2.Image = Properties.Resources.bmp00006; break;
                            case 3: pictureBox3.Image = Properties.Resources.bmp00006; break;
                            case 4: pictureBox4.Image = Properties.Resources.bmp00006; break;
                            case 5: pictureBox5.Image = Properties.Resources.bmp00006; break;
                            case 6: pictureBox6.Image = Properties.Resources.bmp00006; break;
                            case 7: pictureBox7.Image = Properties.Resources.bmp00006; break;
                            case 8: pictureBox8.Image = Properties.Resources.bmp00006; break;
                            case 9: pictureBox9.Image = Properties.Resources.bmp00006; break;
                            case 10: pictureBox10.Image = Properties.Resources.bmp00006; break;
                            case 11: pictureBox11.Image = Properties.Resources.bmp00006; break;
                            case 12: pictureBox12.Image = Properties.Resources.bmp00006; break;

                        }
                    }
                    //如為否 則更改圖片為失敗
                    else
                    {
                        switch (i)
                        {
                            case 1: pictureBox1.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 2: pictureBox2.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 3: pictureBox3.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 4: pictureBox4.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 5: pictureBox5.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 6: pictureBox6.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 7: pictureBox7.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 8: pictureBox8.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 9: pictureBox9.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 10: pictureBox10.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 11: pictureBox11.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;
                            case 12: pictureBox12.Image = Properties.Resources.bmp00005; log_test.log_flag = 1; break;

                        }
                    }
                }
            }

            //測試是否有CheckBox被勾選
            int flag_testcheck = 0;

            //add Time
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            textBox1.AppendText("\r\n      Time :" + date + "\r\n");


            //add USB PORT Each result
            if (checkBox1.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox14.Text == "0") { textBox1.AppendText("       USB Port #01:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #01:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox2.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox15.Text == "0") { textBox1.AppendText("       USB Port #02:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #02:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox3.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox16.Text == "0") { textBox1.AppendText("       USB Port #03:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #03:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox4.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox17.Text == "0") { textBox1.AppendText("       USB Port #04:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #04:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox5.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox18.Text == "0") { textBox1.AppendText("       USB Port #05:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #05:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox6.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox19.Text == "0") { textBox1.AppendText("       USB Port #06:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #06:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox7.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox20.Text == "0") { textBox1.AppendText("       USB Port #07:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #07:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox8.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox21.Text == "0") { textBox1.AppendText("       USB Port #08:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #08:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox9.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox22.Text == "0") { textBox1.AppendText("       USB Port #09:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #09:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox10.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox23.Text == "0") { textBox1.AppendText("       USB Port #10:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #10:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox11.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox24.Text == "0") { textBox1.AppendText("       USB Port #11:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #11:FAIL\r\n"); }
                flag_testcheck = 1;
            }
            if (checkBox12.Checked == true)
            {
                textBox1.AppendText("       Copy File Test\r\n");
                if (textBox25.Text == "0") { textBox1.AppendText("       USB Port #12:PASS\r\n"); }
                else { textBox1.AppendText("       USB Port #12:FAIL\r\n"); }
                flag_testcheck = 1;
            }

            //add USB Port Total Result
            if (flag_testcheck == 0 && log_test.log_flag == 0)
            {
                MessageBox.Show("Warning ! Please select an USB Port !");
            }
            else if (log_test.log_flag == 0)
            {
                textBox1.AppendText("       Test  Result ----------------> PASS\r\n"); 
            }
            else
            {
                textBox1.AppendText("       Test  Result ----------------> FAIL\r\n");
            }

            //製作結果檔
            CreateLogfile();

            //自動程式關閉
            string str1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            IniFile ini = new IniFile(str1 + "\\USBTest.ini");
            if (log_test.log_flag == 0 && checkBox13.Checked == true)
            { timer1.Start(); timer1_Tick(sender, e); }

            textBox1.AppendText("   [Test End]\r\n\r\n");
        }

        //Save Button
        private void button3_Click(object sender, EventArgs e)
        {
            string str1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            IniFile ini = new IniFile(str1 + "\\USBTest.ini");
            if (checkBox1.Checked == true) { ini.Write("USB1", "1"); } else { ini.Write("USB1", "0"); }
            if (checkBox2.Checked == true) { ini.Write("USB2", "1"); } else { ini.Write("USB2", "0"); }
            if (checkBox3.Checked == true) { ini.Write("USB3", "1"); } else { ini.Write("USB3", "0"); }
            if (checkBox5.Checked == true) { ini.Write("USB4", "1"); } else { ini.Write("USB4", "0"); }
            if (checkBox4.Checked == true) { ini.Write("USB5", "1"); } else { ini.Write("USB5", "0"); }
            if (checkBox6.Checked == true) { ini.Write("USB6", "1"); } else { ini.Write("USB6", "0"); }
            if (checkBox7.Checked == true) { ini.Write("USB7", "1"); } else { ini.Write("USB7", "0"); }
            if (checkBox8.Checked == true) { ini.Write("USB8", "1"); } else { ini.Write("USB8", "0"); }
            if (checkBox9.Checked == true) { ini.Write("USB9", "1"); } else { ini.Write("USB9", "0"); }
            if (checkBox10.Checked == true) { ini.Write("USB10", "1"); } else { ini.Write("USB10", "0"); }
            if (checkBox11.Checked == true) { ini.Write("USB11", "1"); } else { ini.Write("USB11", "0"); }
            if (checkBox12.Checked == true) { ini.Write("USB12", "1"); } else { ini.Write("USB12", "0"); }
            if (checkBox13.Checked == true) { ini.Write("AUTO", "1"); } else { ini.Write("AUTO", "0"); }
            textBox1.AppendText("   Saved test configuration\r\n");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (log_test.log_flag == 0) { Environment.ExitCode = 0; }
            else { Environment.ExitCode = 1; }
        }

        public void CreateUSBFile_textBox_added()
        {
            int count = 0;

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType.ToString() == "Removable")
                {
                    count += 1;
                    switch (count)
                    {
                        case 1: textBox14.Text = CreateUSBFileTest(d.Name); break;
                        case 2: textBox15.Text = CreateUSBFileTest(d.Name); break;
                        case 3: textBox16.Text = CreateUSBFileTest(d.Name); break;
                        case 4: textBox17.Text = CreateUSBFileTest(d.Name); break;
                        case 5: textBox18.Text = CreateUSBFileTest(d.Name); break;
                        case 6: textBox19.Text = CreateUSBFileTest(d.Name); break;
                        case 7: textBox20.Text = CreateUSBFileTest(d.Name); break;
                        case 8: textBox21.Text = CreateUSBFileTest(d.Name); break;
                        case 9: textBox22.Text = CreateUSBFileTest(d.Name); break;
                        case 10: textBox23.Text = CreateUSBFileTest(d.Name); break;
                        case 11: textBox24.Text = CreateUSBFileTest(d.Name); break;
                        case 12: textBox25.Text = CreateUSBFileTest(d.Name); break;

                    }
                }
            }
        }

        public void DiskName()
        {
            int count_1 = 0;

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType.ToString() == "Removable")
                {
                    count_1 += 1;
                    CountUSB.count += 1;
                    switch (count_1)
                    {
                        case 1: textBox2.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 2: textBox3.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 3: textBox4.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 4: textBox5.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 5: textBox6.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 6: textBox7.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 7: textBox8.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 8: textBox9.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 9: textBox10.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 10: textBox11.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 11: textBox12.Text = (d.Name + "USB Mass Storage Device"); break;
                        case 12: textBox13.Text = (d.Name + "USB Mass Storage Device"); break;

                    }
                }
            }
        }

        public class CountUSB
        {
            public static int count = 0;
        }

        public void CleanAll()
        {
            textBox1.AppendText("------------Scan USB Key--------------\r\n");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            CleanTextBox14to25();
            CleanPicBox();
            CountUSB.count = 0;

        }

        public void CleanCheckBox()
        {
            checkBox1.Checked = false;
            checkBox1.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox5.Checked = false;
            checkBox4.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
        }

        public void CleanPicBox()
        {
            pictureBox1.Image = Properties.Resources.bmp00002;
            pictureBox2.Image = Properties.Resources.bmp00002;
            pictureBox3.Image = Properties.Resources.bmp00002;
            pictureBox4.Image = Properties.Resources.bmp00002;
            pictureBox5.Image = Properties.Resources.bmp00002;
            pictureBox6.Image = Properties.Resources.bmp00002;
            pictureBox7.Image = Properties.Resources.bmp00002;
            pictureBox8.Image = Properties.Resources.bmp00002;
            pictureBox9.Image = Properties.Resources.bmp00002;
            pictureBox10.Image = Properties.Resources.bmp00002;
            pictureBox11.Image = Properties.Resources.bmp00002;
            pictureBox12.Image = Properties.Resources.bmp00002;
        }

        public void CleanTextBox14to25()
        {
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox19.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox22.Clear();
            textBox23.Clear();
            textBox24.Clear();
            textBox25.Clear();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            //Auto Run

            if (checkBox13.Checked) //設置開機自啟動  
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("JcShutdown", path);
                rk2.Close();
                rk.Close();

                //寫入ini檔為true 
                string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                IniFile ini = new IniFile(str + "\\USBTest.ini");
                ini.Write("AUTO", "1");
                if (checkBox1.Checked == true) { ini.Write("USB1", "1"); } else { ini.Write("USB1", "0"); }
                if (checkBox2.Checked == true) { ini.Write("USB2", "1"); } else { ini.Write("USB2", "0"); }
                if (checkBox3.Checked == true) { ini.Write("USB3", "1"); } else { ini.Write("USB3", "0"); }
                if (checkBox5.Checked == true) { ini.Write("USB4", "1"); } else { ini.Write("USB4", "0"); }
                if (checkBox4.Checked == true) { ini.Write("USB5", "1"); } else { ini.Write("USB5", "0"); }
                if (checkBox6.Checked == true) { ini.Write("USB6", "1"); } else { ini.Write("USB6", "0"); }
                if (checkBox7.Checked == true) { ini.Write("USB7", "1"); } else { ini.Write("USB7", "0"); }
                if (checkBox8.Checked == true) { ini.Write("USB8", "1"); } else { ini.Write("USB8", "0"); }
                if (checkBox9.Checked == true) { ini.Write("USB9", "1"); } else { ini.Write("USB9", "0"); }
                if (checkBox10.Checked == true) { ini.Write("USB10", "1"); } else { ini.Write("USB10", "0"); }
                if (checkBox11.Checked == true) { ini.Write("USB11", "1"); } else { ini.Write("USB11", "0"); }
                if (checkBox12.Checked == true) { ini.Write("USB12", "1"); } else { ini.Write("USB12", "0"); }


            }
            else //取消開機自啟動  
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("JcShutdown", false);
                rk2.Close();
                rk.Close();

                //寫入ini檔為false
                string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                IniFile ini = new IniFile(str + "\\USBTest.ini");
                ini.Write("AUTO", "0");

                timer1.Stop();
            }

        }

        public class IniFile   // revision 11
        {
            string Path;
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile(string IniPath = null)
            {
                Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
            }

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }

        public void CreateLogfile()
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strTimeYear = string.Format("{0:D4}", currentTime.Year);
            string strTimeMonth = string.Format("{0:D2}", currentTime.Month);
            string strTimeDay = string.Format("{0:D2}", currentTime.Day);
            string strTimeHour = string.Format("{0:D2}", currentTime.Hour);
            string strTimeMinute = string.Format("{0:D2}", currentTime.Minute);
            string strTimeSecond = string.Format("{0:D2}", currentTime.Second);

            string logfilename = "USB_Result_" + strTimeYear + strTimeMonth + strTimeDay + ".log";
            if (false == System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\" + logfilename))
            {
                try
                {
                    StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + logfilename, true);
                    writer.Write("USB Port Test : \r\n");
                    writer.Write("-------------------------------------- \r\n");
                    writer.Write("Time\t\t\t     Result\r\n");
                    writer.Close();

                }
                catch
                {
                    MessageBox.Show("Create File.log Error");
                }
            }

            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (log_test.log_flag == 0)
            {
                File.AppendAllText(logfilename, "\r\n" + date + "--------->PASS\r\n");
            }
            else
            {
                File.AppendAllText(logfilename, "\r\n" + date + "--------->FAIL\r\n");
            }

        }
        public class log_test
        {
            public static int log_flag = 0;
        }
        public void CloseWindow()
        {

            Application.Exit();
        }

        int timeLeft = 1;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox13.Checked == false)
            {
                timeLeft = 1; 
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;

            }
            else
            {
                CloseWindow();
            }
        }
        public static string CreateUSBFileTest(string path)
        {

            try
            {
                // Create a file to write to.
                System.IO.StreamWriter File = new System.IO.StreamWriter(path + "USBTest.txt");
                File.WriteLine("USBTest");
                File.Close();

                FileInfo fInfo = new FileInfo(path + "USBTest.txt");
                string length = "0";
                length = fInfo.Length.ToString();
                fInfo.Delete();

                if (length == "9") { return "0"; }
                else { return "1"; }
            }
            catch
            {
                return "1";
            }

        }



    }
}
