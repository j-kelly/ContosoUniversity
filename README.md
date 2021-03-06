# Contoso University

A productionised version of the old Microsoft Contoso University example code (with ALL the business rules found in the original example fully intact). This has been implemented not only to demostrate various patterns and techniques, but also as a demonstration of some of the features of NRepository.

Please feel free to open a discussion on what's been done and why :)

## Areas of interest
* Functional domain service calls (no IOC containers)
* Soft deletes (Query and Command interception)
* L2 Caching- Both on database and projected entities
* Projections - enables reuse common queries. Great for alternative to views and inbuilt into NRepository
* Auto generation of test code factories using Text templates (Depricated - well in this project only ;))
* Single line full object instantiation using EntityGenerator.CreateObject<MoneyMovement.Request>() (a personal favourite when writing tests. Saves time and improves clarity on what data you're actually interested in testing :))
* The complete lack of a 'business layer' which is now found in the domain layer.
* The use of use case query definitions i.e. you won't find an interface with this tightly coupled method on it GetCoursesWithStudents. Query code reuse is done strictly through the strategy pattern and projections.
* A common approach for adding all new behaviours to the system. These include Requests, CommandModel, Reponses, InvariantValidation, ContextualValidation, app services, handlers  and test code. All of which is autogenerated through the use of a single snippet.
* Clear project structure - Helps knowing where to put new things and find the as well

### Technologies used:
* NRepository
* Entity Framework
* ASP.Net MVC
* Unity
* Specflow
* Selenium
* MOQ

### Patterns:
* Functional programming techniques for Domain services
* CQRS
* DDD (Semi)
* MVC
* SOLID
* IOC (MVC controllers only)
* Snippets
