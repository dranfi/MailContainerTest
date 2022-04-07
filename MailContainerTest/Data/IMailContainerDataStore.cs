using MailContainerTest.Types;

namespace MailContainerTest.Data
{
    public interface IMailContainerDataStore
    {
        MailContainer GetMailContainer(int mailContainerNumber);
        void UpdateMailContainer(MailContainer mailContainer);
    }
}
