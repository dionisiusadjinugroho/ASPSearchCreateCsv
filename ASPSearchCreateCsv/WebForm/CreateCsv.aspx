<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateCsv.aspx.cs" Inherits="ASPSearchCreateCsv.CreateCsv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="ButtonGotoSearch" runat="server" Height="36px" OnClick="ButtonGotoSearch_Click" Text="Go to Search Page" Width="125px" />
            
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Record"></asp:Label>
            &nbsp;
            <asp:TextBox ID="TextboxRecord" runat="server" Width="150px">100000</asp:TextBox>
            <br />
            <br />
            <asp:Button ID="ButtonGenerateCSV" runat="server" Height="35px" OnClick="Button1_Click" Text="Generate CSV" Width="136px" />
            
        </div>
    </form>
</body>
</html>
