/*EF
Install-Package EntityFramework
add connectionStrings after configSections
 <connectionStrings>
    <add name="ConnectionString" connectionString="Persist Security Info=True;Password=123456;User ID=sa;Data Source=.\SQLEXPRESS;Initial Catalog=CFDBS12"  providerName="System.Data.SqlClient"/><!--CFDBS   CodeFirstNewDatabaseSample.BloggingContext-->
  </connectionStrings>
: DbContext : base("name=ConnectionString")
when alter add/remove colum
Enable-Migrations
Add-Migration ColumName
Update-Database
*/