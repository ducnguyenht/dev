using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.CMS.ObjectDocument;
using NAS.DAL;
using DevExpress.Data.Filtering;
using Utility;
using System.IO;

namespace NAS.BO.CMS.ObjectDocument
{
    public enum CustomFieldCategoryEnum
    {
        BASIC,
        LIST,
        BUILT_IN
    }

    public abstract class CustomFieldDataEntityBase
    {
        private Guid _ObjectCustomFieldId;
        private Guid _ObjectTypeCustomFieldId;
        private Guid _CMSObjectId;
        private Guid _CustomFieldId;
        private CustomFieldTypeFlag _ObjectCustomFieldFlag;
        private CustomFieldCategoryEnum _CustomFieldCategory;

        //private object _NewBasicDataValue;
        //private BasicCustomFieldTypeEnum _BasicCustomFieldTypeEnum;
        //private List<NASCustomFieldPredefinitionData> _NewBuiltInData;
        //private List<Guid> _NewCustomFieldDataIds;

        public CustomFieldDataEntityBase
            (
                Guid objectCustomFieldId,
                Guid objectTypeCustomFieldId,
                Guid cmsObjectId,
                Guid customFieldId,
                CustomFieldTypeFlag customFieldTypeFlag,
                CustomFieldCategoryEnum customFieldCategory
            )
        {
            this._ObjectCustomFieldId = objectCustomFieldId;
            this._ObjectTypeCustomFieldId = objectTypeCustomFieldId;
            this._CMSObjectId = cmsObjectId;
            this._CustomFieldId = customFieldId;
            this._CustomFieldCategory = customFieldCategory;
            this._ObjectCustomFieldFlag = customFieldTypeFlag;
        }

        public Guid ObjectCustomFieldId
        {
            get { return this._ObjectCustomFieldId; }
        }

        public Guid ObjectTypeCustomFieldId
        {
            get { return this._ObjectTypeCustomFieldId; }
        }

        public Guid CMSObjectId
        {
            get { return this._CMSObjectId; }
        }

        public Guid CustomFieldId
        {
            get { return this._CustomFieldId; }
        }

        public CustomFieldCategoryEnum CustomFieldCategory
        {
            get { return this._CustomFieldCategory; }
        }

        public CustomFieldTypeFlag ObjectCustomFieldFlag
        {
            get { return this._ObjectCustomFieldFlag; }
        }
    }

    public class BasicCustomFieldDataEntity : CustomFieldDataEntityBase
    {
        private object _DataValue;
        private BasicCustomFieldTypeEnum _BasicCustomFieldTypeEnum;

        public BasicCustomFieldDataEntity
            (
                Guid objectCustomFieldId,
                Guid objectTypeCustomFieldId,
                Guid cmsObjectId,
                Guid customFieldId,
                CustomFieldTypeFlag customFieldTypeFlag,
                CustomFieldCategoryEnum customFieldCategory,
                object basicDataValue,
                BasicCustomFieldTypeEnum basicCustomFieldTypeEnum
            )
            : base(objectCustomFieldId,
                objectTypeCustomFieldId,
                cmsObjectId,
                customFieldId,
                customFieldTypeFlag,
                customFieldCategory)
        {
            this._DataValue = basicDataValue;
            this._BasicCustomFieldTypeEnum = basicCustomFieldTypeEnum;
        }

        public object BasicDataValue
        {
            get
            {
                return this._DataValue;
            }
        }

        public BasicCustomFieldTypeEnum BasicCustomFieldType
        {
            get
            {
                return this._BasicCustomFieldTypeEnum;
            }
        }
    }

    public class UserDefinedListCustomFieldDataEntity : CustomFieldDataEntityBase
    {
        private IEnumerable<Guid> _UserDefinedItemIds;

        public UserDefinedListCustomFieldDataEntity
            (
                Guid objectCustomFieldId,
                Guid objectTypeCustomFieldId,
                Guid cmsObjectId,
                Guid customFieldId,
                CustomFieldTypeFlag customFieldTypeFlag,
                CustomFieldCategoryEnum customFieldCategory,
                IEnumerable<Guid> userDefinedItemIds
            )
            : base(objectCustomFieldId,
                objectTypeCustomFieldId,
                cmsObjectId,
                customFieldId,
                customFieldTypeFlag,
                customFieldCategory)
        {
            this._UserDefinedItemIds = userDefinedItemIds;
        }

