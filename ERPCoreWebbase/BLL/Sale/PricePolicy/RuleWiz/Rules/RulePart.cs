// Copyright 2007 Herre Kuijpers - <herre@xs4all.nl>
//
// This source file(s) may be redistributed, altered and customized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace NAS.BO.Sale.PricePolicy.RulesWiz.Rules
{
    public enum KEYTYPE
    {
        SUPPLIER, MANUFACTURER, ITEMUNIT, CUSTOMER
    }


    public abstract class RuleParts<T> : List<T>, ICloneable where T : RulePart
    {
        // finds a rulecondition by format.
        // it is assumed that the text format is unique 
        public T FindByKeyType(KEYTYPE key)
        {
            foreach (RulePart rp in this)
            {
                if (rp.KeyType == key)
                    return (T) rp;
            }
            return null;
        }

        public bool IsComplete
        {
            get
            {
                foreach (RulePart rp in this)
                {
                    if (!rp.HasValue)
                        return false;
                }
                return true;
            }
        }

        #region ICloneable Members
        public abstract object Clone();
        #endregion
    }

    public abstract class RulePart : ICloneable
    {
        public KEYTYPE KeyType;
        //public string TextFormat;
        //public string DefaultValueText;
        public object Value;

        public virtual bool HasValue
        {
            get { return Value != null; }
        }

        //public virtual string ValueText
        //{
        //    get
        //    {
        //        return HasValue ? Value.ToString() : DefaultValueText;
        //    }
        //}

        //public virtual string Text
        //{
        //    get
        //    {
        //        return string.Format(TextFormat, ValueText);
        //    }
        //}

        //public virtual int ValueStart
        //{
        //    get
        //    {
        //        return TextFormat.IndexOf("{0}");
        //    }
        //}

        /// <summary>
        /// allows user interaction to update the Value of this object
        /// </summary>
        /// <param name="sender">calling object, e.g. the wizard form</param>
        /// <returns>true if update succesful, false otherwise</returns>
        public virtual bool UpdateValue(object sender) { 
            return false;  
        }

        #region ICloneable Members
        public abstract object Clone();
        #endregion
    }
}
