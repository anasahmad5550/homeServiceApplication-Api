CREATE TABLE [dbo].[bookings] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [bookedAt]   DATETIME2 (7) NULL,
    [serviceId]  INT           NOT NULL,
    [customerId] INT           NOT NULL,
    [created_at] DATETIME2 (7) DEFAULT (getdate()) NULL,
    [updated_at] DATETIME2 (7) DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Bookings_Customer] FOREIGN KEY ([customerId]) REFERENCES [dbo].[users] ([id]),
    CONSTRAINT [FK_Bookings_Service] FOREIGN KEY ([serviceId]) REFERENCES [dbo].[services] ([id])
);


GO

CREATE TRIGGER trg_bookings_update
ON bookings
FOR UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE bookings
    SET updated_at = GETDATE()
    WHERE id IN (SELECT DISTINCT id FROM inserted);
END;