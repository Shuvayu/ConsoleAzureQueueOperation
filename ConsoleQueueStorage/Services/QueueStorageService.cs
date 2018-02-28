using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleQueueStorage.Services
{
    public class QueueStorageService : IQueueStorageService
    {
        private string _connectionString;
        private CloudStorageAccount _storageAccount;
        private CloudQueueClient _queueClient;

        public QueueStorageService(string connectionString)
        {
            _connectionString = connectionString;
            _storageAccount = CloudStorageAccount.Parse(_connectionString);
            _queueClient = _storageAccount.CreateCloudQueueClient();
        }

        public void CreateQueueStorageQueue(string queueName)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            queue.CreateIfNotExistsAsync().GetAwaiter().GetResult();
            Console.WriteLine("Queue {0} has been created !!!", queueName);
        }

        public void GetMessageFromQueue(string queueName)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            CloudQueueMessage queueMessage = queue.GetMessageAsync().Result;
            Console.WriteLine("Get Message : {0}", queueMessage.AsString);
        }

        public void InsertMessageIntoQueue(string queueName, string message)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            queue.AddMessageAsync(queueMessage).GetAwaiter().GetResult();
            Console.WriteLine("Message has been added to the Queue.");
        }

        public void PeakAtMessageFromtheQueue(string queueName)
        {
            CloudQueue queue = _queueClient.GetQueueReference(queueName);
            CloudQueueMessage queueMessage = queue.PeekMessageAsync().GetAwaiter().GetResult();
            Console.WriteLine("Peak Message : {0}", queueMessage.AsString);
        }
    }
}
