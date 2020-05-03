drop proc if exists [dbo].[GetProductsInStockButNotInCities]
go

create proc [dbo].[GetProductsInStockButNotInCities] as
begin 
  select p.Id, p.Brand, p.Model, p.Price, c.Id, c.[Name] 
  from dbo.Product p
  inner join dbo.City_Product cp on cp.ProductId = p.Id
  inner join dbo.Category c on c.Id = p.SubCategoryId
  where cp.CityId = 6 and p.Id not in
  (select p.Id 
  from dbo.Product p
  inner join dbo.City_Product cp on p.Id=cp.ProductId
  where cp.CityId = 2 or cp.CityId = 3)
end;