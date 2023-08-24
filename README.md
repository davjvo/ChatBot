# ChatBot

## Description

This is a simple chatbot which uses a simple command based solution to get stock data from this api (https://stooq.com/q/l/).

## Features

- Authentication using IdentityServer
- Chatroom using SignalR on a ReactApp
- Implementation of the command /stock=stock_code to get stock data from the api (https://stooq.com/q/l/)
- Message ordered by timestamp on descending order
- Only get lastest 50 messages on the chatroom
- Unit tests

### Bonus tasks completed

- Use of .NET identity to authenticate users
- Handle messages that are not undestood or any exceptions raised within the bot

## Built with

- .NET 7
- React
- SignalR
- RabbitMQ
- Sql Server 2022
- CsvHelper
- IdentityServer

## Prerequisites
- .NET 7 Runtime
- Microsoft SQL Server
- RabbitMQ
- NPM
## How to use

- Start the RabbitMQ Server And Microsoft SQL Server
- Update the connection string on the appsettings.json across the projects
- Run the SQL query at sql/chatbot.sql
- In this order, do:
  - Go to the DavChatBot.WebApi project folder and run the command `dotnet run`
  - Go to the DavChatBot.Worker project folder and run the command `dotnet run`
  - On the file web-app/utils/constants.ts update the BASE_URL and API_BASE_URL
  - Go to the web-app project folder and run the command `npm run start`
- Go to the url http://localhost:3000
- Create an account and login