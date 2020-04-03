using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace stock
{
    public partial class Form1 : Form
    {
        
        public string conn;
        public static string userName;
        public static string name;
        public MySqlConnection connect;
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;

            this.FormBorderStyle = FormBorderStyle.None;

            this.WindowState = FormWindowState.Maximized;
            
        }

        public void db_connection()
        {
            try
            {
                conn = "SERVER=109.234.162.90;PORT=3306;DATABASE=beky4614_pos;UID=beky4614_kylian;PWD=Belgacom002@";
                connect = new MySqlConnection(conn);
                connect.Open();
            }
            catch (MySqlException e)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {
            string userName = username.Text;
            string userPassword = password.Text;
 

            if (userName == "" || userPassword == "")
            {
                MessageBox.Show("Veuillez entrer votre identifiant et votre mot de passe");
                return;
            }
            bool r = validate_login(userName, userPassword);
            if (r)
            {

                login(@userName);
                lastLogin(@userName);
                name = username.Text;
                MessageBox.Show("OK");
                Form2 form2 = new Form2();

                form2.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Identifiants incorrects ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


        }

        public void login(string userName)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE users SET userLog = 1 WHERE userName=@userName";
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public void lastLogin(string userName)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE users SET userLastLogin = NOW() WHERE userName=@userName";
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();

        }
        public bool validate_login(string userName, string userPassword)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from users where userName=@userName and userPassword=@userPassword";
            cmd.Parameters.AddWithValue("@userName", userName);
            cmd.Parameters.AddWithValue("@userPassword", userPassword);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {

                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}   

