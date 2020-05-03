drop proc if exists [dbo].[GetTotalWorthByCity]
go

create proc [dbo].[GetTotalWorthByCity]
as
begin
	select c.[Name], sum(p.Price*cp.Quantity) as [Money]
	from dbo.Product p 
	inner join dbo.City_Product cp on cp.ProductId = p.Id 
	inner join dbo.City c on cp.CityId=c.Id 
	group by c.[Name]
end