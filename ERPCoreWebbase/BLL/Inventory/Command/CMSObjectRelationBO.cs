using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Inventory.Command.CommanDynamicField;
using NAS.DAL.Inventory.Command;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Accounting.JournalAllocation;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using NAS.DAL;

namespace NAS.BO.Inventory.Command
{
    public partial class InventoryCommandBO 
    {
        /// <summary>
        /// Lấy chứng từ phiếu gốc của phiếu xuất/nhập/chuyển kho
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="_InventoryCommandId"></param>
        /// <returns></returns>
        public Bill GetSourceArtifactFromInventoryCommand(
            UnitOfWork uow,
            Guid _InventoryCommandId)
        {
            InventoryCommand command = uow.GetObjectByKey<InventoryCommand>(_InventoryCommandId);
            if (command == null)
                throw new Exception("The InventoryCommand is not exist in system");

            ObjectTypeCustomField ArtifactTypeCustomField = null;
            ObjectCustomField ArtifactCustomField = null;
            Bill billArtifact = null;

            if (command.CommandType.Equals(INVENTORY_COMMAND_TYPE.OUT))
            {
                ArtifactTypeCustomField =
                                ObjectTypeCustomField.GetDefault(uow, DefaultObjectTypeCustomFieldEnum.INVENTORY_OUT_SALE_INVOICE);
                try
                {
                    ArtifactCustomField = command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields
                    .Where(r => r.ObjectTypeCustomFieldId.Equals(ArtifactTypeCustomField)).First();
                }
                catch (Exception)
                {
                    if (ArtifactCustomField == null)
                        return null;
                }
            }
            else if (command.CommandType.Equals(INVENTORY_COMMAND_TYPE.IN))
            {
                ArtifactTypeCustomField =
                                ObjectTypeCustomField.GetDefault(uow, DefaultObjectTypeCustomFieldEnum.INVENTORY_IN_PURCHASE_INVOICE);
                try
                {
                    //ArtifactCustomField = command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields
                    //.Where(r => r.ObjectTypeCustomFieldId.Equals(ArtifactTypeCustomField)).First();

                    foreach (ObjectCustomField cf in command.InventoryCommandObjects.First().ObjectId.ObjectCustomFields)
                    {
                        if (cf.ObjectTypeCustomFieldId.Code != null && cf.ObjectTypeCustomFieldId.Code.Equals(ArtifactTypeCustomField.Code))
                            ArtifactCustomField = cf;
                    }
                }
                catch (Exception)
                {
                    if (ArtifactCustomField == null)
                        return null;
                }
            }
            else
            {
                return null;
            }

            if (ArtifactCustomField != null)
            {
                try
                {
                    billArtifact = uow.GetObjectByKey<Bill>((ArtifactCustomField.ObjectCustomFieldDatas.First().CustomFieldDataId as PredefinitionData).RefId);
                }
                catch (Exception)
                {
                    billArtifact = null;
                }
            }
            return billArtifact;
        }

