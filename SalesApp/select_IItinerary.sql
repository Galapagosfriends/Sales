﻿
INSERT INTO [dbo].[I_Itinerary]
           ([ICategoryPlaceId]
           ,[IProvideId]
           ,[IDaysId]
           ,[Price]
           ,[MeetingPoint]
           ,[StartTourTime]
           ,[EndTourTime]
           ,[Included]
           ,[CreatedBy]
           ,[Created]
           ,[Deleted]
           ,[ModifyDate]
           ,[ModifyBy])
     select [ICategoryPlaceId]
           ,2
           ,[IDaysId]
           ,[Price]
           ,[MeetingPoint]
           ,[StartTourTime]
           ,[EndTourTime]
           ,[Included]
           ,[CreatedBy]
           ,[Created]
           ,[Deleted]
           ,[ModifyDate]
           ,[ModifyBy]
		   from [dbo].[I_Itinerary]
		   where [IProvideId] = 1