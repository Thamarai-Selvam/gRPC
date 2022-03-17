using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Image.ImageService;

namespace gRPCServer
{
    internal class ImageServiceImpl : ImageServiceBase
    {
        public string pathString;
        public int PNGfilesInPath;
        public IEnumerable<string> PNGFiles;

        
        public override Task<ImageResponse> GetImage(ImageRequest request, ServerCallContext context)
        {
            Image.Image image = CreateImage(request.FilePath);
            return Task.FromResult(new ImageResponse() { Image = image });
        }


        public override Task<ImageListResponse> GetImageList(ImageListRequest request, ServerCallContext context)
        {
            Image.ImageList imageList = CreateImageList(request.DirectoryPath);
            return Task.FromResult(new ImageListResponse() { ImageList = imageList });
        }

        public void Init(string pathString)
        {
            this.pathString = pathString ?? throw new ArgumentNullException(nameof(pathString));
            this.PNGFiles = getPNGFilesList();
            this.PNGfilesInPath = getFilesCount();
        }

        private IEnumerable<string> getPNGFilesList()
        {
            return Directory.EnumerateFiles(this.pathString, "*.jpg", SearchOption.AllDirectories);
        }

        private int getFilesCount()
        {
            var size = (from file in this.PNGFiles select file).Count();
            Console.WriteLine("List<Image> length : ", size);
            return size;
        }
        private Image.ImageList CreateImageList(string directoryPath)
        {
            Init(directoryPath);

            ImageList images = new ImageList(); ;
            foreach (var file in this.PNGFiles)
            {

                FileInfo fileInfo = new FileInfo(file);

                Image.Image image = null;
                if (fileInfo != null || fileInfo.Length == 0)
                {
                    byte[] imageBlob = GetImageFromPath(fileInfo.FullName);
                    image = new Image.Image
                    {
                        Name = fileInfo.Name,
                        Length = fileInfo.Length,
                        DirectoryName = fileInfo.DirectoryName,
                        IsReadOnly = fileInfo.IsReadOnly,
                        IsExists = fileInfo.Exists,
                        FullName = fileInfo.FullName,
                        Extension = fileInfo.Extension,
                        CreationTimeUtc = Timestamp.FromDateTime(fileInfo.CreationTimeUtc),
                        LastAccessTimeUtc = Timestamp.FromDateTime(fileInfo.LastAccessTimeUtc),
                        LastWriteTimeUtc = Timestamp.FromDateTime(fileInfo.LastWriteTimeUtc),
                        Attributes = (Image.FileAttributes)fileInfo.Attributes
                        //ImageBlob = ByteString.CopyFrom(imageBlob)
                    };
                    images.Image.Add(image);

                    //Console.WriteLine(image.FullName);
                }
            }

            return images;
        }

        private Image.Image CreateImage(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            Image.Image image = null;
            if (fileInfo != null || fileInfo.Length == 0)
            {
               // byte[] imageBlob = GetImageFromPath(fileInfo.FullName);
                image = new Image.Image{Name = fileInfo.Name,
                                    Length = fileInfo.Length,
                                    DirectoryName = fileInfo.DirectoryName,
                                    IsReadOnly = fileInfo.IsReadOnly,
                                    IsExists = fileInfo.Exists,
                                    FullName = fileInfo.FullName,
                                    Extension = fileInfo.Extension,
                                    CreationTimeUtc = Timestamp.FromDateTime(fileInfo.CreationTimeUtc),
                                    LastAccessTimeUtc = Timestamp.FromDateTime(fileInfo.LastAccessTimeUtc),
                                    LastWriteTimeUtc = Timestamp.FromDateTime(fileInfo.LastWriteTimeUtc),
                                    Attributes = (Image.FileAttributes)fileInfo.Attributes,
                                   // ImageBlob = ByteString.CopyFrom(imageBlob)
                };
                
            }
            return image;
        }


        private byte[] GetImageFromPath(string fileName)
        {
            System.Drawing.Image image1 = System.Drawing.Image.FromFile(fileName);
            using (var ms = new MemoryStream())
            {
                image1.Save(ms, image1.RawFormat);
                return ms.ToArray();
            }
        }


    }


}