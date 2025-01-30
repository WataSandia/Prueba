CREATE TABLE [dbo].[Products1] (
    [ID]        VARCHAR(24)      NOT NULL,
    [Name]      VARCHAR (50)     NULL,
    [CompanyID] VARCHAR(24)      NOT NULL,
    [Amount]    FLOAT (53)       NOT NULL,
    [Status]    VARCHAR (25)     NOT NULL,
    [CreatedAt] SMALLDATETIME    NOT NULL,
    [PaidAt]    SMALLDATETIME    NULL,
    CONSTRAINT [PK_Products1] PRIMARY KEY CLUSTERED ([ID] ASC)
);

