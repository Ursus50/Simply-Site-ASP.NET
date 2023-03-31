<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Lab1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Laboratorium 1</title>
    <link rel="stylesheet" href="StyleSheet1.css" />
    <style type="text/css">
        .auto-style1 {}
        .auto-style2 {}
    </style>
</head>
<body>
    <form id="form1"  runat="server">
        <asp:Panel class="panel" ID= "logowanie" Visible="true" runat = "server">
            <br />
           <div class="pogrubiony">Podaj imię i nazwisko </div> 
            <asp:TextBox class="niebieski" ID="txtName" runat="server" Text="Imię Nazwisko"/>
           
            

           <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="txtName" errormessage="Nie wprowadzono żadnej wartości" Text="*" />

            <asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" 
            ErrorMessage="Identyfikator nie jest zgodny z wzorcem. Baza danych nie obsluguje polskich znakow" Text ="*"
            
            ValidationExpression="^(([A-Z])(([a-z])){2,}\s([A-Z])(([a-z])|()){2,})$"
            ControlToValidate="txtName" />

                <!--ValidationExpression="^(([A-Z]|[Ą,Ć,Ę,Ł,Ń,Ó,Ś,Ź,Ż])(([a-z])|([ą,ć,ę,ł,ń,ó,ś,ź,ż])){2,}\s([A-Z]|[Ą,Ć,Ę,Ł,Ń,Ó,Ś,Ź,Ż])(([a-z])|([ą,ć,ę,ł,ń,ó,ś,ź,ż])){2,})$"--> 


            <br />
           <asp:Button ID="ButtonLog" runat="server" Text="Zarejestruj" OnClick="Logowanie" />
            
            
          
            <asp:ValidationSummary ID="ValSum" Text="" runat="server"/>
            <br />
            <asp:Label ID="kontrolka" runat="server" />
        </asp:Panel>

        <asp:Panel ID= "Panel2" Visible="false" runat = "server" >
            <div class="ogol" >
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <asp:Timer ID="Timer1" runat="server" Interval="15000" ontick="Timer1_Tick">
    </asp:Timer>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="lewa_strona">
                            <div class="lewylewy">

                                <asp:Label class="czerwony" ID="nazwa" Text="Imie nazwisko" runat="server" />
                                <br />
                                <br />
                                <asp:DropDownList class="niebieski"  id="DropDownList1"  AutoPostBack="True" OnSelectedIndexChanged="AktualizacjaTab" runat="server" DataSourceID="SqlDataSource2" DataTextField="Nazwa" DataValueField="Nazwa">
                                </asp:DropDownList>

                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Nazwa] FROM [Uzytkownicy] where [Aktywny] = 1"></asp:SqlDataSource>

                                <br />
                                <br />
                                <br />
                                <asp:DetailsView ID="DetailsView1" width="100%" runat="server" AutoGenerateRows="False" BackColor="LightGoldenrodYellow" border="1px" BorderColor="Tan" BorderWidth="1px" CellPadding="2" DataKeyNames="Nazwa" DataSourceID="SqlDataSource1" EnableModelValidation="True" ForeColor="Black" GridLines="None">
                                    <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                    <EditRowStyle  BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <Fields>
                                        <asp:BoundField  DataField="Nazwa" HeaderText="Nazwa" ReadOnly="True" SortExpression="Nazwa" />
                                        <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
                                        <asp:BoundField DataField="Godzina" HeaderText="Godzina" SortExpression="Godzina" />
                                        <asp:BoundField DataField="IleRysunek" HeaderText="Ile Zmian Rysunek" SortExpression="IleRysunek" />
                                        <asp:BoundField DataField="IlePrzycisk" HeaderText="Ile Zmian Przycisk" SortExpression="IlePrzycisk" />
                                        <asp:BoundField DataField="IleAplikacji" HeaderText="Ile Aplikacji" SortExpression="IleAplikacji" />
                                    </Fields>
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                </asp:DetailsView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Nazwa], [Data], [Godzina], [IleRysunek], [IlePrzycisk], [IleAplikacji] FROM [Uzytkownicy] WHERE ([Nazwa] = @Nazwa)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txtName" Name="Nazwa" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                                <br>
                                <br></br>
                                <br>
                                <br></br>
                                <br>
                                <br></br>
                                <br>
                                <br></br>
                                </br>
                                </br>
                                </br>

                                </br>

                            </div>
                        </div>
                        <div class="prawa_strona">
        
                           <div class="gora">
                                <div class="lewy">     
                                    <asp:ImageButton class="srodekO" runat="server" ID="Pierwsze" Visible="true" onclick="PictureClick" src="Properties/zdjecia/1.jpg" />
                                </div>
                                <div class="prawy">
                                    <asp:ImageButton class="srodekO" runat="server" ID="Drugie" Visible="false" onclick="PictureClick" src="Properties/zdjecia/2.jpg" />
                                </div>

                            </div>
                            <div class="srodek">
                                <asp:Button class="srodekButton" ID="Button1" runat="server" onclick="ButtonClick" Text="Przełącz rysunek" />
                            </div>

                            <div class="dol">
                                <div class="lewy">
                                    <asp:ImageButton class="srodekO" runat="server" ID="Czwarte" Visible="false" onclick="PictureClick" src="Properties/zdjecia/3.jpeg" />
                                </div>
                                <div class="prawy">
                                    <asp:ImageButton class="srodekO" runat="server" ID="Trzecie" Visible="false" onclick="PictureClick" src="Properties/zdjecia/4.jpg" />
                                </div>
                             </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick">
        </asp:AsyncPostBackTrigger>
    </Triggers>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>
    </form>
</body>
</html>