<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddIncident.aspx.cs" Inherits="WebApplication1.AddIncident" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Customer ID<br />
<asp:TextBox ID="txtGetCustomer" runat="server"></asp:TextBox>
<br />
<asp:Button ID="btnGetCustomer" runat="server" OnClick="btnGetCustomer_Click" Text="Get Customer" />
<br />
<asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="CustomerID" DataSourceID="CustomerDataSource" ForeColor="#333333" GridLines="None" Height="50px" Width="125px">
    <AlternatingRowStyle BackColor="White" />
    <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
    <EditRowStyle BackColor="#2461BF" />
    <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
    <Fields>
        <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" InsertVisible="False" ReadOnly="True" SortExpression="CustomerID" />
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
    </Fields>
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
</asp:DetailsView>
<asp:SqlDataSource ID="CustomerDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:TechSupportConnectionString %>" SelectCommand="SELECT [CustomerID], [Name] FROM [Customers] WHERE ([CustomerID] = @CustomerID2)">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtGetCustomer" Name="CustomerID2" PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<br />
Date Opened<br />
<asp:TextBox ID="txtDate" runat="server" ReadOnly="True" BackColor="#ECEDF2"></asp:TextBox>
<br />
<asp:SqlDataSource ID="ProductDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:TechSupportConnectionString %>" SelectCommand="SELECT DISTINCT Registrations.ProductCode, Products.Name FROM Registrations INNER JOIN Products ON Registrations.ProductCode = Products.ProductCode WHERE (Registrations.CustomerID = @CustomerID)">
    <SelectParameters>
        <asp:ControlParameter ControlID="txtGetCustomer" Name="CustomerID" PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<br />
Product<br />
<asp:DropDownList ID="cbxProduct" runat="server" DataSourceID="ProductDataSource" DataTextField="Name" DataValueField="ProductCode">
</asp:DropDownList>
<br />
<br />
Title<br />
<asp:TextBox ID="txtTitle" runat="server" Width="300px"></asp:TextBox>
<br />
<br />
Description<br />
<asp:TextBox ID="txtDescription" runat="server" Height="82px" Width="304px" TextMode="MultiLine"></asp:TextBox>
<br />
<br />
<asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add Incident" />
&nbsp;
<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
    <asp:SqlDataSource ID="IncidentsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:TechSupportConnectionString %>" DeleteCommand="DELETE FROM [Incidents] WHERE [IncidentID] = @IncidentID" InsertCommand="INSERT INTO [Incidents] ([CustomerID], [ProductCode], [DateOpened], [Title], [Description]) VALUES (@CustomerID, @ProductCode, @DateOpened, @Title, @Description)" SelectCommand="SELECT [IncidentID], [CustomerID], [ProductCode], [DateOpened], [Title], [Description] FROM [Incidents]" UpdateCommand="UPDATE [Incidents] SET [CustomerID] = @CustomerID, [ProductCode] = @ProductCode, [DateOpened] = @DateOpened, [Title] = @Title, [Description] = @Description WHERE [IncidentID] = @IncidentID">
        <DeleteParameters>
            <asp:Parameter Name="IncidentID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="CustomerID" Type="Int32" />
            <asp:Parameter Name="ProductCode" Type="String" />
            <asp:Parameter Name="DateOpened" Type="DateTime" />
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CustomerID" Type="Int32" />
            <asp:Parameter Name="ProductCode" Type="String" />
            <asp:Parameter Name="DateOpened" Type="DateTime" />
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="IncidentID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
