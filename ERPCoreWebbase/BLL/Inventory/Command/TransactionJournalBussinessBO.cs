using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.CMS.ObjectDocument;
using DevExpress.Xpo;
using NAS.DAL.Inventory.Command.CommanDynamicField;
using NAS.DAL;
using DevExpress.Data.Filtering;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Inventory.Command;
using Utility;
using NAS.DAL.Invoice;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Accounting.JournalAllocation;
using NAS.BO.System.ArtifactCode;
using NAS.DAL.Nomenclature.Inventory;
using NAS.ETLBO.System.Object;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Inventory.Audit;
using NAS.BO.Accounting;
using NAS.DAL.Inventory.Ledger;
using NAS.BO.Inventory.Ledger;
using NAS.DAL.Nomenclature.Item;

namespace NAS.BO.Inventory.Command
{
    public class SOURCE_ARTIFACT
    {
        public const string SALE_INVOICE = "SALE_INVOICE";
        public const string PURCHASING_INVOICE = "PURCHASING_INVOICE";        
        public const string INVENTORYAUDIT_ARTIFACT = "INVENTORYAUDIT_INVOICE";
        public const string NO_ARTIFACT = "NO_ARTIFACT";
    }

    public class INVENTORY_COMMAND_TYPE
    {
        public const char OUT = 'O';
        public const char IN = 'I';
        public const char MOVE = 'M';
    }

    public partial class InventoryCommandBO
    {
        private BusinessObjectBO BusinessObjectBO = new BusinessObjectBO();
        private AccountingPeriodBO AccountingPeriodBO = new AccountingPeriodBO();  

