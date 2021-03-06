USE [Galadventure_Trabajos]
GO
--/****** Object:  StoredProcedure [dbo].[SP_Insert_CountReservation]    Script Date: 16/6/2017 18:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 alter PROCEDURE [dbo].[SP_Insert_ReservationContinue]
	--declare
	 @rId  INT, @ProductCalenderId INT,  @CategoryProductId INT 
	--set @rId  = 1312
	--set @ProductCalenderId =1416
	--set   @CategoryProductId = 5 
 AS

declare @count int
set @count = 0 

declare @d datetime,  @tourds int ,  @tourDayId int , @ProductCalenderIdNEW int

select 
@d = c.[Date]
, @tourds  =  left(c.[Tourdays],1) 
,@tourDayId = r.TourDaysId 
from [dbo].[PRODUCT_RESERVATION] r 
inner join [dbo].[PRODUCT_CALENDER] c on c.Id = r.ProductCalenderId
where r.Id = @rId ;

set  @count = 0;	
Select @count = count(*) from [dbo].[TOUR_DAYS]
where id = @tourDayId and Name like '%8%'

delete FROM [dbo].[PRODUCT_RESERVATION]
where [BookId] = convert(nvarchar(20),@rId)


IF (@count > 0) 
	BEGIN 

		select distinct --   @rId as rId,   @d as booked , @tourds as tourDays, @tourDayId as TourDayId ,
	    @ProductCalenderIdNEW =  c.Id
		from [dbo].[PRODUCT_CALENDER] c
		where  c.CategoryProductId = @CategoryProductId and c.[Date] = (@d + (@tourds-1))	
	
		insert into  [dbo].[PRODUCT_RESERVATION]  (
				[ProductCalenderId]
			  ,[AgentId]
			  ,[PaymentTypeId]
			  ,[AgencyId]
			  ,[Description]
			  ,[PaxId]
			  ,[Price]
			  ,[CreateDate]
			  ,[PaymentStatusId]
			  ,[ProductReservationTypeId]
			  ,[BloqueoDate]
			  ,[TourDaysId]
			  ,[DivePrice]
			  ,[DiveId]
			  ,[PickUp]
			  ,[DropOff]
			  ,[Restrictions]
			  ,[Continua]
			  ,[CabinId]
			  ,[FacturaNr]
			  ,[NetoPrice]
			  , [BookId]) 
		 SELECT 
			   @ProductCalenderIdNEW as [ProductCalenderId]
			  ,[AgentId]
			  ,[PaymentTypeId]
			  ,[AgencyId]
			  ,[Description]
			  ,[PaxId]
			  , '0' as [Price]
			  ,[CreateDate]
			  ,[PaymentStatusId]
			  ,[ProductReservationTypeId]
			  ,[BloqueoDate]
			  ,case @tourds when '4' then 3 when '5' then 1 end  as [TourDaysId]
			  ,[DivePrice]
			  ,[DiveId]
			  ,[PickUp]
			  ,[DropOff]
			  ,[Restrictions]
			  ,1 as [Continua]
			  ,[CabinId]
			  ,[FacturaNr]
			  ,'0' as [NetoPrice]
			  , @rId as [BookId]
		  FROM [dbo].[PRODUCT_RESERVATION]
		 where Id = @rId

			set  @count = 0;	
			Select @count = count(*) from [dbo].[PRODUCT_RESERVATION] 
			where ProductCalenderId = @ProductCalenderIdNEW

			UPDATE [dbo].[PRODUCT_CALENDER]
			SET [TotalBooks] = @count
			WHERE Id = @ProductCalenderIdNEW  
	END

SELECT	'Return Value' = @ProductCalenderId



