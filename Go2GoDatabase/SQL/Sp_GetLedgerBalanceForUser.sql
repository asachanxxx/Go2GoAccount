ALTER PROCEDURE Sp_GetLedgerBalanceForUser
    @userId INT
AS
BEGIN

select top 1 DriverId , Balance from  UserLedgers 
where DriverId = @userId
ORDER BY Id desc

END
GO


-- -- example to execute the stored procedure we just created
-- EXECUTE Sp_GetLedgerBalanceForUser 3
-- GO