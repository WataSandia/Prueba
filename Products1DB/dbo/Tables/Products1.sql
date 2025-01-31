CREATE TABLE [dbo].[Products1] (
    [ID]        VARCHAR(50)      NOT NULL,
    [Name]      VARCHAR (50)     NULL,
    [CompanyID] VARCHAR(50)      NOT NULL,
    [Amount]    DECIMAL(38, 18)  NOT NULL,
    [Status]    VARCHAR (25)     NOT NULL,
    [CreatedAt] SMALLDATETIME    NOT NULL,
    [PaidAt]    SMALLDATETIME    NULL,
);