        public IEnumerable<Guid> UserDefinedItemIds
        {
            get
            {
                return _UserDefinedItemIds;
            }
        }
    }

    public class PredefinitionCustomFieldEntity : CustomFieldDataEntityBase
    {
        private IEnumerable<Guid> _PredefinitionRefIds;
        private string _PredefinitionType;

        public PredefinitionCustomFieldEntity
            (
                Guid objectCustomFieldId,
                Guid objectTypeCustomFieldId,
                Guid cmsObjectId,
                Guid customFieldId,
                CustomFieldTypeFlag customFieldTypeFlag,
                CustomFieldCategoryEnum customFieldCategory,
                IEnumerable<Guid> predefinitionRefIds,
                string predefinitionType
            )
            : base(objectCustomFieldId,
                objectTypeCustomFieldId,
                cmsObjectId,
                customFieldId,
                customFieldTypeFlag,
                customFieldCategory)
        {
            this._PredefinitionRefIds = predefinitionRefIds;
            this._PredefinitionType = predefinitionType;
        }

        public IEnumerable<Guid> PredefinitionRefIds
        {
            get
            {
                return this._PredefinitionRefIds;
            }
        }

        public string PredefinitionType
        {
            get
            {
                return this._PredefinitionType;
            }
        }
    }

    public class ObjectCustomFieldOption
    {
        public Guid CustomFieldId
        {
            get;
            set;
        }

        public CustomFieldTypeFlag ObjectCustomFieldFlag
        {
            get;
            set;
        }
    }

    public class ObjectBO
    {
        private CustomFieldData InitDefaultDataForBasicCustomFieldType(
            Session session,
            BasicCustomFieldTypeEnum basicCustomFieldType,
            Guid customFieldId)
        {
            CustomFieldData customFieldData = null;
            CustomField customField = null;

            customField = session.GetObjectByKey<CustomField>(customFieldId);

            if (customField == null)
            {
                throw new Exception("Could not found the custom field");
            }

            switch (basicCustomFieldType)
            {
                case BasicCustomFieldTypeEnum.STRING:
                    customFieldData = new CustomFieldDataString(session)
                    {
                        CustomFieldDataId = Guid.NewGuid(),
                        CustomFieldId = customField,
                        StringValue = String.Empty
                    };
                    customFieldData.Save();
                    break;
                case BasicCustomFieldTypeEnum.DATETIME:
                    customFieldData = new CustomFieldDataDateTime(session)
                    {
                        CustomFieldDataId = Guid.NewGuid(),
                        CustomFieldId = customField,
                        DateTimeValue = DateTime.MinValue
                    };
                    customFieldData.Save();
                    break;
                case BasicCustomFieldTypeEnum.FLOAT:
                    customFieldData = new CustomFieldDataFloat(session)
                    {
                        CustomFieldDataId = Guid.NewGuid(),
                        CustomFieldId = customField,
                        FloatValue = float.MinValue
                    };
                    customFieldData.Save();
                    break;
                case BasicCustomFieldTypeEnum.INTEGER:
                    customFieldData = new CustomFieldDataInt(session)
                    {
                        CustomFieldDataId = Guid.NewGuid(),
                        CustomFieldId = customField,
                        IntValue = int.MinValue
                    };
                    customFieldData.Save();
                    break;
                default:
                    break;
            }

            return customFieldData;
        }

