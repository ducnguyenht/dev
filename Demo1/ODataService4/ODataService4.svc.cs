using DevExpress.Xpo;
using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq.Expressions;
using System.ServiceModel;

namespace ODataService4
{
    public class ODataService4Context : XpoContext
    {
        public ODataService4Context(string s1, string s2, IDataLayer dataLayer)
            : base(s1, s2, dataLayer)
        {
        }

        // Place Actions here.
        // [Action]
        // public bool ActionName(EntityType entity) {
        //     return true;
        // }
    }

#if DEBUG

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    [JSONPSupportBehavior]
#else
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [JSONPSupportBehavior]
#endif
    public class ODataService4Service : XpoDataServiceV3
    {
        private static readonly ODataService4Context serviceContext = new ODataService4Context("ODataService4Service", "ODataService4.WebDemo1", CreateDataLayer());

        public ODataService4Service()
            : base(serviceContext, serviceContext)
        {
        }

        private static IDataLayer CreateDataLayer()
        {
            DevExpress.Xpo.Metadata.XPDictionary dict = new DevExpress.Xpo.Metadata.ReflectionDictionary();
            dict.GetDataStoreSchema(typeof(ODataService4Service).Assembly);
            IDataLayer dataLayer = new ThreadSafeDataLayer(dict, global::ODataService4.WebDemo1.ConnectionHelper.GetConnectionProvider(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));
            XpoDefault.DataLayer = dataLayer;
            XpoDefault.Session = null;
            return dataLayer;
        }

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);
        }

        // This method is called just once to initialize global service policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.AcceptAnyAllRequests = true;
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.SetServiceActionAccessRule("*", ServiceActionRights.Invoke);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.DataServiceBehavior.AcceptProjectionRequests = true;
            config.DataServiceBehavior.AcceptCountRequests = true;
            config.AnnotationsBuilder = CreateAnnotationsBuilder(() => serviceContext);
            config.DataServiceBehavior.AcceptReplaceFunctionInQuery = true;
            config.DataServiceBehavior.AcceptSpatialLiteralsInQuery = true;
            config.DisableValidationOnMetadataWrite = true;
#if DEBUG
            config.UseVerboseErrors = true;
#endif
        }

        // Place OData Interceptors here, one for each Entity type.
        // [QueryInterceptor("EntityType")]
        // public Expression<Func<EntityType, bool>> OnQuery() {
        //    return o => true;
        // }

        // This method is called on each service call and returns a token.
        public override object Authenticate(ProcessRequestArgs args)
        {
            return base.Authenticate(args);
        }

        // Place a single XPO Interceptor that here, one for all Entity types.
        public override LambdaExpression GetQueryInterceptor(Type entityType, object token)
        {
            //Example interceptor for the Employee class
            //if (token != null && entityType == typeof(Employee)){
            //    return (Expression<Func<Employee, bool>>)(o => o.UserName != "Admin");
            //}
            return base.GetQueryInterceptor(entityType, token);
        }

        // Place Service Operations here.
        // [WebGet]
        // public void OperationName(Type arg1, ...) {
        // }
    }
}