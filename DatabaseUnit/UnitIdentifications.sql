CREATE TABLE [dbo].[UnitIdentifications]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TokenID] UNIQUEIDENTIFIER NULL, 
    [Created] DATETIME NOT NULL, 
    [SuiT] INT NOT NULL, 
    [AutomaticDeletion] DATETIME NOT NULL ,

    [SpiN] INT NOT NULL, 
    [ISO639_1] NVARCHAR(2) NOT NULL, 
    [BaseUtcOffsetTotalMinutes] INT NOT NULL, 
    CONSTRAINT [FK_UnitIdentifications_ToToken] FOREIGN KEY ([TokenID]) REFERENCES [Tokens]([ID])on delete set null,
)
