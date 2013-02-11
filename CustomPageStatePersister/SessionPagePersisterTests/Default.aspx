<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="SessionPagePersisterTests_Default" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>
        SessionPagePersister Tests</h3>
    <p>
        This page derives from the SessionPersisterBasePage
        class defined in App_Code, which specifies the page's PageStatePersister property
        to use the <a href="http://msdn2.microsoft.com/en-us/library/system.web.ui.sessionpagestatepersister.aspx">
            SessionPageStatePersister class</a>. This class stores session state for users
        in a Queue object in session. The DropDownList shown below stores its data in viewstate,
        which is persisted to session state. Click the "Add Items" button and observe how
        the ViewState persisted in session changes.</p>
    <p>
        <asp:DropDownList ID="ddl" runat="server">
        </asp:DropDownList>&nbsp;<asp:Button ID="btnAddItems" runat="server" Text="Add Items" /></p>
    <p>
        The output below shows the contents in session state.
        Hit the "Refresh" button a few times and examine the changes.</p>
    <p>
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />&nbsp;</p>
    <p>
        <asp:Literal ID="SessionOutput" runat="server" EnableViewState="False"></asp:Literal>&nbsp;</p>
</asp:Content>

