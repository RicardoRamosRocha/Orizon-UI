namespace Orizon.UI.Models.Designer;

public interface IDesignerCommand
{
    string Name { get; }
    void Execute();
    void Undo();
}

public sealed record DesignerSnapshot(DateTimeOffset CreatedAt, string Description, object State);

/// <summary>Executes and tracks reversible in-memory designer commands.</summary>
public sealed class DesignerHistory
{
    private readonly Stack<IDesignerCommand> _undo = [];
    private readonly Stack<IDesignerCommand> _redo = [];
    private readonly List<DesignerSnapshot> _snapshots = [];
    public bool CanUndo => _undo.Count > 0;
    public bool CanRedo => _redo.Count > 0;
    public IReadOnlyList<DesignerSnapshot> Snapshots => _snapshots;

    public void Execute(IDesignerCommand command) { ArgumentNullException.ThrowIfNull(command); command.Execute(); Record(command); }
    public void Record(IDesignerCommand command) { ArgumentNullException.ThrowIfNull(command); _undo.Push(command); _redo.Clear(); }
    public void Snapshot(string description, object state) => _snapshots.Add(new(DateTimeOffset.UtcNow, description, state));
    public IDesignerCommand? Undo() { if (!_undo.TryPop(out var command)) return null; command.Undo(); _redo.Push(command); return command; }
    public IDesignerCommand? Redo() { if (!_redo.TryPop(out var command)) return null; command.Execute(); _undo.Push(command); return command; }
}
