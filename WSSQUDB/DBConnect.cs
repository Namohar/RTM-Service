using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace WSSQUDB
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        private MySqlConnection cmpConnection;
        private string cmpServer;
        private string cmpDatabase;
        private string cmpUid;
        private string cmpPassword;

        DataTable dt = new DataTable();
        public DBConnect()
        {
            InitializeSKU();
            InitializeCMP();
        }

        private void InitializeSKU()
        {
            server = "10.0.4.115";
            database = "skudb";
            uid = "lokesha.b";
            password = "f8kf20qpf8750xfjfqf3";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Convert Zero Datetime=True";

            connection = new MySqlConnection(connectionString);
        }

        private void InitializeCMP()
        {
            cmpServer = "10.0.4.115";
            cmpDatabase = "cmpdb";
            cmpUid = "lokesha.b";
            cmpPassword = "f8kf20qpf8750xfjfqf3";
            string connectionString;
            connectionString = "SERVER=" + cmpServer + ";" + "DATABASE=" +
            cmpDatabase + ";" + "UID=" + cmpUid + ";" + "PASSWORD=" + cmpPassword + ";Convert Zero Datetime=True";

            cmpConnection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();                
                return true;
            }
            catch (MySqlException ex)
            {
               
                //switch (ex.Number)
                //{
                //    case 0:
                //        MessageBox.Show("Cannot connect to server.  Contact administrator");
                //        break;

                //    case 1045:
                //        MessageBox.Show("Invalid username/password, please try again");
                //        break;
                //}
                return false;
            }
        }

        private bool OpenCMPConnection()
        {
            try
            {
                cmpConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                //switch (ex.Number)
                //{
                //    case 0:
                //        MessageBox.Show("Cannot connect to server.  Contact administrator");
                //        break;

                //    case 1045:
                //        MessageBox.Show("Invalid username/password, please try again");
                //        break;
                //}
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CloseCMPConnection()
        {
            try
            {
                cmpConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataTable GetIPSKUData()
        {
            dt = new DataTable();
            string query = "select PROCESSOR, fullname, VALIDATED_BY, SKU_NUMBER, DATE_ADDED, CLIENT, TSHEETS_CLIENT_CODE, DATE_FINISHED, VALIDATED_DATETIME, EDI_VAN from sku left join users on PROCESSOR =  username left join client on CLIENT = CLIENT_NAME where users.group_num >= 50 and users.ACTIVE =1 and DATE_FINISHED BETWEEN CURDATE() - INTERVAL 1 DAY AND CURDATE() order by DATE_FINISHED";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.CommandTimeout = int.MaxValue;
                
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                this.CloseConnection();
            }

            return dt;
        }

        public DataTable GetCMPData()
        {
            dt = new DataTable();
            string query = "select invoiceSubId, assignedTo, validatedBy, batchDate, custAbbr, customer, buildDateBackend, validatedDateBackend from batch left join user on assignedTo = user.fullName where buildDateBackend BETWEEN CURDATE() - INTERVAL 1 DAY AND CURDATE() and user.cgroup='IN-Entry'";
            if (this.OpenCMPConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, cmpConnection);
                cmd.CommandTimeout = int.MaxValue;

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                this.CloseCMPConnection();
            }
            return dt;
        }

        public DataTable GetSKUQC()
        {
            dt = new DataTable();
            string query = "select PROCESSOR, fullname, VALIDATED_BY, SKU_NUMBER, DATE_ADDED, CLIENT, TSHEETS_CLIENT_CODE, DATE_FINISHED, VALIDATED_DATETIME, EDI_VAN from sku left join users on VALIDATED_BY =  username left join client on CLIENT = CLIENT_NAME where users.group_num >= 50 and users.ACTIVE =1 and VALIDATED_DATETIME BETWEEN CURDATE() - INTERVAL 1 DAY AND CURDATE() order by VALIDATED_DATETIME";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.CommandTimeout = int.MaxValue;
                
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                this.CloseConnection();
            }

            return dt;
        }

        public DataTable GetCMPQC()
        {
            dt = new DataTable();
            string query = "select invoiceSubId, assignedTo, validatedBy, batchDate, custAbbr, customer, buildDateBackend, validatedDateBackend from batch left join user on validatedBy = user.fullName where validatedDateBackend BETWEEN CURDATE() - INTERVAL 1 DAY AND CURDATE() and user.cgroup='IN-QC'";
            if (this.OpenCMPConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, cmpConnection);
                cmd.CommandTimeout = int.MaxValue;

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                this.CloseCMPConnection();
            }
            return dt;
        }


    }
}