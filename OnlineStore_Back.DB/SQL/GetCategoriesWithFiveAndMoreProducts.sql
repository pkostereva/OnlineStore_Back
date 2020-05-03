drop proc if exists [dbo].[GetCategoriesWithFiveAndMoreProducts]
go

alter proc [dbo].[GetCategoriesWithFiveAndMoreProducts] as
begin 
  select c.Id, c.[Name], count(c.[Name]) as CountOfProducts 
  from dbo.Product p 
  inner join dbo.Category c on p.SubCategoryId = c.Id 
  group by c.Id, c.[Name]
  having count(c.[Name]) >= 5
end;
