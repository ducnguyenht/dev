using MVC.App_Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public enum LoginStatus
{//dn3
    [Display(Name = "LoginStatus_Requested", ResourceType = typeof(Translate))]
    Requested = 0,
    [Display(Name = "LoginStatus_EmailConfirmed", ResourceType = typeof(Translate))]
    EmailConfirmed = 1,
    [Display(Name = "LoginStatus_Active", ResourceType = typeof(Translate))]
    Active = 2,
    [Display(Name = "LoginStatus_InActive", ResourceType = typeof(Translate))]
    InActive = 3
}