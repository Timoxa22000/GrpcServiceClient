using Grpc.Net.Client;
using GrpcServiceClient;

// создаем канал для обмена сообщениями с сервером
// параметр - адрес сервера gRPC
using var channel = GrpcChannel.ForAddress("https://localhost:7085");
// создаем клиент
var client = new Greeter.GreeterClient(channel);
Console.WriteLine("Игра началась");
Console.WriteLine("\tA\tB\tC");
Console.WriteLine("1\t-\t-\t-");
Console.WriteLine("2\t-\t-\t-");
Console.WriteLine("3\t-\t-\t-");
bool user = true;
bool isRun = false;
while(!isRun)
{
    string hodUser = "Ходит игрок ";
    if (user)
    {
        user = false;
        Console.Write(hodUser + "1: ");
    }
    else
    {
        user = true;
        Console.Write(hodUser + "2: ");
    }
    var item = Console.ReadLine();
    // обмениваемся сообщениями с сервером
    var reply = await client.SayHelloAsync(new HelloRequest { Name = item });
    if (reply.Message[0] == '!')
    {
        user = user == false;
    }
    Console.WriteLine($"{reply.Message}");
    isRun = reply.Message.IndexOf("Игра окончена!") > 0;
    Console.WriteLine(string.Format("Победил игрок: {0}", user ? "1" : "0"));
}
Console.ReadKey();