using System.Collections.Concurrent;

// Implements the repository using in-memory dictionaries.
public class InMemoryWorkflowRepository : IWorkflowRepository
{
    private readonly ConcurrentDictionary<string, WorkflowDefinition> _definitions = new();
    private readonly ConcurrentDictionary<string, WorkflowInstance> _instances = new();

    public void AddDefinition(WorkflowDefinition definition)
    {
        _definitions[definition.Id] = definition;
    }

    public WorkflowDefinition? GetDefinition(string id)
    {
        _definitions.TryGetValue(id, out var definition);
        return definition;
    }

    public void AddInstance(WorkflowInstance instance)
    {
        _instances[instance.Id] = instance;
    }

    public WorkflowInstance? GetInstance(string id)
    {
        _instances.TryGetValue(id, out var instance);
        return instance;
    }

    public void UpdateInstance(WorkflowInstance instance)
    {
        _instances[instance.Id] = instance;
    }
}