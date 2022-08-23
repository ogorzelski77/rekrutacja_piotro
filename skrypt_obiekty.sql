
CREATE DATABASE [piotrogorzelski]
GO
EXEC sys.sp_db_vardecimal_storage_format N'piotrogorzelski', N'ON'
GO
ALTER DATABASE [piotrogorzelski] SET QUERY_STORE = OFF
GO
USE [piotrogorzelski]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1024) NOT NULL,
	[InsertedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedDate] [datetime] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Price]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price](
	[PriceID] [int] IDENTITY(1,1) NOT NULL,
	[PriceListID] [int] NOT NULL,
	[ArticleID] [int] NOT NULL,
	[Price] [decimal](10, 2) NULL,
	[InsertedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[PriceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceList]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceList](
	[PriceListID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Pricelist] PRIMARY KEY CLUSTERED 
(
	[PriceListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [Deleted]    Script Date: 23.08.2022 23:40:06 ******/
CREATE NONCLUSTERED INDEX [Deleted] ON [dbo].[Article]
(
	[Deleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [PriceListID]    Script Date: 23.08.2022 23:40:06 ******/
CREATE NONCLUSTERED INDEX [PriceListID] ON [dbo].[Price]
(
	[PriceListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_InsertionTime]  DEFAULT (getdate()) FOR [InsertedDate]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Price] ADD  CONSTRAINT [DF_Price_InsertionTime]  DEFAULT (getdate()) FOR [InsertedDate]
GO
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Article] FOREIGN KEY([ArticleID])
REFERENCES [dbo].[Article] ([ArticleID])
GO
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_Article]
GO
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_PriceList] FOREIGN KEY([PriceListID])
REFERENCES [dbo].[PriceList] ([PriceListID])
GO
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_PriceList]
GO
/****** Object:  StoredProcedure [dbo].[DeleteArticle]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteArticle] @ArticleID int 
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN
		UPDATE [dbo].[Article]
		SET DeletedDate = getdate(),
		Deleted = 1
		WHERE ArticleID = @ArticleID
		and Deleted = 0
	END
	select @@rowcount as result
END
GO
/****** Object:  StoredProcedure [dbo].[GetArticle]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetArticle] @ArticleID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT top 1 a.ArticleID, a.[Name], a.InsertionTime, a.UpdateTime 
	FROM Article a
	WHERE a.ArticleID = @ArticleID
	and deleted = 0
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetArticleListWithPrice]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetArticleListWithPrice] @PriceListID int, @PageNumber int, @PageSize int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT a.ArticleID, a.[Name], a.InsertedDate, a.ModifiedDate, p.Price, COUNT(*) OVER() as TotalRows from Article a
	JOIN Price p
	on a.ArticleID = p.ArticleID
	WHERE a.Deleted = 0 and p.PriceListID = @PriceListID
	ORDER BY a.ArticleID
	
	OFFSET @PageSize * (@PageNumber - 1) ROWS
	FETCH NEXT @PageSize ROWS ONLY	

END
GO
/****** Object:  StoredProcedure [dbo].[GetPrices]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPrices] @ArticleID int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT ArticleID, p.PricelistID, pl.Name as PricelistName, p.Price
	FROM Price p
	JOIN PriceList pl
	ON p.PriceListID = pl.PriceListID
	WHERE p.ArticleID = @ArticleID
	ORDER BY pl.PriceListID
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateArticle]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUpdateArticle] @ArticleID int = NULL , @Name nvarchar(1024)
AS
BEGIN
	SET NOCOUNT ON;

	if @ArticleID IS NULL
	BEGIN
		INSERT INTO [dbo].[Article]
			   ([Name]
			   ,[InsertedDate]
			   ,[Deleted])
		 VALUES(@Name, getdate(),0)
		 select @@IDENTITY as result

	END
	ELSE
	BEGIN
		UPDATE [dbo].[Article]
		SET Name = @Name,
		ModifiedDate = getdate()
		WHERE ArticleID = @ArticleID
		select @@ROWCOUNT as result
	END


END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePrice]    Script Date: 23.08.2022 23:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdatePrice] @ArticleID int, @PricelistID int, @Price decimal(10,2)
AS
BEGIN
	
	SET NOCOUNT ON;

	UPDATE Price 
	SET Price = @Price,
	ModifiedDate = getdate()
	WHERE ArticleID = @ArticleID
	and PricelistID = @PricelistID

	IF @@rowcount = 0 

	INSERT INTO [dbo].[Price]
           ([PriceListID]
           ,[ArticleID]
           ,[Price]
           ,[InsertedDate]
           )
     VALUES
           (@PricelistID, @ArticleID, @Price, getdate())
      
END
GO
USE [master]
GO
ALTER DATABASE [piotrogorzelski] SET  READ_WRITE 
GO
