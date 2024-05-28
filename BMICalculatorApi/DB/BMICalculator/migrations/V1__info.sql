CREATE TABLE BMIRecords (
                            Id INT PRIMARY KEY IDENTITY(1,1),
                            Name VARCHAR(255) NOT NULL,
                            Height FLOAT NOT NULL,
                            Weight FLOAT NOT NULL,
                            Bmi FLOAT NOT NULL,
                            RecordedDate DATETIME NOT NULL
);
