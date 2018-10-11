using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Data;

namespace WSSQUDB
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataTable GetIPSKU()
        {
            DBConnect objDB = new DBConnect();
            DataTable dt = objDB.GetIPSKUData();
            dt.TableName = "IP";
            return dt;
        }
        [WebMethod]
        public DataTable GetCMP()
        {
            DBConnect objDB = new DBConnect();
            DataTable dt = objDB.GetCMPData();
            dt.TableName = "CMP";
            return dt;
        }

        [WebMethod]
        public DataTable GetQCSKU()
        {
            DBConnect objDB = new DBConnect();
            DataTable dt = objDB.GetSKUQC();
            dt.TableName = "IP";
            return dt;
        }

        [WebMethod]
        public DataTable GetQCCMP()
        {
            DBConnect objDB = new DBConnect();
            DataTable dt = objDB.GetCMPQC();
            dt.TableName = "CMP";
            return dt;
        }
    }
}