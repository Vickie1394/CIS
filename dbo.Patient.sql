CREATE TABLE [dbo].[Patient] (
    [PatientId]        UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (50)    NULL,
    [PhoneNo]          NVARCHAR (50)    NULL,
    [TreatmentDetails] NVARCHAR (50)    NULL,
    [Gender]              NVARCHAR (50)    NULL,
    [Email]            NVARCHAR (50)    NOT NULL,
    [BloodGrouup]      NCHAR (10)       NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED ([PatientId] ASC)
);

