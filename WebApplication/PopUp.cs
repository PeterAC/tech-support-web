/*
 * Author: Peter Cornell
 * Date: 25/11/14
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace WebApplication1
{
    public static class PopUp
    {
        /// <summary>
        /// Generates a javascript pop-up message on the page passed to it via the page parameter
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="page"></param>
        public static void ShowPopUpMsg(string msg, System.Web.UI.Page page)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "showalert", sb.ToString(), true);
        }
    }
}