        public IEnumerable<NAS.DAL.CMS.ObjectDocument.Object> GetRelatedCMSObjectWithInventoryCommandItemTransaction(
            UnitOfWork uow, 
            Guid InventoryCommandItemTransactionId)
        {
            //Get all journal of transaction
            NAS.DAL.Inventory.Command.InventoryCommandItemTransaction transaction =
                uow.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommandItemTransaction>(InventoryCommandItemTransactionId);
            IEnumerable<NAS.DAL.CMS.ObjectDocument.Object> cmsObjects1 =
                transaction.InventoryJournals
                    .Select(r => r.InventoryJournalObjects.FirstOrDefault())
                    .Select(r => r.ObjectId);

            IEnumerable<NAS.DAL.CMS.ObjectDocument.Object> cmsObjects2 = transaction.InventoryJournals
                    .Where(r => r.Credit > 0 && r.JournalType.Equals('A') && r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                    .Select(r => r.InventoryJournalFinancials.FirstOrDefault())
                    .Select(r => r.TransactionId.TransactionObjects.FirstOrDefault())
                    .Select(r => r.ObjectId);

            IEnumerable<NAS.DAL.CMS.ObjectDocument.Object> cmsObjects3 =
                    transaction.InventoryJournals
                    .Where(r => r.Credit > 0 && r.JournalType.Equals('A') && r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE)
                    .Select(r => r.InventoryJournalFinancials.FirstOrDefault())
                    .SelectMany(r => r.TransactionId.GeneralJournals)
                    .Select(r => r.GeneralJournalObjects.FirstOrDefault()).
                    Select(r => r.ObjectId);

            return cmsObjects1 = cmsObjects1.Union(cmsObjects2).Union(cmsObjects3);
        }


        /// <summary>
        /// Tạo dữ liệu cho CMS Object Inventory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uow"></param>
        /// <param name="InventoryObjectId"></param>
        /// <param name="CMSObjectType"></param>
        public void PopulateCMSObjecForInventoryObject<T>(
            UnitOfWork uow,
            Guid InventoryObjectId,
            DAL.CMS.ObjectDocument.ObjectTypeEnum CMSObjectType)
        {
            ObjectBO bo = new ObjectBO();
            if (typeof(T).Equals(typeof(NAS.DAL.Inventory.Command.InventoryCommand)))
            {
                NAS.DAL.Inventory.Command.InventoryCommand inventoryObject =
                    uow.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryObjectId);
                NAS.DAL.CMS.ObjectDocument.Object o = bo.CreateCMSObject(uow, CMSObjectType);
                InventoryCommandObject ICO = new InventoryCommandObject(uow);
                ICO.ObjectId = o;
                ICO.InventoryCommandId = (inventoryObject as NAS.DAL.Inventory.Command.InventoryCommand);
                ICO.Save();
                NAS.DAL.CMS.ObjectDocument.ObjectType type =
                    NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(uow, CMSObjectType);
                if (type == null)
                    throw new Exception("The ObjectType is not exist in system");

                InventoryCommandCustomType ICCT = new InventoryCommandCustomType(uow);
                ICCT.ObjectTypeId = type;
                ICCT.InventoryCommandId = (inventoryObject as NAS.DAL.Inventory.Command.InventoryCommand);
                ICCT.Save();
            }
            else if (typeof(T).Equals(typeof(NAS.DAL.Inventory.Command.InventoryCommandItemTransaction)))
            {
                InventoryCommandItemTransaction transactionObject =
                    uow.GetObjectByKey<InventoryCommandItemTransaction>(InventoryObjectId);
                NAS.DAL.CMS.ObjectDocument.Object o = bo.CreateCMSObject(uow, CMSObjectType);
                InventoryTransactionObject ITO = new InventoryTransactionObject(uow);
                ITO.ObjectId = o;
                ITO.InventoryTransactionId = (transactionObject as NAS.DAL.Inventory.Journal.InventoryTransaction);
                ITO.Save();

                NAS.DAL.CMS.ObjectDocument.ObjectType type =
                    NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(uow, CMSObjectType);
                InventoryTransactionCustomType ITCT = new InventoryTransactionCustomType(uow);
                ITCT.ObjectTypeId = type;
                ITCT.InventoryTransactionId = (transactionObject as NAS.DAL.Inventory.Journal.InventoryTransaction);
                ITCT.Save();
            }
            else if (typeof(T).Equals(typeof(NAS.DAL.Inventory.Command.InventoryCommandFinancialTransaction)))
            {
                InventoryCommandFinancialTransaction transactionObject =
                    uow.GetObjectByKey<InventoryCommandFinancialTransaction>(InventoryObjectId);
                NAS.DAL.CMS.ObjectDocument.Object o = bo.CreateCMSObject(uow, CMSObjectType);
                TransactionObject TO = new TransactionObject(uow);
                TO.ObjectId = o;
                TO.TransactionId = (transactionObject as Transaction);
                TO.Save();
            }
            else if (typeof(T).Equals(typeof(NAS.DAL.Accounting.Journal.GeneralJournal)))
            {
                GeneralJournal journalObject =
                    uow.GetObjectByKey<GeneralJournal>(InventoryObjectId);
                NAS.DAL.CMS.ObjectDocument.Object o = bo.CreateCMSObject(uow, CMSObjectType);
                GeneralJournalObject GJO = new GeneralJournalObject(uow);
                GJO.ObjectId = o;
                GJO.GeneralJournalId = (journalObject as GeneralJournal);
                GJO.Save();
                NAS.DAL.CMS.ObjectDocument.ObjectType type =
                    NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(uow, CMSObjectType);
                GeneralJournalCustomType GJCT = new GeneralJournalCustomType(uow);
                GJCT.ObjectTypeId = type;
                GJCT.GeneralJournalId = (journalObject as GeneralJournal);
                GJCT.Save();
            }
            else if (typeof(T).Equals(typeof(NAS.DAL.Inventory.Journal.InventoryJournal)))
            {
                InventoryJournal journalObject =
                    uow.GetObjectByKey<InventoryJournal>(InventoryObjectId);
                NAS.DAL.CMS.ObjectDocument.Object o = bo.CreateCMSObject(uow, CMSObjectType);
                InventoryJournalObject ITO = new InventoryJournalObject(uow);
                ITO.ObjectId = o;
                ITO.InventoryJournalId = (journalObject as InventoryJournal);
                ITO.Save();

                NAS.DAL.CMS.ObjectDocument.ObjectType type =
                    NAS.DAL.CMS.ObjectDocument.ObjectType.GetDefault(uow, CMSObjectType);
                InventoryJournalCustomType ITCT = new InventoryJournalCustomType(uow);
                ITCT.ObjectTypeId = type;
                ITCT.InventoryJournalId = (journalObject as InventoryJournal);
                ITCT.Save();
            }
            uow.FlushChanges();
        }

