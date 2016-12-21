<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Website.DesktopModules.TaskManagerModule.View" %>
<asp:Repeater ID="rptTaskList" runat="server" />
<HeaderTemplate>
    <ul class="tm_tl"></ul>
</HeaderTemplate>

<ItemTemplate>
    <li class="tm_t">
        <h3>
            <asp:Label ID="lblTaskName" runat="server" Text='<%@DataBinder.Eval(Container.DataItem, "TaskName").ToString() %>' />
        </h3>
        <asp:Label ID="lblTargetCompletionDate" runat="server" Text='<%@DataBinder.Eval(Container.DataItem, "TargetCompletionDate").ToString() %>' CssClass="tm_tcd" />
        <asp:Label ID="lblTaskDescription" runat="server" Text='<%@DataBinder.Eval(Container.DataItem, "TaskDescription").ToString() %>' cssCalss="tm_td" />
    </li>
</ItemTemplate>

<FooterTemplate>
    </ul>
</FooterTemplate>

</asp:Repeater>