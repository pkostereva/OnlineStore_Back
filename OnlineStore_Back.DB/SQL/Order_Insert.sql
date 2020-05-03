drop proc if exists [dbo].[Order_Insert]
go

alter proc [dbo].[Order_Insert] 
	@cityId int
as
begin
	insert into dbo.[Order] (CityId, [Date])
	values (@cityId, GETDATE())
	select SCOPE_IDENTITY()
end