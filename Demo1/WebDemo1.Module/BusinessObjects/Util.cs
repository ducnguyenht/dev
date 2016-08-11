using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using System.Drawing;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using DevExpress.XtraPrinting.Native;

namespace Namoly.Module.Common.Utility
{
    public static class Util
    {
        public static int GetDaysInYear(int year)
        {
            var thisYear = new DateTime(year, 1, 1);
            var nextYear = new DateTime(year + 1, 1, 1);

            return (nextYear - thisYear).Days;
        }

        public static DateTime GetFistDateOfMonth(int month, int year)
        {
            int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);
            DateTime startOfMonth = new DateTime(year, month, 1);
            DateTime endOfMonth = new DateTime(year, month, numberOfDaysInMonth);

            return startOfMonth;
        }

        public static DateTime GetLastDateOfMonth(int month, int year)
        {
            int numberOfDaysInMonth = DateTime.DaysInMonth(year, month);
            DateTime startOfMonth = new DateTime(year, month, 1);
            DateTime endOfMonth = new DateTime(year, month, numberOfDaysInMonth);

            return endOfMonth;
        }

        public static IEnumerable<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d));
        }

        public static IEnumerable<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)); // Map each day to a date
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static decimal RoundWith100VND(decimal value)
        {
            return value = Decimal.Round(value, 0);
            //decimal modulo = value % 100;
            //decimal divison = Math.Truncate(value / 100);
            //if (modulo == 0)
            //    return value;
            //else if (modulo != 0)
            //{
            //    return (divison + 1) * 100;
            //}
            //return 0;
        }

        public static DateTime GetServerTime(Session session)
        {
            CriteriaOperator funcNow = new FunctionOperator(FunctionOperatorType.Now);
            DateTime serverTime = (DateTime)session.Evaluate(typeof(XPObjectType), funcNow, null);
            return serverTime;
        }

        public static DateTime GetServerTime(XPObjectSpace xpObjectSpace)
        {
            CriteriaOperator funcNow = new FunctionOperator(FunctionOperatorType.Now);
            DateTime serverTime = (DateTime)xpObjectSpace.Session.Evaluate(typeof(XPObjectType), funcNow, null);
            return serverTime;
        }

        public static DateTime RoundDateToSecond(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day,
                                dt.Hour, dt.Minute, dt.Second);
        }

        public static Color GetReadableForeColor(Color bg)
        {
            int nThreshold = 105;
            int bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) +
                                          (bg.B * 0.114));

            Color foreColor = (255 - bgDelta < nThreshold) ? Color.Black : Color.White;
            return foreColor;
        }
    }

    public class DistinctCountFunction : ICustomFunctionOperator, ICustomFunctionOperatorBrowsable, ICustomFunctionOperatorFormattable
    {
        static DistinctCountFunction Instance = new DistinctCountFunction();
        public static void Register()
        {
            CriteriaOperator.RegisterCustomFunction(Instance);
        }
        public static void Unregister()
        {
            CriteriaOperator.UnregisterCustomFunction(Instance);
        }
        public object Evaluate(params object[] operands) { throw new NotImplementedException(); }
        public string Name { get { return "DistinctCount"; } }
        public Type ResultType(params Type[] operands)
        {
            if (operands == null || operands.Length == 0) return typeof(object);
            return operands[0];
        }
        public FunctionCategory Category { get { return FunctionCategory.Logical; } }
        public string Description { get { return "Distinct count function"; } }
        public bool IsValidOperandCount(int count) { return count == 1; }
        public bool IsValidOperandType(int operandIndex, int operandCount, Type type) { return operandIndex == 0 && operandCount == 1; }
        public int MaxOperandCount { get { return 1; } }
        public int MinOperandCount { get { return 1; } }
        public string Format(Type providerType, params string[] operands)
        {
            if (operands == null || operands.Length == 0) return string.Empty;
            if (providerType == typeof(MSSqlConnectionProvider))
            {
                return string.Format("Count(distinct {0})", operands[0]);
            }
            return "0";
        }
    }

    public struct Fraction
    {
        public Fraction(decimal _Numerator, decimal _Denominator)
        {
            Numerator = _Numerator;
            Denominator = _Denominator;
        }

        public Fraction(decimal numerator, Fraction denominator)
        {
            //divide the numerator by the denominator fraction
            this = new Fraction(numerator, 1) / denominator;
        }

        public Fraction(Fraction numerator, decimal denominator)
        {
            //multiply the numerator fraction by 1 over the denominator
            this = numerator * new Fraction(1, denominator);
        }

        public Fraction(Fraction fraction)
        {
            this.Numerator = fraction.Numerator;
            this.Denominator = fraction.Denominator;
        }

        public decimal Quotient
        {
            get
            {
                return Numerator / Denominator;
            }
        }

        public static Fraction operator +(Fraction fraction1, Fraction fraction2)
        {
            //Check if either fraction is zero
            if (fraction1.Denominator == 0)
                return fraction2;
            else if (fraction2.Denominator == 0)
                return fraction1;

            //Get Least Common Denominator
            decimal lcd = getLCD(fraction1.Denominator, fraction2.Denominator);

            //Transform the fractions
            fraction1 = fraction1.ToDenominator(lcd);
            fraction2 = fraction2.ToDenominator(lcd);

            //Return sum
            return new Fraction(fraction1.Numerator + fraction2.Numerator, lcd).GetReduced();
        }

        public static Fraction operator -(Fraction fraction1, Fraction fraction2)
        {
            //Get Least Common Denominator
            decimal lcd = getLCD(fraction1.Denominator, fraction2.Denominator);

            //Transform the fractions
            fraction1 = fraction1.ToDenominator(lcd);
            fraction2 = fraction2.ToDenominator(lcd);

            //Return difference
            return new Fraction(fraction1.Numerator - fraction2.Numerator, lcd).GetReduced();
        }

        public static Fraction operator *(Fraction fraction1, Fraction fraction2)
        {
            decimal numerator = fraction1.Numerator * fraction2.Numerator;
            decimal denomenator = fraction1.Denominator * fraction2.Denominator;

            return new Fraction(numerator, denomenator).GetReduced();
        }

        public static Fraction operator /(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(fraction1 * fraction2.GetReciprocal()).GetReduced();
        }

        private static decimal getLCD(decimal a, decimal b)
        {
            //Return the Least Common Denominator between two integers
            return (a * b) / getGCD(a, b);
        }

        private static decimal getGCD(decimal a, decimal b)
        {
            //Drop negative signs
            a = Math.Abs(a);
            b = Math.Abs(b);

            //Return the greatest common denominator between two integers
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            if (a == 0)
                return b;
            else
                return a;
        }

        public Fraction ToDenominator(decimal targetDenominator)
        {
            //Multiply the fraction by a factor to make the denominator
            //match the target denominator
            Fraction modifiedFraction = this;

            //Cannot reduce to smaller denominators
            if (targetDenominator < this.Denominator)
                return modifiedFraction;

            //The target denominator must be a factor of the current denominator
            if (targetDenominator % this.Denominator != 0)
                return modifiedFraction;

            if (this.Denominator != targetDenominator)
            {
                decimal factor = targetDenominator / this.Denominator;
                modifiedFraction.Denominator = targetDenominator;
                modifiedFraction.Numerator *= factor;
            }

            return modifiedFraction;
        }

        public Fraction GetReduced()
        {
            //Reduce the fraction to lowest terms
            Fraction modifiedFraction = this;

            //While the numerator and denominator share a greatest common denominator,
            //keep dividing both by it
            decimal gcd = 0;
            while (Math.Abs(gcd = getGCD(modifiedFraction.Numerator, modifiedFraction.Denominator)) != 1)
            {
                modifiedFraction.Numerator /= gcd;
                modifiedFraction.Denominator /= gcd;
            }

            //Make sure only a single negative sign is on the numerator
            if (modifiedFraction.Denominator < 0)
            {
                modifiedFraction.Numerator = -this.Numerator;
                modifiedFraction.Denominator = -this.Denominator;
            }

            return modifiedFraction;
        }

        public Fraction GetReciprocal()
        {
            //Flip the numerator and the denominator
            return new Fraction(this.Denominator, this.Numerator);
        }

        public decimal Numerator;
        public decimal Denominator;
    }

    public static class SinglePageHelper
    {
        public static void GenerateSinglePageReport(XtraReport report, bool createDocument)
        {
            float sumHeight = 0;

            if (createDocument)
                report.CreateDocument();

            XtraPageSettingsBase pageSettings = report.PrintingSystem.PageSettings;

            XtraPageSettingsBase.ApplyPageSettings(pageSettings, PaperKind.Custom,
                new Size(pageSettings.Bounds.Width, pageSettings.Bounds.Height * report.Pages.Count),
                pageSettings.Margins, pageSettings.MinMargins, pageSettings.Landscape);

            NestedBrickIterator iterator = new NestedBrickIterator(report.Pages[0].InnerBricks);
            while (iterator.MoveNext())
                if (iterator.CurrentBrick is VisualBrick)
                {
                    VisualBrick brick = (VisualBrick)iterator.CurrentBrick;
                    float bottomPos = brick.Rect.Bottom;

                    if (bottomPos > sumHeight)
                        sumHeight = bottomPos;
                }

            sumHeight = GraphicsUnitConverter.Convert(sumHeight, GraphicsUnit.Document, GraphicsUnit.Inch) * 100;

            int totalPageHeight = pageSettings.Margins.Top + pageSettings.Margins.Bottom + Convert.ToInt32(sumHeight);

            XtraPageSettingsBase.ApplyPageSettings(pageSettings, PaperKind.Custom,
                new Size(pageSettings.Bounds.Width, totalPageHeight),
                pageSettings.Margins, pageSettings.MinMargins, pageSettings.Landscape);
        }

        public static void GenerateSinglePageReport(XtraReport report, bool createDocument, int additionHeight)
        {
            float sumHeight = 0;

            if (createDocument)
            {
                report.CreateDocument();
            }

            XtraPageSettingsBase pageSettings = report.PrintingSystem.PageSettings;

            XtraPageSettingsBase.ApplyPageSettings(pageSettings, PaperKind.Custom,
                new Size(pageSettings.Bounds.Width, pageSettings.Bounds.Height * report.Pages.Count),
                pageSettings.Margins, pageSettings.MinMargins, pageSettings.Landscape);

            NestedBrickIterator iterator = new NestedBrickIterator(report.Pages[0].InnerBricks);
            while (iterator.MoveNext())
                if (iterator.CurrentBrick is VisualBrick)
                {
                    VisualBrick brick = (VisualBrick)iterator.CurrentBrick;
                    float bottomPos = brick.Rect.Bottom;

                    if (bottomPos > sumHeight)
                        sumHeight = bottomPos;
                }

            sumHeight = GraphicsUnitConverter.Convert(sumHeight, GraphicsUnit.Document, GraphicsUnit.Inch) * 100;

            int totalPageHeight = pageSettings.Margins.Top + pageSettings.Margins.Bottom + Convert.ToInt32(sumHeight);
            totalPageHeight += additionHeight;

            XtraPageSettingsBase.ApplyPageSettings(pageSettings, PaperKind.Custom,
                new Size(pageSettings.Bounds.Width, totalPageHeight),
                pageSettings.Margins, pageSettings.MinMargins, pageSettings.Landscape);
        }
    }
}
