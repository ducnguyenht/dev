using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.BO.Accounting;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting.UserControl
{
    public partial class ucTransactionHistory : System.Web.UI.UserControl
    {
        Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            GeneralJournalXPO.Session = session;
            //GeneralLedgerXPO.Session = session;            
            //AccountXPO.Session = session;


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingBO ABO = new AccountingBO();
            //XPQuery<GeneralJournal> journal = session.Query<GeneralJournal>();
            //Transaction tr = session.GetObjectByKey<Transaction>(Guid.Parse("fef74738-049d-45a4-b8c5-074431dc4431"));
            //var a = journal.Where(r => r.TransactionId == tr).FirstOrDefault();
            //List<object> TransList = ABO.GetFullDesTransactionList(session);
            //ASPxGridView2.DataSource = TransList;
            //ASPxGridView2.KeyFieldName = "TransactionId";
            //ASPxGridView2.DataBind();

            //List<object> source1 = ABO.getFullLedgerList(session);
            //ASPxGridView1.DataSource = source1;
            //ASPxGridView1.DataBind();

            List<object> source1 = ABO.GetFullDesTransactionList(session);
            ASPxGridView2.DataSource = source1;
            ASPxGridView2.KeyFieldName = "ID";
            ASPxGridView2.DataBind();

        }

        protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            //Account account = ASPxTreeList1.FocusedNode.DataItem as Account;            
            //CriteriaOperator critera = new BinaryOperator("AccountId!Key", account.AccountId);            
            //GeneralLedgerXPO.Criteria = critera.ToString();
            //ASPxGridView1.DataBind();
        }

        protected void ASPxGridView2_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grdTransactionDetail = (ASPxGridView)sender;
            Session["TransactionId"] = grdTransactionDetail.GetMasterRowKeyValue();
        }

        protected void ASPxGridView2_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {

        }
    }
}