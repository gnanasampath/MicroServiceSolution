# MicroServiceSolution

## **Applications**

**Platform Service** - Creates and gets Platforms (ex: .Net , SQL server)<br>
**Command Service** - Creates and gets commands for the platform (ex: For .Net platform command would be dotnet run)

## **Tech Used**
  ASP .Net core<br>
  Entity Framework<br>
  SQL server , In Memory DB<br>
  Rabbit MQ for aync Messaging (Once the platform is created using palform service, the platform data is pushed to CommandService)<br>
  Grpc Service <br>

## **To Do**
  **Introduce HTTPS/TLS**. Currently all the internal connections inside the cluster is using http. <br>
  **Revisit Event Processor** - Current event processor handles only create event. Can be extended in a better way.<br>
  **Can write more better use cases** <br>
  **Service Discovery** - Currently ther services are hardcoded and try using service discovery <br>
