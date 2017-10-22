using ConsoleQueueStorage.Models.Configs;
using ConsoleQueueStorage.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleQueueStorage
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var azureSettings = new AzureSettings();
            Configuration.GetSection("Azure").Bind(azureSettings);

            var TestResourceSettings = new TestResourceSettings();
            Configuration.GetSection("TestResources").Bind(TestResourceSettings);

            IQueueStorageService queueService = new QueueStorageService(azureSettings.StorageAccountConnectionString);

            queueService.CreateQueueStorageQueue(TestResourceSettings.QueueName);

            string message = "Queue Message: ID - " + DateTime.Now.Ticks.ToString();
            queueService.InsertMessageIntoQueue(TestResourceSettings.QueueName, message);
            queueService.GetMessageFromQueue(TestResourceSettings.QueueName);
            queueService.PeakAtMessageFromtheQueue(TestResourceSettings.QueueName);

            Console.ReadKey();
        }
    }
}
