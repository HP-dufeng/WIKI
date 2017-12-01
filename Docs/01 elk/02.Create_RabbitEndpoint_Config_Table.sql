


-- table to hold rabbit endpoints, i.e. what and how to connect to
IF EXISTS (SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('rmq.tb_RabbitEndpoint'))
Drop table rmq.tb_RabbitEndpoint

CREATE TABLE rmq.tb_RabbitEndpoint
  (
    Id int identity not null,
	AliasName nvarchar(128) NOT NULL,
    ServerName varchar(512) NOT null,
    Port int NOT NULL CONSTRAINT df_RabbitEndpoint_Port DEFAULT 5672,  
    LoginName varchar(256) NOT NULL,
    LoginPassword varbinary(128) NOT NULL,
	Exchange varchar(128) NOT NULL,
	ExchangeType varchar(25) NOT NULL,
    RoutingKey varchar(256) NULL, -- if the exchange is amq.topic, we should have a routing key
	Queue varchar(256) NULL,
	IsEnabled bit NULL
    CONSTRAINT [pk_Id] PRIMARY KEY CLUSTERED
    (
      [Id]
    )
  )
GO




--proc to create / update a rabbit endpoint - i.e. a connection on where to send data to
IF (OBJECT_ID(N'[rmq].[pr_UpsertRabbitEndpoint]') IS NULL)
BEGIN
  EXEC('CREATE PROCEDURE [rmq].[pr_UpsertRabbitEndpoint] AS SET NOCOUNT ON;');
END
GO

ALTER PROCEDURE rmq.pr_UpsertRabbitEndpoint   @AliasName nvarchar(128),
											  @ServerName varchar(512),
											  @Port int,											  
											  @LoginName varchar(256),
											  @LoginPassword nvarchar(256),
											  @Exchange varchar(128),
											  @ExchangeType varchar(25),
											  @RoutingKey varchar(256),
											  @Queue varchar(256),
											  @IsEnabled bit
AS

SET NOCOUNT ON;

DECLARE @errMsg nvarchar(max); /*this is used to set an explanatory error message BEFORE you call something, 
                                 it is what we previously used to put inside our RAISERROR's*/


BEGIN TRY

--vary basic proc, no validations etc.

--poor mans encryption
DECLARE @pwd varbinary(128) = CAST(@LoginPassword AS varbinary(128));

SET @errMsg = 'Merging into rmq.tb_RabbitEndpoint';
MERGE rmq.tb_RabbitEndpoint AS tgt
USING(SELECT @AliasName as AliasName ,@ServerName AS ServerName, @Port AS Port,
             @LoginName AS LoginName, @pwd AS LoginPassword,
			 @Exchange as Exchange, @ExchangeType as ExchangeType, @RoutingKey as RoutingKey, @Queue as Queue,
			 @IsEnabled as IsEnabled
			) AS src
ON(tgt.AliasName = src.AliasName)
WHEN NOT MATCHED THEN
  INSERT(AliasName, ServerName, Port, LoginName, LoginPassword, Exchange, ExchangeType, RoutingKey, Queue, IsEnabled)
  VALUES(src.AliasName ,src.ServerName, src.Port, src.LoginName, src.LoginPassword, src.Exchange, src.ExchangeType, src.RoutingKey, src.Queue, IsEnabled)
WHEN MATCHED THEN
  UPDATE
    SET ServerName = src.ServerName, 
	    Port = src.Port, 
		LoginName = src.LoginName, 
		LoginPassword = src.LoginPassword,
		Exchange = src.Exchange,
		ExchangeType = src.ExchangeType,
		RoutingKey = src.RoutingKey,
		Queue = src.Queue,
		IsEnabled = src.IsEnabled;

END TRY
BEGIN CATCH
  DECLARE @thisProc nvarchar(256) = 'rmq.pr_UpsertRabbitEndpoint'; --used for error messages
  DECLARE @sysErrMsg varchar(max);  -- used to set customised error messages
  DECLARE @errSev int; -- error severity
  DECLARE @errState int; -- error state

  --error handling code
  SELECT @sysErrMsg = ERROR_MESSAGE(), @errSev = ERROR_SEVERITY(), @errState = ERROR_STATE()
     
  --re-raise upstream
  RAISERROR('Error in %s: %s. %s.', @errSev, @errState, @thisProc, @errMsg, @sysErrMsg)


END CATCH
GO



