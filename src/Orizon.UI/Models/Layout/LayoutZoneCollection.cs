using System.Collections.ObjectModel;

namespace Orizon.UI.Models.Layout;

/// <summary>
/// Stores the named zones declared by a responsive template.
/// </summary>
public sealed class LayoutZoneCollection : Collection<LayoutRegion>
{
    public LayoutRegion? Find(string name) =>
        this.FirstOrDefault(region =>
            string.Equals(region.Name, name, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyCollection<LayoutRegion> Find(LayoutZone zone) =>
        this.Where(region => region.Zone == zone).ToArray();
}
