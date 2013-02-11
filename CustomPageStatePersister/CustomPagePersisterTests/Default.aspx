<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="CustomPagePersisterTests_Default" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>
        FileSystemPagePersister Tests</h3>
    <p>
        This page derives from the CustomPersisterBasePage
        class defined in App_Code, which specifies the page's PageStatePersister property
        to use the FileSystemPageStatePersister class (which is a class I created in App_Code). 
        This custom class stores session state for users
        in a file in the ~/StateFiles folder. The DropDownList shown below stores its data in viewstate,
        which is persisted to session state. Click the "Add Items" button and observe how
        the ViewState persisted in session changes.</p>
    <p>
        <asp:DropDownList ID="ddl" runat="server">
        </asp:DropDownList>&nbsp;<asp:Button ID="btnAddItems" runat="server" Text="Add Items" /></p>
    <p>
        The output below shows the contents in ~/StateFiles folder. Click on view to view the
        contents of a particular file.
        Hit the "Refresh" button a few times to see this list of files grow.</p>
    <p>
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />&nbsp;</p>
    <p>
        <asp:GridView ID="StateFiles" runat="server" AutoGenerateColumns="False" EnableViewState="False">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Name" DataNavigateUrlFormatString="~/StateFiles/{0}" Text="View" />
                <asp:BoundField DataField="Name" HeaderText="File" />
                <asp:BoundField DataField="Length" DataFormatString="{0:N0}" HeaderText="Size (bytes)" HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        &nbsp;</p>
</asp:Content>

