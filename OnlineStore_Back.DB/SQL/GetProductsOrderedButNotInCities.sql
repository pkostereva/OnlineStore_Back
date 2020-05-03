drop proc if exists [dbo].[GetProductsOrderedButNotInCities]
go

alter proc [dbo].[GetProductsOrderedButNotInCities] as
begin
	select distinct p.Id, p.Brand, p.Model, p.Price, c.Id, c.[Name] 
	from dbo.Product p
	left join dbo.City_Product cp on p.Id = cp.ProductId
	inner join dbo.Category c on c.Id = p.SubCategoryId
	inner join dbo.Order_Product op on op.ProductId = p.Id
	where cp.Quantity = 0 or (cp.Id is null and p.Id is not null)
	group by c.id, c.[Name], p.Id, p.Brand, p.Model, p.Price
end;