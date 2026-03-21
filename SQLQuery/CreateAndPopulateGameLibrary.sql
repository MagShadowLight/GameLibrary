-- Create and Connect GameLibrary database

CREATE DATABASE GameLibrary
GO

USE GameLibrary
GO

-- Create the tables --

CREATE TABLE Games (
	GameId int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Title NVARCHAR(MAX) NOT NULL,
	Developer NVARCHAR(MAX) NOT NULL,
	Publisher NVARCHAR(MAX) NOT NULL,
	ReleaseDate DATETIME2(7) NOT NULL,
	Genre NVARCHAR(MAX) NOT NULL,
	Prices DECIMAL(18,2) NOT NULL
);
GO

CREATE TABLE Users (
	UserId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserName NVARCHAR(MAX) NOT NULL,
	DateofBirth DATETIME2(7) NOT NULL,
	Password NVARCHAR(MAX) NOT NULL,
	Region NVARCHAR(MAX) NOT NULL,
	Bios NVARCHAR(MAX) NOT NULL,
	DateCreated DATETIME2(7) NOT NULL,
	Email NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE Collections (
	CollectionId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserId INT NOT NULL,
	GameId INT NOT NULL,
	DateLastPlayed DATETIME2(7) NOT NULL,
	TimesPlayed INT NOT NULL,
	CONSTRAINT fk_Collections_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
	CONSTRAINT fk_Collections_Games FOREIGN KEY (GameId) REFERENCES Games(GameId)
)
GO

-- Insert the data into a table --

-- Games --

INSERT INTO Games (
	Title,
	Developer,
	Publisher,
	ReleaseDate,
	Genre,
	Prices
)
VALUES (
	'Game 1',
	'Developer 1',
	'Publisher 1',
	'2026-01-05',
	'Action-Adventure',
	19.99
),
(
	'Game 2',
	'Developer 2',
	'Publisher 2',
	'2020-05-25',
	'Soul-like',
	39.99
),
(
	'Game 3',
	'Developer 3',
	'Publisher 1',
	'1980-12-25',
	'Horror',
	29.99
),
(
	'Game 4',
	'Developer 2',
	'Publisher 1',
	'1990-05-20',
	'Platformer',
	29.99
),
(
	'Game 5',
	'Developer 4',
	'Publisher 3',
	'2020-12-20',
	'Puzzle',
	39.99
)
GO

-- Users --

INSERT INTO Users (
	UserName,
	DateofBirth,
	Password,
	Region,
	Bios,
	DateCreated,
	Email
) VALUES
(
	'User 1',
	'2010-05-10',
	'Password1',
	'US',
	'Bios1',
	SYSDATETIME(),
	'Email1@example.com'
),
(
	'User 2',
	'2000-11-10',
	'Password2',
	'EU',
	'Bios2',
	SYSDATETIME(),
	'Email2@example.com'
),
(
	'User 3',
	'2005-01-15',
	'Password3',
	'AU',
	'Bios3',
	SYSDATETIME(),
	'Email3@example.com'
),
(
	'User 4',
	'2007-10-20',
	'Password4',
	'DE',
	'Bios4',
	SYSDATETIME(),
	'Email4@example.com'
),
(
	'User 5',
	'2020-05-20',
	'Password5',
	'US',
	'Bios5',
	SYSDATETIME(),
	'Email5@example.com'
)
GO

-- Collections --

INSERT INTO Collections (
	UserId,
	GameId,
	DateLastPlayed,
	TimesPlayed
) VALUES
(
	3,
	2,
	'2025-05-10',
	1000000
),
(
	1,
	4,
	'2025-03-20',
	2550000
),
(
	2,
	2,
	'2025-01-20',
	2000000
),
(
	4,
	1,
	'2024-12-13',
	100
),
(
	5,
	5,
	'2026-01-10',
	10000
)
GO

-- Create Stored Procedure --

CREATE OR ALTER PROCEDURE dbo.SelectGamesByGenre
    @Genre nvarchar(max)
AS
BEGIN
    SELECT u.GameId, u.Title, u.Developer, u.Publisher, u.ReleaseDate, u.Genre, u.Prices
    FROM dbo.Games u
    WHERE u.Genre = @Genre
    ORDER BY u.GameId
END