# Nonconformity Control Project

### Requirements
1) .NET Core 3.1
2) MySQL

### Setup
1) Clone this repo to your local machine: `git clone https://github.com/robertadelima/nonconformity-control.git`
2) Create a new MySQL database 
3) Add an environment variable for the connection string: ConnectionStrings__DefaultConnection = server=localhost;port=3306;database={YourDbName};uid={YourUserDbName};password={YourPasswordDbName}
4) To apply migrations: `dotnet ef database update`
5) To run de application: `dotnet run`
6) To check Swagger API documentation and use API: https://localhost:5001/swagger/index.html 

![NonconformityControlAPI](https://github.com/robertadelima/nonconformity-control/blob/master/misc/NonconformityControl.gif)
