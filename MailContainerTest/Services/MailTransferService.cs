using MailContainerTest.Data;
using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        public MailContainer? SourceMailContainer { get; set; }
        public MailContainer? DestinationMailContainer { get; set; }
        private string _dataStoreType = null;
        private BackupMailContainerDataStore? _backupMailContainerDataStore;
        private MailContainerDataStore? _mailContainerDataStore;
        public BackupMailContainerDataStore BackupMailContainerDataStore
        {
            get
            {
                if (_backupMailContainerDataStore == null)
                {
                    return new BackupMailContainerDataStore();
                }
                else
                {
                    return _backupMailContainerDataStore;
                }
            }
            set { _backupMailContainerDataStore = value; }
        }
        public MailContainerDataStore MailContainerDataStore
        {
            get
            {
                if (_mailContainerDataStore == null)
                {
                    return new MailContainerDataStore();
                }
                else
                {
                    return _mailContainerDataStore;
                }
            }
            set { _mailContainerDataStore = value; }
        }
        public string DataStoreType
        {
            get
            {
                if (_dataStoreType == null)
                {
                    return ConfigurationManager.AppSettings["DataStoreType"];
                }
                else
                {
                    return _dataStoreType;
                }
            }
            set { _dataStoreType = value; }
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            var dataStoreType = DataStoreType; //ConfigurationManager.AppSettings["DataStoreType"];

            IMailContainerDataStore mailContainerDataStore;

            if (dataStoreType == "Backup")
            {
                mailContainerDataStore = BackupMailContainerDataStore;

            } else
            {
                mailContainerDataStore = MailContainerDataStore;
            }

            SourceMailContainer = mailContainerDataStore.GetMailContainer(request.SourceMailContainerNumber);
            DestinationMailContainer = mailContainerDataStore.GetMailContainer(request.DestinationMailContainerNumber);

            var result = new MakeMailTransferResult();

            if(request.MailType == SourceMailContainer.AllowedMailType && request.MailType == DestinationMailContainer.AllowedMailType 
                && SourceMailContainer.Status == MailContainerStatus.Operational && DestinationMailContainer.Status == MailContainerStatus.Operational 
                && SourceMailContainer.Capacity >= request.NumberOfMailItems )
            {
                SourceMailContainer.Capacity -= request.NumberOfMailItems;
                DestinationMailContainer.Capacity += request.NumberOfMailItems;
                mailContainerDataStore.UpdateMailContainer(SourceMailContainer);
                mailContainerDataStore.UpdateMailContainer(DestinationMailContainer);
                result.Success = true;
            }
            else
            {
                result.Success = false;
            }

            return result;
        }
    }
}
