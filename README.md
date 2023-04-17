# RJP-Bank
RJP Bank Application

<h1> Introdcution: </h1>

  <p>The aim of this assessment is to build an API for opening a new "OpenAccount" for existing customers. The API will have two endpoints - one for creating a new account and one for retrieving user information including account balance and transaction history. </p>

  <p>When the create account endpoint is called with the required information (customerID and initialCredit), a new account will be opened and linked to the customer with the specified ID. If an initial credit amount is provided, a transaction will also be sent to the new account.</p>

  <p>The retrieve user information endpoint will output the user's name, surname, account balance, and transaction history for all accounts associated with the user.</p>

  <p>To achieve these requirements, we have developed a microservices-based solution using C# and .NET Core. The solution consists of two microservices - the AccountMicroservice and the TransactionMicroservice - which are fully independent and communicate through the RabbitMQ messaging system. The presentation layer of the solution uses an AngularJS application to interact with the microservices and provide a user-friendly interface for creating accounts and viewing user information.

In the following sections, we will provide detailed instructions on how to install and use the solution, including how to update configuration settings and run the various components of the solution. </p>


<h2> Instructions:  </h2>

There are many ways to connect microservices together(REST API, Message Queue,...), but we will be using RabbitMQ for this project.

<b> 1- Download solution: </b> 
  <p> Clone and download the solution from the github. https://github.com/AHusseinA/Bank.git </p>
  
<b> 2-  Installing RabbitMQ: </b>
   <p>Before using the application, you need to install RabbitMQ on your machine. You can download and install RabbitMQ from the official website.</p>
   To install Rabbit MQ Server:

          . Go to rabbitmq official website https://www.rabbitmq.com/
          . Click on "Get Started" menu
          . Then click on "Download + installation"
          . Then click on windows installer recommended 
          . Then download erlang 64 bit
          . Then download the rabbitmq .exe file
          . First install erlang 
          . Then install rabbitmq
          . Then go to start menu and search for rabbitmq command prompt
          . Type command "rabbitmq-plugins enable rabbitmq_management"

            All set to go now go to http://localhost:15672
                username: guest
                passowrd: guest 
               
<b> 3- Updating appSettings.json: </b>
  <p> We have created two separate microservices for this project <b> AccountMicroservice </b> and <b> TransactionMicroservice </b>. Each microservice has its own model context and is   fully independent. They communicate with each other using RabbitMQ messages to create accounts and transactions. </p>
  
  <p> In order to use the application, you need to update some global variables in the appSettings.json file. These include the connectionStrings, RabbitMQ configuration, and baseURL. You should update these variables based on your own machine configuration. </p>
  
        "RabbitMQ": {
          "HostName": "localhost",
          "UserName": "guest",
          "Password": "guest",
        },
        "ConnectionStrings": {
          "DefaultConnection": "Data Source=*******;Initial Catalog=RabitMQDemo;User Id=**;Password=***;MultipleActiveResultSets=True;"
        },
        "Urls": {
          "TransactionMicroService": "https://localhost:44371/Transaction"
        },
        
<b> 4- Presentation layer: </b>
<p> We have created an AngularJS application that communicates with the microservices to create accounts and get transactions. To use the application, you need to update the base URLs in the <b> configuration.js </b> file <b> (BankApp\wwwroot\app\configuration.js) </b>. </p>

<b> 5- Running the application: </b>
<p> To run the application, you should make the three projects - AccountMicroservice, TransactionMicroservice, and BankApp - as startup. This will start the microservices and the presentation layer, allowing you to use the application. </p>

<b>6- Database: </b>
<p> find attached database script, file "RJP DB Script.sql" </p>
