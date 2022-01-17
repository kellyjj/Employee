# Employee


this repo is my attempt at doing a mvc web app on linux mint , using vs code.   I am going to try to maintain a employee db.  


script to create test db on sql server
-----------------
ENVIROMENT:  MINT linux 20 

Work done in VS Code. 

Db is sql server, created in Azuer data studion

These dotnet cmds set up my mvc app to be able work with EF EntityFrameworkCore and a sql server db.  NOte:  they dont' actually 
anthing but install the needed refreences in my machine (global)

this will also allow me to do CRUD [Create, Read, Update, and Delete (CRUD) ]

dotnet tool uninstall -g dotnet-aspnet-codegenerator
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool uninstall -g dotnet-ef
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

// this will generate the controller as a part of the crud.  if we wanted to use sql lite add flag -sqlite
dotnet-aspnet-codegenerator controller -name EmployeeController -m Employee -dc EmployeeContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries 





------------------
Create new DB called S_SQR_PERSONNEL

run the following table scripts


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPLOYEE](
	[EMPLOYEEID] [nvarchar](50) NOT NULL,
	[FNAME] [nvarchar](50) NOT NULL,
	[EMPROLE] [nvarchar](50) NOT NULL,
	[ISMANAGER] [nvarchar](1) NOT NULL,
	[LNAME] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[EMPLOYEE] ADD PRIMARY KEY CLUSTERED 
(
	[EMPLOYEEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CREW](
	[CREWKEY] [int] IDENTITY(100,1) NOT NULL,
	[MANAGER] [nvarchar](50) NOT NULL,
	[CREWEMP] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CREW] ADD PRIMARY KEY CLUSTERED 
(
	[CREWKEY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CREW]  WITH CHECK ADD  CONSTRAINT [FK_CREW] FOREIGN KEY([CREWEMP])
REFERENCES [dbo].[EMPLOYEE] ([EMPLOYEEID])
GO
ALTER TABLE [dbo].[CREW] CHECK CONSTRAINT [FK_CREW]
GO
ALTER TABLE [dbo].[CREW]  WITH CHECK ADD  CONSTRAINT [FK_manager] FOREIGN KEY([MANAGER])
REFERENCES [dbo].[EMPLOYEE] ([EMPLOYEEID])
GO
ALTER TABLE [dbo].[CREW] CHECK CONSTRAINT [FK_manager]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPROLES](
	[CODE] [nvarchar](50) NOT NULL,
	[DESCRIPTION] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[EMPROLES] ADD PRIMARY KEY CLUSTERED 
(
	[CODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO



------------------
