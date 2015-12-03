CREATE TABLE ExaminationTypes (
	id INT PRIMARY KEY IDENTITY NOT FOR REPLICATION,
	name VARCHAR(100) NOT NULL,
	"description" VARCHAR(500) NOT NULL
);

CREATE TABLE ExaminationPrices (
	id INT PRIMARY KEY IDENTITY NOT FOR REPLICATION,
	insurance INT NOT NULL REFERENCES Health_insurance,
	price DECIMAL NOT NULL
);


CREATE TABLE Examination (
	id INT PRIMARY KEY IDENTITY NOT FOR REPLICATION,
	examined DATETIME NOT NULL,
	diagnosis VARCHAR(500) NOT NULL,
	"type" INT NOT NULL,
	paid CHAR NOT NULL,
	person_id CHAR(10) NOT NULL REFERENCES Patient
	);