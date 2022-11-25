using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_Close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Save(object sender, EventArgs e)
        {
            try
            {
                if (SaveFile())
                {
                    ClearAllTextBook();
                    label6.Text = CommonConstant.MSG_SUCCESS;
                }
            }
            catch (Exception)
            {

                label6.Text = CommonConstant.MSG_ERROR;
            }
        }

        private void Btn_Review(object sender, EventArgs e)
        {
            ReviewData();
        }

        private void ReviewData()
        {
            listView1.Items.Add("Name: " + textBox1.Text, 1);
            listView1.Items.Add("Age: " + textBox2.Text, 2);
            listView1.Items.Add("Address: " + textBox3.Text, 3);
        }
        private bool SaveFile()
        {
            var dir = CommonConstant.PATH;
            string fileName = SetFileName();

            if (!Validate(dir, fileName))
            {
                return false;       
            }

            File.WriteAllText(Path.Combine(dir, fileName), FileBody());
          
            return true;
        }
        private void ClearAllTextBook()
        {
            listView1.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            label6.Text = "";
        }

        private string SetFileName()
        {
            string nameText = $"{textBox1.Text}_{textBox2.Text}";
            var regexName = Regex.Replace(nameText, @"\s+", "");

            return regexName + ".txt";

        }
        private bool Validate(string dir, string fileName)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                label6.Text = CommonConstant.MSG_MISSING_INPUT;
                return false;
            }
                
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (File.Exists(fileName))
            {
                label6.Text = CommonConstant.MSG_EXSITS;
                return false;
            }
            return true;

        }

        private string FileBody()
        {
            return $"Name: {textBox1.Text} \nTuoi: {textBox2.Text} \nDiaChi:{textBox3.Text}";
        }
    }
}
