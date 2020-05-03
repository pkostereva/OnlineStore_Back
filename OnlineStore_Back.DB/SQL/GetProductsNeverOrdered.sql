drop proc if exists [dbo].[GetProductsNeverOrdered]
go

create proc [dbo].[GetProductsNeverOrdered] as
begin 
  select p.Id, p.Brand, p.Model, p.Price, c.Id, c.[Name] 
  from dbo.Product p 
	left join dbo.Category c on p.SubCategoryId = c.Id
	left join dbo.Order_Product op on op.ProductId = p.Id
  where op.ProductId is null
  group by c.[Name], c.Id, p.Id, p.Model, p.Brand, p.Price
end;