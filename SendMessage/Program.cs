// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Transactions;
using Azure.Messaging.ServiceBus;

Console.WriteLine("Hello, World!");

// // Create Connection
// await using var client = new ServiceBusClient("Endpoint=sb://servicebus-queue-2.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=60t0NJn7cAKV7gzqRniXTvYBWyIkT2agG+ASbHItYPM=");

// // Create Sender
// ServiceBusSender sender = client.CreateSender("firstqueue");

// Send Message
// await sender.SendMessageAsync(new ServiceBusMessage("hello Mr Nicolas - peek message"));

// Create Receiver
// ServiceBusReceiver receiver = client.CreateReceiver("firstqueue", new ServiceBusReceiverOptions
// {
//     ReceiveMode = ServiceBusReceiveMode.PeekLock
// });

// ServiceBusReceivedMessage message  = await receiver.PeekMessageAsync();
// var message = await receiver.PeekMessagesAsync(maxMessages: 2);

// foreach (ServiceBusReceivedMessage msg in message)
// {
//     Console.WriteLine(msg.Body.ToString());
// }

// if (message != null)
// {
//     Console.WriteLine(message.Body.ToString());
// }

// //Receive the message
// ServiceBusReceivedMessage message = await receiver.ReceiveMessageAsync();


// //Sending message to Dead Letter Queue
// await receiver.DeadLetterMessageAsync(message, "reason","description*");

// //Receive the message Dead Letter Queue
// ServiceBusReceiver dlqRec = client.CreateReceiver("firstqueue", new ServiceBusReceiverOptions{
//     SubQueue = SubQueue.DeadLetter
// });

// ServiceBusReceivedMessage msg = await dlqRec.ReceiveMessageAsync();

// receiver.AbandonMessageAsync(message);
// receiver.DeferMessageAsync(message);

// Read the deffer
// ServiceBusReceivedMessage defMessage = await receiver.ReceiveDeferredMessageAsync(message.SequenceNumber);

// Console.WriteLine(msg.Body.ToString());

var options = new ServiceBusClientOptions{
    EnableCrossEntityTransactions = true
};

//Connection
await using var client = new ServiceBusClient("Endpoint=sb://servicebus-queue-2.servicebus.windows.net/;SharedAccessKeyName=ConnectionString;SharedAccessKey=1cLLWBGCyFuGFYrF7NnVaOevQ6jZ9hV7G+ASbJ86z9M=;EntityPath=firstqueue");

// //Create Sender
// ServiceBusSender sender = client.CreateSender("firstqueue");

// //Send message
// await sender.SendMessageAsync(new ServiceBusMessage("Out Side of Transactions"));


// //Create processor
// await using ServiceBusProcessor serviceBusProcessor = client.CreateProcessor("firstqueue", new ServiceBusProcessorOptions
// {
//     AutoCompleteMessages = true,
//     MaxConcurrentCalls = 1
// });

// //Configure message
// serviceBusProcessor.ProcessMessageAsync += MessageHandler;
// serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

// //Configure handler
// async Task MessageHandler(ProcessMessageEventArgs processMessageEventArgs)
// {
//     //Process your message
//     Console.WriteLine(processMessageEventArgs.Message.Body.ToString());
// }

// //Configure error
// async Task ErrorHandler(ProcessErrorEventArgs processErrorEventArgs){
//     Console.WriteLine(processErrorEventArgs.ErrorSource);

//     await Task.CompletedTask;
// }

// //Start processing
// await serviceBusProcessor.StartProcessingAsync();

// ServiceBusReceiver receiver = client.CreateReceiver("firstqueue");

// // for (var i = 0; i < 4; i++)
// // {
// //     ServiceBusReceivedMessage recmsg = await receiver.ReceiveMessageAsync();
// // }


// ServiceBusReceivedMessage recvMsg = await receiver.ReceiveMessageAsync();

// using(var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)){
//     await sender.SendMessageAsync(new ServiceBusMessage("Exception in the Transaction"));
//     await receiver.CompleteMessageAsync(recvMsg);

//     // throw new Exception("Invalid operation");

//     var a = 0;
//     var b=1;
//     var c = b/a;

//     ts.Complete();
// }

ServiceBusReceiver receiver = client.CreateReceiver("firstqueue", new ServiceBusReceiverOptions{
    ReceiveMode = ServiceBusReceiveMode.PeekLock
});

ServiceBusReceivedMessage firstMessage = await receiver.ReceiveMessageAsync();

Console.WriteLine(firstMessage.Body.ToString());
await receiver.CompleteMessageAsync(firstMessage);

Console.ReadLine();