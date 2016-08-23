using MVC.App_Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
public class BindRex : System.ComponentModel.DisplayNameAttribute
{
    public BindRex()
        : base()
    {
    }
    public BindRex(string resourceId)
        : base(resourceId)
    {
    }

    public override string DisplayName { get { return GetMessageFromResource(base.DisplayName); } }

    private static string GetMessageFromResource(string resourceId)
    {
        Type type = typeof(Translate);
        PropertyInfo nameProperty = type.GetProperty(resourceId, BindingFlags.Static | BindingFlags.Public);
        object result = nameProperty.GetValue(nameProperty.DeclaringType, null);
        return result.ToString();
    }
}
