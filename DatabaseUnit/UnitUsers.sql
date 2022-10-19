CREATE TABLE [dbo].[UnitUsers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[UnitIdentificationID] UNIQUEIDENTIFIER NOT NULL, 
    [IsActive] BIT NOT NULL, 
    CONSTRAINT [FK_UnitUsers_ToUnitIdentifications] FOREIGN KEY ([UnitIdentificationID]) REFERENCES [UnitIdentifications]([ID]) on delete cascade , 
)
