CREATE TABLE Users (
      UserId int IDENTITY(0,1) NOT NULL,
      Username varchar(100) NOT NULL,
	Password varchar(40) NOT NULL,
	Fullname varchar(100),
	DateCreated datetime NOT NULL,
	PRIMARY KEY(UserId)
);

CREATE TABLE Rooms (
	RoomId int IDENTITY(0,1) NOT NULL,
	Name varchar(20) NOT NULL,
	Emblem varchar(100),
	DateCreated datetime NOT NULL,
	PRIMARY KEY(RoomId)
)

CREATE TABLE RoomUsers (
	RoomUserId int IDENTITY(0,1) NOT NULL,
	RoomId int NOT NULL FOREIGN KEY REFERENCES Rooms(RoomId),
	UserId int NOT NULL FOREIGN KEY REFERENCES Users(UserId)
)

