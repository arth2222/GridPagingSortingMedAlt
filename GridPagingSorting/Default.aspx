<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GridPagingSorting.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>GridView med sorting og paging</h1>
            <p>Bruker databasen Arthurs, tabell Person, på serveren glemmen.bergersen.dk,4729</p>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting">
            </asp:GridView>
        </div>

        <div>
            <h1>GridView med update, delete, insert</h1>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowDeleting="GridView2_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" />
                    <asp:BoundField DataField="Income" HeaderText="Income" />
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />

                    

                </Columns>
            </asp:GridView>

           <asp:TextBox ID="txtNewFirstName" runat="server" Placeholder="First Name" />
            <asp:TextBox ID="txtNewLastName" runat="server" Placeholder="Last Name" />
            <asp:TextBox ID="txtNewPhone" runat="server" Placeholder="Phone" />
            <asp:TextBox ID="txtNewPostalCode" runat="server" Placeholder="Postal Code" />
            <asp:TextBox ID="txtNewIncome" runat="server" Placeholder="Income" />
            <asp:Button ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" />
        </div>

    </form>
</body>
</html>
