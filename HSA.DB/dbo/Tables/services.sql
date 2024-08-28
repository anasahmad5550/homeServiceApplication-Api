CREATE TABLE [dbo].[services] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [title]       NVARCHAR (100)  NOT NULL,
    [description] NVARCHAR (255)  NULL,
    [location]    NVARCHAR (50)   NULL,
    [price]       DECIMAL (10, 2) NULL,
    [category]    INT             NULL,
    [sellerId]    INT             NULL,
    [created_at]  DATETIME2 (7)   DEFAULT (getdate()) NULL,
    [updated_at]  DATETIME2 (7)   DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Services_Seller] FOREIGN KEY ([sellerId]) REFERENCES [dbo].[users] ([id])
);


GO
CREATE TRIGGER trg_services_update
ON services
FOR UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE services
    SET updated_at = GETDATE()
    WHERE id IN (SELECT DISTINCT id FROM inserted);
END;