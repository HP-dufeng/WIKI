USE WIKI
GO

/*
  This role will be used as authorization (owner)
  for the RabbitMq assemblies.
*/
IF NOT EXISTS(SELECT 1 FROM sys.database_principals WHERE name = 'rmq')
BEGIN
  CREATE ROLE rmq
  AUTHORIZATION dbo;
END


IF NOT EXISTS(SELECT 1 FROM  sys.schemas WHERE name = 'rmq')
BEGIN
  EXECUTE('CREATE SCHEMA rmq;');
END




/*
	CLR is enabled
*/
GO
sp_configure 'clr enabled', 1  
GO  
RECONFIGURE  
GO

select * from sys.dm_clr_properties




