<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View state test</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;Name: &nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        &nbsp; &nbsp;
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /><br />
        <br />
        [Stored in ViewState]: &nbsp; Number of postbacks:
        <asp:Label ID="Label1" runat="server" Text="0"></asp:Label><br />
        <br />
        [Strored in HiddenField]: &nbsp; Number of postbacks:&nbsp;
        <asp:Label ID="Label2" runat="server" Text="0"></asp:Label>
        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
        <br />
        [Strored in Cookies]: &nbsp; Number of postback since the first runs:&nbsp;
        <asp:Label ID="Label3" runat="server" Text="0"></asp:Label>&nbsp;
        <br />
        <br />
        [Strored in QueryStrings]: &nbsp; Number of clicks:&nbsp;
        <asp:Label ID="Label4" runat="server" Text="0"></asp:Label>
        &nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="ClickCount" /><br />
        <br />
        [Strored in ApplicationState]: &nbsp; Number of postbacks on this run so far:&nbsp;
        <asp:Label ID="Label5" runat="server" Text="0"></asp:Label><br />
        <br />
        [Strored in SessionState]: &nbsp; Number of postbacks on this run for THIS user
        so far:&nbsp;
        <asp:Label ID="Label6" runat="server" Text="0"></asp:Label></div>
    </form>
</body>
</html>
