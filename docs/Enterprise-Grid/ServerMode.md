# Server Mode

`OrizonGridDataRequest` transporta `Page`, `PageSize`, `Skip`, `Take`, `Search`, `Sorts`, `Filters`, `Groups`, `Aggregates` e `SelectedColumns`. O resultado `OrizonGridDataResult<T>` devolve itens, total, grupos, summary e tempo.

```csharp
public sealed class ProductProvider : IOrizonGridDataProvider<Product>
{
    public Task<OrizonGridDataResult<Product>> GetDataAsync(
        OrizonGridDataRequest request, CancellationToken cancellationToken) => ...;
}
```

```razor
<orizon-enterprise-grid server-mode server-url="/grid/data"
 server-sorting server-filtering server-grouping server-paging>...</orizon-enterprise-grid>
```

Filtros suportam Equals, NotEquals, Contains, StartsWith, EndsWith, comparações, Between, In e testes de nulo. Valide campos permitidos no provider antes de construir consultas ao banco.
