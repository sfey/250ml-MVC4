using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _250ml_MVC4_2.Helpers
{
    /*
     * Hilfsklasse zur Bestimmung des aufrufenden Controllers
     * und der vorrangegangenen Action
     */
    public class ReferrerHelper
    {
        public static string[] ControllerAndActionInfo()
        {
            if (System.Web.HttpContext.Current.Request.UrlReferrer != null) {
                string referrer = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                string remove = "http://" + System.Web.HttpContext.Current.Request.ServerVariables["server_name"]
                                + ":" + System.Web.HttpContext.Current.Request.ServerVariables["server_port"];
                referrer = referrer.Replace(remove, String.Empty);

                return referrer.Split('/');
            }

            return null;
        }

        public static int ReferrerId() {
            int id = 0;

            if (ControllerAndActionInfo() != null)
            {
                if (ControllerAndActionInfo().GetUpperBound(0) >= 3)
                {
                    id = Convert.ToInt32(ControllerAndActionInfo()[3]);
                }
            
            }


            return id;
        }

        public static string ReferrerAction() {

            if (ControllerAndActionInfo() != null)
            {
                if (ControllerAndActionInfo().GetUpperBound(0) >= 2) {
                    return ControllerAndActionInfo()[2];
                }
            }

            return "Index";
            
            
           // return ( ControllerAndActionInfo().GetUpperBound(0) >= 2) ? ControllerAndActionInfo()[2] : "Index";
        }

        public static string ReferrerController()
        {
            if (ControllerAndActionInfo() != null)
            {
                if (ControllerAndActionInfo().GetUpperBound(0) >= 1)
                {
                    return ControllerAndActionInfo()[1];
                }
            }

            return "Home";



         //   return ( ControllerAndActionInfo().GetUpperBound(0) >= 1) ? ControllerAndActionInfo()[1] : "Home";
        }
    }
}