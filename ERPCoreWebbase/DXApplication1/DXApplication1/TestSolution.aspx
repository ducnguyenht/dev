<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TestSolution.aspx.cs" Inherits="WebModule.TestSolution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        // Create the CurrentCulture object
        var cultureObject = Sys.CultureInfo.CurrentCulture;
        // Get the name field of the CurrentCulture object
        var cultureName = cultureObject.name;
        // Get the dateTimeFormat object from the CurrentCulture object
        var dtfObject = cultureObject.dateTimeFormat;
        // Create an array of format types
        var myArray = ['AMDesignator', 'Calendar', 'DateSeparator', 'FirstDayOfWeek',
                   'CalendarWeekRule', 'FullDateTimePattern', 'LongDatePattern',
                   'LongTimePattern', 'MonthDayPattern', 'PMDesignator', 'RFC1123Pattern',
                   'ShortDatePattern', 'ShortTimePattern', 'SortableDateTimePattern',
                   'TimeSeparator', 'UniversalSortableDateTimePattern', 'YearMonthPattern',
                   'AbbreviatedDayNames', 'ShortestDayNames', 'DayNames',
                   'AbbreviatedMonthNames', 'MonthNames', 'IsReadOnly',
                   'NativeCalendarName', 'AbbreviatedMonthGenitiveNames',
                   'MonthGenitiveNames'];

        var result = 'Culture Name: ' + cultureName;
        for (var i = 0, l = myArray.length; i < l; i++) {
            var arrayVal = myArray[i];
            if (typeof (arrayVal) !== 'undefined') {
                result += "<tr><td>" + arrayVal + "</td><td>" + eval("dtfObject." + arrayVal) + '</td></tr>';
            }
        }
        var resultHeader = "<tr><td><b>FormatType</b></td><td><b>FormatValue</b></td></tr>"
        $get('Label1').innerHTML = "<table border=1>" + resultHeader + result + "</table>";

        var d = new Date();
        $get('Label2').innerHTML = "<p/><h3>dateTimeFormat Example: </h3>" +
    d.localeFormat(Sys.CultureInfo.CurrentCulture.dateTimeFormat.FullDateTimePattern);
 </script>
</asp:Content>
