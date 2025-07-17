// Defines the contract for the core business logic of the workflow engine.
public interface IWorkflowService
{
    IResult CreateDefinition(WorkflowDefinition definition);
    IResult GetDefinition(string id);
    IResult StartInstance(string definitionId);
    IResult ExecuteAction(string instanceId, string actionId);
    IResult GetInstanceStatus(string instanceId);
}