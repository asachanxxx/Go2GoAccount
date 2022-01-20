ALTER PROCEDURE Sp_GetLedgerBalanceForKey
    @userId NVARCHAR(max)
AS
BEGIN

select top 1 DriverId , Balance from  UserLedgers 
where DriverKey = @userId
ORDER BY Id desc

END
GO
