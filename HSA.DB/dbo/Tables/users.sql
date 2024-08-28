CREATE TABLE [dbo].[users] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [email]         NVARCHAR (100) NOT NULL,
    [password_hash] NVARCHAR (255) NOT NULL,
    [first_name]    NVARCHAR (50)  NULL,
    [last_name]     NVARCHAR (50)  NULL,
    [role]          INT            NULL,
    [created_at]    DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [updated_at]    DATETIME2 (7)  DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([email] ASC)
);


GO
CREATE TRIGGER trg_users_update
ON users
FOR UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE users
    SET updated_at = GETDATE()
    WHERE id IN (SELECT DISTINCT id FROM inserted);
END;