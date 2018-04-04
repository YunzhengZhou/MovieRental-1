﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class EmployeeUC : UserControl
    {
        private static EmployeeUC _instance;
        SqlConnection con = new SqlConnection(Form4.connectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapt;
        DataGridViewCellMouseEventArgs e;
        private string[] movieInfo = new string[10];
        bool fn = false, ln = false, strt = false, stat = false, zip = false, citys = false, tele = false, emai = false, credit = false, plan = false, Null = false;
        public static EmployeeUC Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EmployeeUC();
                return _instance;
            }
        }

        public EmployeeUC()
        {
            InitializeComponent();
            DisplayData();
        }

        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DisplayData()
        {
            dataGridView2.Enabled = true;
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("SELECT * from Customer", con);
            adapt.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
            panel2.Visible = false;
            add.Visible = false;
            save.Visible = true;
            button2.Enabled = false;
            button3.Enabled = false;
            update.Enabled = false;
            button6.Enabled = false;
        }


        private void searchBtn_Click_1(object sender, EventArgs e)
        {
            string searchString = searchTxt.Text.Trim();

            // movie name
            con.Open();
            DataTable dt5 = new DataTable();
            adapt = new SqlDataAdapter("select * from Customer where LastName like @search or FirstName like @search", con);
            adapt.SelectCommand.Parameters.AddWithValue("@search", searchString + "%");
            adapt.Fill(dt5);
            con.Close();
            if (dt5.Rows.Count > 0)
                dataGridView2.DataSource = dt5;
            else
            {
                MessageBox.Show("No user found!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayData();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add.Visible = true;
            save.Visible = false;
            panel2.Visible = true;
            dataGridView2.Enabled = false;
            changeFieldState(true);
            clearTable();
        }

        private void clearTable()
        {
            cidtext.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            Street.Text = "";
            City.Text = "";
            State.Text = "";
            ZipCode.Text = "";
            Telephone.Text = "";
            EmailAddress.Text = "";
            Atype.Text = "";
            AccountCreationDate.Text = "";
            CreditCardNumber.Text = "";
        }
        private void save_Click(object sender, EventArgs e)
        {

        }

        private void edit_Click(object sender, EventArgs e)
        {
            

        }

        private void changeFieldState(bool st)
        {
            FirstName.Enabled = st;
            LastName.Enabled = st;
            Street.Enabled = st;
            City.Enabled = st;
            State.Enabled = st;
            ZipCode.Enabled = st;
            Telephone.Enabled = st;
            EmailAddress.Enabled = st;
            CreditCardNumber.Enabled = st;
        }

        private void edit_Click_1(object sender, EventArgs e)
        {
            changeFieldState(true);
            save.Enabled = true;
        }

        private void save_Click_1(object sender, EventArgs e)
        {
            changeFieldState(false);
            save.Enabled = false;
            panel4.BringToFront();
            dataGridView2.Enabled = true;
            panel2.Visible = false;
            sendSaveQuery();
            MessageBox.Show("Save successfully!");
            save.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            dataGridView2.Enabled = true;
            suggestion();
        }

        private void suggestion()
        {
            //MessageBox.Show("update");
            SqlConnection connection = new SqlConnection(Form4.connectionString);
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT top 5 MovieName, M.MID, rate from(Select AVG(Rating) as rate, MID FROM MovieRating group by MID) as T , Movie M where T.MID = M.MID Order by rate DESC", connection);
            //SqlDataAdapter dataAdapter2 = new SqlDataAdapter("SELECT M.MovieName from Movie M, MovieQueue MQ where M.MID = MQ.MID and connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            
            int i = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                //foreach (DataColumn column in dataTable.Columns)
                //{
                MovieBoxRent movieBoxRent = new MovieBoxRent(row["MID"].ToString());
                movieBoxRent.createNewBox(suggest, i);
                //MessageBox.Show(row["MID"].ToString().Trim());
                movieBoxRent.CreatePicture(row["MID"].ToString().Trim());
                movieBoxRent.CreateName(row["MovieName"].ToString());
                //MessageBox.Show(row["MovieName"].ToString());
                movieBoxRent.CreateScore(row["rate"].ToString());
                i++;
                //}
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            dataGridView2.Enabled = true;
            label10.Text = FirstName.Text.Trim()+ ", " + LastName.Text.Trim();
            label12.Text = Street.Text.Trim() + "Street " + City.Text.Trim() + ", " + State.Text.Trim() + " " + ZipCode.Text.Trim();
        }

        private void sendSaveQuery()
        {
            if (cidtext.Text == "")
            {
                sendAddQuery();
                return;
            }
            DateTimeOffset localDate = DateTimeOffset.Now;
            cmd = new SqlCommand("update Customer set LastName=@lastname, FirstName=@firstname," +
                    "Street=@street, City=@city, State=@state, ZipCode=@zipcode, Telephone=@phone, CreditCardNumber=@creditcard," +
                    "AccountType=@type, EmailAddress=@email where CID=@cid", con);
            con.Open();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@cid", cidtext.Text);
            cmd.Parameters.AddWithValue("@lastname", LastName.Text);
            cmd.Parameters.AddWithValue("@firstname", FirstName.Text);
            cmd.Parameters.AddWithValue("@street", Street.Text);
            cmd.Parameters.AddWithValue("@city", City.Text);
            cmd.Parameters.AddWithValue("@state", State.Text);
            cmd.Parameters.AddWithValue("@zipcode", ZipCode.Text);
            cmd.Parameters.AddWithValue("@phone", Telephone.Text);
            cmd.Parameters.AddWithValue("@creditcard", CreditCardNumber.Text);
            cmd.Parameters.AddWithValue("@type", Atype.Text);
            cmd.Parameters.AddWithValue("@email", EmailAddress.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private bool CheckCustomer()
        {
            SqlConnection connection = new SqlConnection(Form4.connectionString);
            connection.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select CID from Customer C where C.CID =" + UC1.id, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["CID"].ToString() == "")
                {
                    return false;
                }
            }
            //MessageBox.Show(dataTable.Rows.Count.ToString());
            connection.Close();
            return true;
        }
        private bool checkallbool()
        {
            if (fn == false && ln == false && strt == false && stat == false && zip == false && citys == false && tele == false && emai == false && credit == false && plan == false && Null == false)
                return true;
            return false;
        }
        private void sendAddQuery()
        {
            if (checkallbool()) {
                string newMID;
                Random generator = new Random();
                int r = generator.Next(0, 999999);
                using (cmd = new SqlCommand("select MAX(CID)+1 from Customer", con))
                {
                    con.Open();
                    newMID = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
                using (cmd = new SqlCommand("insert into Customer(CID, LastName, FirstName, Street, City, State, ZipCode, Telephone, EmailAddress, AccountNumber, AccountType," +
                    "AccountCreationDate, CreditCardNumber) values(@CID,@FN,@SN,@Street,@City,@State,@ZipCode,@Telephone,@Email,@ANumber,@AType,@ADate,@CCN)", con))
                {

                    con.Open();
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@CID", newMID);
                    cmd.Parameters.AddWithValue("@FN", FirstName.Text);
                    cmd.Parameters.AddWithValue("@SN", LastName.Text);
                    cmd.Parameters.AddWithValue("@Street", Street.Text);
                    cmd.Parameters.AddWithValue("@City", City.Text);
                    cmd.Parameters.AddWithValue("@State", State.Text);
                    cmd.Parameters.AddWithValue("@ZipCode", ZipCode.Text);
                    cmd.Parameters.AddWithValue("@Telephone", Telephone.Text);
                    cmd.Parameters.AddWithValue("@Email", EmailAddress.Text);
                    cmd.Parameters.AddWithValue("@ANumber", r);
                    cmd.Parameters.AddWithValue("@AType", Atype.Text);
                    cmd.Parameters.AddWithValue("@ADate", AccountCreationDate.Text);
                    cmd.Parameters.AddWithValue("@CCN", CreditCardNumber.Text);
                    if (checkBlank())
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DisplayData();
                        add.Visible = false;
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("Please fill in all blanks");
                    }

                }
            }
            else
            {
                MessageBox.Show("Please fix the error before add!");
            }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.e = e;
            if (dataGridView2.Rows[e.RowIndex].Cells.Count >= 10)
            {
                cidtext.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                FirstName.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                LastName.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                Street.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                City.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                State.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                ZipCode.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                Telephone.Text = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                EmailAddress.Text = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();
                Atype.Text = dataGridView2.Rows[e.RowIndex].Cells[10].Value.ToString();
                AccountCreationDate.Text = dataGridView2.Rows[e.RowIndex].Cells[11].Value.ToString();
                CreditCardNumber.Text = dataGridView2.Rows[e.RowIndex].Cells[12].Value.ToString();
            }
            button2.Enabled = true;
            button3.Enabled = true;
            update.Enabled = true;
            button6.Enabled = true;
        }

        private void Allcustomer_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void Street_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(Street, strt, 2);
        }

        private void City_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(City, citys, 0);
        }

        private void State_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(State, stat, 0);
        }

        private void ZipCode_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(ZipCode, zip, 2);
        }

        private void Telephone_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(Telephone, tele, 1);
        }

        private void EmailAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreditCardNumber_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(CreditCardNumber, credit, 1);
        }

        private void reportbutton_Click(object sender, EventArgs e)
        {
            reportPanel.Visible = true;
        }

        private void mostcustomer_Click(object sender, EventArgs e)
        {
            createMostActiveCustomer();
            ReportLabel.Text = "Most Active Customer";
        }

        public void createMostActiveCustomer()
        {
            Reporting.Controls.Clear();
            //list.Clear();
            SqlConnection connection = new SqlConnection(Form4.connectionString);
            connection.Open();
            SqlDataAdapter a = new SqlDataAdapter("SELECT M.MovieName, M.Director, M.MovieType, M.ReleaseDate, M.AddDate, O.MID, count(O.MID) from [Order] O, Movie M where O.MID = M.MID group by M.MovieName, M.Director, M.MovieType, M.ReleaseDate, M.AddDate, O.MID" + 
                            " order by cast(O.MID as int) ASC", connection);
            DataTable t = new DataTable();
            a.Fill(t);
            int i = 1;
            int x = 0;
            foreach (DataRow row in t.Rows)
            {
                foreach (DataColumn column in t.Columns)
                {
                    movieInfo[x] = row[column].ToString();
                    x++;
                }
                x = 0;
                MovieGroupBox newGroupBox = new MovieGroupBox();
                newGroupBox.setGroupBox(Reporting, i);
                i++;
                var releaseDate = movieInfo[3].Substring(0, movieInfo[3].IndexOf(' '));
                var addDate = movieInfo[4].Substring(0, movieInfo[4].IndexOf(' '));

                newGroupBox.setImage(newGroupBox.groupBox, movieInfo[5].ToString().Trim());
                newGroupBox.setMovieInfo(newGroupBox.groupBox, movieInfo[0], movieInfo[1], movieInfo[2], releaseDate, addDate, movieInfo[5].ToString());
                newGroupBox.setLabel(newGroupBox.groupBox, "Number of copies sold: " + movieInfo[6]);
                //newGroupBox.SetChooseMovieButton(newGroupBox.groupBox, "Rent");
                //newGroupBox.DeleteMovieFromListButton(newGroupBox.groupBox, "Delete");

            }
        }
        private void Atype_MouseClick(object sender, MouseEventArgs e)
        {
            Atype.DroppedDown = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.Enabled = true;
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select concat(C.FirstName, C.LastName) as Fullname, M.MovieName, OrderDate from Movie as M, Customer as C, [Order] as O where M.MID = O.MID and C.CID = O.CID", con);
            adapt.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
            panel2.Visible = false;
        }

        private bool checkChooseRow()
        {
            if (e == null)
            {
                return false;
            }
            return dataGridView2.Rows[e.RowIndex].Cells.Count > 0;
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (checkChooseRow())
            {
                panel2.Visible = true;
                dataGridView2.Enabled = false;
                changeFieldState(true);
            }
            else
                MessageBox.Show("Please choose a Customer to Edit.");
        }

        private bool checkBlank()
        {
            if (FirstName.Text != "" && LastName.Text != "" && Street.Text != "" && City.Text != "" && 
                State.Text != "" && ZipCode.Text != "" && Telephone.Text != "" && EmailAddress.Text != "" && CreditCardNumber.Text != "")
            {
                return true;
            }
            return false;
        }
        private void add_Click(object sender, EventArgs e)
        {
            sendAddQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("Delete from Customer Where CID=@CID", con);
            sqlCmd.Parameters.AddWithValue("CID", dataGridView2.CurrentRow.Cells["CID"].Value.ToString());
            sqlCmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Delete User Successfully!");
            DisplayData();
        }

        private void FirstName_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(FirstName, fn, 0);
        }

        private void LastName_TextChanged(object sender, EventArgs e)
        {
            checkTextBox(LastName, ln, 0);
        }



        private void checkTextBox(TextBox box, bool value, int k)
        {
            string text = box.Text;
            value = false;
            string str = "";
            switch (k)
            {
                case 0: //name
                    foreach (char c in text)
                    {
                        if (!char.IsLetter(c))
                        {
                            value = true;
                            break;
                        }
                    }
                    str = "Only letters allowed";
                    break;
                case 1: //number
                    foreach (char letter in text)
                    {
                        if (!char.IsDigit(letter))
                        {
                            value = true;
                            break;
                        }
                    }
                    str = "Only numbers allowed";
                    break;
                case 2: //Zipcode
                    foreach (char c in text)
                    {
                        if (!char.IsDigit(c) && !char.IsLetter(c))
                        {
                            value = true;
                            break;
                        }
                    }
                    str = "Only alphanumeric characters allowed";
                    break;
                case 3: // decimal number
                    int numDecimal = 0;

                    foreach (char c in text)
                    {
                        if (!char.IsDigit(c))
                        {
                            if (c == '.' && numDecimal < 1)
                            {
                                numDecimal++;
                                continue;
                            }


                            value = true;
                            break;
                        }
                    }
                    str = "only one decimal point allowed";
                    break;
                default:
                    break;
            }
            string tmp = box.Text.ToUpper();
            if (tmp == "NULL") {
                str = "The value cannot be NULL";
                value = true;
            }
            if (value)
            {
                errorProvider1.SetError(box, str);
            }
            else
                errorProvider1.Clear();
        }
    }
}
