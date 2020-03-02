USE [CARD]
GO
/****** Object:  StoredProcedure [dbo].[RegisterCard]    Script Date: 3/2/2020 4:59:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[RegisterCard]
	@CardNum varchar(25),
	@CardType varchar(15),
	@CardStat varchar(15),
	@ExpDate varchar(6)
AS
	INSERT INTO CardRegisted( CardNum,CardType,CardStat,ExpDate )
	VALUES ( @CardNum,@CardType,@CardStat, @ExpDate);
RETURN 1