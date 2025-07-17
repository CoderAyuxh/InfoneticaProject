// Represents a transition between states.
public record Action(
    string Id,
    string Name,
    List<string> FromStates,
    string ToState,
    bool Enabled = true
);