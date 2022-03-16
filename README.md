# RPG-Catalog-Service-app

Let's imagine a video game where the player will need to acquire a series of items to stay healthy, become stronger and survive the multiple changes that await him in his adventures. There will be a series of stores that will allow the player to purchase items like potions, antidotes and even swords and shields. These items have prices to purchase them and the player will need to present the right amount of some sort of currency (that we will just call jill). Once the player successfully purchases an item it then goes into his inventory where it will be available for him whenever he needs it. 

So these are the backend services for this game which the client side will heavily depend on to be able to store the items catalog the player's deal and inventory and to enable the in-game purchase experience. 

At the core of microservices-based architecture are identified four
microservices:
- Catalog which owns the list of items available for purchase. 
- Inventory which keeps track of the quantity of items that a player owns.
- Identity which manages the list of players and also acts as an identity provider.
- Trading which owns the purchase process that can grant inventory items. 

Each of these services have their own database that has no relationship with other databases and that are of exclusive use by the owning service for inter-service communication the system makes use of a message broker, which allows the services to collaborate by publishing and consuming messages asynchronously. 

For the clients to interact with the microservices this architecture has an api gateway which provides a lot of flexibility to make changes to the services without impacting the clients and lets the services focus on their business while delegating multiple cross-cutting concerns to the gateway. There is also a front-end portal that enables the administration of the items catalog and the inventory and it also includes a store section where players can purchase items. 

Finally there are a few infrastructure components like logging, distributed tracing and monitoring that all microservices can interact with and that can greatly help to troubleshoot issues and make sure the whole system remains healthy. 
