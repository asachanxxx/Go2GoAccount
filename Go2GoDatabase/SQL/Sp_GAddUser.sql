SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Sp_GAddUser]
    @pLogin NVARCHAR(50),
    @pPassword NVARCHAR(50),
    @pFirstName NVARCHAR(40) = NULL,
    @pLastName NVARCHAR(40) = NULL,
    @pRoleId int = NULL,
    @pPhoneNumber NVARCHAR(40) = NULL,
    @pEmail NVARCHAR(40) = NULL,
    @pFKey NVARCHAR(40) = NULL,
    @pUserType int = NULL
AS
    DECLARE @responseMessage int
BEGIN
    BEGIN TRY
    Set NOCOUNT ON

     DECLARE @salt UNIQUEIDENTIFIER=NEWID()

    INSERT INTO dbo.[GUsers]
        (LoginName, PasswordHash, FirstName, LastName,RoleId,Salt,Email,FKey,PhoneNumber,UserType)
    VALUES(@pLogin, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @pFirstName, @pLastName, @pRoleId,@salt,@pEmail,@pFKey,@pPhoneNumber,@pUserType)

    SET @responseMessage= 200

END TRY
BEGIN CATCH
  SET @responseMessage= 500
END CATCH;

    SELECT @responseMessage
END
GO
