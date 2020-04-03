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
using System.Net;

namespace stock
{
    public partial class Form2 : Form
    {

        public string conn;
        public MySqlConnection connect;
        public string userName = Form1.userName;


        public Form2()
        {
            InitializeComponent();
            label1.Text = collectLocalIp();
            label2.Text = collectComputerName();
            collectComputerName();
            postComputerName(collectComputerName());
            postComputerUsed(collectComputerName());

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

        private void leStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void fermerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout(@userName);
            lastlogout(@userName);
            this.Close();
        }

        private void seDéconnecterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout(@userName);
            lastlogout(@userName);
            Form1 form1 = new Form1();

            form1.Show();
            this.Hide();
        }

        public void lastlogout(string userName)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE users SET lastLogout = NOW() WHERE userName=@userName";
            cmd.Parameters.AddWithValue("@userName", Form1.name);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();

        }
        public void logout(string userName)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE users SET userLog = 0 WHERE userName=@userName";
            cmd.Parameters.AddWithValue("@userName", Form1.name);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public string collectLocalIp()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            Console.WriteLine(hostName);
            // Get the IP  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }

        public string collectComputerName()
        {
            string computerName;
            computerName = Environment.MachineName;
            return computerName;
        }

        public void postComputerName(String computerName)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE users SET lastComputer = @collectComputerName WHERE userName=@userName";
            cmd.Parameters.AddWithValue("@collectComputerName", computerName);
            cmd.Parameters.AddWithValue("@userName", Form1.name);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public void postComputerUsed(String computerName)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE computers SET usedComputer = 1 WHERE userName=@computerName";
            cmd.Parameters.AddWithValue("@computerName", computerName);
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
