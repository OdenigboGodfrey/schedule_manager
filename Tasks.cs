using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleManager
{
    class Tasks
    {
        public static void setCurrentTask(DBConnection dbCon, int oldTaskId, int currentTaskId)
        {
            var updateData = new Dictionary<string, string>();

            if (oldTaskId > 0)
            {
                //deactivate previous
                updateData.Add("current", "0");
                DBHelper.Update(dbCon, "tasks", updateData, "id=" + oldTaskId);
            }


            //activate current
            updateData = new Dictionary<string, string>();
            updateData.Add("current", "1");
            DBHelper.Update(dbCon, "tasks", updateData, "id=" + currentTaskId);
        }

        public static void toggleTaskActiveStatus(DBConnection dbCon, int taskId, string status)
        {
            var updateData = new Dictionary<string, string>();


            //activate current
            updateData = new Dictionary<string, string>();
            updateData.Add("active", status);
            DBHelper.Update(dbCon, "tasks", updateData, "id=" + taskId);
        }
    }
}
