using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace Orizon.UI.Grid;

public sealed class OrizonInMemoryGridDataProvider<T>(IEnumerable<T> source) : IOrizonGridDataProvider<T>
{
    private readonly IReadOnlyList<T> _source = source.ToArray();

    public Task<OrizonGridDataResult<T>> GetDataAsync(OrizonGridDataRequest request, CancellationToken cancellationToken = default)
    {
        var watch = Stopwatch.StartNew(); cancellationToken.ThrowIfCancellationRequested();
        IEnumerable<T> query = _source;
        if (!string.IsNullOrWhiteSpace(request.Search)) query = query.Where(item => Properties().Any(property => Convert.ToString(property.GetValue(item), CultureInfo.InvariantCulture)?.Contains(request.Search, StringComparison.OrdinalIgnoreCase) == true));
        foreach (var filter in request.Filters) query = query.Where(item => Matches(Value(item, filter.Field), filter));
        var filtered = query.ToList();
        IOrderedEnumerable<T>? ordered = null;
        foreach (var sort in request.Sorts)
        {
            Func<T, object?> key = item => Value(item, sort.Field);
            ordered = ordered is null ? (sort.Direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? filtered.OrderByDescending(key, ObjectComparer.Instance) : filtered.OrderBy(key, ObjectComparer.Instance)) : (sort.Direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? ordered.ThenByDescending(key, ObjectComparer.Instance) : ordered.ThenBy(key, ObjectComparer.Instance));
        }
        var sorted = (ordered is null ? filtered.AsEnumerable() : ordered).ToList();
        var summary = Aggregate(filtered, request.Aggregates);
        var groups = request.Groups.Count == 0 ? [] : BuildGroups(sorted, request.Groups[0], request.Aggregates);
        var skip = request.Skip > 0 ? request.Skip : Math.Max(0, request.Page) * Math.Max(1, request.PageSize);
        var take = request.Take > 0 ? request.Take : Math.Max(1, request.PageSize);
        var page = sorted.Skip(skip).Take(take).ToArray(); watch.Stop();
        return Task.FromResult(new OrizonGridDataResult<T> { Items = page, TotalCount = filtered.Count, Groups = groups, Summary = summary, ExecutionTime = watch.Elapsed.TotalMilliseconds });
    }

    private static PropertyInfo[] Properties() => typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
    private static object? Value(T item, string field) => field.Split('.').Aggregate<string, object?>(item, (value, name) => value?.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase)?.GetValue(value));
    private static bool Matches(object? actual, OrizonGridFilterDescriptor filter)
    {
        if (filter.Operator == OrizonGridFilterOperator.IsNull) return actual is null;
        if (filter.Operator == OrizonGridFilterOperator.IsNotNull) return actual is not null;
        var text = Convert.ToString(actual, CultureInfo.InvariantCulture) ?? string.Empty;
        var expected = Convert.ToString(filter.Value, CultureInfo.InvariantCulture) ?? string.Empty;
        var comparison = Compare(actual, filter.Value);
        return filter.Operator switch { OrizonGridFilterOperator.Equals => comparison == 0, OrizonGridFilterOperator.NotEquals => comparison != 0, OrizonGridFilterOperator.Contains => text.Contains(expected, StringComparison.OrdinalIgnoreCase), OrizonGridFilterOperator.StartsWith => text.StartsWith(expected, StringComparison.OrdinalIgnoreCase), OrizonGridFilterOperator.EndsWith => text.EndsWith(expected, StringComparison.OrdinalIgnoreCase), OrizonGridFilterOperator.GreaterThan => comparison > 0, OrizonGridFilterOperator.LessThan => comparison < 0, OrizonGridFilterOperator.GreaterOrEqual => comparison >= 0, OrizonGridFilterOperator.LessOrEqual => comparison <= 0, OrizonGridFilterOperator.Between => comparison >= 0 && Compare(actual, filter.ValueTo) <= 0, OrizonGridFilterOperator.In => filter.Values.Any(value => Compare(actual, value) == 0), _ => true };
    }
    private static int Compare(object? actual, object? expected)
    {
        if (actual is null) return expected is null ? 0 : -1; if (expected is null) return 1;
        try { var converted = expected is IConvertible ? Convert.ChangeType(expected, Nullable.GetUnderlyingType(actual.GetType()) ?? actual.GetType(), CultureInfo.InvariantCulture) : expected; return Comparer.DefaultInvariant.Compare(actual, converted); } catch { return string.Compare(Convert.ToString(actual, CultureInfo.InvariantCulture), Convert.ToString(expected, CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase); }
    }
    private static IReadOnlyDictionary<string, object?> Aggregate(IEnumerable<T> items, IEnumerable<OrizonGridAggregateDescriptor> descriptors)
    {
        var rows = items.ToArray(); var result = new Dictionary<string, object?>();
        foreach (var descriptor in descriptors) { var numbers = rows.Select(item => Value(item, descriptor.Field)).Where(value => value is not null).Select(value => { try { return Convert.ToDecimal(value, CultureInfo.InvariantCulture); } catch { return 0; } }).ToArray(); result[$"{descriptor.Field}:{descriptor.Type}"] = descriptor.Type.ToLowerInvariant() switch { "count" => rows.Length, "sum" => numbers.Sum(), "average" or "avg" => numbers.Length == 0 ? 0 : numbers.Average(), "min" => numbers.Length == 0 ? 0 : numbers.Min(), "max" => numbers.Length == 0 ? 0 : numbers.Max(), _ => null }; }
        return result;
    }
    private static IReadOnlyList<OrizonGridGroupResult<T>> BuildGroups(IEnumerable<T> items, OrizonGridGroupDescriptor descriptor, IEnumerable<OrizonGridAggregateDescriptor> aggregates) => items.GroupBy(item => Value(item, descriptor.Field)).Select(group => new OrizonGridGroupResult<T> { Field = descriptor.Field, Key = group.Key, Count = group.Count(), Items = descriptor.Expanded ? group.ToArray() : [], Summary = Aggregate(group, aggregates) }).ToArray();

    private sealed class ObjectComparer : IComparer<object?> { public static readonly ObjectComparer Instance = new(); public int Compare(object? x, object? y) => OrizonInMemoryGridDataProvider<T>.Compare(x, y); }
}
