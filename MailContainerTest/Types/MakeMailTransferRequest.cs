using System;

namespace MailContainerTest.Types
{
    public class MakeMailTransferRequest
    {
        public MakeMailTransferRequest(int sourceContainterNumber, int destinationContainerNumber, int numberOfMailItems, DateTime transferDate, MailType mailType)
        {
            SourceMailContainerNumber = sourceContainterNumber;
            DestinationMailContainerNumber = destinationContainerNumber;
            NumberOfMailItems = numberOfMailItems;
            TransferDate = transferDate;
            MailType = mailType;
        }
        public int SourceMailContainerNumber { get; set; }   
        public int DestinationMailContainerNumber { get; set; }
        public int NumberOfMailItems { get; set; }
        public DateTime TransferDate { get; set; }   
        public MailType MailType { get; set; }  
    }
}
