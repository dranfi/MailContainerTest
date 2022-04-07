namespace MailContainerTest.Types
{
    public class MailContainer
    {
        public int MailContainerNumber { get; set; } 
        public int Capacity { get; set; }   
        public MailContainerStatus Status { get; set; }
        public MailType AllowedMailType { get; set; }

    }
}
