using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Website.DesktopModules.TaskManagerModule.Components
{
    public class Task : ContentItem
    {
        /// <summary>
        /// TaskId identifies and undividuak task
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// A string with the name of the Tesk
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// A string with a description of the Task
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// An integer with the user id of the assigned user for the Task
        /// </summary>
        public int AssignedUserId { get; set; }

        /// <summary>
        /// The ModuleId of where the task was created and gets despalyed
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// A date for the tagetted completion date
        /// </summary>
        public DateTime TargetCompletionDate { get; set; }

        /// <summary>
        /// A date for the actual completion date of the task
        /// </summary>
        public DateTime CompletedOnDate { get; set; }

        /// <summary>
        /// An integer for the user id of the user who created the task
        /// </summary>
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// An integer for the user who last updated the task
        /// </summary>
        public int LastModifiedByUserId { get; set; }

        /// <summary>
        /// The date the task was created
        /// </summary>
        public new DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// The date the task was updated
        /// </summary>
        public new DateTime LastModifiedOnDate { get; set; }

        /// <summary>
        /// The portal where the article resides
        /// </summary>
        public int PortalId { get; set; }



        //Read Only Props
        /// <summary>
        /// The username of the user who created the task
        /// </summary>
        public new string CreatedByUser
        {
            get
            {
                return CreatedByUserId != 0 ? DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, CreatedByUserId).Username : Null.NullString;
            }
        }

        /// <summary>
        /// The username of the user who las updated the task
        /// </summary>
        public string LastUpdatedByUser
        {
            get
            {
                return LastModifiedByUserId != 0 ? DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, LastModifiedByUserId).Username : Null.NullString;
            }
        }

        #region IHydratable Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            //Call the base classes fill method to populate the base class properties
            base.FillInternal(dr);

            TaskId = Null.SetNullInteger(dr["TaskId"]);
            ModuleId = Null.SetNullInteger(dr["ModuleId"]);
            TaskName = Null.SetNullString(dr["TaskName"]);
            TaskDescription = Null.SetNullString(dr["TaskDescription"]);
            AssignedUserId = Null.SetNullInteger(dr["AssignedUserId"]);
            TargetCompletionDate = Null.SetNullDateTime(dr["TargetCompletionDate"]);
            CompletedOnDate = Null.SetNullDateTime(dr["CompletedOnDate"]);
            CreatedByUserId = Null.SetNullInteger(dr["CreatedByUserId"]);
            LastModifiedByUserId = Null.SetNullInteger(dr["LastModifiedByUserId"]);
            CreatedOnDate = Null.SetNullDateTime(dr["CreatedOnDate"]);
            LastModifiedOnDate = Null.SetNullDateTime(dr["LastModifiedOnDate"]);
        }

        /// <summary>
        /// Gets and sets the Key ID
        /// </summary>
        public override int KeyID
        {
            get
            {
                return TaskId;
            }

            set
            {
                TaskId = value;
            }
        }

        #endregion
    }
}