using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

//dn3
public static class EnumHelper
{
    public static string GetDisplay(this Enum value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        DisplayAttribute[] attributes = (DisplayAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false);
        if (attributes.Count() != 1)
        {
            throw new ArgumentOutOfRangeException();
        }

        string resourceId = attributes[0].Name;
        Type type = typeof(MVC.App_Resources.Translate);
        PropertyInfo nameProperty = type.GetProperty(resourceId, BindingFlags.Static | BindingFlags.Public);
        object result = nameProperty.GetValue(nameProperty.DeclaringType, null);

        return (result != null) ? result.ToString() : string.Empty;
    }

    public static Dictionary<int, string> GetDisplayValues(this Type enumType)
    {
        var enumValues = new Dictionary<int, string>();
        int i = 0;
        foreach (Enum value in Enum.GetValues(enumType))
        {
            //enumValues.Add(value.ToString(), value.GetDisplay());
            enumValues.Add(i, value.GetDisplay());
            i++;
        }
        return enumValues;
    }
}
