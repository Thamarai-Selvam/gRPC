using Grpc.Core;
using Image;
using System;
using Google.Protobuf;
using System.Threading.Tasks;
using System.IO;
using Grpc.Net.Client;
using System.Collections.Generic;

namespace gRPCClient
{
    class Program
    {
        const string target = "127.0.0.1:50051";
        static ImageService.ImageServiceClient client = null;
        static void Main(string[] args)
        {
            

            Channel channel = new Channel(target, 
                ChannelCredentials.Insecure,
                new List<ChannelOption> { new ChannelOption(ChannelOptions.MaxReceiveMessageLength, 1000 * 1024 * 1024),
                new ChannelOption(ChannelOptions.MaxSendMessageLength, 1000 * 1024 * 1024)});

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The Client connected successfully");
            });

            client = new ImageService.ImageServiceClient(channel);

            using (var bench = new Benchmark($"gRPCClient|gRPC :: Get Single Image"))
            {
                GetSingleImage();
            }

            using (var bench = new Benchmark($"gRPCClient|gRPC :: Get ImageList"))
            {
                GetImageList();
            }
            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        public static void GetSingleImage()
        {
            
            var ImageRequest = new ImageRequest()
            {
                FilePath = @"D:\Thamarai\Research\Downloads\TestImages\Img-0.jpg"
            };

            Image.ImageResponse response = client.GetImage(ImageRequest);

            Console.WriteLine(response.Image.DirectoryName);
            using (var output = File.Create(@"D:\Thamarai\Research\gRPC\Learnings\gRPCDataTransfer\" + response.Image.Name + ".dat"))
            {
                response.WriteTo(output);
            }
        }

        public static void GetImageList()
        {
            var ImageListRequest = new ImageListRequest()
            {
                DirectoryPath = @"D:\Thamarai\Research\Downloads\TestImages\"
            };

            Image.ImageListResponse response = client.GetImageList(ImageListRequest);

            using (var output = File.Create(@"D:\Thamarai\Research\gRPC\Learnings\gRPCDataTransfer\OutputDB.dat"))
            {
                response.WriteTo(output);
            }
            Console.WriteLine("Response Written to file");

        }
    }
}
