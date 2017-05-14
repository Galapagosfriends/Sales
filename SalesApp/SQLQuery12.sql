
ALTER PROCEDURE [dbo].[SP_Insert_CountReservation]
	@ProductCalenderId INT
AS

declare @count int
set @count = 0 
Select @count = count(*) from [dbo].[PRODUCT_RESERVATION] 
where ProductCalenderId = @ProductCalenderId

UPDATE [dbo].[PRODUCT_CALENDER]
SET [TotalBooks] = @count
WHERE Id = @ProductCalenderId

 SELECT	'Return Value' = @ProductCalenderId


