// Contains the main business logic and validation rules.
public class WorkflowService : IWorkflowService
{
    private readonly IWorkflowRepository _repository;

    public WorkflowService(IWorkflowRepository repository)
    {
        _repository = repository;
    }

    public IResult CreateDefinition(WorkflowDefinition definition)
    {
        if (definition.States.Count(s => s.IsInitial) != 1)
        {
            return Results.BadRequest("A workflow definition must have exactly one initial state.");
        }

        _repository.AddDefinition(definition);
        return Results.Created($"/api/workflow-definitions/{definition.Id}", definition);
    }

    public IResult GetDefinition(string id)
    {
        var definition = _repository.GetDefinition(id);
        return definition is not null ? Results.Ok(definition) : Results.NotFound();
    }

    public IResult StartInstance(string definitionId)
    {
        var definition = _repository.GetDefinition(definitionId);
        if (definition is null)
        {
            return Results.NotFound("Workflow definition not found.");
        }

        var initialState = definition.States.Single(s => s.IsInitial);
        var instance = new WorkflowInstance
        {
            DefinitionId = definitionId,
            CurrentStateId = initialState.Id
        };

        _repository.AddInstance(instance);
        return Results.Created($"/api/instances/{instance.Id}", instance);
    }

    public IResult ExecuteAction(string instanceId, string actionId)
    {
        var instance = _repository.GetInstance(instanceId);
        if (instance is null) return Results.NotFound("Workflow instance not found.");

        var definition = _repository.GetDefinition(instance.DefinitionId);
        if (definition is null) return Results.Problem("Could not find definition for this instance.");

        var currentState = definition.States.SingleOrDefault(s => s.Id == instance.CurrentStateId);
        if (currentState is null) return Results.Problem("Instance is in an invalid state.");

        if (currentState.IsFinal)
        {
            return Results.BadRequest("Cannot execute action on an instance in a final state.");
        }

        var action = definition.Actions.SingleOrDefault(a => a.Id == actionId);
        if (action is null)
        {
            return Results.BadRequest("Action not found in workflow definition.");
        }

        if (!action.Enabled)
        {
            return Results.BadRequest("Action is disabled.");
        }

        if (!action.FromStates.Contains(instance.CurrentStateId))
        {
            return Results.BadRequest($"Action cannot be executed from the current state '{currentState.Name}'.");
        }

        instance.CurrentStateId = action.ToState;
        instance.History.Add(new ActionHistory(actionId, DateTime.UtcNow));
        _repository.UpdateInstance(instance);

        return Results.Ok(instance);
    }

    public IResult GetInstanceStatus(string instanceId)
    {
        var instance = _repository.GetInstance(instanceId);
        return instance is not null ? Results.Ok(instance) : Results.NotFound();
    }
}