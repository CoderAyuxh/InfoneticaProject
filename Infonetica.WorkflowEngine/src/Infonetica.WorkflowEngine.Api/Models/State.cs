// Represents a single state in a workflow.
public record State(
    string Id,
    string Name,
    bool IsInitial = false,
    bool IsFinal = false,
    bool Enabled = true
);