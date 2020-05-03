drop proc if exists [dbo].[Order_Product_Insert]
go

alter proc [dbo].[Order_Product_Insert] 
	@orderId bigint,
	@productId bigint,
	@quantity int,
	@localPrice money
as
begin
	insert into dbo.[Order_Product] (OrderId, ProductId, Quantity, LocalPrice)
	values (@orderId, @productId, @quantity, @localPrice)
end