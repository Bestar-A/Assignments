using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eHealthCare
{
    public partial class eHealthCare : Form
    {
        private readonly string serverName = @"(localdb)\MSSQLLocalDB";
        private readonly string dbName = "Patients";
        private readonly string tableName = "PatientTable";
        public eHealthCare()
        {
            InitializeComponent();
            patientType.SelectedIndex = 0;
            gender.SelectedIndex = 0;
            province.SelectedIndex = 0;
            DataTable dataTable = GetEmptyTableData();
            patientDataTable.DataSource = dataTable;
            CheckAndSetupLocalDB();
            GetDataFromLocalDB(serverName, dbName, tableName);
        }

        private void CheckAndSetupLocalDB()
        {
            if (!CheckIfLocalDBExists(serverName))
            {
                MessageBox.Show("Error while connecting to the Sql Server");
                return;
            }

            if (!CheckIfDatabaseExists(serverName, dbName))
            {
                CreateDatabase(serverName, dbName);
            }

            if(!CheckIfTableExists(serverName, dbName, tableName))
            {
                CreateTable(serverName, dbName, tableName);
            }
        }

        private bool CheckIfLocalDBExists(string serverName)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=" + serverName + ";Integrated Security=True"))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private bool CheckIfDatabaseExists(string serverName, string dbName)
        {
            dbName = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            using (SqlConnection connection = new SqlConnection("Data Source=" + serverName + ";Integrated Security=True"))
            {
                string query = "SELECT COUNT(*) FROM sys.databases WHERE name = @dbName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dbName", dbName);
                    connection.Open();
                    int result = (int)command.ExecuteScalar();
                    connection.Close();
                    return result > 0;
                }
            }
        }

        private void CreateDatabase(string serverName, string dbName)
        {
            string database = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            string createDbCmd = "CREATE DATABASE " + database;

            using (SqlConnection connection = new SqlConnection("Data Source=" + serverName + ";Integrated Security=True"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = createDbCmd;
                command.ExecuteNonQuery();
            }
        }

        private bool CheckIfTableExists(string serverName, string dbName, string tableName)
        {
            string database = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            string connectionString = "Data Source=" + serverName + "; AttachDbFilename=" + database + ";Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            DataTable tableList = connection.GetSchema("Tables");
            foreach (DataRow row in tableList.Rows)
            {
                string table = row["TABLE_NAME"].ToString();
                if (table == tableName) return true;
            }
            return false;
        }

        private void CreateTable(string serverName, string dbName, string tableName)
        {
            string database = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            string connectionString = "Data Source=" + serverName + "; AttachDbFilename=" + database + ";Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string createTableQuery = "CREATE TABLE " + tableName + " (PatientNo NVARCHAR(50) PRIMARY KEY, Fullname NVARCHAR(50) NOT NULL, " + "PatientType NVARCHAR(50) NOT NULL, Gender NVARCHAR(50) NOT NULL, Illness NVARCHAR(50) NOT NULL, " + "PhoneNumber NVARCHAR(50) NOT NULL, Province NVARCHAR(50) NOT NULL, DOB NVARCHAR(50) NOT NULL)" ;
                SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection);
                createTableCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch { MessageBox.Show("Error"); }
        }

        private void InsertData(string serverName, string dbName, string tableName)
        {
            string database = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            string connectionString = "Data Source=" + serverName + ";AttachDbFilename=" + database + ";Integrated Security=True";
            string insertCommand = "INSERT INTO " + tableName + " (PatientNo, Fullname, PatientType, Gender, Illness, PhoneNumber, Province, DOB) " +
                "VALUES (@patientNoVal, @fullnameVal, @patientTypeVal, @genderVal, @illnessVal, @phoneNumberVal, @provinceVal, @DOBVal)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(insertCommand, connection);
                try
                {
                    command.Parameters.AddWithValue("@patientNoVal", patientNo.Text);
                    command.Parameters.AddWithValue("@fullnameVal", patientName.Text);
                    command.Parameters.AddWithValue("@patientTypeVal", patientType.Text);
                    command.Parameters.AddWithValue("@genderVal", gender.Text);
                    command.Parameters.AddWithValue("@illnessVal", illness.Text);
                    command.Parameters.AddWithValue("@phoneNumberVal", phoneNumber.Text);
                    command.Parameters.AddWithValue("@provinceVal", province.Text);
                    command.Parameters.AddWithValue("@DOBVal", birthday.Text);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Data inserted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to insert data.");
                    }
                } catch
                {
                    MessageBox.Show("Can't save data in table. Duplicated patient no or any other stuff");
                }

                connection.Close();
            }
        }

        private void GetDataFromLocalDB(string serverName,string dbName, string tableName)
        {
            string database = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            string connectionString = "Data Source=" + serverName + ";AttachDbFilename=" + database + ";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM " + tableName;
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, tableName);
                patientDataTable.DataSource = dataSet.Tables[tableName];
                connection.Close();
            }
        }

        private void SearchDataFromLocalDB(string serverName, string dbName, string tableName, string searchKey)
        {
            string database = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            string connectionString = "Data Source=" + serverName + ";AttachDbFilename=" + database + ";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM " + tableName + " WHERE PatientNo=@PatientNo";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@PatientNo", searchKey);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, tableName);
                patientDataTable.DataSource = dataSet.Tables[tableName];
                if (dataSet.Tables[tableName].Rows.Count == 0)
                    MessageBox.Show("There is no patient with given Patient No!");
                connection.Close();
            }
        }

        private void DeleteDataFromLocalDB(string serverName, string dbName, string tableName, string searchKey)
        {
            string database = AppDomain.CurrentDomain.BaseDirectory + dbName + ".mdf";
            string connectionString = "Data Source=" + serverName + ";AttachDbFilename=" + database + ";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "DELETE FROM " + tableName + " WHERE PatientNo=@PatientNo";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@PatientNo", searchKey);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, tableName);
                connection.Close();
            }
            GetDataFromLocalDB(serverName, dbName, tableName);
        }

        private DataTable GetEmptyTableData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("PatientNo", typeof(int));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("PatientType", typeof(string));
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("Illness", typeof(string));
            dataTable.Columns.Add("PhoneNumber", typeof(string));
            dataTable.Columns.Add("Province", typeof(string));
            dataTable.Columns.Add("DOB", typeof(string));
            return dataTable;
        }

        private string CheckEmptyField()
        {
            string res = patientNo.Text == "" ? "Patient Number" :
                patientName.Text == "" ? "Patient Name" :
                phoneNumber.Text == "" ? "Phone Number" :
                illness.Text == "" ? "Nature of Illness" :
                birthday.Text == "" ? "Date of Birth" : "";
            return res;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string emptyField = CheckEmptyField();
            if (emptyField != "")
            {
                MessageBox.Show(emptyField + " is empty", "Error Description in input");
                return;
            }
            InsertData(serverName, dbName, tableName);
            patientName.Text = ""; patientNo.Text = "";
            phoneNumber.Text = ""; birthday.Text = "";
            illness.Text = "";
            GetDataFromLocalDB(serverName, dbName, tableName);
        }

        private void DallBtn_Click(object sender, EventArgs e)
        {
            GetDataFromLocalDB(serverName, dbName, tableName);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchKey = searchText.Text;
            SearchDataFromLocalDB(serverName, dbName, tableName, searchKey);
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string searchKey = searchText.Text;
            DialogResult result = MessageBox.Show("Caution: You are about to delete a Patient from table\nPatientNo:" + searchKey,
                "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                DeleteDataFromLocalDB(serverName, dbName, tableName, searchKey);
                searchText.Text = "";
            }
        }
    }
}
