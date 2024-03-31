using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegister
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            this.FormClosing += CloseHandler;
        }

        private bool CheckValidData()
        {
            if (nameField.Text == "")
            {
                MessageBox.Show("Please input name");
                return false;
            }
            else if(lastNameField.Text == "")
            {
                MessageBox.Show("Please input last name");
                return false;
            } else if(fatherNameField.Text == "")
            {
                MessageBox.Show("Please input sponsor name");
                return false;
            } else if(nationField.Text == "")
            {
                MessageBox.Show("Please input nationality");
                return false;
            } else
            {
                string birthday = birthdayField.Text;
                string joinDate = joinDateField.Text;
                if(birthday == "")
                {
                    MessageBox.Show("Please input birthday");
                    return false;
                } else if(joinDate == "")
                {
                    MessageBox.Show("Please input join date");
                    return false;
                }
                else
                {
                    if(!Regex.IsMatch(birthday, @"^(0[1-9]|1[0-2])\.(0[1-9]|[12][0-9]|3[01])\.\d{4}$"))
                    {
                        MessageBox.Show("Please use format MM.DD.YYYY in Birthday field");
                        return false;
                    }
                    else if (!Regex.IsMatch(joinDate, @"^(0[1-9]|1[0-2])\.(0[1-9]|[12][0-9]|3[01])\.\d{4}$"))
                    {
                        MessageBox.Show("Please use format MM.DD.YYYY in Join Date field");
                        return false;
                    }
                }
            }
            return true;
        }

        private void WriteData()
        {
            string textToWrite = "{\n" + nameField.Text + "\n" + lastNameField.Text + "\n" + fatherNameField.Text +
                   "\n" + birthdayField.Text + "\n" + joinDateField.Text + "\n" + nationField.Text + "\n}";
            string path = @"ListOfStudents.txt";

            if (File.Exists(path))
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(textToWrite);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(textToWrite);
                }
            }
            nameField.Text = ""; lastNameField.Text = ""; fatherNameField.Text = "";
            birthdayField.Text = ""; joinDateField.Text = ""; nationField.Text = "";
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CheckValidData())
            {
                WriteData();
                MessageBox.Show("Student Registered Successfully!!");
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm.instance.Show();
        }

        private void CloseHandler(object sender, FormClosingEventArgs e)
        {
            MainForm.instance.Close();
        }
    }
}