        /// <summary>
        /// Tìm đối tượng CMS object của phiếu kho
        /// </summary>
        /// <param name="session"></param>
        /// <param name="InventoryCommandId"></param>
        /// <returns></returns>
        public NAS.DAL.CMS.ObjectDocument.Object getCMSInventoryCommandObject(
            Session session,
            Guid InventoryCommandId)
        {
            try
            {
                NAS.DAL.Inventory.Command.InventoryCommand command = session.GetObjectByKey<NAS.DAL.Inventory.Command.InventoryCommand>(InventoryCommandId);
                if (command == null)
                    throw new Exception("The InventoryCommand is not exist in system");

                if (command.InventoryCommandObjects == null || command.InventoryCommandObjects.Count == 0)
                    throw new Exception("The InventoryCommand is wrong in configuration");
                return command.InventoryCommandObjects.Select(r => r.ObjectId).First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Tìm đối tượng CMS object của InventoryJournal
        /// </summary>
        /// <typeparam name="T">is NAS.DAL.Inventory.Command.InventoryCommandFinancialTransaction
        ///         or NAS.DAL.Inventory.Command.InventoryCommandItemTransaction
        /// </typeparam>
        /// <param name="transactionId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public NAS.DAL.CMS.ObjectDocument.Object GetCMSTransaction<T>(
            Guid transactionId,
            ObjectTypeEnum type)
        {
            if (type != ObjectTypeEnum.INVENTORY_IN && type != ObjectTypeEnum.INVENTORY_MOVE && type != ObjectTypeEnum.INVENTORY_OUT)
                throw new Exception("The Type is invalid");

            if (!typeof(T).Equals(typeof(NAS.DAL.Inventory.Command.InventoryCommandFinancialTransaction)) &&
                !typeof(T).Equals(typeof(NAS.DAL.Inventory.Command.InventoryCommandItemTransaction)))
            {
                throw new Exception("The Generic Type is invalid");
            }

            if (transactionId != null && !transactionId.Equals(Guid.Empty))
            {
                NAS.DAL.CMS.ObjectDocument.Object cmsObject = null;
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    if (typeof(T).Equals(typeof(NAS.DAL.Inventory.Command.InventoryCommandItemTransaction)))
                    {
                        InventoryCommandItemTransaction transaction = uow.GetObjectByKey<InventoryCommandItemTransaction>(transactionId);
                        InventoryTransactionObject transactionObject =
                            transaction.InventoryTransactionObjects.FirstOrDefault();
                        if (transactionObject == null)
                        {
                            ObjectBO objectBO = new ObjectBO();
                            cmsObject = objectBO.CreateCMSObject(uow, type);
                            InventoryTransactionObject newTransactionObject = new InventoryTransactionObject(uow)
                            {
                                ObjectId = cmsObject,
                                InventoryTransactionId = transaction
                            };

                            NAS.DAL.CMS.ObjectDocument.ObjectType objectType
                                = uow.FindObject<ObjectType>(new BinaryOperator("Name", Enum.GetName(typeof(ObjectTypeEnum), type)));

                            if (objectType == null)
                                throw new Exception("The OjectType is not exist in system");

                            InventoryTransactionCustomType newTransactionCustomType = new InventoryTransactionCustomType(uow)
                            {
                                ObjectTypeId = objectType,
                                InventoryTransactionId = transaction
                            };

                            newTransactionCustomType.Save();
                            uow.CommitChanges();
                        }
                        else
                        {
                            cmsObject = transactionObject.ObjectId;
                        }
                        return cmsObject;
                    }
                    else
                    {
                        InventoryCommandFinancialTransaction transaction = uow.GetObjectByKey<InventoryCommandFinancialTransaction>(transactionId);
                        TransactionObject transactionObject =
                            transaction.TransactionObjects.FirstOrDefault();
                        if (transactionObject == null)
                        {
                            ObjectBO objectBO = new ObjectBO();
                            cmsObject = objectBO.CreateCMSObject(uow, type);
                            TransactionObject newTransactionObject = new TransactionObject(uow)
                            {
                                ObjectId = cmsObject,
                                TransactionId = transaction
                            };
                            uow.CommitChanges();
                        }
                        else
                        {
                            cmsObject = transactionObject.ObjectId;
                        }
                        return cmsObject;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Tìm đối tượng CMS object của InventoryJournal
        /// </summary>
        /// <param name="InventoryJournalId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public NAS.DAL.CMS.ObjectDocument.Object GetCMSInventoryJournal(
            Guid InventoryJournalId,
            ObjectTypeEnum type)
        {
            if (type != ObjectTypeEnum.INVENTORY_IN && type != ObjectTypeEnum.INVENTORY_MOVE && type != ObjectTypeEnum.INVENTORY_OUT)
                throw new Exception("The Type is invalid");

            if (InventoryJournalId != null && !InventoryJournalId.Equals(Guid.Empty))
            {
                NAS.DAL.CMS.ObjectDocument.Object cmsObject = null;
                using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                {
                    InventoryJournal journal = uow.GetObjectByKey<InventoryJournal>(InventoryJournalId);
                    InventoryJournalObject journalObject =
                        journal.InventoryJournalObjects.FirstOrDefault();
                    if (journalObject == null)
                    {
                        ObjectBO objectBO = new ObjectBO();
                        cmsObject = objectBO.CreateCMSObject(uow, type);
                        InventoryJournalObject newJournalObject = new InventoryJournalObject(uow)
                        {
                            ObjectId = cmsObject,
                            InventoryJournalId = journal
                        };

                        NAS.DAL.CMS.ObjectDocument.ObjectType objectType
                                = uow.FindObject<ObjectType>(new BinaryOperator("Name", Enum.GetName(typeof(ObjectTypeEnum), type)));

                        if (objectType == null)
                            throw new Exception("The OjectType is not exist in system");

                        InventoryJournalCustomType newJournalCustomType = new InventoryJournalCustomType(uow)
                        {
                            ObjectTypeId = objectType,
                            InventoryJournalId = journal
                        };

                        newJournalCustomType.Save();

                        uow.CommitChanges();
                    }
                    else
                    {
                        cmsObject = journalObject.ObjectId;
                    }
                    return cmsObject;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
