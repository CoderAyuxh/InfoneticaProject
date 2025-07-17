// Defines the contract for data storage operations.
public interface IWorkflowRepository
{
    void AddDefinition(WorkflowDefinition definition);
    WorkflowDefinition? GetDefinition(string id);
    void AddInstance(WorkflowInstance instance);
    WorkflowInstance? GetInstance(string id);
    void UpdateInstance(WorkflowInstance instance);
}