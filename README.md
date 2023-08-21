# ChatBot

## Description

This is a simple chatbot which uses a simple command based solution to get stock data from this api (https://stooq.com/q/l/).

## Features

- Authentication using IdentityServer
- Chatroom using SignalR on a ReactApp (Partial)
- Implementation of the command /stock=stock_code to get stock data from the api (https://stooq.com/q/l/) (Partial)
- Message ordered by timestamp on descending order (Pending)
- Only get lastest 50 messages on the chatroom (Pending)
- Unit tests (Pending)

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

## Installation

- Start the RabbitMQ Server And Microsoft SQL Server
- Update the connection string on the appsettings.json across the projects
- Run the SQL query at sql/chatbot.sql
- Go to the DavChatBot.WebApi project folder and run the command `dotnet run`
- Go to the DavChatBot.Worker project folder and run the command `dotnet run`
- Go to the web-app project folder and run the command `npm run start`
- Go to the url http://localhost:7158
- Create an account and login