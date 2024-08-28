CREATE TABLE [dbo].[reviews] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [title]       NVARCHAR (100) NOT NULL,
    [description] NVARCHAR (255) NULL,
    [rating]      INT            NOT NULL,
    [bookingId]   INT            NOT NULL,
    [created_at]  DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [updated_at]  DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CHECK ([rating]>=(1) AND [rating]<=(5)),
    CONSTRAINT [FK_Reviews_Booking] FOREIGN KEY ([bookingId]) REFERENCES [dbo].[bookings] ([id])
);


GO
CREATE TRIGGER trg_reviews_update
ON reviews
FOR UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE reviews
    SET updated_at = GETDATE()
    WHERE id IN (SELECT DISTINCT id FROM inserted);
END;