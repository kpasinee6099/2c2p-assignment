USE [CARD]
GO
/****** Object:  StoredProcedure [dbo].[isExistCard]    Script Date: 3/2/2020 4:58:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[isExistCard] 
	@CardNum varchar(20)
AS
BEGIN
	SELECT * FROM CardRegisted WHERE CardNum = @CardNum
END