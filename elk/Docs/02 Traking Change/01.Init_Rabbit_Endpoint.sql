
USE WIKI
GO


EXEC rmq.pr_UpsertRabbitEndpoint @AliasName = 'QuestionChanged',
								 @ServerName = '10.1.2.241',
								 @Port = 5672,
								 @LoginName = 'admin',
								 @LoginPassword = '123.123a',
								 @Exchange = 'WIKI_Exchange_Content',
								 @ExchangeType = 'direct',
								 @RoutingKey = 'WIKI_Queue_Question',
								 @Queue = 'WIKI_Queue_Question',
								 @IsEnabled = 1

EXEC rmq.pr_UpsertRabbitEndpoint @AliasName = 'AnswerChanged',
								 @ServerName = '10.1.2.241',
								 @Port = 5672,
								 @LoginName = 'admin',
								 @LoginPassword = '123.123a',
								 @Exchange = 'WIKI_Exchange_Content',
								 @ExchangeType = 'direct',
								 @RoutingKey = 'WIKI_Queue_Answer',
								 @Queue = 'WIKI_Queue_Answer',
								 @IsEnabled = 1

EXEC rmq.pr_UpsertRabbitEndpoint @AliasName = 'DocumentChanged',
								 @ServerName = '10.1.2.241',
								 @Port = 5672,
								 @LoginName = 'admin',
								 @LoginPassword = '123.123a',
								 @Exchange = 'WIKI_Exchange_Content',
								 @ExchangeType = 'direct',
								 @RoutingKey = 'WIKI_Queue_Document',
								 @Queue = 'WIKI_Queue_Document',
								 @IsEnabled = 1

select * from rmq.tb_RabbitEndPoint