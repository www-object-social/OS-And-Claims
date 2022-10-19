CREATE TABLE [dbo].[UnitConnections]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    [Host] NVARCHAR(MAX) NOT NULL, 
    [UnitIdentificationID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_UnitConnections_ToUnitIdentifications] FOREIGN KEY ([UnitIdentificationID]) REFERENCES [UnitIdentifications]([ID]) on delete cascade 
)
