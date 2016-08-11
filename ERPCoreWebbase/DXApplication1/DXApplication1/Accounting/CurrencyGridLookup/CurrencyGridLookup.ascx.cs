using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Accounting;
using DevExpress.Web.ASPxGridLookup;
using System.ComponentModel;
using DevExpress.Web.ASPxEditors;

namespace WebModule.Accounting.CurrencyGridLookup
{
    [ParseChildren(true), PersistChildren(false)]
    public partial class CurrencyGridLookup : System.Web.UI.UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [AutoFormatEnable]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ASPxGridLookup GridLookup
        {
            get { return gridlookupCurrency; }
        }

        [AutoFormatEnable]
        [Category("Validation")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ValidationSettings ValidationSettings
        {
            get
            {
                return gridlookupCurrency.ValidationSettings;
            }
        }

        [Category("Client-Side")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [AutoFormatDisable]
        [Themeable(false)]
        [MergableProperty(false)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridLookupClientSideEvents ClientSideEvents
        {
            get
            {
                return gridlookupCurrency.ClientSideEvents;
            }
        }

        public string ClientInstanceName
        {
            get
            {
                return gridlookupCurrency.ClientInstanceName;
            }
            set
            {
                gridlookupCurrency.ClientInstanceName = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return gridlookupCurrency.IsValid;
            }
            set
            {
                gridlookupCurrency.IsValid = value;
            }
        }

        public object Value
        {
            get
            {
                return gridlookupCurrency.Value;
            }
            set
            {
                gridlookupCurrency.Value = value;
            }
        }

        public string Text
        {
            get
            {
                return gridlookupCurrency.Text;
            }
            set
            {
                gridlookupCurrency.Text = value;
            }
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsCurrecy.Session = session;
        }

        public override void Dispose()
        {
            base.Dispose();
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ResetToDefault()
        {
            CurrencyBO currencyBO = new CurrencyBO();
            NAS.DAL.Accounting.Currency.Currency currency = currencyBO.GetDefaultCurrency(session);
            if (currency != null)
            {
                gridlookupCurrency.Text = currency.Code;
            }
            else
            {
                gridlookupCurrency.GridView.Selection.UnselectAll();
            }
        }

        public void SetSelectedCurrencyByKey(Guid currencyId)
        {
            NAS.DAL.Accounting.Currency.Currency currency =
                session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(currencyId);
            if (currency != null)
            {
                gridlookupCurrency.Text = currency.Code;
            }
            else
            {
                gridlookupCurrency.GridView.Selection.UnselectAll();
            }
        }

        public NAS.DAL.Accounting.Currency.Currency GetSelectedCurrency(Session _session)
        {
            var selectedCurrencyId = GetSelectedCurrencyKey();
            
            if (selectedCurrencyId == null)
                return null;

            return _session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(selectedCurrencyId);
        }

        public object GetSelectedCurrencyKey()
        {
            return gridlookupCurrency.Value;
        }
    }
}