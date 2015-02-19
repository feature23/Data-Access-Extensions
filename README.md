# Data Access Extensions
A collection of extension methods that make data access operations in .NET a little easier.

## Usage

### IDbConnection

Provided in the F23.DataAccessExtensions assembly is a set of extension methods on IDbConnection to simplify
the execution of stored procedures, as well as improve performance as these calls are optimized as RPC calls.

Examples:

```C#
using F23.DataAccessExtensions;
...

IList<Customer> customers = connection.ExecuteSproc<Customer>("GetCustomers");

Customer customer = connection.ExecuteSproc<Customer>("GetCustomer", 
    DbDataParameter.Create("@id", 123)
  ).FirstOrDefault();
  
IList<int> ids = connection.ExecuteSprocSingleColumn<int>("GetAllCustomerIDs");
  
int affected = connection.ExecuteSprocNonQuery("SetCustomerInactive", DbDataParameter.Create("@id", 123));

XDocument xml = connection.ExecuteSprocXmlScalar("GetMenuHierarchy");

```

Transactions are also supported by passing in an IDbTransaction as the first parameter.

All methods also have Async versions that you can use with async/await:

```C#
IList<Customer> customers = await connection.ExecuteSprocAsync<Customer>("GetCustomers");

Customer customer = (await connection.ExecuteSprocAsync<Customer>("GetCustomer", 
    DbDataParameter.Create("@id", 123)
  )).FirstOrDefault();
  
IList<int> ids = await connection.ExecuteSprocSingleColumnAsync<int>("GetAllCustomerIDs");
  
int affected = await connection.ExecuteSprocNonQueryAsync("SetCustomerInactive", DbDataParameter.Create("@id", 123));

XDocument xml = await connection.ExecuteSprocXmlScalarAsync("GetMenuHierarchy");

```

These methods were designed such that you can create friendly "wrapper" methods that make callers feel like they're 
just executing a method rather than doing anything with ADO.NET, while also getting strongly named/typed parameters. Example:

```C#
public static class MySprocExtensions
{
  public static Customer GetCustomer(this IDbConnection conn, int id)
  {
    return conn.ExecuteSproc<Customer>("GetCustomer", DbDataParameter.Create("@id", id)).FirstOrDefault();
  }
  
  public static async Task<Customer> GetCustomerAsync(this IDbConnection conn, int id)
  {
    return (await conn.ExecuteSprocAsync<Customer>("GetCustomer", DbDataParameter.Create("@id", id))).FirstOrDefault();
  }
}

// usage:

IDbConnection conn = new SqlConnection("...");
var customer = conn.GetCustomer(123);

// or:
var customer = await conn.GetCustomerAsync(123);
```

### Entity Framework

The F23.DataAccessExtensions.EntityFramework assembly includes further extension methods that build on top
of the ones above for IDbConnection, but designed specifically for Entity Framework code-first. These calls often
result in better performance than calling Database.SqlQuery due to using native ADO.NET RPC calls under the hood.

Examples:

```C#
using (var context = new MyEFContext())
{
  var customers = context.ExecuteSproc<Customer>("GetCustomers");
  // or:
  var customers = await context.ExecuteSprocAsync<Customer>("GetCustomers");
}
```

Like the IDbConnection example above, we encourage you to create extension methods that strongly type the parameters
and give a "method call" feel to the execution:


```C#
public static class MySprocExtensions
{
  public static IList<Customer> GetCustomersByState(this DbContext conn, string state)
  {
    return conn.ExecuteSproc<Customer>("GetCustomersByState", DbDataParameter.Create("@state", state));
  }
  
  public static Task<IList<Customer>> GetCustomersByStateAsync(this DbContext conn, string state)
  {
    return conn.ExecuteSprocAsync<Customer>("GetCustomersByState", DbDataParameter.Create("@state", state));
  }
}

// usage:

var context = new MyEFContext();
var customers = context.GetCustomersByState("FL");

// or:
var customers = await conn.GetCustomersByStateAsync("FL");
```
