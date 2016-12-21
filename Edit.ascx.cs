/*
' Copyright (c) 2016  Courtney Popielarz
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/


using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Users;
using Website.DesktopModules.TaskManagerModule.Components;

namespace Website.DesktopModules.TaskManagerModule
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditTaskManagerModule class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from TaskManagerModuleModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : TaskManagerModuleModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if(!Page.IsPostBack)
                {
                    ddlAssignedUser.DataSource = UserController.GetUsers(PortalId);
                    ddlAssignedUser.DataTextField = "Username";
                    ddlAssignedUser.DataValueField = "UserId";
                    ddlAssignedUser.DataBind();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Task t;
            t = new Task
                {
                    AssignedUserId = Convert.ToInt32(ddlAssignedUser.SelectedValue),
                    //CompletedOnDate = Convert.ToDateTime(txtCompletionDate.Text.Trim()),
                    CreatedByUserId = UserId,
                    CreatedOnDate = DateTime.Now,
                    //TargetCompletionDate = Convert.ToDateTime(txtTargetCompletionDate.Text.Trim()),
                    TaskName = txtName.Text.Trim(),
                    TaskDescription = txtDescription.Text.Trim(),
                    ModuleId = ModuleId
                };

            //check for dates
            DateTime outputDate;
            if(DateTime.TryParse(txtCompletionDate.Text.Trim(),out outputDate))
            {
                t.CompletedOnDate = outputDate;
            }

            if (DateTime.TryParse(txtTargetCompletionDate.Text.Trim(), out outputDate))
            {
                t.TargetCompletionDate = outputDate;
            }

            TaskController.SaveTask(t, TabId);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }
    }
}