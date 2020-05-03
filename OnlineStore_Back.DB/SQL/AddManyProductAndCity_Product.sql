drop proc if exists [dbo].[AddManyProductAndCity_Product]
go

create proc [dbo].[AddManyProductAndCity_Product]	 
as 
begin 

	create table #RandomBrand (id int, [name] nvarchar(50))

	insert into #RandomBrand ([id], [name])
	select 1, 'Tefal' union
	select 2, 'Bork' union
	select 3, 'Haier' union
	select 4, 'LG' union
	select 5, 'Gorenje' union
	select 6, 'Zarget' union
	select 7, 'SNAIGE' union
	select 8, 'Caso' union
	select 9, 'Indesit' union
	select 10, 'Sharp' union
	select 11, 'Hiberg' union
	select 12, 'Tesler' union
	select 13, 'Beko' union
	select 14, 'Candy' union
	select 15, 'Hotpoint-Ariston' union
	select 16, 'Kitfort' union
	select 17, 'Midea' union
	select 18, 'Scandilux' union
	select 19, 'Electrolux' union
	select 20, 'Redmond' union
	select 21, 'Phillips' union
	select 22, 'Babyliss' union
	select 23, 'Rowenta' union
	select 24, 'Scarlett' union
	select 25, 'Vitek' union
	select 26, 'Maxwell' union
	select 27, 'Panasonic' union
	select 28, 'Braun' union
	select 29, 'Andis' union
	select 30, 'Samsung'

	declare @brand nvarchar(100),
		@model nvarchar(100),
		@categoryId int,
		@subcategoryId int,
		@price money,
		@productId bigint,
		@cityId int,
		@quantity int,
		@productCounter int,
		@cityCounter int

	set @productCounter = 0;

	while @productCounter < 100000
	begin
		set @brand = (select top 1 [Name] from #RandomBrand order by newid())
		set @model = (select convert(nvarchar(100), left(newid(),20)))
		set @categoryId = (select top 1 [Id] from dbo.Category where ParentId is null order by newid())
		set @subcategoryId = (select top 1 [id] from dbo.Category where ParentId=@categoryId order by newid())
		set @price = (select (round(1000+(rand(checksum(newid()))*59990),0)) )
		insert into [dbo].[Product]
			([Brand],
			[Model],
			[CategoryId],
			[SubCategoryId],
			[Price])
			values
				(@brand,
				@model,
				@categoryId,
				@subcategoryId,
				@price)
		set @productId = scope_identity()

		set @cityCounter = (select (round(1+(rand(checksum(newid()))*5),0)));
		while @cityCounter > 0
		begin
			set @cityId = (select top 1 [Id] from dbo.City order by newid())
			set @quantity = (select (round((rand(checksum(newid()))*200),0)) )

			if @cityCounter != 6
			insert into dbo.[City_Product]
				([ProductId],
				[CityId],
				[Quantity])
				values 
					(@productId, 
					@cityId, 
					@quantity)
		set @cityCounter -= 1
	end;
		set @productCounter += 1
	end;
end