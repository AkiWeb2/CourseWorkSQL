

--create database TestBD;
use TestBD;



create table sclads(
id int IDENTITY(1,1) NOT NULL,
type_off varchar(50) NOT NULL,
count_off int NOT NULL,
postavka varchar(50) NOT NULL,
sclad varchar(50) NOT NULL,
PRIMARY KEY (id)
);


--create table register(
--idUser int IDENTITY(1,1) NOT NULL,
--loginUser varchar(50) NOT NULL,
--passwordUser varchar(50) NOT NULL,

--PRIMARY KEY (idUser) 
--);

--insert into register(loginUser, passwordUser)values('Admin', '12125690');
--insert into register(loginUser, passwordUser)values('Bugalteria', '1212');
--insert into register(loginUser, passwordUser)values('Otpravka', '5690');



--create table WorkPepl(
--id int IDENTITY(1,1) NOT NULL,
--Names varchar(50) NOT NULL,
--Fil varchar(50) NOT NULL,
--Otl varchar(50) NOT NULL,
--job varchar(50) NOT NULL,
--NumPhon varchar(50) NOT NULL,
--Hous varchar(50) NOT NULL,
--PRIMARY KEY (id)
--);

--create table Prise(
--id int IDENTITY(1,1) NOT NULL,
--idUser int NOT NULL,
--prise int NOT NULL,
--Prem int NOT NULL,
--PRIMARY KEY (id),
--FOREIGN KEY (idUser) REFERENCES WorkPepl (id)
--ON DELETE NO ACTION
--ON UPDATE NO ACTION
--);
------drop table Otgruska
--create table Otgruska(
--id int IDENTITY(1,1) NOT NULL,
--Names varchar(50) NOT NULL,
--Counts int NOT NULL,
--otdel varchar(50) NOT NULL,
--zavod varchar(50) NOT NULL,
--car varchar(50) NOT NULL,
--PRIMARY KEY (id),

--);

--create table Otpravka(
--id int IDENTITY(1,1) NOT NULL,
--Names varchar(50) NOT NULL,
--Counts int NOT NULL,
--otdel varchar(50) NOT NULL,
--car varchar(50) NOT NULL,
--post varchar(50) NOT NULL,
--PRIMARY KEY (id),
--);
--drop table Otpravka
