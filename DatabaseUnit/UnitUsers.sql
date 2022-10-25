CREATE TABLE [dbo].[UnitUsers]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[UnitIdentificationID] UNIQUEIDENTIFIER NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [IsVerified] BIT NOT NULL, 
    [VerdificationCode] VARBINARY(MAX) NOT NULL, 
    [IsBlock] BIT NOT NULL, 
    [Expires] DATETIME NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [UserID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_UnitUsers_ToUnitIdentifications] FOREIGN KEY ([UnitIdentificationID]) REFERENCES [UnitIdentifications]([ID]) on delete cascade, 
    CONSTRAINT [FK_UnitUsers_ToUser] FOREIGN KEY ([UserID]) REFERENCES [User]([ID]) on delete cascade, 
)
