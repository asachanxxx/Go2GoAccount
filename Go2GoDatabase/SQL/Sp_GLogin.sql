ALTER PROCEDURE Sp_GLogin
    @pLoginName NVARCHAR(254),
    @pPassword NVARCHAR(50)
AS
    DECLARE @responseMessage int
BEGIN

    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 UserID FROM [dbo].[GUsers] WHERE LoginName=@pLoginName)
    BEGIN
        SET @userID=(SELECT UserID FROM [dbo].[GUsers] WHERE LoginName=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

       IF(@userID IS NULL)
           SET @responseMessage='201' -- Incorrect password
       ELSE 
           SET @responseMessage='200'
    END
    ELSE
       SET @responseMessage='202' -- Invalid login

SELECT @responseMessage
END