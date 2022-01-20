SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Sp_GLoginWeb]
    @pLoginName NVARCHAR(254),
    @pPassword NVARCHAR(50)
AS
    DECLARE @responseMessage int
BEGIN
    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1
        UserID
    FROM [dbo].[GUsers]
    WHERE LoginName=@pLoginName)
    BEGIN
        SET @userID=(SELECT UserID
        FROM [dbo].[GUsers]
        WHERE LoginName=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

        IF(@userID IS NULL)
           SET @responseMessage='201' -- Incorrect password
       ELSE 
       BEGIN
            SET @responseMessage='200'

            SELECT UserID, FKey , Email, LoginName, FirstName, LastName, UserType, PhoneNumber, RoleId , '' as Token , 200 as ResponseCode
            from GUsers
            WHERE UserID = @userID
        END
    END
    ELSE
       SET @responseMessage='202'
    -- Invalid login
     IF @responseMessage != '200' 
     SELECT @responseMessage
   
END
GO


-- EXEC Sp_GLoginWeb
--     @pLoginName = N'Asanga',
--     @pPassword = N'Chan'