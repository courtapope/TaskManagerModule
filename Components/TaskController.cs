using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using Website.DesktopModules.TaskManagerModule.Data;

namespace Website.DesktopModules.TaskManagerModule.Components
{
    public class TaskController
    {
        public static Task GetTask(int taskId)
        {
            return CBO.FillObject<Task>(DataProvider.Instance().GetTask(taskId));
        }

        public static List<Task> GetTasks(int moduleId)
        {
            return CBO.FillCollection<Task>(DataProvider.Instance().GetTasks(moduleId));
        }

        public static void DeleteTask(int taskId)
        {
            DataProvider.Instance().DeleteTask(taskId);
        }

        public static void DeleteTasks(int moduleId)
        {
            DataProvider.Instance().DeleteTasks(moduleId);
        }

        public static int SaveTask(Task t, int tabId)
        {
            if(t.TaskId<1)
            {
                t.TaskId = DataProvider.Instance().AddTask(t);

                //add content item integration

            }
            else
            {
                DataProvider.Instance().UpdateTask(t);
            }
            return t.TaskId;
        }
    }
}