        public NAS.DAL.Inventory.Command.InventoryCommand CreateInventoryCommandByInventoryAudit(
        UnitOfWork uow,
        InventoryAuditArtifact ArtifactId, 
        DAL.CMS.ObjectDocument.ObjectTypeEnum _Type, 
        char _CommandType)        
        {
            NAS.DAL.Inventory.Command.InventoryCommand command = new DAL.Inventory.Command.InventoryCommand(uow);
            command.Code = ArtifactId.Code;
            command.CreateDate = DateTime.Now;
            command.IssueDate = DateTime.Now;
            command.Name = command.Code;            
            command.RowStatus = Utility.Constant.ROWSTATUS_TEMP;
            command.ParentInventoryCommandId = ArtifactId;

            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            

            if (_CommandType == INVENTORY_COMMAND_TYPE.OUT)
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.OUT;
                command.Description = string.Format("Điều chỉnh giảm theo phiếu kiểm kê số '{0}'", ArtifactId.Code);
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_OUTPUT);
            }
            else 
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.IN;
                command.Description = string.Format("Điều chỉnh tăng theo phiếu kiểm kê số '{0}'", ArtifactId.Code);
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_INPUT);
            }
            command.ParentInventoryCommandId = ArtifactId;
            uow.FlushChanges();

            NAS.DAL.CMS.ObjectDocument.ObjectType type
                = NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(uow, _Type);

            PopulateCMSObjecForInventoryObject<NAS.DAL.Inventory.Command.InventoryCommand>(uow,
                command.InventoryCommandId,
                _Type);
            PopulateActorsForInventoryCommand(uow, command.InventoryCommandId);

            return command;
        }


        /// <summary>
        /// Tạo Phiếu kho từ chứng từ mua/bán
        /// </summary>
        /// <param name="ArtifactId"></param>
        /// <param name="_Type"></param>
        /// <returns></returns>
        public NAS.DAL.Inventory.Command.InventoryCommand CreateInventoryCommandByBill(
            UnitOfWork uow,
            Guid ArtifactId, 
            DAL.CMS.ObjectDocument.ObjectTypeEnum _Type)
        {
            NAS.DAL.Invoice.Bill invoice = uow.GetObjectByKey<NAS.DAL.Invoice.Bill>(ArtifactId);
            if (invoice == null)
                throw new Exception("The Bill is not exist in system");

            if (invoice.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
            {
                if (_Type == ObjectTypeEnum.INVENTORY_OUT)
                    throw new Exception("Phiếu bán đã ghi sổ nên không thể thao tác xuất kho");
                else if (_Type == ObjectTypeEnum.INVENTORY_IN)
                    throw new Exception("Phiếu mua đã ghi sổ nên không thể thao tác nhập kho");
            }

            if (invoice.RowStatus != Utility.Constant.ROWSTATUS_ACTIVE)
            {
                if (_Type == ObjectTypeEnum.INVENTORY_OUT)
                    throw new Exception("Phiếu bán chưa được lưu nên không thể tiến hành xuất kho");
                else if (_Type == ObjectTypeEnum.INVENTORY_IN)
                    throw new Exception("Phiếu mua chưa được lưu nên không thể tiến hành nhập kho");
            }

            NAS.DAL.Inventory.Command.InventoryCommand command = new DAL.Inventory.Command.InventoryCommand(uow);
            command.Code = string.Empty;
            command.CreateDate = DateTime.Now;
            command.IssueDate = DateTime.Now;
            command.Name = command.Code;
            command.RowStatus = Utility.Constant.ROWSTATUS_TEMP;

            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            if (invoice is NAS.DAL.Invoice.SalesInvoice)
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.OUT;
                command.Description = "Xuất hàng cho khách";
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_OUTPUT);
            }
            else if (invoice is NAS.DAL.Invoice.PurchaseInvoice)
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.IN;
                command.Description = "Nhập hàng mua về";
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_INPUT);
            }

            uow.FlushChanges();

            PopulateCMSObjecForInventoryObject<NAS.DAL.Inventory.Command.InventoryCommand>(uow,
                command.InventoryCommandId,
                _Type);

            ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();
            ObjectTypeCustomField ArtifactTypeCustomField = null;
            ObjectTypeCustomField CustomerTypeCustomField = null;
            ObjectCustomField ArtifactCustomField = null;
            ObjectCustomField CustomerCustomField = null;
            List<Guid> Artifacts = new List<Guid>();
            Artifacts.Add(ArtifactId);
            List<Guid> Customers = new List<Guid>();

            if (invoice.SourceOrganizationId != null)
                Customers.Add(invoice.SourceOrganizationId.OrganizationId);

            if (_Type == ObjectTypeEnum.INVENTORY_OUT)
            {
                /////////////////Init data cho field mã chứng từ bán////////////////////////////////////////
                ArtifactTypeCustomField =
                    ObjectTypeCustomField.GetDefault(uow, DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);

                ArtifactCustomField = command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields
                .Where(r => r.ObjectTypeCustomFieldId.Equals(ArtifactTypeCustomField)).First();
                        
                objectCustomFieldBO.UpdatePredefinitionData(
                    ArtifactCustomField.ObjectCustomFieldId,
                    Artifacts,
                    PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_SALE,
                    CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY);

                ///////////////Init data cho Field khách hàng////////////////////////////////////////
                if (invoice.SourceOrganizationId != null)
                {
                    CustomerTypeCustomField =
                        ObjectTypeCustomField.GetDefault(uow, DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_CLIENT);
                    CustomerCustomField = command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields
                    .Where(r => r.ObjectTypeCustomFieldId.Equals(CustomerTypeCustomField)).First();
                    objectCustomFieldBO.UpdatePredefinitionData(
                        CustomerCustomField.ObjectCustomFieldId,
                        Customers,
                        PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                        CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY);
                }
            }
            else if (_Type == ObjectTypeEnum.INVENTORY_IN)
            {
                /////////////////Init data cho field mã chứng từ bán////////////////////////////////////////
                ArtifactTypeCustomField =
                    ObjectTypeCustomField.GetDefault(uow, DefaultObjectTypeCustomFieldEnum.INVENTORY_IN_PURCHASE_INVOICE);
                //ArtifactCustomField = command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields
                //.Where(r => r.ObjectTypeCustomFieldId.Code.Equals(ArtifactTypeCustomField.Code)).First();

                foreach (ObjectCustomField cf in command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields)
                {
                    if (cf.ObjectTypeCustomFieldId.Code != null && cf.ObjectTypeCustomFieldId.Code.Equals(ArtifactTypeCustomField.Code))
                        ArtifactCustomField = cf;
                }

                objectCustomFieldBO.UpdatePredefinitionData(
                    ArtifactCustomField.ObjectCustomFieldId,
                    Artifacts,
                    PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_INVOICE_PURCHASE,
                    CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY);

                /////////////////Init data cho Field khách hàng////////////////////////////////////////
                if (invoice.SourceOrganizationId != null)
                {
                    CustomerTypeCustomField = ObjectTypeCustomField.GetDefault(uow, DefaultObjectTypeCustomFieldEnum.INVENTORY_IN_CLIENT);
                    //CustomerCustomField = command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields
                    //.Where(r => r.ObjectTypeCustomFieldId.Equals(CustomerTypeCustomField)).First();

                    foreach (ObjectCustomField cf in command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields)
                    {
                        if (cf.ObjectTypeCustomFieldId.Code != null && cf.ObjectTypeCustomFieldId.Code.Equals(CustomerTypeCustomField.Code))
                            CustomerCustomField = cf;
                    }

                    objectCustomFieldBO.UpdatePredefinitionData(
                        CustomerCustomField.ObjectCustomFieldId,
                        Customers,
                        PredefinitionCustomFieldTypeEnum.SINGLE_CHOICE_LIST_CUSTOMER,
                        CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_READONLY);
                }
            }
            else if (_Type == ObjectTypeEnum.INVENTORY_MOVE)
            {
                //Chưa rõ   
            }
            PopulateActorsForInventoryCommand(uow, command.InventoryCommandId);
            return command;
        }

        public NAS.DAL.Inventory.Command.InventoryCommand CreateInventoryCommandByMovingArtifact(
            UnitOfWork uow,
            Guid MovingInventoryCommandId,
            DAL.CMS.ObjectDocument.ObjectTypeEnum _Type)
        {
            NAS.DAL.Inventory.Command.InventoryCommand MovingCommand = uow.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(MovingInventoryCommandId);
            if (MovingCommand == null)
                throw new Exception("The InventoryCommand is not exist in system");

            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();

            NAS.DAL.Inventory.Command.InventoryCommand command = new DAL.Inventory.Command.InventoryCommand(uow);
            command.Code = string.Empty;
            command.CreateDate = DateTime.Now;
            command.IssueDate = DateTime.Now;
            command.Name = command.Code;
            command.ParentInventoryCommandId = MovingCommand;
            command.RowStatus = Utility.Constant.ROWSTATUS_TEMP;

            if (_Type == ObjectTypeEnum.INVENTORY_OUT)
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.OUT;
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_OUTPUT);
                command.Description = "Xuất chuyển kho nội bộ";
            }
            else if (_Type == ObjectTypeEnum.INVENTORY_IN)
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.IN;
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_INPUT);
                command.Description = "Nhập chuyển kho nội bộ";
            }
            uow.FlushChanges();

            NAS.DAL.CMS.ObjectDocument.ObjectType type
                = NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(uow, _Type);

            PopulateCMSObjecForInventoryObject<NAS.DAL.Inventory.Command.InventoryCommand>(uow,
                command.InventoryCommandId,
                _Type);

            PopulateActorsForInventoryCommand(uow, command.InventoryCommandId);
            return command;
        }

        /// <summary>
        /// Tạo Phiếu kho
        /// </summary>
        /// <param name="CMSObjectType"></param>
        /// <returns></returns>
        public NAS.DAL.Inventory.Command.InventoryCommand CreateInventoryNewCommand(
            UnitOfWork uow,
            DAL.CMS.ObjectDocument.ObjectTypeEnum CMSObjectType)
        {
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            NAS.DAL.Inventory.Command.InventoryCommand command = new DAL.Inventory.Command.InventoryCommand(uow);
            command.Code = string.Empty;
            command.CreateDate = DateTime.Now;
            command.IssueDate = DateTime.Now;
            command.Name = command.Code;
            if (CMSObjectType == ObjectTypeEnum.INVENTORY_IN){
                command.CommandType = INVENTORY_COMMAND_TYPE.IN;
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_INPUT);
            }
            else if (CMSObjectType == ObjectTypeEnum.INVENTORY_OUT)
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.OUT;
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_OUTPUT);
            }
            else
            {
                command.CommandType = INVENTORY_COMMAND_TYPE.MOVE;
                command.Description = "Chuyển kho nội bộ";
                command.Code = artifactCodeRuleBO.GetArtifactCodeOfArtifactType(ArtifactTypeEnum.INVENTORY_MOVE);
            }

            command.RowStatus = Utility.Constant.ROWSTATUS_TEMP;
            uow.FlushChanges();
            NAS.DAL.CMS.ObjectDocument.ObjectType type = NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(uow, DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_OUT);
            PopulateCMSObjecForInventoryObject<NAS.DAL.Inventory.Command.InventoryCommand>(uow,
                command.InventoryCommandId,
                CMSObjectType);
            //uow.CommitChanges();
            PopulateActorsForInventoryCommand(uow, command.InventoryCommandId);
            return command;
        }
        /// <summary>
        /// Tạo InventoryCommandItemTransaction
        /// </summary>
        /// <param name="_Session"></param>
        /// <param name="_Code"></param>
        /// <param name="_IssueDate"></param>
        /// <param name="_Description"></param>
        /// <param name="CMSObjectType"></param>
        public InventoryCommandItemTransaction createInventoryCommandItemTransaction(
            UnitOfWork _Uow,
            string _Code, 
            DateTime _IssueDate,
            string _Description, 
            DAL.CMS.ObjectDocument.ObjectTypeEnum CMSObjectType,
            Guid _InventoryCommandId, 
            Guid InventoryId, out string errormsg)
        {
            try
            {
                InventoryCommand command = _Uow.GetObjectByKey<InventoryCommand>(_InventoryCommandId);
                
                if (command == null)
                    throw new Exception("The InventoryCommand is not exist in system");

                if (command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                    throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");
                
                NAS.DAL.Inventory.Command.InventoryCommandItemTransaction transaction = new DAL.Inventory.Command.InventoryCommandItemTransaction(_Uow);
                transaction.Code = _Code;
                transaction.IssueDate = _IssueDate;
                transaction.CreateDate = DateTime.Now;
                transaction.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                transaction.Description = _Description;
                transaction.InventoryCommandId = command;
                _Uow.FlushChanges();
                PopulateCMSObjecForInventoryObject<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>(_Uow,
                    transaction.InventoryTransactionId,
                    CMSObjectType);

                ObjectBO objectBO = new ObjectBO();
                    
                InventoryCommandObject commandObject = command.InventoryCommandObjects.FirstOrDefault();
                InventoryTransactionObject transactionObject = transaction.InventoryTransactionObjects.FirstOrDefault();

                if (commandObject != null)
                {
                    //Copy artifact's data to cms object of transaction
                    objectBO.CopyReadOnlyCustomFieldData(
                        commandObject.ObjectId.ObjectId,
                        transactionObject.ObjectId.ObjectId);
                }
                errormsg = string.Empty;
                if (command.CommandType.Equals(INVENTORY_COMMAND_TYPE.OUT))
                    errormsg = AutoGenerateInventoryJournalForOutputCommand(_Uow, transaction.InventoryTransactionId, InventoryId);
                else if (command.CommandType.Equals(INVENTORY_COMMAND_TYPE.IN))
                    AutoGenerateInventoryJournalForInputCommand_NoneMovingCase(_Uow, transaction.InventoryTransactionId);
                               
                return transaction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Xóa Phiếu kho
        /// </summary>
        /// <param name="_Session"></param>
        /// <param name="_InventoryCommandId"></param>
        public void DeleteLogicInventoryCommand(Session _Session, Guid _InventoryCommandId)
        {
            try
            {
                NAS.DAL.Inventory.Command.InventoryCommand command =
                    _Session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(_InventoryCommandId);

                XPCollection<NAS.DAL.Inventory.Command.InventoryCommand> childCommands = new
                    XPCollection<NAS.DAL.Inventory.Command.InventoryCommand>(_Session,
                        CriteriaOperator.And(
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                            new BinaryOperator("ParentInventoryCommandId", command)));

                if (command != null)
                {
                    if (command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        throw new Exception("Phiếu kho này đã ghi sổ nên không thể xóa!");
                    command.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    command.Save();

                    if (command.InventoryCommandItemTransactions != null && command.InventoryCommandItemTransactions.Count > 0){
                        foreach (InventoryCommandItemTransaction transaction in command.InventoryCommandItemTransactions)
                        { 
                            transaction.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                            transaction.Save();
                        }
                    }
                }

                if (childCommands != null && childCommands.Count > 0)
                {
                    foreach (InventoryCommand childCommand in childCommands)
                    {
                        DeleteLogicInventoryCommand(_Session, childCommand.InventoryCommandId);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Xóa Phiếu kho
        /// </summary>
        /// <param name="_Session"></param>
        /// <param name="_InventoryCommandId"></param>
        public void DeleteLogicInventoryTransaction(Session _Session, Guid _InventoryTransactionId)
        {
            try
            {
                NAS.DAL.Inventory.Command.InventoryCommandItemTransaction transaction =
                    _Session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>(_InventoryTransactionId);

                if (transaction != null)
                {
                    if (transaction.InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        throw new Exception("Phiếu kho này đã ghi sổ nên không thể xóa!");

                    if (transaction.InventoryJournals.Where(j => j.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                        j.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY).Count() > 0)
                        throw new Exception("Tồn tại thông tin phụ thuộc nên không thể xóa");
                    transaction.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    transaction.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Tạo InventoryJournal 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="_InventoryTransactionId"></param>
        /// <param name="_AccountId"></param>
        /// <param name="_InventoryId"></param>
        /// <param name="_LotId"></param>
        /// <param name="_ItemUnitId"></param>
        /// <param name="_JournalType"></param>
        /// <param name="_Credit"></param>
        /// <param name="_Debit"></param>
        /// <param name="_Description"></param>
        public InventoryJournal createInventoryJournal(
            UnitOfWork uow, 
            Guid _InventoryTransactionId, 
            Guid _AccountId, 
            Guid _InventoryId, 
            Guid _LotId, 
            Guid _ItemUnitId, 
            char _JournalType, 
            double _Credit,
            double _Debit,
            string _Description, 
            DAL.CMS.ObjectDocument.ObjectTypeEnum CMSObjectType) 
        {
            InventoryTransaction IT = uow.GetObjectByKey<InventoryTransaction>(_InventoryTransactionId);
            if (IT == null)
                throw new Exception("The InventoryTransaction is not exist in system");

            NAS.DAL.Accounting.AccountChart.Account ACC = uow.GetObjectByKey<NAS.DAL.Accounting.AccountChart.Account>(_AccountId);
            if (ACC == null)
                throw new Exception("The Account is not exist in system");

            NAS.DAL.Nomenclature.Inventory.Inventory INV = uow.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(_InventoryId);
            if (INV == null)
                throw new Exception("The Inventory is not exist in system");

            NAS.DAL.Inventory.Lot.Lot LOT = uow.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(_LotId);
            if (LOT == null)
                throw new Exception("The Lot is not exist in system");

            NAS.DAL.Nomenclature.Item.ItemUnit IU = uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(_ItemUnitId);
            if (IU == null)
                throw new Exception("The ItemUnit is not exist in system");

            if ((IT as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

            InventoryJournal IJ = new InventoryJournal(uow);
            IJ.InventoryTransactionId = IT;
            IJ.AccountId = ACC;
            IJ.InventoryId = INV;
            IJ.LotId = LOT;
            IJ.ItemUnitId = IU;
            IJ.JournalType = _JournalType;
            IJ.Credit = _Credit;
            IJ.Debit = _Debit;
            IJ.Description = _Description;
            IJ.CreateDate = DateTime.Now;
            IJ.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            IJ.Save();
            uow.FlushChanges();
            PopulateCMSObjecForInventoryObject<NAS.DAL.Inventory.Journal.InventoryJournal>(
                uow,
                IJ.InventoryJournalId,
                CMSObjectType);

            ObjectBO objectBO = new ObjectBO();
            InventoryCommandObject commandObject = (IT as InventoryCommandItemTransaction).InventoryCommandId.InventoryCommandObjects.FirstOrDefault();
            if (commandObject != null)
            {
                //Copy artifact's data to cms object of transaction
                objectBO.CopyReadOnlyCustomFieldData(
                    commandObject.ObjectId.ObjectId,
                    IJ.InventoryJournalObjects.FirstOrDefault().ObjectId.ObjectId);
            }
            IJ.Save();
            return IJ;
        }

        /// <summary>
        /// Tạo double InventoryJournal trường hợp chuyển kho
        /// </summary>
        /// <param name="_InventoryTransactionId"></param>
        /// <param name="_FromInventoryId"></param>
        /// <param name="_ToInventoryId"></param>
        /// <param name="_LotId"></param>
        /// <param name="_ItemUnitId"></param>
        /// <param name="_NumberOfItem"></param>
        /// <param name="_Description"></param>
        public void createDoubleInventoryJournalForMovingTransaction(
            UnitOfWork uow,
            Guid _InventoryTransactionId,
            Guid _FromInventoryId,
            Guid _ToInventoryId,
            Guid _LotId,
            Guid _ItemUnitId,
            double _PlanNumberOfItem,
            double _NumberOfItem,
            string _Description, DAL.CMS.ObjectDocument.ObjectTypeEnum _Type)
        {
            InventoryCommandItemTransaction IT = uow.GetObjectByKey<InventoryCommandItemTransaction>(_InventoryTransactionId);
            if (IT == null)
                throw new Exception("The InventoryTransaction is not exist in system");

            if ((IT as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

            NAS.DAL.Accounting.AccountChart.Account defaultAccount = NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.NAAN_DEFAULT);
            InventoryJournal creditJournal = createInventoryJournal(uow, _InventoryTransactionId, defaultAccount.AccountId,
                _FromInventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, _NumberOfItem, 0, _Description, _Type);
            createInventoryJournal(uow, _InventoryTransactionId, defaultAccount.AccountId,
                _ToInventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, 0, _NumberOfItem, _Description, _Type);

            createInventoryJournal(uow, _InventoryTransactionId, defaultAccount.AccountId,
                    _FromInventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, _PlanNumberOfItem, 0, _Description, _Type);
            createInventoryJournal(uow, _InventoryTransactionId, defaultAccount.AccountId,
                _ToInventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, 0, _PlanNumberOfItem, _Description, _Type);

            if (creditJournal == null)
                throw new Exception("The Credit InventoryJournal was not created");

            CreateDefaultInventoryCommandFinancialTransaction(
                uow, 
                IT.InventoryCommandId.InventoryCommandId, 
                creditJournal.InventoryJournalId, 
                string.Format("BILL{0}", 
                DateTime.Now.ToString()), 
                _NumberOfItem,
                _ItemUnitId, Guid.Empty, IT.IssueDate, string.Empty, ObjectTypeEnum.INVENTORY_MOVE);

        }

        /// <summary>
        /// Tạo double InventoryJournal trường hợp nhập kho hoặc xuất kho
        /// </summary>
        /// <param name="_InventoryTransactionId"></param>
        /// <param name="_InventoryId"></param>
        /// <param name="_LotId"></param>
        /// <param name="_ItemUnitId"></param>
        /// <param name="_PlanNumberOfItem"></param>
        /// <param name="_NumberOfItem"></param>
        /// <param name="_Description"></param>
        /// <param name="_CommandType"></param>
        public void createDoubleInventoryJournalForInOutTransaction(
            UnitOfWork uow,
            Guid _InventoryTransactionId,
            Guid _InventoryId,
            Guid _LotId,
            Guid _ItemUnitId,
            double _PlanNumberOfItem,
            double _NumberOfItem,
            string _Description, 
            NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum _Type)
        {
            InventoryCommandItemTransaction IT = uow.GetObjectByKey<InventoryCommandItemTransaction>(_InventoryTransactionId);
            if (IT == null)
                throw new Exception("The InventoryTransaction is not exist in system");

            if ((IT as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

            NAS.DAL.Nomenclature.Inventory.Inventory defaultINV =
                NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, DAL.Nomenclature.Inventory.DefaultInventoryEnum.DEFAULTCST);

            NAS.DAL.Accounting.AccountChart.Account suppAccount =
                NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.SUPPLIER_INVENTORY);

            NAS.DAL.Accounting.AccountChart.Account custAccount =
                NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.CUSTOMER_INVENTORY);

            NAS.DAL.Accounting.AccountChart.Account ownerAccount =
                NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.OWNER_INVENTORY);
            InventoryJournal creditJournal = null;
            if (_Type == ObjectTypeEnum.INVENTORY_OUT)
            {
                creditJournal = createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, _NumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, custAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, 0, _NumberOfItem, _Description, _Type);

                createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, _PlanNumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, custAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, 0, _PlanNumberOfItem, _Description, _Type);

                if (creditJournal == null)
                    throw new Exception("The Credit InventoryJournal was not created");
                CreateDefaultInventoryCommandFinancialTransaction(
                    uow, 
                    IT.InventoryCommandId.InventoryCommandId, 
                    creditJournal.InventoryJournalId, 
                    string.Format("BILL{0}", DateTime.Now.ToString()), 
                    _NumberOfItem,
                    _ItemUnitId, 
                    Guid.Empty, IT.IssueDate, string.Empty, ObjectTypeEnum.INVENTORY_OUT);
            }

            if (_Type == ObjectTypeEnum.INVENTORY_IN)
            {
                creditJournal = createInventoryJournal(uow, _InventoryTransactionId, suppAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, _NumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, 0, _NumberOfItem, _Description, _Type);

                createInventoryJournal(uow, _InventoryTransactionId, suppAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, _PlanNumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, 0, _PlanNumberOfItem, _Description, _Type);

                if (creditJournal == null)
                    throw new Exception("The Credit InventoryJournal was not created");
                CreateDefaultInventoryCommandFinancialTransaction(
                    uow, 
                    IT.InventoryCommandId.InventoryCommandId, 
                    creditJournal.InventoryJournalId, 
                    string.Format("BILL{0}", DateTime.Now.ToString()), 
                    _NumberOfItem,
                    _ItemUnitId, 
                    Guid.Empty, 
                    IT.IssueDate, 
                    string.Empty, 
                    ObjectTypeEnum.INVENTORY_IN);
            }
        }

        public InventoryJournal createDoubleInventoryJournalForInOutMovingTransaction(
            UnitOfWork uow,
            Guid _InventoryTransactionId,
            Guid _InventoryId,
            Guid _LotId,
            Guid _ItemUnitId,
            double _PlanNumberOfItem,
            double _NumberOfItem,
            string _Description,
            NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum _Type)
        {
            InventoryCommandItemTransaction IT = uow.GetObjectByKey<InventoryCommandItemTransaction>(_InventoryTransactionId);
            if (IT == null)
                throw new Exception("The InventoryTransaction is not exist in system");

            if ((IT as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

            NAS.DAL.Nomenclature.Inventory.Inventory defaultINV =
                NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, DAL.Nomenclature.Inventory.DefaultInventoryEnum.TRANSITINVENTORY);

            NAS.DAL.Accounting.AccountChart.Account onTheWayAccount =
                NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.ON_THE_WAY);

            NAS.DAL.Accounting.AccountChart.Account ownerAccount =
                NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.OWNER_INVENTORY);
            InventoryJournal creditJournal = null;
            if (_Type == ObjectTypeEnum.INVENTORY_OUT)
            {
                creditJournal = createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, _NumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, onTheWayAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, 0, _NumberOfItem, _Description, _Type);

                createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, _PlanNumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, onTheWayAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, 0, _PlanNumberOfItem, _Description, _Type);

                if (creditJournal == null)
                    throw new Exception("The Credit InventoryJournal was not created");
                CreateDefaultInventoryCommandFinancialTransaction(uow, IT.InventoryCommandId.InventoryCommandId, creditJournal.InventoryJournalId, string.Format("BILL{0}", DateTime.Now.ToString()), _NumberOfItem,
                _ItemUnitId, Guid.Empty, IT.IssueDate, string.Empty, ObjectTypeEnum.INVENTORY_OUT);
            }

            if (_Type == ObjectTypeEnum.INVENTORY_IN)
            {
                creditJournal = createInventoryJournal(uow, _InventoryTransactionId, onTheWayAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, _NumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.ACTUAL, 0, _NumberOfItem, _Description, _Type);

                createInventoryJournal(uow, _InventoryTransactionId, onTheWayAccount.AccountId,
                    defaultINV.InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, _PlanNumberOfItem, 0, _Description, _Type);
                createInventoryJournal(uow, _InventoryTransactionId, ownerAccount.AccountId,
                    _InventoryId, _LotId, _ItemUnitId, JounalTypeConstant.PLANNING, 0, _PlanNumberOfItem, _Description, _Type);

                if (creditJournal == null)
                    throw new Exception("The Credit InventoryJournal was not created");
                CreateDefaultInventoryCommandFinancialTransaction(uow, IT.InventoryCommandId.InventoryCommandId, creditJournal.InventoryJournalId, string.Format("BILL{0}", DateTime.Now.ToString()), _NumberOfItem,
                _ItemUnitId, Guid.Empty, IT.IssueDate, string.Empty, ObjectTypeEnum.INVENTORY_IN);
            }

            uow.FlushChanges();
            return creditJournal;
        }

        /// <summary>
        /// Search InventoryJournal đối ứng với InventoryJournal được truyền vào
        /// </summary>
        /// <param name="session"></param>
        /// <param name="_OriginalInventoryJournalId"></param>
        /// <returns></returns>
        public InventoryJournal searchRelevantInventoryJournal(
            Session session, 
            Guid _OriginalInventoryJournalId)
        {
            InventoryJournal OIJ = session.GetObjectByKey<InventoryJournal>(_OriginalInventoryJournalId);
            if (OIJ == null)
                throw new Exception("The InventoryJournalId is not exist in system");

            return OIJ.InventoryTransactionId.InventoryJournals.Where(i => 
                i.InventoryTransactionId == OIJ.InventoryTransactionId && 
                i != OIJ && 
                i.Debit == OIJ.Credit && 
                i.ItemUnitId == OIJ.ItemUnitId &&
                i.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE &&
                i.JournalType == JounalTypeConstant.ACTUAL &&
                i.LotId == OIJ.LotId).FirstOrDefault();
        }

        /// <summary>
        /// Search Plan InventoryJournal có Credit > 0 ứng với InventoryJournal được truyền vào
        /// </summary>
        /// <param name="session"></param>
        /// <param name="_OriginalInventoryJournalId"></param>
        /// <returns></returns>
        public InventoryJournal searchRelevantPlanCreditInventoryJournal(
            Session session, 
            Guid _OriginalInventoryJournalId)
        {
            InventoryJournal OIJ = session.GetObjectByKey<InventoryJournal>(_OriginalInventoryJournalId);
            if (OIJ == null)
                throw new Exception("The InventoryJournalId is not exist in system");

            return OIJ.InventoryTransactionId.InventoryJournals.Where(i =>
                i.InventoryTransactionId == OIJ.InventoryTransactionId &&
                i != OIJ &&
                i.Credit >= OIJ.Credit && i.Debit == 0 &&
                i.ItemUnitId == OIJ.ItemUnitId &&
                i.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE &&
                i.JournalType == JounalTypeConstant.PLANNING &&
                i.AccountId == OIJ.AccountId).FirstOrDefault();
        }

        public InventoryJournal searchRelevantPlanDebitInventoryJournal(
            Session session, 
            Guid _OriginalInventoryJournalId)
        {
            InventoryJournal OIJ = session.GetObjectByKey<InventoryJournal>(_OriginalInventoryJournalId);
            if (OIJ == null)
                throw new Exception("The InventoryJournalId is not exist in system");

            InventoryJournal debitActualIJ = searchRelevantInventoryJournal(session, _OriginalInventoryJournalId);

            return OIJ.InventoryTransactionId.InventoryJournals.Where(i =>
                i.InventoryTransactionId == OIJ.InventoryTransactionId &&
                i != OIJ &&
                i.Credit == 0 && i.Debit >= debitActualIJ.Debit &&
                i.ItemUnitId == debitActualIJ.ItemUnitId &&
                i.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE &&
                i.JournalType == JounalTypeConstant.PLANNING &&
                i.AccountId == debitActualIJ.AccountId).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_SourceInventoryJournalId"></param>
        /// <returns></returns>
        public InventoryJournal searchReleventMovingInventoryJournal(Guid _SourceOutputInventoryJournalId, Guid _OwnedInventoryCommandId)
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                InventoryJournal SourceOutputInventoryJournal = uow.GetObjectByKey<InventoryJournal>(_SourceOutputInventoryJournalId);
                if (SourceOutputInventoryJournal == null)
                    throw new Exception("The InventoryJournal is not exist in system");

                InventoryCommand OwnedInventoryCommand = uow.GetObjectByKey<InventoryCommand>(_OwnedInventoryCommandId);
                if (OwnedInventoryCommand == null)
                    throw new Exception("The InventoryCommand is not exist in system");

                return OwnedInventoryCommand.InventoryCommandItemTransactions.FirstOrDefault().InventoryJournals.Where(j =>
                    j.ItemUnitId == SourceOutputInventoryJournal.ItemUnitId &&
                    j.Credit > 0 &&
                    j.Debit == 0 &&
                    j.JournalType == 'A' &&
                    j.LotId == SourceOutputInventoryJournal.LotId).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                uow.Dispose();
            }
        }

        public InventoryCommand searchRelevantInventoryCommand(UnitOfWork uow, Guid InventoryCommandId)
        {
            try
            {
                InventoryCommand InventoryCommand = uow.GetObjectByKey<InventoryCommand>(InventoryCommandId);
                if (InventoryCommand == null)
                    throw new Exception("The InventoryCommand is not exist in system");
                if (InventoryCommand.ParentInventoryCommandId == null)
                    return null;

                foreach (InventoryCommand command in InventoryCommand.ParentInventoryCommandId.InventoryCommands)
                {
                    if (command.InventoryCommandId != InventoryCommandId
                        &&
                       (command.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        &&
                       (InventoryCommand.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || InventoryCommand.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        )
                        return command;

                    if (command.InventoryCommandId != InventoryCommandId
                        &&
                       (command.RowStatus == Utility.Constant.ROWSTATUS_TEMP && InventoryCommand.RowStatus == Utility.Constant.ROWSTATUS_TEMP)
                        )
                        return command;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public InventoryCommand searchRelevantInventoryCommand(Guid InventoryCommandId)
        {
            UnitOfWork uow = XpoHelper.GetNewUnitOfWork();
            try
            {
                InventoryCommand InventoryCommand = uow.GetObjectByKey<InventoryCommand>(InventoryCommandId);
                if (InventoryCommand == null)
                    throw new Exception("The InventoryCommand is not exist in system");
                if (InventoryCommand.ParentInventoryCommandId == null)
                    return null;

                foreach (InventoryCommand command in InventoryCommand.ParentInventoryCommandId.InventoryCommands)
                {
                    if (command.InventoryCommandId != InventoryCommandId
                        &&
                       (command.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        &&
                       (InventoryCommand.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || InventoryCommand.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                        )
                        return command;

                    if (command.InventoryCommandId != InventoryCommandId
                        &&
                       (command.RowStatus == Utility.Constant.ROWSTATUS_TEMP && InventoryCommand.RowStatus == Utility.Constant.ROWSTATUS_TEMP)
                        )
                        return command;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                uow.Dispose();
            }
            return null;
        }

        /// <summary>
        /// Xóa cặp Double InventoryJournal 
        /// </summary>
        /// <param name="_OriginalInventoryJournalId"></param>
        public void deleteDoubleInventoryJournal(Session _Session, Guid _OriginalInventoryJournalId)
        { 
            try
            {
                InventoryJournal OIJ = _Session.GetObjectByKey<InventoryJournal>(_OriginalInventoryJournalId);
                if (OIJ == null)
                    throw new Exception("The InventoryJournalId is not exist in system");

                if ((OIJ.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                    throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

                InventoryJournal RelevantIJ = searchRelevantInventoryJournal(_Session, _OriginalInventoryJournalId);
                if (RelevantIJ == null)
                    throw new Exception("The Input InventoryJournal don't have relevant InventoryJournal in system");
                try
                {
                    InventoryJournal CreditIJ = searchRelevantPlanCreditInventoryJournal(_Session, _OriginalInventoryJournalId);
                    //CreditIJ.Delete();
                    CreditIJ.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    CreditIJ.Save();
                    InventoryJournal DebitIJ = searchRelevantPlanDebitInventoryJournal(_Session, _OriginalInventoryJournalId);
                    //DebitIJ.Delete();
                    DebitIJ.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    DebitIJ.Save();
                }
                catch (Exception)
                {}
                //OIJ.Delete();
                //RelevantIJ.Delete();   
                OIJ.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                OIJ.Save();
                RelevantIJ.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                RelevantIJ.Save();
            
                InventoryCommandFinancialTransaction transaction = OIJ.InventoryJournalFinancials.FirstOrDefault().TransactionId as InventoryCommandFinancialTransaction;

                DeleteDefaultInventoryCommandFinancialTransaction(_Session, transaction.TransactionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update Double InventoryJournal
        /// </summary>
        /// <param name="_OriginalInventoryJournalId"></param>
        /// <param name="_FromInventoryId"></param>
        /// <param name="_ToInventoryId"></param>
        /// <param name="_LotId"></param>
        /// <param name="_ItemUnitId"></param>
        /// <param name="_NumberOfItem"></param>
        /// <param name="_Description"></param>
        public void UpdateDoubleInventoryJournal(
            UnitOfWork uow,
            Guid _OriginalInventoryJournalId,
            Guid _FromInventoryId,
            Guid _ToInventoryId,
            Guid _LotId,
            Guid _ItemUnitId,
            double _PlanNumberOfItem,
            double _NumberOfItem,
            string _Description)
        {
                InventoryJournal OIJ = uow.GetObjectByKey<InventoryJournal>(_OriginalInventoryJournalId);
                if (OIJ == null)
                    throw new Exception("The InventoryJournalId is not exist in system");

                if ((OIJ.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                    throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

                InventoryJournal RelevantIJ = searchRelevantInventoryJournal(uow, _OriginalInventoryJournalId);

                if (RelevantIJ == null)
                    throw new Exception("The Input InventoryJournal don't have relevant InventoryJournal in system");

                InventoryJournal PlanCreditIJ = searchRelevantPlanCreditInventoryJournal(uow, _OriginalInventoryJournalId);

                if (PlanCreditIJ == null)
                    throw new Exception("The Input InventoryJournal don't have relevant Plan credit InventoryJournal in system");
                
                InventoryJournal PlanDebitIJ = searchRelevantPlanDebitInventoryJournal(uow, _OriginalInventoryJournalId);
                if (PlanDebitIJ == null)
                    throw new Exception("The Input InventoryJournal don't have relevant Plan debit InventoryJournal in system");

                NAS.DAL.Nomenclature.Inventory.Inventory FromINV = uow.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(_FromInventoryId);
                if (FromINV == null)
                    throw new Exception("The Inventory is not exist in system");

                NAS.DAL.Nomenclature.Inventory.Inventory ToINV = uow.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(_ToInventoryId);
                if (ToINV == null)
                    throw new Exception("The Inventory is not exist in system");

                NAS.DAL.Inventory.Lot.Lot LOT = uow.GetObjectByKey<NAS.DAL.Inventory.Lot.Lot>(_LotId);
                if (LOT == null)
                    throw new Exception("The Lot is not exist in system");

                NAS.DAL.Nomenclature.Item.ItemUnit IU = uow.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(_ItemUnitId);
                if (IU == null)
                    throw new Exception("The ItemUnit is not exist in system");

                OIJ.InventoryId = FromINV;
                RelevantIJ.InventoryId = ToINV;
                OIJ.LotId = RelevantIJ.LotId = LOT;
                OIJ.ItemUnitId = RelevantIJ.ItemUnitId = IU;
                OIJ.Credit = RelevantIJ.Debit = _NumberOfItem;
                OIJ.Description = RelevantIJ.Description = _Description;
                OIJ.Save();
                RelevantIJ.Save();

                PlanCreditIJ.InventoryId = FromINV;
                PlanDebitIJ.InventoryId = ToINV;
                PlanCreditIJ.Credit = _PlanNumberOfItem;
                PlanDebitIJ.Debit = _PlanNumberOfItem;
                PlanCreditIJ.ItemUnitId = PlanDebitIJ.ItemUnitId = IU;
                PlanCreditIJ.LotId = PlanDebitIJ.LotId = LOT;
                PlanCreditIJ.Description = PlanDebitIJ.Description = _Description;
                PlanCreditIJ.Save();
                PlanDebitIJ.Save();
                
                InventoryCommand command = (OIJ.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId;
                NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum _Type = ObjectTypeEnum.INVENTORY_MOVE;
                if (command.CommandType == INVENTORY_COMMAND_TYPE.IN)
                    _Type = ObjectTypeEnum.INVENTORY_IN;
                else if (command.CommandType == INVENTORY_COMMAND_TYPE.OUT)
                    _Type = ObjectTypeEnum.INVENTORY_OUT;
                else if (command.CommandType == INVENTORY_COMMAND_TYPE.MOVE)
                    _Type = ObjectTypeEnum.INVENTORY_MOVE;

                UpdateDefaultInventoryCommandFinancialTransaction(
                    uow,
                    command.InventoryCommandId,
                    OIJ.InventoryJournalId,
                    _NumberOfItem,
                    _ItemUnitId,
                    string.Empty,
                    _Type);
        }

        
        /// <summary>
        /// Lấy danh sách thực giao của phiếu bán
        /// </summary>
        /// <param name="_Session"></param>
        /// <param name="_BillId"></param>
        /// <param name="_ObjectTypeCustomField"></param>
        /// <returns></returns>
        public IEnumerable<InventoryJournal> GetActualInventoryJournalOfBill(
            Session _Session, 
            Guid _BillId, 
            DefaultObjectTypeCustomFieldEnum _ObjectTypeCustomField)
        {
            try
            {
                XPCollection<InventoryJournal> ret = null;
                ObjectBO objectBO = new ObjectBO();
                List<Guid> billIds = new List<Guid>();
                billIds.Add(_BillId);

                ObjectTypeCustomField objectTypeCustomField = ObjectTypeCustomField.GetDefault(
                    _Session, 
                    _ObjectTypeCustomField);
                var relatedObjectList =
                    objectBO.FindCMSObjectsOfBuiltInCustomField(
                        _Session,
                        objectTypeCustomField.ObjectTypeCustomFieldId,
                        billIds);

                if (relatedObjectList == null)
                    return null;

                var relatedObjectIdList = relatedObjectList.Select(r => r.ObjectId);

                XPCollection<InventoryCommandItemTransaction> Transactions
                    = new XPCollection<InventoryCommandItemTransaction>(_Session,
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual));

                CriteriaOperator criteria = CriteriaOperator.And(
                    new BinaryOperator("Credit", 0, BinaryOperatorType.Greater),
                    new BinaryOperator("JournalType", 'A', BinaryOperatorType.Equal),
                    new NotOperator(new NullOperator("InventoryTransactionId")),
                    new BinaryOperator("InventoryTransactionId.RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                    new ContainsOperator("InventoryJournalObjects",
                        new InOperator("ObjectId.ObjectId", relatedObjectIdList)));

                ret = new XPCollection<InventoryJournal>(_Session,
                        Transactions.SelectMany(r => r.InventoryJournals), criteria);
                IEnumerable<InventoryJournal> ret2 = ret.Where(r => r.InventoryTransactionId != null &&
                             (r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) &&
                             (r.InventoryTransactionId as InventoryCommandItemTransaction) != null &&
                             (r.InventoryTransactionId.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE || r.InventoryTransactionId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) &&
                             (r.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId != null &&
                             ((r.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                          || (r.InventoryTransactionId as InventoryCommandItemTransaction).InventoryCommandId.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY));
                return ret2;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<InventoryCommandFinancialTransaction> GetInventoryFinancialTransactionOfInventoryCommand(
            Session _Session,
            Guid _InventoryCommandId)
        {
            InventoryCommand command = _Session.GetObjectByKey<InventoryCommand>(_InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");
            return command.InventoryCommandFinancialTransactions.Select(r=> r).Where(r => 
                r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                || r.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY);
        }

        public XPCollection<Transaction> GetInventoryFinancialTransactionOfSourceBill(Session session,
            Guid billId)
        {
            NAS.DAL.Invoice.Bill bill = session.GetObjectByKey<NAS.DAL.Invoice.Bill>(billId);
            if (bill == null)
                throw new Exception("The bill is not exist in system");
            ObjectBO objectBO = new ObjectBO();
            List<Guid> billIds = new List<Guid>();
            billIds.Add(billId);
            ObjectTypeCustomField objectTypeCustomField = null;

            if (bill is NAS.DAL.Invoice.PurchaseInvoice)
                objectTypeCustomField = ObjectTypeCustomField.GetDefault(
                    session,
                    DefaultObjectTypeCustomFieldEnum.INVENTORY_IN_PURCHASE_INVOICE);
            else if (bill is NAS.DAL.Invoice.SalesInvoice)
                objectTypeCustomField = ObjectTypeCustomField.GetDefault(
                    session,
                    DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);

            var relatedObjectList =
                objectBO.FindCMSObjectsOfBuiltInCustomField(
                    session,
                    objectTypeCustomField.ObjectTypeCustomFieldId,
                    billIds);

            if (relatedObjectList == null)
                return null;

            var relatedObjectIdList = relatedObjectList.Select(r => r.ObjectId);

            CriteriaOperator criteria = CriteriaOperator.And(
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal),
                new ContainsOperator("TransactionObjects",
                    new InOperator("ObjectId.ObjectId", relatedObjectIdList)));

            return new XPCollection<Transaction>(session, criteria);
        }

        /// <summary>
        /// Create InventoryCommandFinancialTransaction with relevant Artifact
        /// </summary>
        /// <param name="_session">Session of Process</param>
        /// <param name="_ArtifactId">Guid of relevant SalesInvoice/PurchasingInvoice</param>
        /// <param name="_InventoryCommandId">Guid of InventoryCommand</param>
        /// <param name="_InventoryCommandId">Guid of InventoryJournal</param>
        /// <param name="_Code">Code of Transaction</param>
        /// <param name="_TotalOfMoney">Total Amount of Transaction</param>
        /// <param name="_CurrencyId">Currency of Journal in Transaction </param>
        /// <param name="_IssueDate">Issue Date of Transaction</param>
        /// <param name="_Description">Desciption of Transaction </param>
        /// <param name="CMSObjectType">CMS Type </param>
        public void CreateDefaultInventoryCommandFinancialTransaction(
            UnitOfWork uow,
            Guid _InventoryCommandId,
            Guid _InventoryJournalId,
            string _Code,
            double _Quantity,
            Guid _ItemUnitId,
            Guid _CurrencyId,
            DateTime _IssueDate,
            string _Description,
            DAL.CMS.ObjectDocument.ObjectTypeEnum CMSObjectType)
        {
            InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(_InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");

            if (command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

            ObjectBO objectBO = new ObjectBO();
            InventoryCommandObject commandObject = command.InventoryCommandObjects.FirstOrDefault();
            InventoryCommandFinancialTransaction transaction = new InventoryCommandFinancialTransaction(uow)
            {
                Code = _Code,
                CreateDate = DateTime.Now,
                Description = _Description,
                IssueDate = _IssueDate,
                RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                InventoryCommandId = command
            };
            uow.FlushChanges();
            InventoryJournal inventoryJournal = uow.GetObjectByKey<InventoryJournal>(_InventoryJournalId);

            if (inventoryJournal == null)
                throw new Exception("The InventoryJournal is not exist in system");

            InventoryJournalFinancial inventoryJournalFinancial = new InventoryJournalFinancial(uow)
            {
                TransactionId = transaction,
                InventoryJournalId = inventoryJournal,
                RowStatus = Utility.Constant.ROWSTATUS_ACTIVE
            };
            inventoryJournalFinancial.Save();
            uow.FlushChanges();

            double Total = 0;
            NAS.BO.Accounting.Journal.GeneralJournalBO generalJournalBO = new NAS.BO.Accounting.Journal.GeneralJournalBO();
            NAS.DAL.Accounting.AccountChart.Account defaultAcc
                    = NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.NAAN_DEFAULT);

            if (CMSObjectType != ObjectTypeEnum.INVENTORY_MOVE)
            {
                PopulateCMSObjecForInventoryObject<NAS.DAL.Inventory.Command.InventoryCommandFinancialTransaction>(uow,
                            transaction.TransactionId,
                            CMSObjectType);

                TransactionObject transactionObject = transaction.TransactionObjects.FirstOrDefault();

                if (commandObject != null)
                {
                    //Copy artifact's data to cms object of transaction
                    objectBO.CopyReadOnlyCustomFieldData(
                        commandObject.ObjectId.ObjectId,
                        transactionObject.ObjectId.ObjectId);
                }

                uow.FlushChanges();

                ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();
                COGSBO CogsBO = new COGSBO();
                CurrencyBO currencyBO = new Accounting.CurrencyBO();

                Bill billArtifact = GetSourceArtifactFromInventoryCommand(uow, _InventoryCommandId);

                if (billArtifact != null)
                {
                    if (billArtifact is SalesInvoice)
                    {
                        BillItem billItem = (billArtifact as SalesInvoice).BillItems.Where(b => b.ItemUnitId.ItemUnitId == _ItemUnitId).FirstOrDefault();
                        if (billItem != null)
                        {
                            if (command.RelevantInventoryId == null)
                                throw new Exception("Relevant Inventory not exist!");                           

                            COGS Cogs = CogsBO.GetLastCOGS(uow, billItem.ItemUnitId.ItemUnitId, currencyBO.GetDefaultCurrency(uow).CurrencyId, command.RelevantInventoryId.InventoryId);

                            if (Cogs != null)
                                Total = _Quantity * Cogs.COGSPrice;
                            else
                                Total = _Quantity * billItem.Price;
                        }
                    }
                    else if (billArtifact is NAS.DAL.Invoice.PurchaseInvoice)
                    {
                        BillItem billItem = (billArtifact as NAS.DAL.Invoice.PurchaseInvoice).BillItems.Where(b => b.ItemUnitId.ItemUnitId == _ItemUnitId).FirstOrDefault();
                        if (billItem != null)
                        {
                            Total = _Quantity * billItem.Price;
                        }
                    }
                }
                else {
                    InventoryCommand RelevantInventoryCommand = searchRelevantInventoryCommand(uow, _InventoryCommandId);
                    Guid inventoryId = Guid.Empty;
                    if (RelevantInventoryCommand != null && RelevantInventoryCommand.CommandType.Equals('O') && command.CommandType.Equals('I'))
                        inventoryId = RelevantInventoryCommand.RelevantInventoryId.InventoryId;
                    else
                        inventoryId = command.RelevantInventoryId.InventoryId;

                    COGS Cogs = CogsBO.GetLastCOGS(uow, _ItemUnitId, currencyBO.GetDefaultCurrency(uow).CurrencyId, inventoryId);
                    if (Cogs != null)
                        Total = _Quantity * Cogs.COGSPrice;
                    else
                        Total = 0; 
                }

                transaction.Amount = Total;
                if (command.ParentInventoryCommandId != null && 
                    (command.ParentInventoryCommandId.CommandType.Equals('A') || command.ParentInventoryCommandId.CommandType.Equals('M')))
                {
                    transaction.Description = inventoryJournal.Description;
                }
                else if (command.ParentInventoryCommandId == null && command.CommandType.Equals('O'))
                    transaction.Description = string.Format("Xuất hàng hóa '{0}' cho khách hàng", inventoryJournal.ItemUnitId.ItemId.Code);
                else if (command.ParentInventoryCommandId == null && command.CommandType.Equals('I'))
                    transaction.Description = string.Format("Nhập hàng hóa '{0}' từ nhà cung cấp", inventoryJournal.ItemUnitId.ItemId.Code);
                uow.FlushChanges();

                GeneralJournal creditJournal = generalJournalBO.CreateGeneralJournal(uow, transaction.TransactionId, defaultAcc.AccountId, Side.CREDIT, Total, string.Empty, JounalTypeFlag.ACTUAL);
                GeneralJournal debitJournal = generalJournalBO.CreateGeneralJournal(uow, transaction.TransactionId, defaultAcc.AccountId, Side.DEBIT, Total, string.Empty, JounalTypeFlag.ACTUAL);
                uow.FlushChanges();
                PopulateCMSObjecForInventoryObject<NAS.DAL.Accounting.Journal.GeneralJournal>(uow,
                            creditJournal.GeneralJournalId,
                            CMSObjectType);
                uow.FlushChanges();
                PopulateCMSObjecForInventoryObject<NAS.DAL.Accounting.Journal.GeneralJournal>(uow,
                            debitJournal.GeneralJournalId,
                            CMSObjectType);
                uow.FlushChanges();

                GeneralJournalObject creditJournalObject = creditJournal.GeneralJournalObjects.FirstOrDefault();
                GeneralJournalObject debitJournalObject = debitJournal.GeneralJournalObjects.FirstOrDefault();

                if (commandObject != null)
                {
                    //Copy artifact's data to cms object of transaction
                    objectBO.CopyReadOnlyCustomFieldData(
                        commandObject.ObjectId.ObjectId,
                        creditJournalObject.ObjectId.ObjectId);

                    objectBO.CopyReadOnlyCustomFieldData(
                        commandObject.ObjectId.ObjectId,
                        debitJournalObject.ObjectId.ObjectId);
                }
            }
            else {
                GeneralJournal creditJournal = generalJournalBO.CreateGeneralJournal(uow, transaction.TransactionId, defaultAcc.AccountId, Side.CREDIT, Total, string.Empty, JounalTypeFlag.ACTUAL);
                GeneralJournal debitJournal = generalJournalBO.CreateGeneralJournal(uow, transaction.TransactionId, defaultAcc.AccountId, Side.DEBIT, Total, string.Empty, JounalTypeFlag.ACTUAL);
            }
            uow.FlushChanges();
        }

        /// <summary>
        /// Cập nhật Inventory Transaction tài chính
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="_InventoryCommandId"></param>
        /// <param name="_InventoryJournalId"></param>
        /// <param name="_Quantity"></param>
        /// <param name="_ItemUnitId"></param>
        /// <param name="_Description"></param>
        /// <param name="CMSObjectType"></param>
        public void UpdateDefaultInventoryCommandFinancialTransaction(
            UnitOfWork uow,
            Guid _InventoryCommandId,
            Guid _InventoryJournalId,
            double _Quantity,
            Guid _ItemUnitId,
            string _Description,
            DAL.CMS.ObjectDocument.ObjectTypeEnum CMSObjectType)
        {
            InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(_InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");

            if (command.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY)
                throw new Exception("Phiếu kho này đã ghi sổ nên không thể chỉnh sửa!");

            InventoryJournal inventoryJournal = uow.GetObjectByKey<InventoryJournal>(_InventoryJournalId);

            if (inventoryJournal == null)
                throw new Exception("The InventoryJournal is not exist in system");

            InventoryCommandObject commandObject = command.InventoryCommandObjects.FirstOrDefault();
            ObjectBO objectBO = new ObjectBO();
            InventoryCommandFinancialTransaction transaction = inventoryJournal.InventoryJournalFinancials.FirstOrDefault().TransactionId as InventoryCommandFinancialTransaction;
            transaction.UpdateDate = DateTime.Now;

            COGSBO CogsBO = new COGSBO();
            CurrencyBO currencyBO = new Accounting.CurrencyBO();
            double Total = 0;
            NAS.BO.Accounting.Journal.GeneralJournalBO generalJournalBO = new NAS.BO.Accounting.Journal.GeneralJournalBO();
            NAS.DAL.Accounting.AccountChart.Account defaultAcc
                    = NAS.DAL.Accounting.AccountChart.Account.GetDefault(uow, DAL.Accounting.AccountChart.DefaultAccountEnum.NAAN_DEFAULT);

            if (CMSObjectType != ObjectTypeEnum.INVENTORY_MOVE)
            {
                Bill billArtifact = GetSourceArtifactFromInventoryCommand(uow, _InventoryCommandId);

                if (billArtifact != null)
                {
                    if (billArtifact is SalesInvoice)
                    {
                        BillItem billItem = (billArtifact as SalesInvoice).BillItems.Where(b => b.ItemUnitId.ItemUnitId == _ItemUnitId).FirstOrDefault();
                        if (billItem != null)
                        {
                            COGS Cogs = CogsBO.GetLastCOGS(uow, billItem.ItemUnitId.ItemUnitId, currencyBO.GetDefaultCurrency(uow).CurrencyId, command.RelevantInventoryId.InventoryId);

                            if (Cogs != null)
                                Total = _Quantity * Cogs.COGSPrice;
                            else
                                Total = _Quantity * billItem.Price;
                        }
                    }
                    else if (billArtifact is NAS.DAL.Invoice.PurchaseInvoice)
                    {
                        BillItem billItem = (billArtifact as NAS.DAL.Invoice.PurchaseInvoice).BillItems.Where(b => b.ItemUnitId.ItemUnitId == _ItemUnitId).FirstOrDefault();
                        if (billItem != null)
                        {
                            Total = _Quantity * billItem.Price;
                        }
                    }
                }
                else {
                    InventoryCommand RelevantInventoryCommand = searchRelevantInventoryCommand(uow, _InventoryCommandId);
                    Guid inventoryId = Guid.Empty;
                    if (RelevantInventoryCommand != null && RelevantInventoryCommand.CommandType.Equals('O') && command.CommandType.Equals('I'))
                        inventoryId = RelevantInventoryCommand.RelevantInventoryId.InventoryId;
                    else
                        inventoryId = command.RelevantInventoryId.InventoryId;

                    COGS Cogs = CogsBO.GetLastCOGS(uow, _ItemUnitId, currencyBO.GetDefaultCurrency(uow).CurrencyId, inventoryId);
                    if (Cogs != null)
                        Total = _Quantity * Cogs.COGSPrice;
                    else
                        Total = 0;
                }

                transaction.Amount = Total;
                transaction.Description = string.Format("Xuất hàng hóa '{0}' cho khách hàng", inventoryJournal.ItemUnitId.ItemId.Code);
                
                foreach(GeneralJournal j in transaction.GeneralJournals)
                {
                    j.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                }

                GeneralJournal creditJournal = generalJournalBO.CreateGeneralJournal(uow, transaction.TransactionId, defaultAcc.AccountId, Side.CREDIT, Total, string.Empty, JounalTypeFlag.ACTUAL);
                GeneralJournal debitJournal = generalJournalBO.CreateGeneralJournal(uow, transaction.TransactionId, defaultAcc.AccountId, Side.DEBIT, Total, string.Empty, JounalTypeFlag.ACTUAL);
                uow.FlushChanges();
                PopulateCMSObjecForInventoryObject<NAS.DAL.Accounting.Journal.GeneralJournal>(uow,
                            creditJournal.GeneralJournalId,
                            CMSObjectType);
                uow.FlushChanges();
                PopulateCMSObjecForInventoryObject<NAS.DAL.Accounting.Journal.GeneralJournal>(uow,
                            debitJournal.GeneralJournalId,
                            CMSObjectType);
                uow.FlushChanges();

                GeneralJournalObject creditJournalObject = creditJournal.GeneralJournalObjects.FirstOrDefault();
                GeneralJournalObject debitJournalObject = debitJournal.GeneralJournalObjects.FirstOrDefault();

                if (commandObject != null)
                {
                    //Copy artifact's data to cms object of transaction
                    objectBO.CopyReadOnlyCustomFieldData(
                        commandObject.ObjectId.ObjectId,
                        creditJournalObject.ObjectId.ObjectId);

                    objectBO.CopyReadOnlyCustomFieldData(
                        commandObject.ObjectId.ObjectId,
                        debitJournalObject.ObjectId.ObjectId);
                }

                uow.FlushChanges();
            }
            uow.FlushChanges();
        }

        public void DeleteDefaultInventoryCommandFinancialTransaction(Session _Session, Guid _TransactionId)
        {
            InventoryCommandFinancialTransaction transaction = _Session.GetObjectByKey<InventoryCommandFinancialTransaction>(_TransactionId);
            if (transaction == null)
                throw new Exception("The InventoryCommandFinancialTransaction is not exist in system");
            transaction.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
            transaction.Save();
        }

        public string AutoGenerateInventoryJournalForOutputCommand(UnitOfWork uow, Guid _InventoryCommandItemTransactionId, Guid InventoryId)
        {
            string errormsg = string.Empty;
            InventoryCommandItemTransaction transaction = uow.GetObjectByKey<InventoryCommandItemTransaction>(_InventoryCommandItemTransactionId);
            if (transaction == null)
                throw new Exception("The InventoryCommandItemTransaction is not exist in system");

            if (transaction.InventoryCommandId == null)
                throw new Exception("The InventoryCommand not attach for InventoryCommandItemTransaction");

            NAS.DAL.Nomenclature.Inventory.Inventory Inventory = uow.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(InventoryId);

            if (Inventory == null)
                throw new Exception("The Inventory is not exist in system");

            Bill billArtifact = GetSourceArtifactFromInventoryCommand(uow, transaction.InventoryCommandId.InventoryCommandId);

            if (billArtifact != null && billArtifact is SalesInvoice)
            {
                IEnumerable<InventoryJournal> ActualInventoryJournal = GetActualInventoryJournalOfBill(
                uow, billArtifact.BillId, DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);

                XPCollection<BillItem> billItems = (billArtifact as SalesInvoice).BillItems;
                NAS.DAL.Nomenclature.Inventory.Inventory defaultInventory = 
                    NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, DAL.Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);
                NAS.DAL.Inventory.Lot.Lot defaultLot = 
                    NAS.DAL.Inventory.Lot.Lot.GetDefault(uow, NAS.DAL.Inventory.Lot.DefaultLotEnum.NOT_AVAILABLE);

                int numOfCompleteItemUnit = 0;
                int idx = 1;
                foreach (BillItem billItem in billItems)
                {
                    if (!billItem.ItemUnitId.ItemId.ItemObjects.FirstOrDefault().ObjectId.ObjectTypeId.Name.Equals("PRODUCT"))
                        continue;
                    double DeliveredNumber = ActualInventoryJournal.Where(j => j.ItemUnitId == billItem.ItemUnitId).Sum(j => j.Credit);
                    double NeededNumOfItem = billItem.Quantity - DeliveredNumber;

                    if (NeededNumOfItem == 0)
                    {
                        numOfCompleteItemUnit += 1;
                        continue;
                    }

                    InventoryLedgerBO LedgerBO = new InventoryLedgerBO();
                    double newestBalance = LedgerBO.GetItemUnitBalance(uow, billItem.ItemUnitId, Inventory.InventoryId);

                    if (newestBalance < NeededNumOfItem)
                        errormsg += string.Format("{0}. Hàng hóa {1} (ĐVT {2}) trong kho {3} là {4} nên không đủ để xuất hàng. \n",
                            idx++,
                            billItem.ItemUnitId.ItemId.Code,
                            billItem.ItemUnitId.UnitId.Code,
                            Inventory.Code,
                            newestBalance.ToString()
                            );
                    else
                    {
                        createDoubleInventoryJournalForInOutTransaction(
                        uow,
                        _InventoryCommandItemTransactionId,
                        defaultInventory.InventoryId,
                        defaultLot.LotId, billItem.ItemUnitId.ItemUnitId, NeededNumOfItem, NeededNumOfItem, string.Empty, ObjectTypeEnum.INVENTORY_OUT);
                        uow.FlushChanges();
                    }
                    //double realNum = 0;
                    //if (newestBalance < NeededNumOfItem)
                    //    realNum = newestBalance;
                    //else
                    //    realNum = NeededNumOfItem;
                }

                //if (!errormsg.Equals(string.Empty))
                //    throw new Exception(errormsg);

                if (billItems.Count == numOfCompleteItemUnit)
                {
                    throw new Exception("Đã giao đủ hàng cho phiếu bán");
                }
            }

            return errormsg;
        }

        public bool CheckIsCompletedDeliveryForSaleInvoice(UnitOfWork uow, Guid SaleInvoiceId)
        {
            NAS.DAL.Invoice.SalesInvoice Bill = uow.GetObjectByKey<NAS.DAL.Invoice.SalesInvoice>(SaleInvoiceId);
            if (Bill == null)
                throw new Exception("The SalesInvoice is not exist in system");
            XPCollection<BillItem> billItems = Bill.BillItems;

            if (billItems == null || billItems.Count == 0)
                return false;

            IEnumerable<InventoryJournal> ActualInventoryJournal = GetActualInventoryJournalOfBill(
                uow, Bill.BillId, DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);

            if (ActualInventoryJournal == null || ActualInventoryJournal.Count() == 0)
                return false;

            int numOfCompleteItemUnit = 0;
            foreach (BillItem billItem in billItems)
            {
                if (!billItem.ItemUnitId.ItemId.ItemObjects.FirstOrDefault().ObjectId.ObjectTypeId.Name.Equals("PRODUCT"))
                    continue;
                double DeliveredNumber = ActualInventoryJournal.Where(j => j.ItemUnitId == billItem.ItemUnitId).Sum(j => j.Credit);
                double NeededNumOfItem = billItem.Quantity - DeliveredNumber;

                if (NeededNumOfItem == 0)
                {
                    numOfCompleteItemUnit += 1;
                    continue;
                }
            }

            if (numOfCompleteItemUnit < billItems.Count)
                return false;

            return true;
        }

        public NAS.DAL.Nomenclature.Inventory.Inventory getFirstInventoryJournalForOutputCommand(Session uow, Guid InventoryTransaction)
        {
            InventoryCommandItemTransaction transaction = uow.GetObjectByKey<InventoryCommandItemTransaction>(InventoryTransaction);
            if (transaction == null)
                throw new Exception("The InventoryCommandItemTransaction is not exist in system");
            if (transaction.InventoryJournals != null && transaction.InventoryJournals.Count > 0)
            {
                foreach (InventoryJournal journal in transaction.InventoryJournals)
                {
                    if (journal.JournalType == 'A' && journal.Credit > 0 && journal.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                        return journal.InventoryId;
                }
            }

            return null;
        }

        public NAS.DAL.Nomenclature.Inventory.Inventory getFirstInventoryJournalForInputCommand(Session uow, Guid InventoryTransaction)
        {
            InventoryCommandItemTransaction transaction = uow.GetObjectByKey<InventoryCommandItemTransaction>(InventoryTransaction);
            if (transaction == null)
                throw new Exception("The InventoryCommandItemTransaction is not exist in system");
            if (transaction.InventoryJournals != null && transaction.InventoryJournals.Count > 0)
            {
                foreach (InventoryJournal journal in transaction.InventoryJournals)
                {
                    if (journal.JournalType == 'A' && journal.Debit > 0 && journal.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                        return journal.InventoryId;
                }
            }

            return null;
        }

        public void AutoGenerateInventoryJournalForInputCommand_MovingCase(UnitOfWork uow, Guid _SourceOutputInventoryJournal, Guid _InputInventoryCommand)
        {
            ArtifactCodeRuleBO artifactCodeRuleBO = new ArtifactCodeRuleBO();
            InventoryJournal SourceOutputInventoryJournal = uow.GetObjectByKey<InventoryJournal>(_SourceOutputInventoryJournal);
            if (SourceOutputInventoryJournal == null)
                throw new Exception("The InventoryJournal is not exist in system");

            InventoryCommand InputInventoryCommand = uow.GetObjectByKey<InventoryCommand>(_InputInventoryCommand);
            if (InputInventoryCommand == null)
                throw new Exception("The InventoryCommand is not exist in system");

            InventoryJournal planCreditJournal = searchRelevantPlanDebitInventoryJournal(uow, _SourceOutputInventoryJournal);
            if (planCreditJournal == null)
                throw new Exception("The InventoryJournal is not exist in system");

            NAS.DAL.Nomenclature.Inventory.Inventory firstInventory = getFirstInventoryJournalForInputCommand(uow, InputInventoryCommand.InventoryCommandItemTransactions.FirstOrDefault().InventoryTransactionId);

            Guid _InventoryId = Guid.Empty;
            if (firstInventory == null)
                _InventoryId = NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, DAL.Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE).InventoryId;
            else
                _InventoryId = firstInventory.InventoryId;

            createDoubleInventoryJournalForInOutMovingTransaction(
                            uow,
                            InputInventoryCommand.InventoryCommandItemTransactions.FirstOrDefault().InventoryTransactionId,
                            _InventoryId,
                            SourceOutputInventoryJournal.LotId.LotId,
                            SourceOutputInventoryJournal.ItemUnitId.ItemUnitId,
                            planCreditJournal.Debit,
                            SourceOutputInventoryJournal.Credit,
                            "",
                            NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.INVENTORY_IN);
        }

        public void PopulateActorsForInventoryCommand(UnitOfWork uow, Guid _InventoryCommandId)
        {
            try
            {
                InventoryCommandActorType BuyerActorType = InventoryCommandActorType.GetDefault(uow, DefaultInventoryCommandActorTypeEnum.BUYER);
                InventoryCommandActorType ChiefAccountantActorType = InventoryCommandActorType.GetDefault(uow, DefaultInventoryCommandActorTypeEnum.CHIEFACCOUNTANT);
                InventoryCommandActorType CreatorActorType = InventoryCommandActorType.GetDefault(uow, DefaultInventoryCommandActorTypeEnum.CREATOR);
                InventoryCommandActorType DrirectorActorType = InventoryCommandActorType.GetDefault(uow, DefaultInventoryCommandActorTypeEnum.DIRECTOR);
                InventoryCommandActorType StoreKeeperActorType = InventoryCommandActorType.GetDefault(uow, DefaultInventoryCommandActorTypeEnum.STOREKEEPER);
                InventoryCommandActorType ShipperActorType = InventoryCommandActorType.GetDefault(uow, DefaultInventoryCommandActorTypeEnum.SHIPPER);
                InventoryCommandActorType ReceiverActorType = InventoryCommandActorType.GetDefault(uow, DefaultInventoryCommandActorTypeEnum.RECEIVER);

                InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(_InventoryCommandId);
                if (command == null)
                    throw new Exception("The InventoryCommandItemTransaction is not exist in system");

                InventoryCommandActor BuyerActor = new InventoryCommandActor(uow);
                BuyerActor.InventoryCommandActorTypeId = BuyerActorType;
                BuyerActor.InventoryCommandId = command;
                BuyerActor.Save();

                InventoryCommandActor ChiefAccountantActor = new InventoryCommandActor(uow);
                ChiefAccountantActor.InventoryCommandActorTypeId = ChiefAccountantActorType;
                ChiefAccountantActor.InventoryCommandId = command;
                ChiefAccountantActor.Save();

                InventoryCommandActor CreatorActor = new InventoryCommandActor(uow);
                CreatorActor.InventoryCommandActorTypeId = CreatorActorType;
                CreatorActor.InventoryCommandId = command;
                CreatorActor.Save();

                InventoryCommandActor DrirectorActor = new InventoryCommandActor(uow);
                DrirectorActor.InventoryCommandActorTypeId = DrirectorActorType;
                DrirectorActor.InventoryCommandId = command;
                DrirectorActor.Save();

                InventoryCommandActor StoreKeeperActor = new InventoryCommandActor(uow);
                StoreKeeperActor.InventoryCommandActorTypeId = StoreKeeperActorType;
                StoreKeeperActor.InventoryCommandId = command;
                StoreKeeperActor.Save();

                InventoryCommandActor ShipperActor = new InventoryCommandActor(uow);
                ShipperActor.InventoryCommandActorTypeId = ShipperActorType;
                ShipperActor.InventoryCommandId = command;
                ShipperActor.Save();

                InventoryCommandActor ReceiverActor = new InventoryCommandActor(uow);
                ShipperActor.InventoryCommandActorTypeId = ReceiverActorType;
                ShipperActor.InventoryCommandId = command;
                ShipperActor.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AutoGenerateInventoryJournalForInputCommand_NoneMovingCase(UnitOfWork uow, Guid _InventoryCommandItemTransactionId)
        {
            InventoryCommandItemTransaction transaction = uow.GetObjectByKey<InventoryCommandItemTransaction>(_InventoryCommandItemTransactionId);
            if (transaction == null)
                throw new Exception("The InventoryCommandItemTransaction is not exist in system");

            if (transaction.InventoryCommandId == null)
                throw new Exception("The InventoryCommand not attach for InventoryCommandItemTransaction");

            Bill billArtifact = GetSourceArtifactFromInventoryCommand(uow, transaction.InventoryCommandId.InventoryCommandId);

            if (billArtifact != null && billArtifact is NAS.DAL.Invoice.PurchaseInvoice)
            {
                IEnumerable<InventoryJournal> ActualInventoryJournal = GetActualInventoryJournalOfBill(
                uow, billArtifact.BillId, DefaultObjectTypeCustomFieldEnum.INVENTORY_IN_PURCHASE_INVOICE);

                XPCollection<BillItem> billItems = (billArtifact as NAS.DAL.Invoice.PurchaseInvoice).BillItems;
                NAS.DAL.Nomenclature.Inventory.Inventory defaultInventory =
                    NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(uow, DAL.Nomenclature.Inventory.DefaultInventoryEnum.NOT_AVAILABLE);
                NAS.DAL.Inventory.Lot.Lot defaultLot =
                    NAS.DAL.Inventory.Lot.Lot.GetDefault(uow, NAS.DAL.Inventory.Lot.DefaultLotEnum.NOT_AVAILABLE);

                int numOfCompleteItemUnit = 0;
                foreach (BillItem billItem in billItems)
                {
                    if (billItem.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE && 
                        !billItem.ItemUnitId.ItemId.ItemObjects.FirstOrDefault().ObjectId.ObjectTypeId.Name.Equals("PRODUCT"))
                        continue;
                    double DeliveredNumber = ActualInventoryJournal.Where(j => j.ItemUnitId == billItem.ItemUnitId).Sum(j => j.Credit);
                    double NeededNumOfItem = billItem.Quantity - DeliveredNumber;

                    if (NeededNumOfItem == 0)
                    {
                        numOfCompleteItemUnit += 1;
                        continue;
                    }

                    createDoubleInventoryJournalForInOutTransaction(
                        uow,
                        _InventoryCommandItemTransactionId,
                        defaultInventory.InventoryId,
                        defaultLot.LotId, billItem.ItemUnitId.ItemUnitId, NeededNumOfItem, NeededNumOfItem, string.Empty, ObjectTypeEnum.INVENTORY_IN);
                }

                if (billItems.Count == numOfCompleteItemUnit)
                {
                    throw new Exception("Đã giao đủ hàng cho phiếu bán");
                }
            }
        }

        public bool CheckIsCompletedDeliveryForPurchaseInvoice(UnitOfWork uow, Guid PurchaseInvoiceId)
        {
            NAS.DAL.Invoice.PurchaseInvoice Bill = uow.GetObjectByKey<NAS.DAL.Invoice.PurchaseInvoice>(PurchaseInvoiceId);
            if (Bill == null)
                throw new Exception("The PurchaseInvoice is not exist in system");
            XPCollection<BillItem> billItems = Bill.BillItems;

            if (billItems == null || billItems.Count == 0)
                return false;

            IEnumerable<InventoryJournal> ActualInventoryJournal = GetActualInventoryJournalOfBill(
                uow, Bill.BillId, DefaultObjectTypeCustomFieldEnum.INVENTORY_IN_PURCHASE_INVOICE);

            if (ActualInventoryJournal == null || ActualInventoryJournal.Count() == 0)
                return false;

            int numOfCompleteItemUnit = 0;
            foreach (BillItem billItem in billItems)
            {
                if (billItem.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE && 
                    !billItem.ItemUnitId.ItemId.ItemObjects.FirstOrDefault().ObjectId.ObjectTypeId.Name.Equals("PRODUCT"))
                    continue;
                double DeliveredNumber = ActualInventoryJournal.Where(j => j.ItemUnitId == billItem.ItemUnitId).Sum(j => j.Credit);
                double NeededNumOfItem = billItem.Quantity - DeliveredNumber;

                if (NeededNumOfItem == 0)
                {
                    numOfCompleteItemUnit += 1;
                    continue;
                }
            }

            if (numOfCompleteItemUnit < billItems.Count)
                return false;

            return true;
        }

        public void SaveMovingInventoryCommand(
            UnitOfWork uow, Guid MovingInventoryCommandId, string MovingCode, DateTime MovingIssueDate, string MovingDescription,
            Guid OutCmdInventoryCommandId, string OutCmdCode, DateTime OutCmdIssueDate, string OutCmdDescription, Guid OutCmdInventoryId,
            Guid InCmdInventoryCommandId, string InCmdCode, DateTime InCmdIssueDate, string InCmdDescription, Guid InCmdInventoryId
        )
        {
            InventoryCommand MovingCommand = uow.GetObjectByKey<InventoryCommand>(MovingInventoryCommandId);
            if (MovingCommand == null)
                throw new Exception("The InventoryCommand is not exist in system");
            if (MovingCommand.RowStatus == Utility.Constant.ROWSTATUS_TEMP)
            {
                MovingCommand.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            }

            MovingCommand.Code = MovingCode;
            MovingCommand.IssueDate = MovingIssueDate;
            MovingCommand.Description = MovingDescription;
            MovingCommand.Name = MovingCode;
            MovingCommand.CommandType = 'M';
            MovingCommand.Save();

            SaveInOutInventoryCommand(uow, OutCmdInventoryCommandId, OutCmdCode, OutCmdIssueDate, OutCmdDescription, OutCmdInventoryId);
            SaveInOutInventoryCommand(uow, InCmdInventoryCommandId, InCmdCode, InCmdIssueDate, InCmdDescription, InCmdInventoryId);

        }

        public void SaveInOutInventoryCommand(UnitOfWork uow, Guid InventoryCommandId, string code, DateTime IssueDate, string Description, Guid InventoryId)
        {
            InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");

            InventoryCommand RelevantInventoryCommand = searchRelevantInventoryCommand(uow, InventoryCommandId);

            if (RelevantInventoryCommand != null)
            {
                NAS.DAL.Nomenclature.Inventory.Inventory FirstInventory = getFirstInventoryJournalForOutputCommand(
                    uow,
                    RelevantInventoryCommand.InventoryCommandItemTransactions.FirstOrDefault().InventoryTransactionId);

                if (FirstInventory == null)
                    return;

                NAS.DAL.Nomenclature.Inventory.Inventory naInventory = NAS.DAL.Nomenclature.Inventory.Inventory.GetDefault(
                    uow, DefaultInventoryEnum.NOT_AVAILABLE);

                Guid newInventoryId = InventoryId;

                if (RelevantInventoryCommand.ParentInventoryCommandId.CommandType.Equals('M') && 
                    newInventoryId.Equals(FirstInventory.InventoryId) && !newInventoryId.Equals(naInventory.InventoryId))
                {
                    throw new Exception(string.Format("Kho '{0}' đã được chọn trong Phiếu chuyển kho '{1}'! Vui lòng chọn kho khác",
                        FirstInventory.Code, RelevantInventoryCommand.ParentInventoryCommandId.Code));
                }
            }

            if (command.RowStatus == Utility.Constant.ROWSTATUS_TEMP)
            {
                command.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
            }

            command.Code = code;
            command.IssueDate = IssueDate;
            command.Description = Description;
            command.Name = command.Code;

            if (command.CommandType == INVENTORY_COMMAND_TYPE.OUT)
                command.CommandType = 'O';
            else if (command.CommandType == INVENTORY_COMMAND_TYPE.IN)
                command.CommandType = 'I';

            command.Save();

            UpdateInventoryForAllRelevantInventoryJournal(uow, InventoryCommandId, InventoryId);
        }

        public void UpdateInventoryForAllRelevantInventoryJournal(UnitOfWork uow, Guid InventoryCommandId, Guid InventoryId)
        {
            InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");

            if (command.CommandType != INVENTORY_COMMAND_TYPE.IN && command.CommandType != INVENTORY_COMMAND_TYPE.OUT)
                return;

            NAS.DAL.Nomenclature.Inventory.Inventory Inventory = uow.GetObjectByKey<NAS.DAL.Nomenclature.Inventory.Inventory>(InventoryId);

            if (Inventory == null)
                throw new Exception("The Inventory is not exist in system");

            IEnumerable<InventoryJournal> journals = command.InventoryCommandItemTransactions.SelectMany(j => j.InventoryJournals).Where(
                    j => (j.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE ||
                              j.RowStatus == Utility.Constant.ROWSTATUS_BOOKED_ENTRY) &&
                         (j.Credit > 0 || j.Debit > 0) &&
                         (j.JournalType == 'A' || j.JournalType == 'P'));

            IEnumerable<InventoryJournal> UpdateJournals = null;

            if (command.CommandType == INVENTORY_COMMAND_TYPE.OUT)
                UpdateJournals = journals.Where(j => j.Credit > 0 && j.Debit == 0);
            else if (command.CommandType == INVENTORY_COMMAND_TYPE.IN)
                UpdateJournals = journals.Where(j => j.Debit > 0 && j.Credit == 0);   

            if (UpdateJournals == null)
                    return;

            foreach(InventoryJournal j in UpdateJournals)
            {
                j.InventoryId = Inventory;
                j.Save();
            }

            //uow.FlushChanges();
        }

        public Person GetSelectedActorInventoryCommandCombobox(Guid InventoryCommandId, DefaultInventoryCommandActorTypeEnum DefaultActorType)
        {
            Session session = XpoHelper.GetNewSession();
            try
            {
                InventoryCommand command = session.GetObjectByKey<InventoryCommand>(InventoryCommandId);
                if (command == null)
                    throw new Exception("The InventoryCommand is not exist in system");
                InventoryCommandActorType ActorType = NAS.DAL.Inventory.Command.InventoryCommandActorType.GetDefault(session, DefaultActorType);
                if (ActorType == null)
                    throw new Exception("The InventoryCommandActorType is not exist in system");

                InventoryCommandActor Actor = session.FindObject<InventoryCommandActor>(CriteriaOperator.And(
                        new BinaryOperator("InventoryCommandActorTypeId!Key", ActorType.InventoryCommandActorTypeId, BinaryOperatorType.Equal),
                        new BinaryOperator("InventoryCommandId!Key", InventoryCommandId, BinaryOperatorType.Equal)
                    ));

                if (Actor == null)
                {
                    Actor = new InventoryCommandActor(session);
                    Actor.InventoryCommandActorTypeId = ActorType;
                    Actor.InventoryCommandId = command;
                    Actor.Save();
                }

                return Actor.PersonId;
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                session.Dispose();
            }
        }

        public void UpdateSelectedActorInventoryCommandCombobox(UnitOfWork uow, Guid InventoryCommandId, DefaultInventoryCommandActorTypeEnum DefaultActorType, Guid PersonId)
        {
            InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");
            InventoryCommandActorType ActorType = NAS.DAL.Inventory.Command.InventoryCommandActorType.GetDefault(uow, DefaultActorType);
            if (ActorType == null)
                throw new Exception("The InventoryCommandActorType is not exist in system");
            Person person =  uow.GetObjectByKey<Person>(PersonId);
            if (person == null)
                throw new Exception("The person is not exist in system");

            InventoryCommandActor Actor = uow.FindObject<InventoryCommandActor>(CriteriaOperator.And(
                    new BinaryOperator("InventoryCommandActorTypeId!Key", ActorType.InventoryCommandActorTypeId, BinaryOperatorType.Equal),
                    new BinaryOperator("InventoryCommandId!Key", InventoryCommandId, BinaryOperatorType.Equal)
                ));

            if (Actor == null)
            {
                Actor = new InventoryCommandActor(uow);
                Actor.InventoryCommandActorTypeId = ActorType;
                Actor.InventoryCommandId = command;
            }
            Actor.PersonId = person;
            Actor.Save();
        }
    }
}
