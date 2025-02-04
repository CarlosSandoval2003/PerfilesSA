CREATE DATABASE PERFILES_SA;
GO

USE PERFILES_SA;

-- Tabla para departamentos
CREATE TABLE Departamentos (
    DepartamentoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);

-- Tabla para empleados
CREATE TABLE Empleados (
    EmpleadoID INT PRIMARY KEY IDENTITY(1,1),
    Nombres NVARCHAR(100) NOT NULL,
    DPI NVARCHAR(20) NOT NULL UNIQUE,
    FechaNacimiento DATE NOT NULL,
    Genero NVARCHAR(10) NOT NULL,
    FechaIngreso DATE NOT NULL,
    Edad AS DATEDIFF(YEAR, FechaNacimiento, GETDATE()),
    TiempoLaborado AS DATEDIFF(YEAR, FechaIngreso, GETDATE()),
    Direccion NVARCHAR(250) NULL,
    NIT NVARCHAR(20) NULL,
    DepartamentoID INT FOREIGN KEY REFERENCES Departamentos(DepartamentoID),
    Estado BIT NOT NULL DEFAULT 1
);

INSERT INTO Departamentos (Nombre, Estado)
VALUES ('Recursos Humanos', 1);


