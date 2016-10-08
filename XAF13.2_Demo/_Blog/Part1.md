# DevExpress 13.2 Review - Part 1 #

First of all I'd like to say happy birthday to DevExpress for their 15th Anniversary! Congratulations keep on rocking!

## The new stuff ##

The first thing i normally start with a new DevExpress release is to fire up the ProjectConverter:

### New ProjectConverter ###
The old one:
![](http://i.imgur.com/eyWW790.png)

The new one:

![](http://i.imgur.com/tiBTocg.png)
![](http://i.imgur.com/eG3Wqvo.png)

As you can see the project converter got a face lift, but also can now handle multiple project folders. Handy if you have a large source base :).

## expressAppFramework (XAF) ##

### Easy status notification from Updaters and Application ###

![](http://i.imgur.com/ZGYHe1Q.png)


    public class NotificationUpdater : ModuleUpdater
    {
        public NotificationUpdater(IObjectSpace objectSpace, Version currentDBVersion)
            : base(objectSpace, currentDBVersion)
        {
        }

        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            UpdateStatus("DBUpdater", "UpdateDatabaseBeforeUpdateSchema", "Before updating the schema");
            base.UpdateDatabaseBeforeUpdateSchema();
        }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            UpdateStatus("DBUpdater", "UpdateDatabaseAfterUpdateSchema", "After updating the schema");
            base.UpdateDatabaseAfterUpdateSchema();
        }
    }

### XPView Support for XPObjectSpace ###

If you have to deal with larger projects you often have to select only some properties from BusinessObjects to improve the performance (reporting, filtering, PropertyEditors, ect.)

Now the XPObjectSpace provides a method called GetDataView().


With a ListView in ServerMode XPO generates the following query:


    06.12.13 18:51:02.221 Executing sql 'select N0.`OID`,N0.`Name`,N0.`Property1`,N0.`Property2`,N0.`Property3`,N0.`Property4`,N0.`Property5`,N0.`Property6`,N0.`Property7`,N0.`Property8`,N0.`Property9`,N0.`Property10`,N0.`OptimisticLockField`,N0.`GCRecord` from `LargeBusinessObject` N0
     where N0.`GCRecord` is null
     order by N0.`Name` asc,N0.`OID` asc limit 128 '
    06.12.13 18:51:04.392 Result: rowcount = 128, total = 188413040, N0.{OID,Int32} = 512, N0.{Name,String} = 2274, N0.{Property1,String} = 18991174, N0.{Property2,String} = 18898246, N0.{Property3,String} = 18716842, N0.{Property4,String} = 18855078, N0.{Property5,String} = 18756318, N0.{Property6,String} = 18881274, N0.{Property7,String} = 18838262, N0.{Property8,String} = 18836256, N0.{Property9,String} = 18755904, N0.{Property10,String} = 18879876, N0.{OptimisticLockField,Int32} = 512, N0.{GCRecord,Int32} = 512

With the new GetDataView() method we can do something like this:

    var view = ObjectSpace.CreateDataView(typeof(LargeBusinessObject), new List<DataViewExpression>()
            {
                new DataViewExpression("Name", LargeBusinessObject.Field.GetOperand(m => m.Name)),
                new DataViewExpression("Property", new OperandProperty("Property1")),
            }, null, new List<SortProperty>());

    foreach (ViewRecord item in view)
    {
        var name = item["Name"];
        var property = item["Property"];

        var obj = ObjectSpace.CreateObject<XPViewBusinessObjectProxy>();
        obj.Name = name as string;
        obj.Property = property as string;

        (View.CollectionSource as DevExpress.ExpressApp.CollectionSource).Add(obj);
    }

This will generate the following query:

    06.12.13 18:54:32.369 Executing sql 'select N0.`Name`,N0.`Property1` from `LargeBusinessObject` N0
     where N0.`GCRecord` is null '
    06.12.13 18:54:34.226 Result: rowcount = 1000, total = 147096588, N0.{Name,String} = 17786, N0.{Property1,String} = 147078802

As you clearly can see, only the Name and the Property1 will be fetched from the database. This can be very handy if you have to process large amounts of data for sums or calculations, without having to fetch the whole object.

To clarify the performance improvements:

We have 1000 objects with a property called `IntPropertyToCalculate`.

When we calculate the sum of the `IntPropertyToCalculate` in 3 modes:

#### PureClientSide ####

With a `ObjectSpace.GetObjects(null)` it takes about **20.000 ms**:

    private int CalculateSumPureClientMode()
    {
        using (var os = Application.CreateObjectSpace())
        {
            var objects = os.GetObjects<LargeBusinessObject>(null);

            var result = 0;

            foreach (var item in objects)
                result += item.IntPropertyToCalculate;

            return result;    
        }
    }


#### ClientSide ####

With a `DataView` selecting only the `IntPropertyToCalculate` and do the sum on the client side it takes about **130 ms**:

    private int CalculateSumClientMode()
    {
        using (var os = Application.CreateObjectSpace())
        {
            var objects = os.CreateDataView(typeof(LargeBusinessObject), new List<DataViewExpression>
            {
                new DataViewExpression("IntPropertyToCalculate", new OperandProperty("IntPropertyToCalculate"))
            }, null, new List<SortProperty>());

            var result = 0;

            foreach (ViewRecord item in objects)
                result += (int)item["IntPropertyToCalculate"];

            return result;
        }
    }

#### ServerSide ####

With a `DataView` selecting the sum of `IntPropertyToCalculate` ServerSide it takes about **9 ms**:

    private int CalculateSumServerMode()
    {
        using (var os = Application.CreateObjectSpace())
        {
            var objects = os.CreateDataView(typeof(LargeBusinessObject), new List<DataViewExpression>
            {
                new DataViewExpression("CalculatedServerSide", CriteriaOperator.Parse("Sum(IntPropertyToCalculate)"))
            }, null, new List<SortProperty>());

            var result = 0;

            foreach (ViewRecord item in objects)
                result += (int)item["CalculatedServerSide"];

            return result;
        }
    }


Of course, this performance measurement is not really accurate (only one run and so on), but i think you get the point. (there are about 700MB of data in this table).


## Just the tip of the iceberg ##

Stay tuned for the second/third/fourth/..nth part, there are a lot of new things to see like:


- RibbonControl and SpreadSheat for ASP.NET
- Enhanced Dashboard,
- Warnings for Validation
- New ReportingModule
- Flyout Panel
- WinForms Taskbar Assistant

And many more!
Greetings Manuel