// See https://aka.ms/new-console-template for more information
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

Console.WriteLine("Hello, World!");

//Connection
await using var client = new ServiceBusClient("Endpoint=sb://servicebus-namespace2.servicebus.windows.net/;SharedAccessKeyName=NewConnection;SharedAccessKey=cNYCz1K2OzQ9EGvmYtUfOyhu3cTybvGu3+ASbD7sAV8=;EntityPath=firsttopic");

// //Sender
// ServiceBusSender sender = client.CreateSender("firsttopic");

// //Send the message
// await sender.SendMessageAsync(new ServiceBusMessage("Sending message to multiple subscription"));

// var receiver = client.CreateReceiver("firsttopic","S1");

// ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();

// Console.WriteLine(message.Body.ToString());

// await receiver.CompleteMessageAsync(message);

// ServiceBusAdministrationClient administrationClient = new ServiceBusAdministrationClient("Endpoint=sb://servicebus-namespace2.servicebus.windows.net/;SharedAccessKeyName=NewConnection;SharedAccessKey=cNYCz1K2OzQ9EGvmYtUfOyhu3cTybvGu3+ASbD7sAV8=;EntityPath=firsttopic");

// await administrationClient.CreateSubscriptionAsync(
//     new CreateSubscriptionOptions("firsttopic", "CorrelationSubscription"),
//     new CreateRuleOptions("correlationfilter", new CorrelationRuleFilter()
//     {
//         Subject = "Nicolas"
//     }));

//Filters

//SQL
// await administrationClient.CreateSubscriptionAsync(
//     new CreateSubscriptionOptions("firsttopic", "SQLFilterSubscription"),
//     new CreateRuleOptions("sqlrules",
//     new SqlRuleFilter("Name='Nicolas'")));

//Boolean

// await administrationClient.CreateSubscriptionAsync(new CreateSubscriptionOptions("firsttopic", "BooleanFilter"),
//     new CreateRuleOptions("booleanrules", new TrueRuleFilter()));

//Correlation filter

//Send Message
// var sender = client.CreateSender("firsttopic");
// Customer obj = new Customer { Age = 32, Name = "Nicolas" };

// var message = new ServiceBusMessage
// {
//     ApplicationProperties = { { "Name", obj.Name }, { "Age", obj.Age } }
// };

// await sender.SendMessageAsync(message);

//Second message
// var obj2 = new Customer { Age = 34, Name = "Nicolas" };

// var message2 = new ServiceBusMessage
// {
//     ApplicationProperties = { { "Name", obj2.Name }, { "Age", obj2.Age } }
// };

// await sender.SendMessageAsync(message2);

// var sender = client.CreateSender("firsttopic");

// await sender.SendMessageAsync(new ServiceBusMessage("Boolean message test"));

// var obj = new Customer { Age = 35, Name = "Nicolas" };

// var message = new ServiceBusMessage
// {
//     Subject = "Nick",
//     ApplicationProperties = { { "Name", obj.Name }, { "Age", obj.Age } }
// };

// await sender.SendMessageAsync(message);

// await administrationClient.CreateSubscriptionAsync(
//     new CreateSubscriptionOptions("firsttopic", "SQLFilterAndActionSubscription"),
//     new CreateRuleOptions
//     {
//         Action = new SqlRuleAction("Set age = age *2;"),
//         Filter = new SqlRuleFilter("name='hero'"),
//         Name = "sqlfilterandaction"
//     });

// var obj = new Customer
// {
//     Age = 32,
//     Name = "hero"
// };

// var sender = client.CreateSender("firsttopic");

// var message = new ServiceBusMessage
// {
//     ApplicationProperties = { { "name", obj.Name }, { "age", obj.Age } }
// };

// await sender.SendMessageAsync(message);

var receiver = client.CreateReceiver("firsttopic", "SQLFilterSubscription",
    new ServiceBusReceiverOptions
    {
        ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
    });

var receivedMessage = await receiver.ReceiveMessageAsync(TimeSpan.FromSeconds(10));

if(receivedMessage is not null){
    foreach(var prop in receivedMessage.ApplicationProperties){
        Console.WriteLine($"{prop.Key} {prop.Value}");
    }
}

Console.ReadLine();

class Customer
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
}