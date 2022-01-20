CREATE PROCEDURE Sp_GetLedgerEntry
   @userId NVARCHAR(max)
AS
BEGIN
   select top 15 DriverKey,TripId,TrxId,[Description],Flag, Amount,Balance,TrxDate from  UserLedgers 
    where DriverKey = @userId
    ORDER BY TrxDate desc
END
GO
