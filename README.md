# Job Board API

Needs SQL Server 

* Start SQL In Docker
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest


* Create Schema and Empty Job Table
CREATE SCHEMA JobBoard;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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