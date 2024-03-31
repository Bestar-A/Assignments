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
    public partial class ShowData : Form
    {
        private int studentNo;
        private int totalNo;
        private int startLine;
        private int endLine;
        public ShowData()
        {
            InitializeComponent();
            this.FormClosing += CloseHandler;
            studentNo = 1;
            totalNo = GetStudentsCount();
            ShowStudentData(studentNo);
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            MainForm.instance.Show();
            this.Hide();
        }

        private void ShowStudentData(int idx)
        {
            int fieldCnt = 0; int lineNo = 0;
            numberField.Text = idx + "/" + totalNo;
            if (idx == 1) { PreviousBtn.Enabled = false; NextBtn.Enabled = true; }
            else if (idx == totalNo) { NextBtn.Enabled = false; PreviousBtn.Enabled = true; }
            else { PreviousBtn.Enabled = true; NextBtn.Enabled = true; }
            string filePath = @"ListOfStudents.txt";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lineNo++;
                        if (idx == 0)
                        {
                            if (line == "}") { endLine = lineNo; return; }
                            switch(fieldCnt)
                            {
                                case 0:
                                    nameField.Text = line; break;
                                case 1:
                                    lastNameField.Text = line; break;
                                case 2:
                                    fatherNameField.Text = line; break;
                                case 3:
                                    birthdayField.Text = line; break;
                                case 4:
                                    joinDateField.Text = line; break;
                                case 5:
                                    nationField.Text = line; break;
                            }
                            fieldCnt++;
                        }
                        if (line == "{")
                        {
                            idx--;
                            if (idx == 0) startLine = lineNo;
                        }
                    }
                }
            }
            return;
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

        private void CloseHandler(object sender, FormClosingEventArgs e)
        {
            MainForm.instance.Close();
        }

        private void PreviousBtn_Click(object sender, EventArgs e)
        {
            studentNo = studentNo > 1 ? studentNo - 1 : studentNo;
            ShowStudentData(studentNo);
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            studentNo = studentNo < totalNo ? studentNo + 1 : studentNo;
            ShowStudentData(studentNo);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string filePath = @"ListOfStudents.txt"; 

            string tempFilePath = Path.GetTempFileName();

            using (StreamReader reader = new StreamReader(filePath))
            using (StreamWriter writer = new StreamWriter(tempFilePath))
            {
                string line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    if (lineNumber < startLine || lineNumber > endLine)
                    {
                        writer.WriteLine(line);
                    }
                    lineNumber++;
                }
            }

            File.Delete(filePath); 
            File.Move(tempFilePath, filePath); 
            Console.WriteLine("Lines have been successfully deleted from the file.");

            studentNo = studentNo > 1 ? studentNo - 1 : studentNo;
            totalNo--;
            if(totalNo == 0)
            {
                MessageBox.Show("There is no registered students left");
                this.Hide();
                MainForm.instance.Show();
                return;
            }
            ShowStudentData(studentNo);
        }
    }
}
