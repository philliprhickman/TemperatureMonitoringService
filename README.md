# Temperature Monitoring Service

## Introduction
This project connects to an Arduino UNO R3 microcontroller board to read temperature and humidity data. The Worker Service sets up polling and polls the data every 5 minutes by sending a request to the COM port the UNO is attached to. This data is then stored in a database table for later retrieval via a report or Website for visualization.

### Why I created this project
I created this project as a way of testing .NET 5 Worker Services, active logging using the Serilog library and serial port communications. I use the following technologies:
- Visual Studio 2019 
- .NET 5
- Visual Micro Arudino IDE for Visual Studio
- SQL Server Express

### Key takeaways
- How to program an Arduino UNO R3 using the Visual Micro Arduino IDE in Visual Studio 2019.
- How to use Serilog for structured logging.
- How to use transient services in .NET 5.
- General introduction to .NET 5 SDK.
