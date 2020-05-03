drop proc if exists [dbo].[GetMostSoldProductInCities]
go

alter proc [dbo].[GetMostSoldProductInCities] as
begin
  select c.[Name],
  (select top 1 concat(p.Model, ' ', p.Brand)
  from dbo.Product p
		inner join dbo.Order_Product op on op.ProductId = p.Id
		inner join dbo.[Order] o on op.OrderId = o.Id
		inner join dbo.City ct on ct.Id = o.CityId
		where c.Id = o.CityId
		group by p.Model, p.Brand
		order by sum(op.Quantity) desc) Product
	from dbo.City c
	where c.Id != 6
end;