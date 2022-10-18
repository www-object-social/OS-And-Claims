CREATE TABLE [dbo].[TokenSecurities]
(
	[Code] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY , 
    [TokenID] UNIQUEIDENTIFIER NOT NULL, 
    [Created] DATETIME NOT NULL , 
    CONSTRAINT [FK_TokenSecurities_ToTokens] FOREIGN KEY ([TokenID]) REFERENCES [Tokens]([ID]) on delete cascade
)
