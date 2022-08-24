use piotrogorzelski
INSERT INTO [dbo].[Article]
           ([Name]
           ,[InsertedDate]
           )
   
select top 100 n1.name + ' ' +n2.name +' '+ n3.name as Artykul,getdate()
from
(
(
select 'Polo' as [name]
union all
select 'T-Shirt'
union all
select 'Koszula'
union all
select 'Spodenki'
union all
select 'Czapka'
union all
select 'Buty'
) n1
cross join
(
select 'Wrangler' as [name]
union all
select 'Lee'
union all
select 'Levi''s'
union all
select 'Mustang'
)  n2
cross join
(
select 'Zielony' as [name]
union all
select 'Czerwony'
union all
select 'Niebieski'
union all
select 'Bialy'
union all
select 'Czarny'
) n3
)
order by newid()

SET IDENTITY_INSERT [dbo].[PriceList] ON 
GO
INSERT [dbo].[PriceList] ([PriceListID], [Name]) VALUES (1, N'Hurtowy')
GO
INSERT [dbo].[PriceList] ([PriceListID], [Name]) VALUES (2, N'Detaliczny')
GO
INSERT [dbo].[PriceList] ([PriceListID], [Name]) VALUES (3, N'Promocja "Witaj Szkolo"')
GO
SET IDENTITY_INSERT [dbo].[PriceList] OFF


while (1=1)
begin
	
INSERT INTO [dbo].[Price]
           ([PriceListID]
           ,[ArticleID]
           ,[Price]
           ,[InsertedDate]
           )
   
   
   select top 3 pl.PriceListID,a.ArticleID,RAND()*50+50*(1+pl.PriceListID*0.1),getdate()

	from pricelist pl
	cross join Article a
	left join Price p on  p.PriceListID = pl.PriceListID
	and a.ArticleID = p.ArticleID
	where p.ArticleID is null
	order by ArticleID, PriceListID
	if @@ROWCOUNT = 0 break
end