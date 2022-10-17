CREATE TABLE [dbo].[PongPingUniformResourceIdentifiers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Created] DATETIME NOT NULL, 
    [UsedConnections] INT NOT NULL, 
    [Path] NVARCHAR(MAX) NOT NULL, 
    [Use] DATETIME NOT NULL
)
