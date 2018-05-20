# Moneybox Money Withdrawal

## My comments

### Account class
* I included constants for the values that trigger notifications, this way we remove hardcoded values. In a real application, I believe these values would be loaded from app settings.
* The protected constructor and set methods aim to allow persistence/serialization to work fine and to keep the account values read-only for other non-inherited classes. This way, only Account class methods can modify theses properties and so enforce their correct usage.

### TransferMoney class
* Refactored to comply with new Account class design. The result is a simpler code.
* Notification logic was kept inside this class to avoid calling NotificationService from Account. I tried to keep the minimum change possible but in a full app maybe notification could be invoked by Account changes (through an observer pattern, maybe?)
* As this class use two operation from different accounts, it still lacks some logic to keep data consistent in the case when the repository successfully updates *source* (from) account but fails to update *target* (to) account. Would be a nice idea to implement a rollback operation in *source* if an exception is thrown when updating *target*. Once again, to keep changes minimum and for not trying to "predict requirements", I've left it as it is.

### WithdrawMoney class
No great news here. an simpler "one-way" version of TransferMoney class.

## Description

The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:

* Domain - this contains the domain models for a user and an account, and a notification service.
* Features - this contains two operations, one which is implemented (transfer money) and another which isn't (withdraw money)
* DataAccess - this contains a repository for retrieving and saving an account (and the nested user it belongs to)

## The task

The task is to implement a money withdrawal in the WithdrawMoney.Execute(...) method in the features folder. For consistency, the logic should be the same as the TransferMoney.Execute(...) method i.e. notifications for low funds and exceptions where the operation is not possible. 

As part of this process however, you should look to refactor some of the code in the TransferMoney.Execute(...) method into the domain models, and make these models less susceptible to misuse. We're looking to make our domain models rich in behaviour and much more than just plain old objects, however we don't want any data persistance operations (i.e. data access repositories) to bleed into our domain. This should simplify the task of implementing WidthdrawMoney.Execute(...).

## Guidlines

* You should spend no more than 1 hour on this task, although there is no time limit
* You should fork or copy this repository into your own public repository (Gihub, BitBucket etc.) before you do your work
* Your solution must compile and run first time
* You should not alter the notification service or the the account repository interfaces
* You may add unit/integration tests using a test framework (and/or mocking framework) of your choice
* You may edit this README.md if you want to give more details around your work (e.g. why you have done something a particular way, or anything else you would look to do but didn't have time)

Once you have completed your work, send us a link to your public repository.

Good luck!
