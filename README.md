### Mail Container Test 

The code for this exercise has been developed to manage the transfer of mail items from one container to another for processing.

#### Process for transferring mail

- Lookup the container the mail is being transferred from.
- Check the containers are in a valid state for the transfer to take place.
- Reduce the container capacity on the source container and increase the destination container capacity by the same amount.

#### Restrictions

- A container can only hold one type of mail.


#### Assumptions

- For the sake of simplicity, we can assume the containers have an unlimited capacity.

### The exercise brief

The exercise is to take the code in the solution and refactor it into a more suitable approach with the following things in mind:

- Testability
- Readability
- SOLID principles
- Architectural design of the code

You should not change the method signature of the MakeMailTransfer method.

You should add suitable tests into the MailContainerTest.Test project.

There are no additional constraints, use the packages and approach you feel appropriate, aim to spend no more than 2 hours. Please update the readme with specific comments on any areas that are unfinished and what you would cover given more time.

### Results
Given more time I would have created an app.config for the datastore and implemented example of IMailContainerDataStore.GetMailContainer(int mailContainerNumber) and IMailContainerDataStore.UpdateMailContainer(MailContainer mailContainerNumber) by reading/writing to a local json file.
Given more time I would have installed .NET 6 on my machine to deploy the solution in .NET6 rather than .NET5
I would add more test cases of MailTransferService.
I would have prefer to Mock DataStoreType, MailContainerDataStore and BackupMailContainerDataStore instead of setting them directly but they would need to be virtual.
Given the requirement ask for 1 allowed type, I simplified the solution by removing AlloweType and just using MailType allowing only 1 type to be set rather than a bitewise setting of the Enum.