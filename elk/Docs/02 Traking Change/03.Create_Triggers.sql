
/********************* Start: Create QuestionIndexTrigger **************************************************************/
IF EXISTS(SELECT 1 FROM sys.triggers WHERE object_id = OBJECT_ID('dbo.QuestionIndexTrigger'))
	DROP trigger dbo.QuestionIndexTrigger

GO
CREATE TRIGGER dbo.QuestionIndexTrigger 
   ON  [dbo].[Questions]
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @Id uniqueidentifier = NEWID();
	declare @ContentId bigint;
	declare @IsDelete varchar(5);

	select @ContentId = inserted.Id from inserted
	if @ContentId is not null
	begin
		set @IsDelete = 'false';
		insert into dbo.Questions_ChangeLog(Id, QuestionId, IsDelete, IsIndex)
		values(@Id, @ContentId, 0, 0)
	end
	else
	begin
		set @IsDelete = 'true';
		select @ContentId = deleted.Id from deleted
		insert into dbo.Questions_ChangeLog(Id, QuestionId, IsDelete, IsIndex)
		values(@Id, @ContentId, 1, 0)
	end

    -- tracking change 
	--insert into dbo.Questions_ChangeLog(Id, QuestionId, IsDelete, IsIndex)
	--select @Id, inserted.Id, 0, 0 from inserted

	--IF NOT EXISTS(select * from inserted) AND EXISTS(select * from deleted)
	--BEGIN
	--	insert into dbo.Questions_ChangeLog(Id, QuestionId, IsDelete, IsIndex)
	--select @Id, deleted.Id, 1, 0 from deleted
	--END

	-- send msg to mq
	declare @message varchar(200);
	declare @jsonData varchar(300);
	set @jsonData = 
	'{' 
		+ '"Id":'+ '"' + convert(varchar(50),@Id) + '"' + ',' +
		+ '"ContentId":'+ '"' + convert(varchar(50),@ContentId) + '"' + ',' + 
		+ '"IsDelete":'+ '"' + @IsDelete + '"' + ',' + 
	'}';
	set @message = '{"messageType":[],"message":' + @jsonData + '}';
	exec rmq.sp_PostRabbitMsg @endpointName = 'QuestionChanged', @msg = @message

END
GO
print('QuestionIndexTrigger created');


/********************* Start: Create AnswerIndexTrigger **************************************************************/
IF EXISTS(SELECT 1 FROM sys.triggers WHERE object_id = OBJECT_ID('dbo.AnswerIndexTrigger'))
	DROP trigger dbo.AnswerIndexTrigger

GO
CREATE TRIGGER dbo.AnswerIndexTrigger 
   ON  [dbo].[Answers]
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @Id uniqueidentifier = NEWID();
	declare @ContentId bigint;
	declare @IsDelete varchar(5);

	select @ContentId = inserted.Id from inserted
	if @ContentId is not null
	begin
		set @IsDelete = 'false';
		insert into dbo.Answers_ChangeLog(Id, AnswerId, IsDelete, IsIndex)
		values(@Id, @ContentId, 0, 0)
	end
	else
	begin
		set @IsDelete = 'true';
		select @ContentId = deleted.Id from deleted
		insert into dbo.Answers_ChangeLog(Id, AnswerId, IsDelete, IsIndex)
		values(@Id, @ContentId, 1, 0)
	end

	-- send msg to mq
	declare @message varchar(200);
	declare @jsonData varchar(300);
	set @jsonData = 
	'{' 
		+ '"Id":'+ '"' + convert(varchar(50),@Id) + '"' + ',' +
		+ '"ContentId":'+ '"' + convert(varchar(50),@ContentId) + '"' + ',' + 
		+ '"IsDelete":'+ '"' + @IsDelete + '"' + ',' + 
	'}';
	set @message = '{"messageType":[],"message":' + @jsonData + '}';
	exec rmq.sp_PostRabbitMsg @endpointName = 'AnswerChanged', @msg = @message

END
GO
print('AnswerIndexTrigger created');


/********************* Start: Create DocumentIndexTrigger **************************************************************/
IF EXISTS(SELECT 1 FROM sys.triggers WHERE object_id = OBJECT_ID('dbo.DocumentIndexTrigger'))
	DROP trigger dbo.DocumentIndexTrigger

GO
CREATE TRIGGER dbo.DocumentIndexTrigger 
   ON  [dbo].[Documents]
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @Id uniqueidentifier = NEWID();
	declare @ContentId bigint;
	declare @IsDelete varchar(5);

	select @ContentId = inserted.Id from inserted
	if @ContentId is not null
	begin
		set @IsDelete = 'false';
		insert into dbo.Documents_ChangeLog(Id, DocumentId, IsDelete, IsIndex)
		values(@Id, @ContentId, 0, 0)
	end
	else
	begin
		set @IsDelete = 'true';
		select @ContentId = deleted.Id from deleted
		insert into dbo.Documents_ChangeLog(Id, DocumentId, IsDelete, IsIndex)
		values(@Id, @ContentId, 1, 0)
	end

	-- send msg to mq
	declare @message varchar(200);
	declare @jsonData varchar(300);
	set @jsonData = 
	'{' 
		+ '"Id":'+ '"' + convert(varchar(50),@Id) + '"' + ',' +
		+ '"ContentId":'+ '"' + convert(varchar(50),@ContentId) + '"' + ',' + 
		+ '"IsDelete":'+ '"' + @IsDelete + '"' + ',' + 
	'}';
	set @message = '{"messageType":[],"message":' + @jsonData + '}';
	exec rmq.sp_PostRabbitMsg @endpointName = 'DocumentChanged', @msg = @message

END
GO
print('DocumentIndexTrigger created');


/********************* Start: Create DocumentAttachmentIndexTrigger **************************************************************/
IF EXISTS(SELECT 1 FROM sys.triggers WHERE object_id = OBJECT_ID('dbo.DocumentAttachmentIndexTrigger'))
	DROP trigger dbo.DocumentAttachmentIndexTrigger

GO
CREATE TRIGGER dbo.DocumentAttachmentIndexTrigger 
   ON  [dbo].[DocumentAttachments]
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @Id uniqueidentifier = NEWID();
	declare @ContentId bigint;
	declare @IsDelete varchar(5);

	select @ContentId = inserted.DocumentId from inserted
	if @ContentId is not null
	begin
		set @IsDelete = 'false';
		insert into dbo.Documents_ChangeLog(Id, DocumentId, IsDelete, IsIndex)
		values(@Id, @ContentId, 0, 0)
	end
	else
	begin
		set @IsDelete = 'true';
		select @ContentId = deleted.DocumentId from deleted
		insert into dbo.Documents_ChangeLog(Id, DocumentId, IsDelete, IsIndex)
		values(@Id, @ContentId, 1, 0)
	end

	-- send msg to mq
	declare @message varchar(200);
	declare @jsonData varchar(300);
	set @jsonData = 
	'{' 
		+ '"Id":'+ '"' + convert(varchar(50),@Id) + '"' + ',' +
		+ '"ContentId":'+ '"' + convert(varchar(50),@ContentId) + '"' + ',' + 
		+ '"IsDelete":'+ '"' + @IsDelete + '"' + ',' + 
	'}';
	set @message = '{"messageType":[],"message":' + @jsonData + '}';
	exec rmq.sp_PostRabbitMsg @endpointName = 'DocumentChanged', @msg = @message

END
GO
print('DocumentAttachmentIndexTrigger created');

