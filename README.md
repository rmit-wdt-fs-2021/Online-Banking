Second assignment for Web development technologies flexible semester.

# What I learned 
1. Creating a asp.net web api using the repository pattern.
2. Creating background services in web apps. The billpay background service is responsible for triggering scheduled payments.
3. Dependency Injection using MS DI.
4. How to build secure web apps with authentication (custom and then with Identity Api) and use filters to prevent attacks like anti-forgery.
5. How to implement pagination.

To start:

Internetbanking App: Open in Visual studio and debug, logins are the same as A1, however there are more registered logins in the database, password is abc123 for most of them.

Admin API: Go to InternetBanking/AdminApi/AdminApi and dotnet build, dotnet run

Admin App, start up in Visual studio, admin api needs to be running, username and password is admin



Identity has been implemented and new users can be registered, these new users will start off with a savings account.

Billpay works correctly, if an account doesn't have enough money the transaction will be rejected, errors will be displayed in stack trace

