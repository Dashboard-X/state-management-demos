<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HiddenFile.aspx.cs" Inherits="HiddenFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
   <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp; &nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="No Action Button" />&nbsp;
        
        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
    
    </div>
        <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
            <br />
            &nbsp; &nbsp;Click couter</strong> (Saved in Hidden Field) :&nbsp;
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </form>
</body>
</html>
