CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PatientName] NVARCHAR(50) NOT NULL, 
    [Reason] NVARCHAR(MAX) NOT NULL, 
    [DateAppointment] DATE NOT NULL
)
