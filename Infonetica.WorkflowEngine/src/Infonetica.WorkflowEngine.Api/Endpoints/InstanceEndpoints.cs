// Groups the API endpoints related to workflow instances.
public static class InstanceEndpoints
{
    public static void MapInstanceEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/instances");

        group.MapPost("/", (IWorkflowService service, StartInstanceRequest request) =>
            service.StartInstance(request.DefinitionId));

        group.MapPost("/{id}/execute", (IWorkflowService service, string id, ExecuteActionRequest request) =>
            service.ExecuteAction(id, request.ActionId));

        group.MapGet("/{id}", (IWorkflowService service, string id) =>
            service.GetInstanceStatus(id));
    }
}

public record StartInstanceRequest(string DefinitionId);
public record ExecuteActionRequest(string ActionId);