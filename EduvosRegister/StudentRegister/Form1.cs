using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegister
{
    public partial class MainForm : Form
    {
        public static MainForm instance;
        public MainForm()
        {
            InitializeComponent();
            instance = this;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            Register register = new Register(); 
            register.Show();
            this.Hide();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int GetStudentsCount()
        {
            int cnt = 0;
            string filePath = @"ListOfStudents.txt";
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        string fileContent = streamReader.ReadToEnd();
                        foreach (char c in fileContent)
                        {
                            if (c == '{')
                            {
                                cnt++;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file does not exist.");
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the file: " + e.Message);
            }
            return cnt;
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            int studentNo = GetStudentsCount();
            if(studentNo == 0)
            {
                MessageBox.Show("There is no registered student\nPlease add student first");
                return;
            }
            ShowData showData = new ShowData();
            showData.Show();
            this.Hide();
        }
    }
}
