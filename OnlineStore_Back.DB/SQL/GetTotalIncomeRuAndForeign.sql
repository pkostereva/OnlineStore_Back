drop proc if exists [dbo].[GetTotalIncomeRuAndForeign]
go

alter proc [dbo].[GetTotalIncomeRuAndForeign] as
begin 
  select [0] as 'SalesAmountRussia', [1] as 'SalesAmountWorld'
  from (
  select sum(p.Price*op.Quantity) as total, c.IsForeign
  from dbo.Product p
  inner join dbo.Order_Product op on op.ProductId = p.Id
  inner join dbo.[Order] o on o.Id = op.OrderId
  inner join dbo.City c on c.Id = o.CityId
  where c.IsForeign is not null
  group by c.IsForeign) as s
  pivot
  (
  avg(total)
  for s.IsForeign in ([0], [1])
  ) as PivotTable;
end;