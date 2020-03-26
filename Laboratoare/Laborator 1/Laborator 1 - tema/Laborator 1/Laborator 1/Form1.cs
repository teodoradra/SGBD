using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Laborator_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SqlConnection dbConn;
            SqlDataAdapter daCustomers, daOrders;
            DataSet ds;
            SqlCommandBuilder cb;
            BindingSource bsCustomers, bsOrders;
            dbConn = new SqlConnection(@"Data Source = SQLSRVEXPRESS; Initial Catalog =Northwind; Integrated Security = true");
            ds = new DataSet();
            daCustomers = new SqlDataAdapter("SELECT * FROM Customers", dbConn);
            daOrders = new SqlDataAdapter("SELECT * FROM Orders", dbConn);
            cb = new SqlCommandBuilder(daOrders);
            daCustomers.Fill(ds, "Customers");
            daOrders.Fill(ds, "Orders");
            DataRelation dr = new DataRelation("FK_Orders_Customers",
            ds.Tables["Customers"].Columns["CustomerID"],
            ds.Tables["Orders"].Columns["CustomerID"]);
            ds.Relations.Add(dr);
            //data binding
            bsCustomers = new BindingSource();
            bsCustomers.DataSource = ds;
            bsCustomers.DataMember = "Customers";
            bsOrders = new BindingSource();
            bsOrders.DataSource = bsCustomers;
            bsOrders.DataMember = "FK_Orders_Customers";
            dgvCustomers.DataSource = bsCustomers;
            dgvOrders.DataSource = bsOrders;
            daOrders.Update(ds, "Orders");
        }
    }
}
