using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using NLog.Internal;
using System.Collections.Generic;

namespace Practical_Work_1
{

    // In Windows form app creaza o procedura fillData(Data grid view ca si control- recomanda)
    // Sql connection
    //o metoda care da connection string
    // apoi create new sqlDataAdaptrs -- (unul pt fiecare tabel
    // create data set (unul pt toate tabelele)
    //populate the data set (dataele din cele 2 tabele (tabela parinte tabel fiu - din lab 1) si relations between the 2 tables)
    // un alt obiect de tip realtion pe care il punem in data set(se face in sapt 3) *(conetarerea tabelelor prin relatiile existente -- cheie straina)
    //afisam datele + add,delete,update

    public partial class Form1 : Form
    {

        SqlConnection dbConn;
        SqlDataAdapter daClient, daAppointment;
        DataSet ds;
        SqlCommandBuilder cb;
        BindingSource bsClient, bsAppointment;
        DataRelation dr;

        public Form1()
        {
            InitializeComponent();
            connectToDataBase();
            ds = new DataSet();
            fillData();
            createDataRelation();
            dataBiding();


        }


        public void dataBiding()
        {
            bsClient = new BindingSource();
            bsClient.DataSource = ds;
            bsClient.DataMember = "Client";
            bsAppointment = new BindingSource();
            bsAppointment.DataSource = bsClient;
            bsAppointment.DataMember = "FK__Appointment__CID__6D0D32F4";

            dgvClient.DataSource = bsClient;
            dgvAppointment.DataSource = bsAppointment;
        }

        public void createDataRelation() {
            dr = new DataRelation("FK__Appointment__CID__6D0D32F4",
                ds.Tables["Client"].Columns["CID"],
                ds.Tables["Appointment"].Columns["CID"]);
            ds.Relations.Add(dr);
        }

        public void fillData() {
            daClient = new SqlDataAdapter("Select * from Client", dbConn);

/*            daClient.InsertCommand = new SqlCommand("insert into Client(CID, FirstName, LastName, Email, PhoneNumber)" +
                " values (@CID, @FirstName, @LastName, @Email, @PhoneNumber)", dbConn);
            daClient.UpdateCommand = new SqlCommand("update client" +
                                "set FirstName =@FirstName, LastName = @LastName, Email = @Email, PhoneNo = @PhoneNo" +
                                "where CID = @CID ", dbConn);
            daClient.DeleteCommand = new SqlCommand("Delete from Client" +
                                    " where CID = @CID", dbConn);
            daClient.InsertCommand.Parameters.Add("@CID", SqlDbType.Int, 5, "CID");*/

            daAppointment = new SqlDataAdapter("SELECT * from Appointment", dbConn);

/*            daAppointment.InsertCommand = new SqlCommand("insert into Appointment (CID,SID,EmID,TyID,AtID,Duration,Price,Date) " +
                "values (@CID,@SID,@EmID,@TyID,@AtID,@Duration,@Price,@Date)", dbConn);
            daAppointment.DeleteCommand = new SqlCommand("DELETE FROM Appointment WHERE CID = @CID, SID = @SID, Date = @Date", dbConn);
            daAppointment.UpdateCommand = new SqlCommand("Update Appointment " +
                "set Duration = @Duration, Price = @Price, Date = @Date" +
                "where CID = @CID, SID = @SID, EmID = @EmID, TyID = @TyID , AtID = @AtID", dbConn);*/
            cb = new SqlCommandBuilder(daAppointment);
            daClient.Fill(ds, "Client");
            daAppointment.Fill(ds, "Appointment");
            cb.GetUpdateCommand();

        }

        public void connectToDataBase() { 
            dbConn = new SqlConnection("Data Source = DESKTOP-KCS7V0D\\SQLEXPRESS; Initial Catalog = Nail_Salon;" + "Integrated Security = true;");
        }

        private void button1_Click_1(object sender, EventArgs e)
        { //update
            try
            {
                daAppointment.Update(ds, "Appointment");
                e.ToString();
            }
            catch (InvalidOperationException d) {
                d.ToString();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                daAppointment.Update(ds, "Appointment");
            }
            catch (InvalidOperationException d )
            {
                d.ToString();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                daAppointment.Update(ds, "Appointment");
            }
            catch (InvalidOperationException d)
            {
                d.ToString();
            }


        }
    }
}
