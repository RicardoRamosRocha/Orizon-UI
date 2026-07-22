using System.Text.Json.Serialization;

namespace Orizon.UI.Grid;

/// <summary>Describes paging, sorting, filtering, grouping, and projection for a grid data request.</summary>
public sealed class OrizonGridDataRequest
{
    /// <summary>Gets or sets the zero-based page index.</summary>
    public int Page { get; set; }
    /// <summary>Gets or sets the requested page size. The default is 50.</summary>
    public int PageSize { get; set; } = 50;
    /// <summary>Gets or sets the number of records to skip.</summary>
    public int Skip { get; set; }
    /// <summary>Gets or sets the maximum number of records to return.</summary>
    public int Take { get; set; }
    /// <summary>Gets or sets the global search term.</summary>
    public string? Search { get; set; }
    /// <summary>Gets or sets the ordered sort descriptors.</summary>
    public List<OrizonGridSortDescriptor> Sorts { get; set; } = [];
    /// <summary>Gets or sets the active filter descriptors.</summary>
    public List<OrizonGridFilterDescriptor> Filters { get; set; } = [];
    /// <summary>Gets or sets the grouping descriptors.</summary>
    public List<OrizonGridGroupDescriptor> Groups { get; set; } = [];
    /// <summary>Gets or sets the aggregate descriptors.</summary>
    public List<OrizonGridAggregateDescriptor> Aggregates { get; set; } = [];
    /// <summary>Gets or sets the fields selected for projection or export.</summary>
    public List<string> SelectedColumns { get; set; } = [];
    /// <summary>Gets or sets the parent key used for hierarchical data requests.</summary>
    public string? ParentKey { get; set; }
}

/// <summary>Describes a field and direction used to sort grid data.</summary>
/// <param name="Field">The field name.</param><param name="Direction">The sort direction, normally <c>asc</c> or <c>desc</c>.</param>
public sealed record OrizonGridSortDescriptor(string Field, string Direction = "asc");
/// <summary>Describes a field used to group grid data.</summary>
/// <param name="Field">The field name.</param><param name="Expanded">Whether group items are included in the result.</param>
public sealed record OrizonGridGroupDescriptor(string Field, bool Expanded = true);
/// <summary>Describes an aggregate calculation for a grid field.</summary>
/// <param name="Field">The field name.</param><param name="Type">The aggregate type, such as count, sum, average, min, or max.</param>
public sealed record OrizonGridAggregateDescriptor(string Field, string Type);

/// <summary>Specifies the comparison performed by a grid filter.</summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrizonGridFilterOperator { Equals, NotEquals, Contains, StartsWith, EndsWith, GreaterThan, LessThan, GreaterOrEqual, LessOrEqual, Between, In, IsNull, IsNotNull }

/// <summary>Describes a filter applied to a grid field.</summary>
public sealed class OrizonGridFilterDescriptor
{
    /// <summary>Gets or sets the field name.</summary>
    public string Field { get; set; } = string.Empty;
    /// <summary>Gets or sets the comparison operator. The default is <see cref="OrizonGridFilterOperator.Contains"/>.</summary>
    public OrizonGridFilterOperator Operator { get; set; } = OrizonGridFilterOperator.Contains;
    /// <summary>Gets or sets the primary comparison value.</summary>
    public object? Value { get; set; }
    /// <summary>Gets or sets the upper comparison value used by range filters.</summary>
    public object? ValueTo { get; set; }
    /// <summary>Gets or sets the candidate values used by set filters.</summary>
    public List<object?> Values { get; set; } = [];
}

/// <summary>Represents a page of grid data and its aggregate metadata.</summary>
/// <typeparam name="T">The row type.</typeparam>
public sealed class OrizonGridDataResult<T>
{
    /// <summary>Gets the returned rows.</summary>
    public IReadOnlyList<T> Items { get; init; } = [];
    /// <summary>Gets the total number of rows after filtering.</summary>
    public int TotalCount { get; init; }
    /// <summary>Gets grouped results when grouping was requested.</summary>
    public IReadOnlyList<OrizonGridGroupResult<T>> Groups { get; init; } = [];
    /// <summary>Gets aggregate values keyed by field and aggregate type.</summary>
    public IReadOnlyDictionary<string, object?> Summary { get; init; } = new Dictionary<string, object?>();
    /// <summary>Gets the provider execution time in milliseconds.</summary>
    public double ExecutionTime { get; init; }
}

/// <summary>Represents one group in a grouped grid result.</summary>
/// <typeparam name="T">The row type.</typeparam>
public sealed class OrizonGridGroupResult<T>
{
    /// <summary>Gets the group key.</summary>
    public object? Key { get; init; }
    /// <summary>Gets the grouped field name.</summary>
    public string Field { get; init; } = string.Empty;
    /// <summary>Gets the number of rows in the group.</summary>
    public int Count { get; init; }
    /// <summary>Gets the group rows when the group is expanded.</summary>
    public IReadOnlyList<T> Items { get; init; } = [];
    /// <summary>Gets aggregate values for the group.</summary>
    public IReadOnlyDictionary<string, object?> Summary { get; init; } = new Dictionary<string, object?>();
}

/// <summary>Provides data to an Enterprise Grid request.</summary>
/// <typeparam name="T">The row type.</typeparam>
public interface IOrizonGridDataProvider<T>
{
    /// <summary>Returns data and metadata for the specified grid request.</summary>
    /// <param name="request">The grid request.</param><param name="cancellationToken">A token used to cancel the operation.</param><returns>The grid result.</returns>
    Task<OrizonGridDataResult<T>> GetDataAsync(OrizonGridDataRequest request, CancellationToken cancellationToken = default);
}
