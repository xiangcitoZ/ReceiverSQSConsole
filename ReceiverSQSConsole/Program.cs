


using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using ReceiverSQSConsole.Models;
using ReceiverSQSConsole.Services;

Console.WriteLine("App Service SQS Receiver");

var serviceProvider = new ServiceCollection()
    .AddAWSService<IAmazonSQS>()
    .AddTransient<ServiceSQS>()
    .BuildServiceProvider() ;

ServiceSQS service = serviceProvider.GetService<ServiceSQS>() ;
List<Mensaje> messages = await service.ReciveMessagesAsync();
if(messages == null)
{
    Console.WriteLine("No existen mensajes en la cola");
}
else
{
    Console.WriteLine("Número de mensajes: " + messages.Count);
    foreach(Mensaje data in messages)
    {
        Console.WriteLine("-------START--------");
        Console.WriteLine("Asunto: " + data.Asunto);
        Console.WriteLine("Email: " + data.Email);
        Console.WriteLine("Contenido: " + data.Contenido);
        Console.WriteLine("Fecha: " + data.Fecha);
        Console.WriteLine("-------END--------");
    }
    Console.WriteLine("Fin de lectura de mensajes");
}
Console.WriteLine("Fin de programa  ");