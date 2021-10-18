<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchTextCsv.aspx.cs" Inherits="ASPSearchCreateCsv.SearchTextCsv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="ButtonGotoCreateCSV" runat="server" Text="Go to Create CSV Page" OnClick="ButtonGotoCreateCSV_Click"/>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Insert Data"></asp:Label>&nbsp;&nbsp; <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Button ID="ButtonInsertData" runat="server" Text="Insert Data" OnClick="ButtonInsertData_Click"/>
            &nbsp;
            <asp:Label ID="LabelInfoUpload" runat="server" Text="Label" Visible="False"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LabelRecord" runat="server" Text="Records : 0"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
        &nbsp;&nbsp; <asp:TextBox ID="TextBoxSearch" runat="server" Width="420px"></asp:TextBox>
        &nbsp;&nbsp;
            <asp:Button ID="ButtonSearch" runat="server" Text="Search Data" OnClick="ButtonSearch_Click"/>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" Height="147px" Width="602px">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript"> 

     function handleEnter (obj, event) 
     {        
     var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;        
     if (keyCode == 13) 
     {                    
        document.getElementById(obj).click();
                    return false;        
     }        
     else  {
           return true;   
            }   
     } 
</script> 
