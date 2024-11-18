CREATE DATABASE CourseWorkBuses;
GO

USE CourseWorkBuses;
GO

CREATE TABLE Buses (
    BusID INT PRIMARY KEY IDENTITY(1,1),
    BusBrand NVARCHAR(100) NOT NULL,
    BusModel NVARCHAR(100) NOT NULL,
    BusYearOfManufacture INT NOT NULL,
    BusLicensePlate NVARCHAR(20) NOT NULL,
    BusCapacity INT NOT NULL,
    BusStatus NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Workers (
    WorkerID INT PRIMARY KEY IDENTITY(1,1),
    WorkerLastName NVARCHAR(100) NOT NULL,
    WorkerFirstName NVARCHAR(100) NOT NULL,
    WorkerMiddleName NVARCHAR(100),
    WorkerPosition NVARCHAR(100) NOT NULL,
    WorkerContacts NVARCHAR(255) NOT NULL,
    WorkerHireDate DATE NOT NULL
);
GO

CREATE TABLE Flights (
    FlightID INT PRIMARY KEY IDENTITY(1,1),
    BusID INT NOT NULL FOREIGN KEY REFERENCES Buses(BusID),
    DeparturePoint NVARCHAR(100) NOT NULL,
    DepartureDate DATE NOT NULL,
    DepartureTime TIME NOT NULL,
    ArrivalPoint NVARCHAR(100) NOT NULL,
    ArrivalDate DATE NOT NULL,
    ArrivalTime TIME NOT NULL
);
GO

CREATE TABLE Tickets (
    TicketID INT PRIMARY KEY IDENTITY(1,1),
    FlightID INT NOT NULL FOREIGN KEY REFERENCES Flights(FlightID),
    TicketType NVARCHAR(50) NOT NULL,
    TicketPrice DECIMAL(10, 2) NOT NULL,
    TicketStatus NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE Clients (
    ClientID INT PRIMARY KEY IDENTITY(1,1),
    ClientLastName NVARCHAR(100) NOT NULL,
    ClientFirstName NVARCHAR(100) NOT NULL,
    ClientMiddleName NVARCHAR(100),
    ClientContacts NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE Repair (
    RepairID INT PRIMARY KEY IDENTITY(1,1),
    BusID INT NOT NULL FOREIGN KEY REFERENCES Buses(BusID),
    RepairStatus NVARCHAR(100) NOT NULL,
    RepairNotes NVARCHAR(MAX) NOT NULL
);
GO
