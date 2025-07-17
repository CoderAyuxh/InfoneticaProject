// Groups the API endpoints related to workflow definitions.
public static class DefinitionEndpoints
{
    public static void MapDefinitionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/workflow-definitions");

        group.MapPost("/", (IWorkflowService service, WorkflowDefinition definition) =>
            service.CreateDefinition(definition));

        group.MapGet("/{id}", (IWorkflowService service, string id) =>
            service.GetDefinition(id));
    }
}