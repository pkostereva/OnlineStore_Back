drop proc if exists [dbo].[Product_Search]
go

create proc [dbo].[Product_Search]	 

  @Id int = null,
  @Price money = null,
  @Brand nvarchar(100) = null,
  @Model nvarchar(100) = null,
  @CategoryId int = null,
  @SubcategoryId int = null
as
begin  

declare @sql nvarchar(max)

set @sql=N'select  
    p.Id,
    p.Price,
    p.Brand,
    p.Model,
    c.Id,
    c.Name
    
    from dbo.Product p
    inner join dbo.Category c on c.Id = p.CategoryId
    inner join dbo.Category c1 on c1.Id = p.SubcategoryId
    where '

  if @Id is not null 
  set @sql=@sql+N'p.Id=' + CAST (@Id as nvarchar)+ ' and ' 
  
  if @Price is not null
  set @sql=@sql+N'p.Price='+ CAST ( @Price as nvarchar) + ' and ' 

  if @Brand is not null
  set @sql=@sql+N'p.Brand='''+ @Brand + ''' and ' 

  if @Model is not null 
  set @sql=@sql+N'p.Model=''' + @Model + ''' and ' 

  if @CategoryId is not null 
  set @sql=@sql+N'c.Id=' + CAST (@CategoryId as nvarchar)+ ' and ' 
  
  if @SubcategoryId is not null 
  set @sql=@sql+N'c1.Id=' + CAST (@SubcategoryId as nvarchar)+ ' and ' 

  set @sql=@sql+N' 1=1'

  print @sql
  execute sp_executesql @SQL

end

--exec [dbo].[Product_Search]