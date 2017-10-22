namespace ConsoleQueueStorage.Services
{
    public interface IQueueStorageService
    {
        void CreateQueueStorageQueue(string queueName);
        void GetMessageFromQueue(string queueName);
        void InsertMessageIntoQueue(string queueName, string message);
        void PeakAtMessageFromtheQueue(string queueName);
    }
}
