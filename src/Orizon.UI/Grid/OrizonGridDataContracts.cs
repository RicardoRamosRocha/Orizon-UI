using System.Text.Json.Serialization;

namespace Orizon.UI.Grid;

public sealed class OrizonGridDataRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; } = 50;
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? Search { get; set; }
    public List<OrizonGridSortDescriptor> Sorts { get; set; } = [];
    public List<OrizonGridFilterDescriptor> Filters { get; set; } = [];
    public List<OrizonGridGroupDescriptor> Groups { get; set; } = [];
    public List<OrizonGridAggregateDescriptor> Aggregates { get; set; } = [];
    public List<string> SelectedColumns { get; set; } = [];
    public string? ParentKey { get; set; }
}

public sealed record OrizonGridSortDescriptor(string Field, string Direction = "asc");
public sealed record OrizonGridGroupDescriptor(string Field, bool Expanded = true);
public sealed record OrizonGridAggregateDescriptor(string Field, string Type);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrizonGridFilterOperator { Equals, NotEquals, Contains, StartsWith, EndsWith, GreaterThan, LessThan, GreaterOrEqual, LessOrEqual, Between, In, IsNull, IsNotNull }

public sealed class OrizonGridFilterDescriptor
{
    public string Field { get; set; } = string.Empty;
    public OrizonGridFilterOperator Operator { get; set; } = OrizonGridFilterOperator.Contains;
    public object? Value { get; set; }
    public object? ValueTo { get; set; }
    public List<object?> Values { get; set; } = [];
}

public sealed class OrizonGridDataResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public IReadOnlyList<OrizonGridGroupResult<T>> Groups { get; init; } = [];
    public IReadOnlyDictionary<string, object?> Summary { get; init; } = new Dictionary<string, object?>();
    public double ExecutionTime { get; init; }
}

public sealed class OrizonGridGroupResult<T>
{
    public object? Key { get; init; }
    public string Field { get; init; } = string.Empty;
    public int Count { get; init; }
    public IReadOnlyList<T> Items { get; init; } = [];
    public IReadOnlyDictionary<string, object?> Summary { get; init; } = new Dictionary<string, object?>();
}

public interface IOrizonGridDataProvider<T>
{
    Task<OrizonGridDataResult<T>> GetDataAsync(OrizonGridDataRequest request, CancellationToken cancellationToken = default);
}
