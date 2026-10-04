# Opeyemi Obute — Assignment 4

Open OrderDetailsMaintenance.sln in Visual Studio after extracting the archive.
Requires the .NET 8 desktop development workload and SQL Server Express LocalDB.
Restore NuGet packages, then run the project.

## Implementation
- Northwind.mdf and Northwind_log.ldf are Content with Copy if newer (PreserveNewest).
- Customers and NorthwindContext were generated using EF Core scaffolding, selecting only Customers.
- App.config contains the Northwind connection string. Program.cs sets DataDirectory to the executable folder.
- Find uses Customers.Find(id); Save uses Customers.Update(customer) and SaveChanges(); Exit closes the form.
- Contact, Address, City, and Country are editable. Other scaffolded columns retain their existing values.
- Search ID changes disable Save until another successful Find.

## Scaffolding command
Run in Visual Studio Package Manager Console with the project selected:

```powershell
Scaffold-DbContext 'Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFilename=|DataDirectory|\Northwind.mdf;Integrated Security=True;TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\DataLayer -Context NorthwindContext -Tables Customers -DataAnnotations -Force
```

An equivalent dotnet ef dbcontext scaffold command was executed successfully during implementation, using the absolute MDF path for design-time access. Re-scaffolding overwrites the context; restore its App.config configuration afterward. The correct API is ConfigurationManager.ConnectionStrings (plural).

## Verification
- Build: zero warnings and zero errors.
- Actual form: ALFKI displayed Maria Anders, Obere Str. 57, Berlin, Germany.
- Actual Save: a temporary city edit persisted and was confirmed through a separate database connection; the original value was restored.
- Unknown ID ZZZZZ: customer-not-found message, cleared fields, disabled Save.
- Exit: application closed.
- ANATR and CACTU were verified present in the database.

The supplied database files are included unchanged, avoiding any dependency on the SQL Server version used during testing. Output database copies may be replaced if the source files are newer. Do not delete bin if you need to preserve edits made to its database.

## Submission
Upload opeyemi_obute_assignment4.doc and submit your GitHub repository URL in Canvas. GitHub publishing and Canvas submission are pending repository/submission details.
