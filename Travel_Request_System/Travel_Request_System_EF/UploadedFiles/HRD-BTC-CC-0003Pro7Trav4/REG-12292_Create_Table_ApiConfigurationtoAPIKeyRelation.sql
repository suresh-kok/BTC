
IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ApiConfigurationtoAPIKeyRelation' AND TABLE_SCHEMA='dbo')
BEGIN

	CREATE TABLE [dbo].[ApiConfigurationtoAPIKeyRelation](
		[ExternalAPIConfigurationID] [int] NOT NULL,
		[APIKey] [uniqueidentifier] NOT NULL,
		[SolutionID] [int] NOT NULL
		CONSTRAINT [PK_ApiConfigurationtoAPIKeyRelation_ExternalAPIConfigurationID_APIKey] PRIMARY KEY CLUSTERED 
        (
			[ExternalAPIConfigurationID] ASC,
			[APIKey] ASC
        ),
		CONSTRAINT [IX_ApiConfigurationtoAPIKeyRelation_APIKey_SolutionID] UNIQUE NONCLUSTERED 
        (
			[APIKey] ASC,
			[SolutionID] ASC
        ),
	) ON [PRIMARY]
END

