

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_AddUserLedgerEntry]
  @UserLedger tblUserBalance READONLY
AS
BEGIN
    DECLARE @DriverKey NVARCHAR(max)
    DECLARE @Balance DECIMAL(18,2) 
    SET @Balance = 0
    select @DriverKey = DriverKey from  @UserLedger

    SELECT top 1 @Balance = IsNull(Balance,0) from UserLedgers 
    where DriverKey = @DriverKey
    ORDER by Id desc

    INSERT into UserLedgers (DriverId,	DriverKey,	TripId,	TreansactionType,	Description, Amount,	Balance,	TrxDate,	TrxId,Flag, RecordDate)
    SELECT DriverId,	DriverKey,	RefId,	TreansactionType,	Description,	Amount,	
    CASE
    WHEN Flag = 1 THEN @Balance + Amount
    WHEN Flag = 2 THEN  @Balance - Amount
    END as Balance,
    GETDATE(),
    '',	
    Flag,GETDATE()
    from @UserLedger

    update UserLedgers set TrxId = 'UID' + RIGHT('00000000' + CAST(SCOPE_IDENTITY() AS VARCHAR(8)), 8) where Id = SCOPE_IDENTITY()
END
GO


-- SELECT * from UserLedgers