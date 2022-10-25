CREATE TABLE [dbo].[User]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Created] DATETIME NOT NULL, 
    [ExpiresNumberOfDays] INT NOT NULL, 
    [ExpiresNumberOfMonths] INT NOT NULL, 
    [ExpiresNumberOfYears] INT NOT NULL, 
    [Expires] DATETIME NOT NULL,
    [ISO639_1] NVARCHAR(2) NOT NULL, 
    [ISO3166] NVARCHAR(2) NOT NULL, 
    [IsDeleted] BIT NOT NULL,
)
