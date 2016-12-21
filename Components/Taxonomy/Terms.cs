using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Entities.Content.Common;

namespace Website.DesktopModules.TaskManagerModule.Components.Taxonomy
{
    public class Terms
    {

        public void ManageTaskTerms(Task objTask, ContentItem objContent)
        {
            RemoveTaskTerms(objContent);

            foreach(var term in objTask.Terms)
            {
                Util.GetTermController().AddTermToContent(term, objContent);
            }
        }

        public void RemoveTaskTerms(ContentItem objContent)
        {
            Util.GetTermController().RemoveTermsFromContent(objContent);
        }

    }
}