drop proc if exists [dbo].[GetOrderWithDetailsByOrderId]
go

create proc [dbo].[GetOrderWithDetailsByOrderId] 
	@orderId bigint
as
begin
	select o.Id, o.[Date], c.Id, c.[Name], op.Id, op.Quantity, op.LocalPrice, p.Id, p.Brand, p.Model, cat.Id, cat.[Name] 
	from dbo.[Order] o
	inner join dbo.City c on c.Id = o.CityId
	inner join dbo.Order_Product op on o.Id = op.OrderId
	inner join dbo.Product p on p.Id = op.ProductId
	inner join dbo.Category cat on cat.Id = p.SubCategoryId
	where o.Id = 144328
end