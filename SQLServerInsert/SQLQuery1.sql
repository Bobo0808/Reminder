CREATE DATABASE Todo;
use Todo;

CREATE table Webuser(
UserID int IDENTITY (1,1) NOT NULL,
Username nvarchar(200) NOT NULL,
Email nvarchar(200) NOT NULL,
PWD nvarchar(200) NOT NULL,
Active int NOT NULL,
PRIMARY KEY (UserID)
);
CREATE TABLE Todolist(
TodoID int IDENTITY (1,1) NOT NULL,
UserID int NOT NULL,
Category nvarchar(200) NOT NULL,
Remark text NOT NULL,
CreateDate DATETIME NOT NULL,
ModifyDate DATETIME,
ScaheduledDate DateTime,
IsCompleted BIT,
FOREIGN KEY (UserID) REFERENCES Webuser(UserID),
PRIMARY KEY (TodoID)
);