        private bool AttachObjectCustomFieldWithDefaultValue(Session session
            , DAL.CMS.ObjectDocument.Object CMSObject
            , NAS.DAL.CMS.ObjectDocument.ObjectTypeCustomField objectTypeCustomField
            , CustomFieldTypeFlag option)
        {
            try
            {
                //Create ObjectCustomField
                ObjectCustomField objectCustomField = new ObjectCustomField(session)
                {
                    ObjectCustomFieldId = Guid.NewGuid(),
                    ObjectId = CMSObject,
                    ObjectTypeCustomFieldId = objectTypeCustomField,
                    CustomFieldType = option.Value
                };
                objectCustomField.Save();

                //Insert default value for custom field
                CustomFieldData customFieldData = null;

                string customFieldTypeCode = objectTypeCustomField.CustomFieldId.CustomFieldTypeId.Code;

                BasicCustomFieldTypeEnum basicCustomFieldType = BasicCustomFieldTypeEnum.NONE;

                bool isBasicType =
                    Enum.TryParse<BasicCustomFieldTypeEnum>(customFieldTypeCode, out basicCustomFieldType);

                if (isBasicType)
                {
                    customFieldData = InitDefaultDataForBasicCustomFieldType(session,
                        (BasicCustomFieldTypeEnum)Enum.Parse(typeof(BasicCustomFieldTypeEnum), customFieldTypeCode),
                        objectTypeCustomField.CustomFieldId.CustomFieldId);

                    if (customFieldData != null)
                    {
                        //Insert into ObjectCustomFieldData
                        ObjectCustomFieldData defaultObjectCustomFieldData =
                            new ObjectCustomFieldData(session)
                            {
                                ObjectCustomFieldDataId = Guid.NewGuid(),
                                CustomFieldDataId = customFieldData,
                                ObjectCustomFieldId = objectCustomField
                            };
                        defaultObjectCustomFieldData.Save();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NAS.DAL.CMS.ObjectDocument.Object CreateCMSObject(Session session, Guid CMSObjectTypeId)
        {
            try
            {
                //Get object type
                NAS.DAL.CMS.ObjectDocument.ObjectType CMSObjectType =
                    session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.ObjectType>(CMSObjectTypeId);

                string objectTypeCode = CMSObjectType.Name;

                ObjectTypeEnum objectType = (ObjectTypeEnum)Enum.Parse(typeof(ObjectTypeEnum), objectTypeCode);

                return CreateCMSObject(session, objectType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NAS.DAL.CMS.ObjectDocument.Object CreateCMSObject(Session session,
            ObjectTypeEnum type,
            params ObjectCustomFieldOption[] options)
        {
            NAS.DAL.CMS.ObjectDocument.Object CMSObject = null;
            try
            {
                //Get object type
                NAS.DAL.CMS.ObjectDocument.ObjectType CMSObjectType = ObjectType.GetDefault(session, type);
                //Create new CMS Object
                CMSObject = new DAL.CMS.ObjectDocument.Object(session)
                {
                    ObjectId = Guid.NewGuid(),
                    ObjectTypeId = CMSObjectType
                };
                CMSObject.Save();

                foreach (var objectTypeCustomField in CMSObjectType.ObjectTypeCustomFields)
                {
                    CustomFieldTypeFlag flag = CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT;
                    if (options.Length > 0)
                    {
                        ObjectCustomFieldOption option =
                            options.Where(r => r.CustomFieldId == objectTypeCustomField.CustomFieldId.CustomFieldId).FirstOrDefault();
                        if (option != null)
                        {
                            flag = option.ObjectCustomFieldFlag;
                        }
                    }
                    AttachObjectCustomFieldWithDefaultValue(session, CMSObject, objectTypeCustomField, flag);
                }
                return CMSObject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCMSObject(Session session, Guid CMSObjectId)
        {
            try
            {
                NAS.DAL.CMS.ObjectDocument.Object CMSObject =
                    session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(CMSObjectId);
                CMSObject.Delete();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Obsolete("For performance purpose of attaching new custom fields so this method will be delete in future. Use UpdateCustomFields method instead.", true)]
        public bool UpdateCMSObjects(Session session, Guid CMSObjectTypeId)
        {
            try
            {
                //Get object type
                NAS.DAL.CMS.ObjectDocument.ObjectType CMSObjectType =
                    session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.ObjectType>(CMSObjectTypeId);
                //Insert new customfield for all object
                if (CMSObjectType.ObjectTypeCustomFields != null)
                {
                    foreach (var objectTypeCustomField in CMSObjectType.ObjectTypeCustomFields)
                    {
                        //If is new custom field of the object type
                        if (objectTypeCustomField.ObjectCustomFields == null
                            || objectTypeCustomField.ObjectCustomFields.Count == 0)
                        {
                            //Attach new custom field for all cms object of the object type if exist
                            if (CMSObjectType.Objects != null)
                            {
                                foreach (var CMSObject in CMSObjectType.Objects)
                                {
                                    AttachObjectCustomFieldWithDefaultValue(session, CMSObject, objectTypeCustomField, CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCustomFields(Guid CMSObjectId)
        {
            if (CMSObjectId.Equals(Guid.Empty))
            {
                return true;
            }
            UnitOfWork uow = null;
            try
            {
                uow = XpoHelper.GetNewUnitOfWork();
                //Get object
                NAS.DAL.CMS.ObjectDocument.Object CMSObject =
                    uow.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(CMSObjectId);
                if (CMSObject == null)
                {
                    throw new Exception("Cannot update custom fields of null");
                }
                ObjectType objectType = CMSObject.ObjectTypeId;
                if (objectType == null)
                {
                    throw new Exception("The object is not of any object type");
                }
                if (objectType.ObjectTypeCustomFields != null)
                {
                    foreach (var objectTypeCustomField in objectType.ObjectTypeCustomFields)
                    {
                        //Check the custom field has already attached in the object
                        int countAttachment =
                            objectTypeCustomField.ObjectCustomFields.Where(r => r.ObjectId == CMSObject).Count();
                        if (countAttachment == 0)
                        {
                            //Attach the custom field for the object and insert default value for the field
                            AttachObjectCustomFieldWithDefaultValue(uow, CMSObject, objectTypeCustomField, CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
                        }
                    }
                }
                uow.CommitChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (uow != null) uow.Dispose();
            }
        }

        public XPCollection<NAS.DAL.CMS.ObjectDocument.Object> FindCMSObjectsOfBuiltInCustomField(
            Session session,
            Guid objectTypeCustomFieldId,
            List<Guid> refIds)
        {
            try
            {
                if (refIds == null)
                    return null;

                XPCollection<NAS.DAL.CMS.ObjectDocument.Object> objectList = null;
                //Get object type custom field
                ObjectTypeCustomField objectTypeCustomField =
                    session.GetObjectByKey<ObjectTypeCustomField>(objectTypeCustomFieldId);

                if (objectTypeCustomField == null)
                    return null;
                if (objectTypeCustomField.ObjectCustomFields == null
                    || objectTypeCustomField.ObjectCustomFields.Count == 0)
                    return null;

                CriteriaOperator criteria =
                    new ContainsOperator("ObjectCustomFieldDatas",
                        new InOperator("CustomFieldDataId.<PredefinitionData>RefId", refIds));

                XPCollection<ObjectCustomField> objectCustomFieldList =
                    objectTypeCustomField.ObjectCustomFields;
                objectCustomFieldList.Criteria = criteria;

                objectList =
                    new XPCollection<DAL.CMS.ObjectDocument.Object>(session,
                        objectCustomFieldList.Select(r => r.ObjectId));
                //(XPCollection<NAS.DAL.CMS.ObjectDocument.Object>)
                //    objectCustomFieldList.Select(r => r.ObjectId);

                return objectList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CustomFieldDataEntityBase> GetCustomFieldData(Guid CMSObjectId, bool isOnlyGetReadOnly)
        {
            List<CustomFieldDataEntityBase> ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get object
                NAS.DAL.CMS.ObjectDocument.Object cmsObject =
                    session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(CMSObjectId);
                //Get readonly object custom field
                IEnumerable<ObjectCustomField> objectCustomFields = null;
                if (isOnlyGetReadOnly)
                {
                    objectCustomFields = cmsObject.ObjectCustomFields.Where(r =>
                        !r.CustomFieldType.Equals(Utility.CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_DEFAULT));
                }
                else
                {
                    objectCustomFields = cmsObject.ObjectCustomFields;
                }
                if (objectCustomFields == null)
                    return null;

                ret = new List<CustomFieldDataEntityBase>();

                foreach (var objectCustomField in objectCustomFields)
                {
                    ret.Add(GetObjectCustomFieldData(objectCustomField));
                }

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public void CopyCustomFieldData(Guid sourceCMSObjectId, Guid targetCMSObjectId, bool isOnlyGetReadOnly)
        {
            ObjectCustomFieldBO objectCustomFieldBO = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get source object
                NAS.DAL.CMS.ObjectDocument.Object sourceCMSObject =
                    session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(sourceCMSObjectId);

                if (sourceCMSObject == null)
                    throw new Exception("Source object cannot be null");

                NAS.DAL.CMS.ObjectDocument.Object targetCMSObject =
                    session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(targetCMSObjectId);

                if (targetCMSObject == null)
                    throw new Exception("Target object cannot be null");

                if (!sourceCMSObject.ObjectTypeId.Equals(targetCMSObject.ObjectTypeId))
                    throw new Exception("Object type of the source and the target is different");

                if (isOnlyGetReadOnly)
                {
                    int countReadOnlyData =
                        sourceCMSObject.ObjectCustomFields.Count(r =>
                        !r.CustomFieldType.Equals(Utility.CustomFieldTypeConstant.CUSTOM_FIELD_TYPE_DEFAULT));
                    if (countReadOnlyData == 0)
                    {
                        return;
                    }
                }

                IEnumerable<CustomFieldDataEntityBase> sourceCMSObjectData =
                    GetCustomFieldData(sourceCMSObjectId, isOnlyGetReadOnly);

                objectCustomFieldBO = new ObjectCustomFieldBO();

                //Update target's custom fields before copy data from source
                UpdateCustomFields(targetCMSObject.ObjectId);

                foreach (var sourceData in sourceCMSObjectData)
                {

                    ObjectCustomField targetObjectCustomField = targetCMSObject.ObjectCustomFields
                        .Where(r => r.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId
                                == sourceData.ObjectTypeCustomFieldId)
                        .First();

                    BasicCustomFieldDataEntity tempBasicDataEntity = null;
                    UserDefinedListCustomFieldDataEntity tempUserDefinedListEntity = null;
                    PredefinitionCustomFieldEntity tempPredefinitionDataEntity = null;

                    switch (sourceData.CustomFieldCategory)
                    {
                        case CustomFieldCategoryEnum.BASIC:
                            tempBasicDataEntity = (BasicCustomFieldDataEntity)sourceData;
                            //Update basic data
                            objectCustomFieldBO.UpdateBasicData(
                                targetObjectCustomField.ObjectCustomFieldId,
                                tempBasicDataEntity.BasicDataValue,
                                tempBasicDataEntity.BasicCustomFieldType,
                                tempBasicDataEntity.ObjectCustomFieldFlag);
                            break;
                        case CustomFieldCategoryEnum.LIST:
                            tempUserDefinedListEntity = (UserDefinedListCustomFieldDataEntity)sourceData;
                            //Update user defined list data
                            objectCustomFieldBO.UpdateUserDefinedListData(
                                targetObjectCustomField.ObjectCustomFieldId,
                                tempUserDefinedListEntity.UserDefinedItemIds,
                                tempUserDefinedListEntity.ObjectCustomFieldFlag);
                            break;
                        case CustomFieldCategoryEnum.BUILT_IN:
                            tempPredefinitionDataEntity = (PredefinitionCustomFieldEntity)sourceData;
                            //Update predefinition data
                            PredefinitionCustomFieldTypeEnum predefinitionCustomFieldTypeEnum;
                            bool isValidPredefinitionType =
                                Enum.TryParse<PredefinitionCustomFieldTypeEnum>(
                                    tempPredefinitionDataEntity.PredefinitionType,
                                    out predefinitionCustomFieldTypeEnum);
                            if (!isValidPredefinitionType)
                                throw new Exception("Invalid predeninition type");

                            objectCustomFieldBO.UpdatePredefinitionData(
                                targetObjectCustomField.ObjectCustomFieldId,
                                tempPredefinitionDataEntity.PredefinitionRefIds,
                                predefinitionCustomFieldTypeEnum,
                                tempPredefinitionDataEntity.ObjectCustomFieldFlag);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public void CopyReadOnlyCustomFieldData(Guid sourceCMSObjectId, Guid targetCMSObjectId)
        {
            CopyCustomFieldData(sourceCMSObjectId, targetCMSObjectId, true);
        }

        public CustomFieldDataEntityBase GetObjectCustomFieldData(ObjectCustomField objectCustomField)
        {
            try
            {
                CustomFieldDataEntityBase ret = null;

                if (objectCustomField == null)
                    return null;

                //Determine CustomFieldCategoryEnum
                string customFieldTypeString =
                    objectCustomField.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldTypeId.Code;

                CustomFieldTypeEnum customFieldTypeEnum;

                bool isParseSuccess =
                    Enum.TryParse<CustomFieldTypeEnum>(customFieldTypeString, out customFieldTypeEnum);

                if (!isParseSuccess)
                    throw new Exception(String.Format("Not supported the CustomFieldType '{0}'", customFieldTypeString));

                var customFieldData =
                        objectCustomField.ObjectCustomFieldDatas.Select(r => r.CustomFieldDataId);

                if (customFieldData == null || customFieldData.Count() == 0)
                    return null;

                ObjectCustomFieldData objectCustomFieldData = customFieldData.First().ObjectCustomFieldDatas.First();

                CustomFieldTypeFlag customFieldTypeFlag =
                    CustomFieldTypeFlag.Parse(objectCustomField.CustomFieldType);

                #region User defined list data type
                if (customFieldTypeEnum == CustomFieldTypeEnum.MULTI_CHOICE_LIST
                    || customFieldTypeEnum == CustomFieldTypeEnum.SINGLE_CHOICE_LIST)
                {
                    ret = new UserDefinedListCustomFieldDataEntity(
                            objectCustomFieldData.ObjectCustomFieldId.ObjectCustomFieldId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectId.ObjectId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldId,
                            customFieldTypeFlag,
                            CustomFieldCategoryEnum.LIST,
                            customFieldData.Select(r => r.CustomFieldDataId)
                        );
                }
                #endregion

                #region Basic data type
                else if (customFieldTypeEnum == CustomFieldTypeEnum.DATETIME
                    || customFieldTypeEnum == CustomFieldTypeEnum.FLOAT
                    || customFieldTypeEnum == CustomFieldTypeEnum.INTEGER
                    || customFieldTypeEnum == CustomFieldTypeEnum.STRING)
                {
                    BasicCustomFieldTypeEnum basicType =
                        (BasicCustomFieldTypeEnum)Enum.Parse(typeof(BasicCustomFieldTypeEnum),
                                Enum.GetName(typeof(CustomFieldTypeEnum), customFieldTypeEnum));
                    object dataValue = null;

                    switch (customFieldTypeEnum)
                    {
                        case CustomFieldTypeEnum.STRING:
                            basicType = BasicCustomFieldTypeEnum.STRING;
                            dataValue = ((CustomFieldDataString)customFieldData.First()).StringValue;
                            break;
                        case CustomFieldTypeEnum.DATETIME:
                            basicType = BasicCustomFieldTypeEnum.DATETIME;
                            dataValue = ((CustomFieldDataDateTime)customFieldData.First()).DateTimeValue;
                            break;
                        case CustomFieldTypeEnum.FLOAT:
                            basicType = BasicCustomFieldTypeEnum.FLOAT;
                            dataValue = ((CustomFieldDataFloat)customFieldData.First()).FloatValue;
                            break;
                        case CustomFieldTypeEnum.INTEGER:
                            basicType = BasicCustomFieldTypeEnum.INTEGER;
                            dataValue = ((CustomFieldDataInt)customFieldData.First()).IntValue;
                            break;
                    }

                    ret = new BasicCustomFieldDataEntity(
                        objectCustomFieldData.ObjectCustomFieldId.ObjectCustomFieldId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectId.ObjectId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldId,
                            customFieldTypeFlag,
                            CustomFieldCategoryEnum.BASIC,
                            dataValue,
                            basicType);
                }
                #endregion Basic data type

                #region Predefinition data type
                //Predefinition data type
                else
                {
                    string predefinitionTypeString = ((PredefinitionData)customFieldData.First()).PredefinitionType;
                    ret = new PredefinitionCustomFieldEntity(
                            objectCustomFieldData.ObjectCustomFieldId.ObjectCustomFieldId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectTypeCustomFieldId.ObjectTypeCustomFieldId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectId.ObjectId,
                            objectCustomFieldData.ObjectCustomFieldId.ObjectTypeCustomFieldId.CustomFieldId.CustomFieldId,
                            customFieldTypeFlag,
                            CustomFieldCategoryEnum.BUILT_IN,
                            customFieldData.Select(r => ((PredefinitionData)r).RefId),
                            predefinitionTypeString
                        );
                }
                #endregion Predefinition data type

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBasicData(Guid cmsObjectId,
            Guid objectTypeCustomFieldId,
            object value,
            BasicCustomFieldTypeEnum basicCustomFieldType)
        {
            return UpdateBasicData(cmsObjectId,
                objectTypeCustomFieldId,
                value,
                basicCustomFieldType,
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
        }

        public bool UpdateBasicData(Guid cmsObjectId,
            Guid objectTypeCustomFieldId,
            object value,
            BasicCustomFieldTypeEnum basicCustomFieldType,
            CustomFieldTypeFlag customFieldTypeFlag)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();
                ObjectCustomField objectCustomField =
                    objectCustomFieldBO.GetObjectCustomField(session, cmsObjectId, objectTypeCustomFieldId);
                if (objectCustomField == null)
                {
                    throw new Exception("Could not found ObjectCustomField");
                }
                BasicCustomFieldTypeBOBase basicCustomFieldTypeBO = null;
                switch (basicCustomFieldType)
                {
                    case BasicCustomFieldTypeEnum.STRING:
                        basicCustomFieldTypeBO = new CustomFieldDataStringBO();
                        break;
                    case BasicCustomFieldTypeEnum.DATETIME:
                        basicCustomFieldTypeBO = new CustomFieldDataDateTimeBO();
                        break;
                    case BasicCustomFieldTypeEnum.FLOAT:
                        basicCustomFieldTypeBO = new ObjectCustomFieldDataFloatBO();
                        break;
                    case BasicCustomFieldTypeEnum.INTEGER:
                        basicCustomFieldTypeBO = new ObjectCustomFieldDataIntegerBO();
                        break;
                    default:
                        break;
                }
                return basicCustomFieldTypeBO.UpdateCustomFieldData(objectCustomField.ObjectCustomFieldId,
                    value,
                    customFieldTypeFlag);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();

            }
        }

        public bool UpdatePredefinitionData(Guid cmsObjectId,
            Guid objectTypeCustomFieldId,
            IEnumerable<Guid> refIds,
            PredefinitionCustomFieldTypeEnum predefinitionCustomFieldType)
        {
            return UpdatePredefinitionData(cmsObjectId,
                objectTypeCustomFieldId,
                refIds,
                predefinitionCustomFieldType,
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
        }

        public bool UpdatePredefinitionData(Guid cmsObjectId,
            Guid objectTypeCustomFieldId,
            IEnumerable<Guid> refIds,
            PredefinitionCustomFieldTypeEnum predefinitionCustomFieldType,
            CustomFieldTypeFlag customFieldTypeFlag)
        {

            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                ObjectCustomFieldDataPreDefinitionBO objectCustomFieldDataPreDefinitionBO =
                    new ObjectCustomFieldDataPreDefinitionBO();

                ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();
                ObjectCustomField objectCustomField =
                    objectCustomFieldBO.GetObjectCustomField(session, cmsObjectId, objectTypeCustomFieldId);
                if (objectCustomField == null)
                {
                    throw new Exception("Could not found ObjectCustomField");
                }

                return objectCustomFieldDataPreDefinitionBO.UpdateCustomFieldData(
                    objectCustomField.ObjectCustomFieldId,
                    refIds,
                    predefinitionCustomFieldType,
                    customFieldTypeFlag);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();

            }
        }

        public bool UpdateUserDefinedListData(Guid cmsObjectId,
            Guid objectTypeCustomFieldId,
            IEnumerable<Guid> itemIds)
        {
            return UpdateUserDefinedListData(cmsObjectId,
                objectTypeCustomFieldId,
                itemIds,
                CustomFieldTypeFlag.CUSTOM_FIELD_TYPE_DEFAULT);
        }

        public bool UpdateUserDefinedListData(Guid cmsObjectId,
            Guid objectTypeCustomFieldId,
            IEnumerable<Guid> itemIds,
            CustomFieldTypeFlag customFieldTypeFlag)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                ObjectCustomFieldDataUserDefinedListBO objectCustomFieldDataUserDefinedListBO =
                    new ObjectCustomFieldDataUserDefinedListBO();

                ObjectCustomFieldBO objectCustomFieldBO = new ObjectCustomFieldBO();
                ObjectCustomField objectCustomField =
                    objectCustomFieldBO.GetObjectCustomField(session, cmsObjectId, objectTypeCustomFieldId);
                if (objectCustomField == null)
                {
                    throw new Exception("Could not found ObjectCustomField");
                }

                return objectCustomFieldDataUserDefinedListBO
                    .UpdateCustomFieldData(objectCustomField.ObjectCustomFieldId, itemIds, customFieldTypeFlag);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public void SetDynamicObjectListItem(Session session, Guid objectId, DynamicObjectListSerializeDataItem data)
        {
            DynamicObjectListSerialize dynamicObjectList = null;

            NAS.DAL.CMS.ObjectDocument.Object CMSObject =
                session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(objectId);

            if (CMSObject == null)
                return;

            if (CMSObject.DynamicObjectList != null)
            {
                using (MemoryStream stream = new MemoryStream(CMSObject.DynamicObjectList))
                {
                    dynamicObjectList = DynamicObjectListSerialize.Deserialize(stream);
                    if (dynamicObjectList == null)
                    {
                        dynamicObjectList = new DynamicObjectListSerialize();
                    }
                }
            }
            else
            {
                dynamicObjectList = new DynamicObjectListSerialize();
            }

            using (MemoryStream stream = new MemoryStream())
            {
                dynamicObjectList[data.GetKey()] = data;
                DynamicObjectListSerialize.Serialize(dynamicObjectList, stream);
                CMSObject.DynamicObjectList = stream.ToArray();
                CMSObject.Save();
            }
        }

        public void RemoveDynamicObjectListItem(Session session, Guid objectId, Guid objectCustomFieldId)
        {
            DynamicObjectListSerialize dynamicObjectList = null;

            NAS.DAL.CMS.ObjectDocument.Object CMSObject =
                session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(objectId);

            if (CMSObject == null)
                return;

            if (CMSObject.DynamicObjectList != null)
            {

                using (MemoryStream stream = new MemoryStream(CMSObject.DynamicObjectList))
                {
                    dynamicObjectList = DynamicObjectListSerialize.Deserialize(stream);
                    if (dynamicObjectList == null)
                    {
                        dynamicObjectList = new DynamicObjectListSerialize();
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    string key = objectCustomFieldId.ToString().Replace("-", "");
                    if (dynamicObjectList.ContainsKey(key))
                    {
                        dynamicObjectList.Remove(key);
                    }
                    DynamicObjectListSerialize.Serialize(dynamicObjectList, stream);
                    CMSObject.DynamicObjectList = stream.ToArray();
                    CMSObject.Save();
                }
            }
        }

        public DynamicObjectListSerialize GetDynamicObjectList(Guid objectId)
        {
            DynamicObjectListSerialize ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                NAS.DAL.CMS.ObjectDocument.Object cmsObject = 
                    session.GetObjectByKey<NAS.DAL.CMS.ObjectDocument.Object>(objectId);

                if (cmsObject == null)
                    throw new Exception("Could not found CMS Object");

                if (cmsObject.DynamicObjectList != null)
                {
                    using (MemoryStream stream = new MemoryStream(cmsObject.DynamicObjectList))
                    {
                        ret = DynamicObjectListSerialize.Deserialize(stream);
                    }
                }

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }           
        }

    }
}
