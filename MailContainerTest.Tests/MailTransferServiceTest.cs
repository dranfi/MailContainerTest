using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using Moq;

namespace MailContainerTest.Tests
{
    [TestClass]
    public class MailTransferServiceTest
    {
        [TestMethod]
        public void SuccessTest()
        {
            Mock<MailTransferService> mts = new Mock<MailTransferService>();
            mts.Object.DataStoreType = "live";
            Mock<MailContainerDataStore> mcds = new Mock<MailContainerDataStore>();
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 1)))
                .Returns(new MailContainer() { AllowedMailType = MailType.LargeLetter, Capacity = 5, MailContainerNumber = 1, Status = MailContainerStatus.Operational });
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 2)))
                .Returns(new MailContainer() { AllowedMailType = MailType.LargeLetter, Capacity = 5, MailContainerNumber = 2, Status = MailContainerStatus.Operational });
            mts.Object.MailContainerDataStore = mcds.Object;
            var request = new MakeMailTransferRequest(1, 2, 3, DateTime.Now, MailType.LargeLetter);
            var result = mts.Object.MakeMailTransfer(request);
            Assert.AreEqual(true, result.Success);
            Assert.AreEqual(2, mts.Object.SourceMailContainer.Capacity);
            Assert.AreEqual(8, mts.Object.DestinationMailContainer.Capacity);
        }
        [TestMethod]
        public void DestinationTypeTest()
        {
            Mock<MailTransferService> mts = new Mock<MailTransferService>();
            mts.Object.DataStoreType = "live";
            Mock<MailContainerDataStore> mcds = new Mock<MailContainerDataStore>();
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 1)))
                .Returns(new MailContainer() { AllowedMailType = MailType.LargeLetter, Capacity = 5, MailContainerNumber = 1, Status = MailContainerStatus.Operational });
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 2)))
                .Returns(new MailContainer() { AllowedMailType = MailType.StandardLetter, Capacity = 5, MailContainerNumber = 2, Status = MailContainerStatus.Operational });
            mts.Object.MailContainerDataStore = mcds.Object;
            var request = new MakeMailTransferRequest(1, 2, 3, DateTime.Now, MailType.LargeLetter);
            var result = mts.Object.MakeMailTransfer(request);
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void WrongSourceTypeTest()
        {
            Mock<MailTransferService> mts = new Mock<MailTransferService>();
            mts.Object.DataStoreType = "live";
            Mock<MailContainerDataStore> mcds = new Mock<MailContainerDataStore>();
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 1)))
                .Returns(new MailContainer() { AllowedMailType = MailType.SmallParcel, Capacity = 5, MailContainerNumber = 1, Status = MailContainerStatus.Operational });
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 2)))
                .Returns(new MailContainer() { AllowedMailType = MailType.LargeLetter, Capacity = 5, MailContainerNumber = 2, Status = MailContainerStatus.Operational });
            mts.Object.MailContainerDataStore = mcds.Object;
            var request = new MakeMailTransferRequest(1, 2, 3, DateTime.Now, MailType.LargeLetter);
            var result = mts.Object.MakeMailTransfer(request);
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void WrongStatusTest()
        {
            Mock<MailTransferService> mts = new Mock<MailTransferService>();
            mts.Object.DataStoreType = "live";
            Mock<MailContainerDataStore> mcds = new Mock<MailContainerDataStore>();
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 1)))
                .Returns(new MailContainer() { AllowedMailType = MailType.LargeLetter, Capacity = 5, MailContainerNumber = 1, Status = MailContainerStatus.OutOfService });
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 2)))
                .Returns(new MailContainer() { AllowedMailType = MailType.StandardLetter, Capacity = 5, MailContainerNumber = 2, Status = MailContainerStatus.NoTransfersIn });
            mts.Object.MailContainerDataStore = mcds.Object;
            var request = new MakeMailTransferRequest(1, 2, 3, DateTime.Now, MailType.LargeLetter);
            var result = mts.Object.MakeMailTransfer(request);
            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public void NotEnoughMailToTransferTest()
        {
            Mock<MailTransferService> mts = new Mock<MailTransferService>();
            mts.Object.DataStoreType = "live";
            Mock<MailContainerDataStore> mcds = new Mock<MailContainerDataStore>();
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 1)))
                .Returns(new MailContainer() { AllowedMailType = MailType.LargeLetter, Capacity = 5, MailContainerNumber = 1, Status = MailContainerStatus.Operational });
            mcds.Setup(x => x.GetMailContainer(It.Is<int>(c => c == 2)))
                .Returns(new MailContainer() { AllowedMailType = MailType.StandardLetter, Capacity = 5, MailContainerNumber = 2, Status = MailContainerStatus.Operational });
            mts.Object.MailContainerDataStore = mcds.Object;
            var request = new MakeMailTransferRequest(1, 2, 10, DateTime.Now, MailType.LargeLetter);
            var result = mts.Object.MakeMailTransfer(request);
            Assert.AreEqual(false, result.Success);
        }
    }
}
