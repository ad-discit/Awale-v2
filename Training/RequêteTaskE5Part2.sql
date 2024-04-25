CREATE DATABASE HospitalDB;
USE HospitalDB;

-- Doctor table
CREATE TABLE Doctor (
    ID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50)
);

-- Specialization table
CREATE TABLE Specialization (
    ID INT PRIMARY KEY,
    Description VARCHAR(255)
);

-- Patient
CREATE TABLE Patient (
    ID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    InsuranceNumber VARCHAR(50)
);


CREATE TABLE Diagnose (
    DoctorID INT,
    PatientID INT,
    Code VARCHAR(50),
    Details TEXT,
    PRIMARY KEY (DoctorID, PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctor(ID),
    FOREIGN KEY (PatientID) REFERENCES Patient(ID)
);

-- Alter table D to add the FK for Specialization in Doc.
ALTER TABLE Doctor
ADD SpecializationID INT,
ADD FOREIGN KEY (SpecializationID) REFERENCES Specialization(ID);


-- Adding the City
ALTER TABLE Patient
ADD City VARCHAR(100);

-- Change Description to DescriptionEsp in the Specialization table
ALTER TABLE Specialization
CHANGE Description DescriptionEsp VARCHAR(255);

-- Drop the LastName column from the Doctor table
ALTER TABLE Doctor
DROP COLUMN LastName;
