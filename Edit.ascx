<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Edit.ascx.cs" Inherits="Website.DesktopModules.TaskManagerModule.Edit" %>
<%@ Register TagPrefix="dnndev" TagName="label" Src="~/controls/LabelControl.ascx" %>

<dnndev:label ID="lblName" runat="server" Text="Name" HelpText="This is my help text" />
<asp:TextBox ID="txtName" runat="server" />
<br />

<dnndev:label ID="lblDescription" runat="server" Text="Description" HelpText="Enter the description for your task" />
<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="20" />
<br />

<dnndev:label ID="lblAssignedUser" runat="server" Text="Assigned User" HelpText="Choose a user for the task" />
<asp:DropDownList ID="ddlAssignedUser" runat="server" />
<br />

<dnndev:label ID="lblTargetCompletionDate" runat="server" Text="Target Completion Date" HelpText="When should the task be completed" />
<asp:TextBox ID="txtTargetCompletionDate" runat="server" />
<br />

<dnndev:label ID="lblCompletionDate" runat="server" Text="Actual Completion Date" HelpText="When did the task get completed" />
<asp:TextBox ID="txtCompletionDate" runat="server" />
<br />

<asp:LinkButton ID="btnSubmit" runat="server" Text="Save Task" OnClick="btnSubmit_Click" />
&nbsp

<asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
<br />