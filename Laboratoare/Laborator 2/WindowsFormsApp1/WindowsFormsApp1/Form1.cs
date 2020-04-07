﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;




namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private SqlDataAdapter da = new SqlDataAdapter();
        private DataSet ds = new DataSet();
        private string childTableName = ConfigurationManager.AppSettings["ChildTableName"];
        private string parentTableName = ConfigurationManager.AppSettings["ParentTableName"];
        private string columnNamesInsertParameters = ConfigurationManager.AppSettings["ColumnNamesInsertParameters"];
        private List<string> columnNames = new List<string>(ConfigurationManager.AppSettings["ChildLabelNames"].Split(','));
        private List<string> paramsNames = new List<string>(ConfigurationManager.AppSettings["columnNamesInsertParameters"].Split(','));
        private List<string> columnInitiate = new List<string>(ConfigurationManager.AppSettings["ChildTextBoxContent"].Split(','));
        private SqlConnection connection;
        private int nr = Convert.ToInt32(ConfigurationManager.AppSettings["ChildNumberOfColumns"]);
        private TextBox[] textBoxes;
        private Label[] labels;


        public Form1()
        {
            InitializeComponent();
            GetConnectionString();
            PopulatePanel();
            dataGridViewParent.SelectionChanged += new EventHandler(LoadChildren);
            dataGridViewChild.SelectionChanged += new EventHandler(LoadInformation);
            LoadParent();
        }


        private void GetConnectionString()
        {
            connection = new SqlConnection("Data Source = DESKTOP-KCS7V0D\\SQLEXPRESS; Initial Catalog = Nail_Salon;" + "Integrated Security = true;");
        }

        private void LoadInformation(object sender, EventArgs e)
        {
            LoadInformation();
        }

        private void LoadInformation()
        {
            for (int i = 0; i < nr; i++)
                textBoxes[i].Text = Convert.ToString(dataGridViewChild.CurrentRow.Cells[i].Value);
        }

        private void PopulatePanel()
        {
            textBoxes = new TextBox[nr];
            labels = new Label[nr];

            for (int i = 0; i < nr; i++)
            {
                textBoxes[i] = new TextBox();
                textBoxes[i].Text = columnInitiate[i];
                labels[i] = new Label();
                labels[i].Text = columnNames[i];
            }

            for (int i = 0; i < nr; i++)
            {
                flowLayoutPanel1.Controls.Add(labels[i]);
                flowLayoutPanel1.Controls.Add(textBoxes[i]);
            }
        }

        private void LoadParent()
        {
            string select = ConfigurationSettings.AppSettings["SelectParent"];
            da.SelectCommand = new SqlCommand(select, connection);
            ds.Clear();
            da.Fill(ds);
            dataGridViewParent.DataSource = ds.Tables[0];
        }

        private void LoadChildren(object sender, EventArgs e)
        {
            LoadChildren();
        }

        private void LoadChildren()
        {
            int parentId = (int)dataGridViewParent.CurrentRow.Cells[0].Value;
            string select = ConfigurationManager.AppSettings["SelectChild"];
            SqlCommand cmd = new SqlCommand(select, connection);
            cmd.Parameters.AddWithValue("@id", parentId);
            SqlDataAdapter daChild = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            daChild.Fill(dataSet);
            dataGridViewChild.DataSource = dataSet.Tables[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(ConfigurationManager.AppSettings["greeting"]);
        }

        private void viewButton_Click(object sender, EventArgs e)
        {
            string select = ConfigurationManager.AppSettings["SelectQuery"];
            SqlCommand cmd = new SqlCommand(select, connection);
            SqlDataAdapter daChild = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            daChild.Fill(dataSet);
            dataGridViewChild.DataSource = dataSet.Tables[0];
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insert into " + childTableName + " ( " + ConfigurationManager.AppSettings["ChildLabelNames"] + " ) values ( " + columnNamesInsertParameters + " )", connection);
                for (int i = 0; i < nr; i++)
                {
                    cmd.Parameters.AddWithValue(paramsNames[i], textBoxes[i].Text);
                }
                SqlDataAdapter daChild = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                connection.Open();
                daChild.Fill(dataSet);
                connection.Close();
                MessageBox.Show("Added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                connection.Close();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("You have to enter: CID, SID, EmID, Date");
                string delete = ConfigurationManager.AppSettings["DeleteChild"];
                SqlCommand cmd = new SqlCommand(delete, connection);
                for (int i = 0; i < nr; i++)
                {
                    cmd.Parameters.AddWithValue(paramsNames[i], textBoxes[i].Text);
                }

                SqlDataAdapter daChild = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                connection.Open();
                cmd.ExecuteNonQuery();
                daChild.Fill(dataSet);
                connection.Close();
                MessageBox.Show("Deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                connection.Close();
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string update = ConfigurationManager.AppSettings["UpdateQuery"];
                SqlCommand cmd = new SqlCommand(update, connection);
                for (int i = 0; i < nr; i++)
                {
                    cmd.Parameters.AddWithValue(paramsNames[i], textBoxes[i].Text);
                }
                SqlDataAdapter daChild = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                connection.Open();
                daChild.Fill(dataSet);
                connection.Close();
                MessageBox.Show("Updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                connection.Close();
            }
        }
    }
}
