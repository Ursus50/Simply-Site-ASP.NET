using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Lab1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void ButtonClick(object sender, EventArgs e)          //po przycisnieciu przycisku pojawia sie losowy inny obrazek (stary obrazek znika)
        {
            if (nazwa.Text == DropDownList1.Text)
            {

                Random random = new Random();
                int i, c;
                ImageButton[] tab = { Pierwsze, Drugie, Trzecie, Czwarte }; //tablica zawierajaca ID obrazkow

                for (c = 0; c < 4; c++)              //znalezienie indeksu aktualnie wyswietlanego obrazka
                    if (tab[c].Visible == true)
                        break;

                do
                {                                   //wylosowanie indeksu nowego obrazka do wyswieltlenia
                    i = random.Next(4);
                } while (i == c);
                tab[c].Visible = false;             //wylaczenie widocznosci starego obrazka
                tab[i].Visible = true;              //wlaczenie widocznosci nowego, wylosowanego obrazka
       
                updatePrzycisku();                  //aktualizacja danych w DetailView oraz w DropDownList
                aktualizacjaInfo();
            }

        }
        protected void PictureClick(object sender, EventArgs e)         //po przycisnieciu obrazka pojawia sie kolejny zgodnie z ruchem wskazowek zegara (stary obrazek znika)
        {
            if (nazwa.Text == DropDownList1.Text)
            {
                int i, c;
                ImageButton[] tab = { Pierwsze, Drugie, Trzecie, Czwarte }; //tablica zawierajaca ID obrazkow

                for (c = 0; c < 4; c++)             //znalezienie indeksu aktualnie wyswietlanego obrazka
                    if (tab[c].Visible == true)
                        break;

                i = (c + 1) % 4;                    //wyznaczenie indeksu kolejnego obrazka
                tab[c].Visible = false;             //wylaczenie widocznosci starego obrazka
                tab[i].Visible = true;              //wlaczenie widocznosci nowego, wylosowanego obrazka

                updateObrazka();                    //aktualizacja danych w DetailView oraz w DropDownList
                aktualizacjaInfo();
            }
        }

        protected void AktualizacjaTab(object sender, EventArgs e)          //po wybraniu uzytkownika z listy uaktualnia sie tabela i lista
        {
            aktualizacjaListy();
            aktualizacjaInfo();
        }

        public bool czyJestUzytkownik()                     //sprawdzenie czy uzytkownik podany przy logowaniu znajduje sie w bazie danych
        {
            String str = "select count(*) from uzytkownicy where nazwa = '" + txtName.Text +"'" ;       //zapytanie, ktire bedzie kierowane do bazy danych

            string polString = SqlDataSource1.ConnectionString;         //stworzenie polaczenia oraz zapytania
            SqlConnection pol = new SqlConnection(polString);
            SqlCommand komenda = new SqlCommand(str, pol);

            pol.Open();
            int i = (int)komenda.ExecuteScalar();           //pobranie ilosci istniejacych uzytkownikow o zadanej nazwie 0 - brak 1-znajduje sie juz w bazie danych
            pol.Close();

            if (i == 1)             //czy znajduje sie taki uzytkownik w bazie danych 1-tak else-brak
                return true;
            else 
                return false;

        }

        public void updatePrzycisku()       //aktualizacja w bazie danych pola IlePrzycisk
        {
            String str = "update uzytkownicy set IlePrzycisk = (IlePrzycisk + 1) where nazwa ='" + DropDownList1.SelectedValue + "';";

            string polString = SqlDataSource1.ConnectionString;
            SqlConnection pol = new SqlConnection(polString);
            SqlCommand komenda = new SqlCommand(str, pol);

            pol.Open();
            komenda.ExecuteNonQuery();
            pol.Close();
        }
        public void updateObrazka()         //aktualizacja w bazie danych pola IleRysunek
        {

            String str = "update uzytkownicy set IleRysunek = (IleRysunek + 1) where nazwa ='" + DropDownList1.SelectedValue + "';";

            string polString = SqlDataSource1.ConnectionString;
            SqlConnection pol = new SqlConnection(polString);
            SqlCommand komenda = new SqlCommand(str, pol);

            pol.Open();
            komenda.ExecuteNonQuery();
            pol.Close();
        }


        public void updateDatyGodziny()     //aktualizacja w bazie danych godziny, daty oraz ID sesji uzytkownika, jezeli już wczesniej znajdowal sie w bazie danych
        {
            String str = "update uzytkownicy set data ='" + DateTime.Today.ToString("dd.MM.yyyy") + "', Godzina = '" + DateTime.Now.ToString("HH:mm") + "', IDsesji ='" + Session.SessionID.ToString() + "', aktywny = 1 where nazwa ='" + txtName.Text.ToString() + "';";

            string polString = SqlDataSource1.ConnectionString;
            SqlConnection pol = new SqlConnection(polString);
            SqlCommand komenda = new SqlCommand(str, pol);

            pol.Open();
            komenda.ExecuteNonQuery();
            pol.Close();
        }

        protected void aktualizacjaAplikacji()      //aktualizacja u wszystkich aktywnych (aktywny = 1) uzytkownikow liczby ile jest aktywnych aplikacji
        {
            string polString = SqlDataSource1.ConnectionString;

            String str = "select count(*) from uzytkownicy where Aktywny = 1";

            SqlConnection pol = new SqlConnection(polString);
            SqlCommand komenda = new SqlCommand(str, pol);

            pol.Open();
            int i = (int)komenda.ExecuteScalar();       //pobranie ilosci aktywnych aplikacji
            string sLiczba = i.ToString();
            pol.Close();

            str = "update uzytkownicy set IleAplikacji = " + sLiczba + " where aktywny = 1";

            komenda = new SqlCommand(str, pol);
            pol.Open();
            komenda.ExecuteNonQuery();          //zmiana ilosci aktywnych aplikacji u wszystkich aktywnych
            pol.Close();
        }


        public bool czyJestAktywny()            //sprawdzenie, czy jezeli wpisany uzytkownik jest juz w bazie danych, to czy jest aktywny (aktywny = 1)
        {
            String str = "select count(*) from uzytkownicy where nazwa = '" + txtName.Text + "' and aktywny = 1";

            string polString = SqlDataSource1.ConnectionString;
            SqlConnection pol = new SqlConnection(polString);
            pol.Open();
            SqlCommand komenda = new SqlCommand(str, pol);      //pobranie ilosci aktywnych uzytkownikow o zadanej nazwie przy logowaniu 0-nie jest aktywny 1-jest aktywny
            int i = (int)komenda.ExecuteScalar();
            pol.Close();

            if (i == 1)
                return true;
            else
                return false;

        }


        public void aktualizacjaInfo()              //aktualizacja DetailView
        {

            string polString = SqlDataSource1.ConnectionString;
            SqlConnection pol = new SqlConnection(polString);
            pol.Open();
            SqlDataSource1.SelectCommand = "SELECT[Nazwa], [Data], [Godzina], [IleRysunek], [IlePrzycisk], [IleAplikacji] FROM[Uzytkownicy] WHERE([Nazwa] = '" + DropDownList1.SelectedValue +"');";
            DetailsView1.DataSourceID = "SqlDataSource1";

            pol.Close();
            Session.Timeout = 2;                    //ustawienie zamkniecia sesji po 2 minutach braku aktywnosci (uaktualniania wartosci w sesji)
        }


        public void aktualizacjaListy()             //aktualizacja DropDownList
        {

            string polString = SqlDataSource1.ConnectionString;
            SqlConnection pol = new SqlConnection(polString);
            string tmp = DropDownList1.Text;
            pol.Open();

            SqlDataSource2.SelectCommand = "SELECT[Nazwa] FROM[Uzytkownicy] where Aktywny = 1;";
            DropDownList1.DataSourceID = "SqlDataSource2";

            pol.Close();
            DropDownList1.SelectedValue = tmp;
            Session.Timeout = 2;                    //ustawienie zamkniecia sesji po 2 minutach braku aktywnosci
        }

        protected void Logowanie(object sender, EventArgs e)                //po przycisnieciu przycisku pojawia sie losowy inny obrazek (stary obrazek znika)
        {
            bool flaga;

            if (czyJestUzytkownik())                //czy podany uzytkownik przy logowaniu znajduje sie juz w bazie danych
            {
                if (czyJestAktywny())               //jest w bazie danych i aktualnie jego seseja jest aktywna wiec nie mozna sie drugi raz zalogowac
                    flaga = false;
                else
                {
                    flaga = true;                   //jest w bazie danych ale jest nie aktywny wiec jego data, godzina oraz ID sesji musza byc zaktualizowane
                    updateDatyGodziny();
                }
            }
            else        //brak uzytkownika w bazie danych wiec go wprowadzamy
            {
                flaga = true;
                String str = "insert into uzytkownicy values ('" + txtName.Text + "','" + DateTime.Today.ToString("dd.MM.yyyy") + "','" + DateTime.Now.ToString("HH:mm") + "', 0, 0, 0, 1, '"+ Session.SessionID.ToString() + "')";
                string polString = SqlDataSource1.ConnectionString;
                SqlConnection pol = new SqlConnection(polString);
                SqlCommand komenda = new SqlCommand(str, pol);

                pol.Open();
                komenda.ExecuteNonQuery();          //wykonanie inserta
                pol.Close();
            }


            if (flaga)                 //poprawne zalogowanie sie
            {
                logowanie.Visible = false;              //zmiana widocznosci paneli
                Panel2.Visible = true;
                nazwa.Text = txtName.Text;
                aktualizacjaListy();
                aktualizacjaAplikacji();
                DropDownList1.SelectedValue = txtName.Text;

                Session.Timeout = 2;                    //ustawienie zamkniecia sesji po 2 minutach braku aktywnosci 
            }
            else
            {
                kontrolka.Text = "*Użytkownik jest zalogowany";     //w przypadku gdy wystepuje juz osoba o takim samym loginie
            }
            
        }

        protected void Timer1_Tick(object sender, EventArgs e)      //aktualizacja DetailView oraz DropDownList co 10 sekund
        {
            aktualizacjaListy();
            aktualizacjaInfo();
        }

    }
}