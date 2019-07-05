## About project:
Web Application that provides information about the cryptocurrencies status by [CoinMarketCap API](https://coinmarketcap.com/api/documentation/v1/)<br>
First of all user need to register with unique email address.<br>
After registration you can see the list of cryptocurrencies states and order them by different parameters.<br>

Information about your Account stored in Database. It means that after registration you can login by email and password everytime.

## For developers:

### Dependencies:
MS SQL Server<br>
MS SQL Server Management Studio<br>
Database with table Users [id(primary key), email, password) <br>

### Create:
.NET Framework Web App

### By NuGet install:
EntityFramework<br>
PagedList.Mvc


### How to run:
* Clone project
* Run MS SQL Server
* Connect to Server by Management Studio
* Create Database and table Users [id(primary key), email, password) 

* Change in `Web.config`
```
<connectionStrings>
    <add name="DbConnectionString" connectionString="data source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
</connectionStrings>
```
* Compile and run project 
