﻿using System.Web;
using System.Web.Mvc;

namespace Travel_Request_System_EF
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
