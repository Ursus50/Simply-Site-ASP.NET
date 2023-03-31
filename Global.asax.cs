using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace Lab1
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)       
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)          //wykrycie zakonczenia sesji, zmienienie statusu uzytkownika na nieaktywny, dekrementacja u wszystkich aktywnych liczby aplikacji
        {

            string tmp = Session.SessionID;     //pobranie ID konczocej sie sesji

            String str = "update uzytkownicy set Aktywny = 0 where IDsesji ='" + tmp + "';";

            string polString ="Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection pol = new SqlConnection(polString);       //stworzenie polaczenia oraz zapytania
            SqlCommand komenda = new SqlCommand(str, pol);

            pol.Open();
            komenda.ExecuteNonQuery();          //wykonanie zmienienia stanu uzytkowika na nieaktywny
            pol.Close();

            str = "select count(*) from uzytkownicy where Aktywny = 1";

            komenda = new SqlCommand(str, pol);

            pol.Open();
            int i = (int)komenda.ExecuteScalar();       //pobranie liczby aktywnych aplikacji
            string sLiczba = i.ToString();
            pol.Close();

            str = "update uzytkownicy set IleAplikacji = " + sLiczba + " where aktywny = 1";

            komenda = new SqlCommand(str, pol);
            pol.Open();
            komenda.ExecuteNonQuery();                  //zmienienia u wszystkich aktywnych liczby aktywnych aplikacji
            pol.Close();

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}