
# Demo JobBoard API

  
Demo C# API for accessing SQL Server DB

  
## Getting Started


These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

  
### Prerequisites

  
What things you need to install the software and how to install them


> SQL Server
  

### Installing

  

A step by step series of examples that tell you how to get a development env running

  
Run SQL in Docker

```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```


Add Database called ' JobBoard'


Add JobBoard Schema

```
CREATE SCHEMA JobBoard;
GO
```
  Add JobBoard  Table
```

CREATE TABLE [JobBoard].[Job](
    [JobID] [int] NULL,
	[location] [nvarchar](200) NULL,
	[JobTitle] [nvarchar](100) NULL,
	[CompanyID] [int] NULL,
	[Description] [nvarchar](200) NULL,
	[Salary] [int] NULL,
	[ContactEmail] [nvarchar](100) NULL,
	[DatePosted] [datetime2](7) NULL,
	[DateExpires] [datetime2](7) NULL
) ON [PRIMARY]
GO
```


## Swagger

 https://localhost:5001/swagger/index.html
 
## Author
 
 **Andy Collins**