CREATE TABLE [dbo].[Charge] (
    [ID]        UNIQUEIDENTIFIER NOT NULL,
    [Amount]    DECIMAL (18)     NOT NULL,
    [CreatedAt] SMALLDATETIME    NOT NULL,
    [PaidAt]    SMALLDATETIME    NULL,
    [CompanyID] UNIQUEIDENTIFIER NOT NULL,
    [StatusID]  INT              NOT NULL,
    CONSTRAINT [PK_Charge] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Charge_Company] FOREIGN KEY ([CompanyID]) REFERENCES [dbo].[Company] ([ID]),
    CONSTRAINT [FK_Charge_Status] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Status] ([ID])
);

