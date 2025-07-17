var builder = WebApplication.CreateBuilder(args);

// Add services to the dependency injection container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register our custom services for Dependency Injection.
builder.Services.AddSingleton<IWorkflowRepository, InMemoryWorkflowRepository>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Map the endpoints from our dedicated endpoint classes.
app.MapDefinitionEndpoints();
app.MapInstanceEndpoints();

app.MapGet("/", () => "Workflow Engine API is running. Navigate to /swagger for documentation.");

app.Run();