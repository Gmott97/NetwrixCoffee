CREATE DATABASE CoffeeMachine;

CREATE TABLE CoffeeMachineRecord (
  CoffeeMachineRecordId INT IDENTITY (1,1) NOT NULL,
  NumEspressoShots INT NOT NULL,
  AddMilk BIT NOT NULL,
  CreatedDate DATETIME NOT NULL,
  DayOfWeek INT NOT NULL
);

CREATE NONCLUSTERED INDEX IX_CoffeeMachineRecord_DayOfWeek
  ON CoffeeMachineRecord (DayOfWeek ASC);