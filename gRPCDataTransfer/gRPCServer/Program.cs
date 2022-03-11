using Grpc.Core;
using Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPCServer
{
    class Program
    {
        const int port = 50051;
        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server(new List<ChannelOption> { new ChannelOption(ChannelOptions.MaxReceiveMessageLength, 500 * 1024 * 1024),
                new ChannelOption(ChannelOptions.MaxSendMessageLength, 500 * 1024 * 1024)})
                {
                    Services = { ImageService.BindService(new ImageServiceImpl()) },
                    Ports = {new ServerPort("localhost", port, ServerCredentials.Insecure)}
                };

                server.Start();
                Console.WriteLine("gRPCServer :: Server is listening on port : " + port);
                Console.ReadKey();

            }
            catch(IOException e)
            {
                Console.WriteLine("gRPCServer :: Server Failed to Start!! : ", e.Message);
                throw;

            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }

    }
}
