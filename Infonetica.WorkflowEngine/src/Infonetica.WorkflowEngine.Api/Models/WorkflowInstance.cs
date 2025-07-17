// Represents a running instance of a workflow definition.
public class WorkflowInstance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DefinitionId { get; set; }
    public string CurrentStateId { get; set; }
    public List<ActionHistory> History { get; set; } = new();
}

// A simple record to track which action was executed and when.
public record ActionHistory(string ActionId, DateTime Timestamp);