drop proc if exists [dbo].[GetOrdersByTimeSpan] 
go

alter proc [dbo].[GetOrdersByTimeSpan] 
	@start Datetime2(7), 
	@end Datetime2(7)
as
begin 
  select 
  o.Id, o.[Date], 
  c.Id, c.[Name], 
  sum(op.Quantity) as TotalQuantity, 
  sum(p.Price*op.Quantity) as TotalCost,
  p.Id, p.Brand, p.Model, 
  cat.Id, cat.[Name]
  from dbo.[Order] o 
  inner join dbo.Order_Product op on op.ProductId = o.Id
  inner join dbo.City c on o.CityId = c.Id 
  inner join dbo.Product p on op.ProductId = p.Id
  inner join dbo.Category cat on cat.Id = p.SubCategoryId
  where o.[Date] between @start and @end
  group by o.[Date], o.Id, c.Id, c.[Name], p.Id, p.Brand, p.Model, cat.Id, cat.[Name]
  order by o.[Date] asc
end;
