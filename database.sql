CREATE TABLE Users (
      Id int IDENTITY(0,1) NOT NULL,
      Username varchar(100) NOT NULL,
	Password varchar(40) NOT NULL,
	Fullname varchar(100),
	DateCreated datetime NOT NULL
);