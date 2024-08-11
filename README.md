# MicroServiceSolution

## **Applications**

**Platform Service** - Creates and gets Platforms (ex: .Net , SQL server)
**Command Service** - Creates and gets commands for the platform (ex: For .Net platform command would be dotnet run)

## **Tech Used**
  ASP .Net core
  Entity Framework
  SQL server , In Memory DB
  Rabbit MQ for aync Messaging (Once the platform is created using palform service, the platform data is pushed to CommandService)
  Grpc Service

## **To Do**
  **Introduce HTTPS/TLS**. Currently all the internal connections inside the cluster is using http
  **Revisit Event Processor** - Current event processor handles only create event. Can be extended in a better way.
  **Can write more better use cases**
  **Service Discovery** - Currently ther services are hardcoded and try using service discovery
