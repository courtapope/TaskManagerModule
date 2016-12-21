using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Content.Common;

namespace Website.DesktopModules.TaskManagerModule.Components.Taxonomy
{
    public class Content
    {

        private const string ContentTypeName = "TaskManagerTask";

        /// <summary>
        /// This should only be run after the Tast exists in the data store.
        /// </summary>
        /// <param name="objTask"></param>
        /// <param name="tabId"></param>
        /// <returns>The newly created ContentItemID from the data store.</returns>
        public ContentItem CreateContentItem(Task objTask, int tabId)
        {
            var typeController = new ContentTypeController();
            var colContentTypes = (from t in typeController.GetContentTypes() where t.ContentType == ContentTypeName);      /////////////
            int contentTypeId;

            if (colContentTypes.Count() > 0)
            {
                var contentType = colContentTypes.Single();
                contentTypeId = contentType == null ? CreateContentType() : contentType.ContentTypeId;
            }
            else
            {
                contentTypeId = CreateContentType();
            }

            var objContent = new ContentItem
            {
                Content = objTask.TaskName + " " + objTask.TaskDescription,
                ContentTypeId = contentTypeId,
                Indexed = false,
                ContentKey = "tid=" + objTask.TaskId,
                ModuleID = objTask.ModuleId,
                TabID = tabId
            };

            objContent.ContentItemId = Util.GetContentController().AddContentItem(objContent);

            //Add Terms
            var cntTerm = new Terms();
            cntTerm.ManageTaskTerms(objTask, objContent);

            return objContent;

        }

        /// <summary>
        /// This is used to update the ContentItems table. Should be called when a Task is updated.
        /// </summary>
        /// <param name="objTask"></param>
        /// <param name="tabId"></param>
        public void UpdateContentItem(Task objTask, int tabId)
        {
            var objContent = Util.GetContentController().GetContentItem(objTask.ContentItemId);

            if (objContent == null) return;
            objContent.Content = objTask.TaskName + " " + objTask.TaskDescription;
            objContent.TabID = tabId;
            Util.GetContentController().UpdateContentItem(objContent);

            //Update Terms
            var cntTerm = new Terms();
            cntTerm.ManageTaskTerms(objTask, objContent);
        }

        /// <summary>
        /// This removes a cotent item associated with a task from the data store. Should run everytime a Task is deleted.
        /// </summary>
        /// <param name="objTask">The Content Iten we wish to remove from the data store.</param>
        public void DeleteContentItem(Task objTask)
        {
            if (objTask.ContentItemId <= Null.NullInteger) return;
            var objContent = Util.GetContentController().GetContentItem(objTask.ContentItemId);
            if (objContent == null) return;

            //remove any metadata/terms associated first (perhaps we should just rely on ContentItem cascade deley......)
            var cntTerms = new Terms();
            cntTerms.RemoveTaskTerms(objTask);

            Util.GetContentController().DeleteContentItem(objContent);
        }

        #region Private Mathods

        /// <summary>
        /// Creates a Content Type (for taxonmy) int the data store.
        /// </summary>
        /// <returns>The primary key value of the new ContentType.</returns>
        private static int CreateContentType()
        {
            var typeController = new ContentTypeController();
            var objContentType = new ContentType(ContentType = ContentTypeName);

            return typeController.AddContentType(objContentType);
        }

        #endregion

    }
}