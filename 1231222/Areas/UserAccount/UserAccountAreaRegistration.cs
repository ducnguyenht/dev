﻿using System.Web.Mvc;

namespace MVC.Areas.UserAccount
{
    public class UserAccountAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserAccount";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserAccount_default",
                "UserAccount/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(//dn3
                name: "UserAccounts",
                url: "user-accounts",
                defaults: new { controller = "UserAccounts", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}