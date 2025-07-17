⚙️ Configurable Workflow Engine API
This project is a minimal backend service that implements a configurable state-machine API, as per the Infonetica take-home exercise. It allows clients to define custom workflows, create instances of them, and transition them through various states by executing actions, all with full validation.

Core Concepts
The engine is built around four main concepts:

State: A specific status in a workflow (e.g., 'Draft', 'Approved').

Action: A transition that moves a workflow instance from one state to another.

Workflow Definition: A template that defines the states and actions for a particular workflow.

Workflow Instance: A live, running version of a workflow definition.

✨ Features Implemented
Dynamic Workflow Definitions: Define custom workflows with any number of states and actions via a single API call.

Server-Generated IDs: The server automatically generates unique IDs for all workflow definitions, states, and actions to ensure data integrity.

State Machine Validation: A robust set of rules are enforced on every action execution, preventing invalid state transitions. This includes checks for:

Whether the instance is in a final state.

Whether the action is valid for the instance's current state.

Whether the action is enabled.

Detailed History Tracking: Every workflow instance maintains a detailed, timestamped history of every action performed, including the state it transitioned from and to.

Instance Timestamps: Each instance tracks its CreatedAt and LastUpdatedAt times for better auditing.

RESTful API Design: A clean and logical API structure, with endpoints grouped by functionality (/workflow-definitions and /instances).

Interactive API Documentation: Integrated Swagger UI for easy, interactive testing of all API endpoints directly from the browser.

🚀 Project Flow
The typical flow of interaction with the API follows these logical steps:

Define a Workflow: First, a client sends a POST request to /api/workflow-definitions with the structure of a new workflow, including all its possible states and the actions that transition between them.

Start an Instance: Once a definition exists, a client can create a running instance of it by sending a POST request to /api/instances, specifying the definitionId they want to use. The new instance is automatically placed in the definition's initial state.

Execute Actions: The client moves the instance through the workflow by sending POST requests to the /api/instances/{id}/execute endpoint with the desired actionId. The service validates the request and, if successful, updates the instance's state.

Inspect Status & History: At any point, the client can check the current status of an instance with a GET request to /api/instances/{id} or view its complete transition history with a GET request to /api/instances/{id}/history.

🛠️ How to Use the API (Walkthrough)
After running the application, you can use the interactive Swagger UI at /swagger or a tool like curl to perform the following steps.

1. Create a Workflow Definition
Send a POST request to /api/workflow-definitions with a body like this. Note that you do not need to provide any IDs.

{
  "name": "Article Publishing Workflow",
  "states": [
    { "name": "Draft", "isInitial": true },
    { "name": "In Review" },
    { "name": "Published", "isFinal": true }
  ],
  "actions": [
    { "name": "Submit for Review", "fromStates": ["Draft"], "toState": "In Review" },
    { "name": "Publish Article", "fromStates": ["In Review"], "toState": "Published" }
  ]
}

Response: You will get a 201 Created response containing the full definition with server-generated IDs. Copy the main id of the definition for the next step.

2. Start an Instance
Send a POST request to /api/instances. Replace the placeholder with the ID you copied.

{
  "definitionId": "PASTE_THE_DEFINITION_ID_HERE"
}

Response: You will get a 201 Created response with the new instance details. Copy the id of this instance.

3. Execute an Action
Send a POST request to /api/instances/{id}/execute. Replace {id} with your instance ID. To find the actionId to use, you can look at the definition you received in step 1.

{
  "actionId": "PASTE_THE_ACTION_ID_FOR_Submit for Review_HERE"
}

Response: You will get a 200 OK response showing the instance has now moved to the "In Review" state.

💻 How to Run
Prerequisites: Ensure you have the .NET 8 SDK installed.

Navigate to the API project folder:

cd src/Infonetica.WorkflowEngine.Api

Run the application:

dotnet run

The API will be available at the URL shown in your terminal (e.g., http://localhost:5000).

🏗️ Tech Stack
.NET 8 & C# 12

ASP.NET Core Minimal API

Swagger / OpenAPI for documentation.

In-Memory Persistence for data storage.

📝 Assumptions and Limitations
In-Memory Storage: All data is lost when the application is restarted, as per the project requirements.

Error Handling: The API provides meaningful error messages for invalid operations. More detailed logging could be added for internal server errors.

Action-State Mapping: The fromStates and toState fields in an action are assumed to correspond to the Name of a state, not its auto-generated ID, for ease of use when defining a workflow