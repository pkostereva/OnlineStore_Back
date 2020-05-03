drop proc if exists [dbo].[AddManyOrders]
go

create proc [dbo].[AddManyOrders]
as 
begin
	declare 
		@orderId bigint,
		@cityId int,
		@productId bigint,
		@quantity int,
		@localprice money,
		@orderCounter int,
		@orderDetailsCounter int

	set @orderCounter = 0;

	while @orderCounter < 50000
	begin
		set @cityId = (select top 1 [Id] from dbo.City order by newid() )
		--set @cityId = (select rand()*(5-2)+5)
		insert into [dbo].[Order]
			([CityId],
			[Date])
			values
				(@cityId,
				dateadd(day, rand(checksum(newid()))*(1+datediff(day, '2015-01-01', '2020-05-01')), '2015-01-01'))
		set @orderId = scope_identity()

		set @orderDetailsCounter = (select (round(1+(rand(checksum(newid()))*5),0)));
		while @orderDetailsCounter > 0
		begin
			set @productId = (select rand()*(2110024-110028)+110028);
			set @quantity = (select (round(1+(rand(checksum(newid()))*10),0)) )
			set @localprice = (select (round(1000+(rand(checksum(newid()))*1000),0)) )
			insert into dbo.[Order_Product]
				([ProductId],
				[OrderId],
				[Quantity],
				[LocalPrice])
				values 
					(@productId, 
					@orderId, 
					@quantity,
					@localprice)
		set @orderDetailsCounter -= 1
		end;
		set @orderCounter += 1
	end;
end