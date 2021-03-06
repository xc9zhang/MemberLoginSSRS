

USE [TEReporting]
GO

/****** Object:  Table [dbo].[DailyBooking]    Script Date: 4/21/2016 8:17:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DailyBooking](
	[Id] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[Booking] [int] NOT NULL,
	[Passenger] [int] NOT NULL,
	[Source] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CancelledBooking] [int] NULL,
	[CancelledPassenger] [int] NULL,
 CONSTRAINT [PK_DailyBooking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



USE [TEReporting]
GO

/****** Object:  Table [dbo].[ChinaDailyBooking]    Script Date: 4/21/2016 8:16:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ChinaDailyBooking](
	[Id] [int] NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[Booking] [int] NOT NULL,
	[Passenger] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CancelledBooking] [int] NULL,
	[CancelledPassenger] [int] NULL,
	[Import] [bit] NULL,
	[ChinaClientId] [int] NULL,
	[DK] [varchar](max) NULL,
 CONSTRAINT [PK_ChinaDailyBooking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO




USE [TEReporting]
GO
/****** Object:  StoredProcedure [dbo].[SP_FeedBookingData]    Script Date: 4/20/2016 7:03:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_FeedDailyBookingData]
AS
    BEGIN

        SET NOCOUNT ON;

		declare @FromDate DATETIME = DATEADD(dd, 0, DATEDIFF(dd, 0, getdate())) 
        declare @ToDate DATETIME =  DATEADD(ms, -2, DATEADD(dd, 1, DATEDIFF(dd, 0, getdate())))


        DELETE  FROM [dbo].[DailyBooking]
        WHERE   ( @FromDate IS NULL
                  OR BookingDate >= @FromDate
                )
                AND ( @ToDate IS NULL
                      OR BookingDate <= @ToDate
                    )
                AND Source = 'TEB2B'



		



        INSERT  INTO [dbo].[DailyBooking]
                ( [ClientId] ,
                  [BookingDate] ,
                  [Booking] ,
                  [CancelledBooking] ,
                  [Passenger] ,
                  [CancelledPassenger] ,
                  [Source] ,
                  [CreatedOn]



		        )
                SELECT  Client.ClientId ,
                        TEB2B.createdate ,
                        CASE WHEN TEB2B.status <> 'cancelled' THEN 1
                             ELSE 0
                        END ,
                        CASE WHEN TEB2B.status = 'cancelled' THEN 1
                             ELSE 0
                        END ,
                        CASE WHEN TEB2B.status <> 'cancelled'
                             THEN TEB2B.Passenger
                             ELSE 0
                        END ,
                        CASE WHEN TEB2B.status = 'cancelled'
                             THEN TEB2B.Passenger
                             ELSE 0
                        END ,
                        'TEB2B' ,
                        GETDATE()
                FROM    TESQL2.[e_com].[dbo].[TEB2B_booking_counts] TEB2B
                        INNER JOIN [dbo].[Client] Client ON Client.ApplicationId = TEB2B.ApplicationID
                WHERE   ( @FromDate IS NULL
                          OR TEB2B.createdate >= @FromDate
                        )
                        AND ( @ToDate IS NULL
                              OR TEB2B.createdate <= @ToDate
                            )











        DELETE  FROM [dbo].[DailyBooking]
        WHERE   ( @FromDate IS NULL
                  OR BookingDate >= @FromDate
                )
                AND ( @ToDate IS NULL
                      OR BookingDate <= @ToDate
                    )
                AND Source = 'Hopper'



						




		declare @HopperFromDate DATETIME = DATEADD(dd, -1, @FromDate) 
        declare @HopperToDate DATETIME =  DATEADD(dd, -1, @ToDate) 



        INSERT  INTO [dbo].[DailyBooking]
                ( [ClientId] ,
                  [BookingDate] ,
                  [Booking] ,
                  [CancelledBooking] ,
                  [Passenger] ,
                  [Source] ,
                  [CreatedOn]



		        )
                SELECT  Client.ClientId ,
                        Hopper.processDate ,
                        Hopper.TotalBookings - Hopper.TotalCancelledBookings ,
                        Hopper.TotalCancelledBookings ,
                        Hopper.TotalTicketCount ,
                        'Hopper' ,
                        GETDATE()
                FROM    TESQL1.[ImportPNR].[dbo].[Report.Summary] Hopper
                        INNER JOIN [dbo].[Client] Client ON Client.ApplicationId = 9999999
                WHERE   ( @FromDate IS NULL
                          OR Hopper.processDate >= @HopperFromDate
                        )
                        AND ( @ToDate IS NULL
                              OR Hopper.processDate <= @HopperToDate
                            )






        DELETE  FROM [dbo].[DailyBooking]
        WHERE   ( @FromDate IS NULL
                  OR BookingDate >= @FromDate
                )
                AND ( @ToDate IS NULL
                      OR BookingDate <= @ToDate
                    )
                AND Source = 'TE2U'



						







        INSERT  INTO [dbo].[DailyBooking]
                ( [ClientId] ,
                  [BookingDate] ,
                  [Booking] ,
                  [CancelledBooking] ,
                  [Passenger] ,
                  [CancelledPassenger] ,
                  [Source] ,
                  [CreatedOn]



		        )
                SELECT  Client.ClientId ,
                        TE2U.BookTimeUTC ,
                        CASE WHEN TE2U.[Status] <> 99 THEN 1
                             ELSE 0
                        END ,
                        CASE WHEN TE2U.[Status] = 99 THEN 1
                             ELSE 0
                        END ,
                        CASE WHEN TE2U.[Status] <> 99 THEN TE2U.Numticket
                             ELSE 0
                        END ,
                        CASE WHEN TE2U.[Status] = 99 THEN TE2U.Numticket
                             ELSE 0
                        END ,
                        'TE2U' ,
                        GETDATE()
                FROM    [TE2U].[SabreBook].[dbo].[OrderView] TE2U
                        INNER JOIN [dbo].[Client] Client ON Client.ApplicationId = 8888888
                WHERE   ( @FromDate IS NULL
                          OR TE2U.BookTimeUTC >= @FromDate
                        )
                        AND ( @ToDate IS NULL
                              OR TE2U.BookTimeUTC <= @ToDate
                            )
                        AND ( ( TE2U.Import = 1
                                AND TE2U.[Status] IN ( 50, 10, 99 )
                              )
                              OR ( TE2U.Import = 0
                                   AND TE2U.[Status] IN ( 50, 99 )
                                 )
                            )







        DELETE  FROM [dbo].[ChinaDailyBooking]
        WHERE   ( @FromDate IS NULL
                  OR BookingDate >= @FromDate
                )
                AND ( @ToDate IS NULL
                      OR BookingDate <= @ToDate
                    )







        INSERT  INTO [dbo].[ChinaDailyBooking]
                ( [BookingDate] ,
                  [Booking] ,
                  [CancelledBooking] ,
                  [Passenger] ,
                  [CancelledPassenger] ,
                  Import ,
				  DK,
                  [CreatedOn]
		        )
                SELECT  TE2U.BookTimeUTC ,
                        CASE WHEN TE2U.[Status] <> 99 THEN 1
                             ELSE 0
                        END ,
                        CASE WHEN TE2U.[Status] = 99 THEN 1
                             ELSE 0
                        END ,
                        CASE WHEN TE2U.[Status] <> 99 THEN TE2U.Numticket
                             ELSE 0
                        END ,
                        CASE WHEN TE2U.[Status] = 99 THEN TE2U.Numticket
                             ELSE 0
                        END ,
                        TE2U.Import ,
						TE2U.DK,
                        GETDATE()
                FROM    [TE2U].[SabreBook].[dbo].[OrderView] TE2U
                WHERE   ( @FromDate IS NULL
                          OR TE2U.BookTimeUTC >= @FromDate
                        )
                        AND ( @ToDate IS NULL
                              OR TE2U.BookTimeUTC <= @ToDate
                            )
                        AND ( ( TE2U.Import = 1
                                AND TE2U.[Status] IN ( 50, 10, 99 )
                              )
                              OR ( TE2U.Import = 0
                                   AND TE2U.[Status] IN ( 50, 99 )
                                 )
                            )









    END




GO
USE [TEReporting]
GO
/****** Object:  StoredProcedure [dbo].[SP_SSRS_GetBookingDailyData]    Script Date: 4/21/2016 8:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [dbo].[SP_SSRS_GetBookingDailyData]
AS


    BEGIN

        SET NOCOUNT ON;
		declare @FromDate DATETIME = DATEADD(dd, 0, DATEDIFF(dd, 0, getdate())) 
        declare @ToDate DATETIME =  DATEADD(ms, -2, DATEADD(dd, 1, DATEDIFF(dd, 0, getdate())))



		SELECT    datepart(hour,ChinaBooking.BookingDate) AS HHour ,
                            SUM(ChinaBooking.Passenger) AS Tickets ,
                            'Ctrip'  as Client
               FROM      dbo.ChinaDailyBooking ChinaBooking
                  WHERE     ChinaBooking.DK IN ('ZZ666','XX666') 
                  GROUP BY  datepart(hour,ChinaBooking.BookingDate) 
		union 

		SELECT    datepart(hour,Booking.BookingDate) AS HHour ,
                            SUM(Booking.Passenger) AS Tickets ,
                            Client.ClientName as  Client
                  FROM      dbo.DailyBooking Booking
                            INNER JOIN Client ON dbo.Client.ClientId = Booking.ClientId
                  WHERE     Client.ClientName IN ( 'Hopper', 'TE2U' )

                  GROUP BY  datepart(hour,Booking.BookingDate),Client.ClientName

                  UNION
                  SELECT    datepart(hour,Booking.BookingDate) AS HHour ,
                            SUM(Booking.Passenger) AS Tickets ,
                            'TEB2B' as client
                  FROM      dbo.DailyBooking Booking
                  WHERE     Booking.Source = 'TEB2B'
                  GROUP BY  datepart(hour,Booking.BookingDate)


    END








