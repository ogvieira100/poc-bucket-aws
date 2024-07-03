using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Ola
{
    class TestClass
    {
        private const string bucketName = "balde-teste";
        private const string keyName = "arq1.png";
        private const string filePath = "C:\\Users\\ogvieira\\Downloads\\DTB_2022";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private static IAmazonS3 s3Client;
        static async Task Main(string[] args)
        {

            s3Client = new AmazonS3Client(awsAccessKeyId: "xxxxxxx",
                awsSecretAccessKey: "xxxxxxxx", region:RegionEndpoint.USEast1);
            await ReadObjectDataAsync();
        }

        static async Task ReadObjectDataAsync()
        {
            MemoryStream ms = null;
            GetObjectRequest getObjectRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };

            using (var response = await s3Client.GetObjectAsync(getObjectRequest))
            {
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    using (ms = new MemoryStream())
                    {
                        await response.ResponseStream.CopyToAsync(ms);
                        File.WriteAllBytes(Path.Combine(filePath, keyName), ms.ToArray()); 
                    }
                }
            }
        }
    }
}