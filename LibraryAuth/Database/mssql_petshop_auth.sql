-- Create database
CREATE DATABASE PetshopAuth;

-- Drop database
DROP DATABASE PetshopAuth;

-- Use Database
USE PetshopAuth;


----------------------------------------------------------
				  /* CREATE TABLES */
----------------------------------------------------------


-------------------------- SELECT ------------------------

-- Person
SELECT * FROM Users;
SELECT * FROM Person;
SELECT * FROM Pictures;
SELECT * FROM Contacts;
SELECT * FROM Addresses;
SELECT * FROM Schedules;

-- Pet
SELECT * FROM Pet;
SELECT * FROM Images;
SELECT * FROM Health;

------------------------------ DROP ------------------------

-- Person
DROP TABLE Users;
DROP TABLE Person;
DROP TABLE Pictures;
DROP TABLE Contacts;
DROP TABLE Addresses;
DROP TABLE Schedules;

-- Pet
DROP TABLE Pet;
DROP TABLE Images;
DROP TABLE Health;

------------------------- Person -------------------------

-- Creating Pictures

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Pictures' and type in (N'U'))

CREATE TABLE [dbo].[Pictures](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Tag] NVARCHAR(255) NULL,
	[Path] NVARCHAR(255) NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Pictures - Exists this table !!!'
GO

-- Creating Contacts

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Contacts' and type in (N'U'))

CREATE TABLE [dbo].[Contacts](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Email] NVARCHAR(250) NOT NULL,
	[Mobile] NVARCHAR(250) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Contacts - Exists this table !!!'
GO

-- Creating Addresses

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Addresses' and type in (N'U'))

CREATE TABLE [dbo].[Addresses](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Country] NVARCHAR(255) NOT NULL,
	[States] NVARCHAR(255) NOT NULL,
	[City] NVARCHAR(255) NOT NULL,
	[Neighborhoods] NVARCHAR(255) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Addresses - Exists this table !!!'
GO

-- Creating Users

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Users' and type in (N'U'))

CREATE TABLE [dbo].[Users](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Username] NVARCHAR(255) UNIQUE NOT NULL,
	[Password] NVARCHAR(255) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
);
ELSE
	PRINT 'Users - Exists this table !!!'
GO

-- Creating Person

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Person' and type in (N'U'))

CREATE TABLE [dbo].[Person] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[FirstName] NVARCHAR(250) NOT NULL,
	[LastName] NVARCHAR(250) NOT NULL,
	[Age] INT NOT NULL,
	[Genre] NVARCHAR(250) NOT NULL,
	[Birthday] DATETIME2 NOT NULL,
	[UserId] INT NOT NULL,
	[PictureId] INT NOT NULL,
	[AddressId] INT NOT NULL,
	[ContactId] INT NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (UserId) REFERENCES [dbo].[Users](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (PictureId) REFERENCES [dbo].[Pictures](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (ContactId) REFERENCES [dbo].[Contacts](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (AddressId) REFERENCES [dbo].[Addresses](Id) ON UPDATE CASCADE ON DELETE CASCADE,
);
ELSE
	PRINT 'Person - Exists this table !!!'
GO

-- Creating Schedules

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Schedules' and type in (N'U'))

CREATE TABLE [dbo].[Schedules](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Services] NVARCHAR(255) NOT NULL,
	[Date] DATETIME2 NOT NULL,
	[Time] DATETIME2 NOT NULL,
	[PersonId] INT NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (PersonId) REFERENCES [dbo].[Person](Id) ON UPDATE CASCADE ON DELETE CASCADE
);
ELSE
	PRINT 'Schedules - Exists this table !!!'
GO

------------------------- Pet ----------------------------

-- Creating Images

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Images' and type in (N'U'))

CREATE TABLE [dbo].[Images](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Tag] NVARCHAR(255) NULL,
	[Path] NVARCHAR(255) NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);
ELSE
	PRINT 'Images - Exists this table !!!'
GO

-- Creating Health

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Health' and type in (N'U'))

CREATE TABLE [dbo].[Health](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Status] NVARCHAR(255) NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);
ELSE
	PRINT 'Health - Exists this table !!!'
GO

-- Creating Pet

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Pet' and type in (N'U'))

CREATE TABLE [dbo].[Pet] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(250) NOT NULL,
	[Type] NVARCHAR(250) NOT NULL,
	[Genre] NVARCHAR(250) NOT NULL,
	[Age] INT NOT NULL,
	[Birthday] DATETIME2 NOT NULL,
	[ImageId] INT NOT NULL,
	[PersonId] INT NOT NULL,
	[HealthId] INT NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (ImageId) REFERENCES [dbo].[Images](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (HealthId) REFERENCES [dbo].[Health](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (PersonId) REFERENCES [dbo].[Person](Id) ON UPDATE CASCADE ON DELETE CASCADE
);
ELSE
	PRINT 'Pet - Exists this table !!!'
GO

----------------------------------------------------------
			  /* Crud Procedure Operation */
----------------------------------------------------------

/* ********************* User ********************* */

/* List */
CREATE PROCEDURE [dbo].[ListUser]
	-- @IdPerson AS INT
AS BEGIN
	-- Person
	SELECT * FROM [dbo].[Users] u1;
END
GO

/* Detail */
CREATE PROCEDURE [dbo].[GetUser]
	@IdUser AS INT
AS BEGIN
	SELECT 
		-- Users
		u1.[Username],
		u1.[Password]		
	FROM [dbo].[Users] u1
	WHERE u1.Id = @IdUser
END
GO

/* Create */
CREATE PROCEDURE [dbo].[PostUser]	
	-- Users
	@Username AS NVARCHAR(250),
	@Password AS NVARCHAR(250)
AS BEGIN
	-- User
	INSERT INTO [dbo].[Users]([Username], [Password]) 
	VALUES (@Username, @Password);	
END
GO

/* Update */
CREATE PROCEDURE [dbo].[PutUser]
	@IdUser AS INT,
	@Username AS NVARCHAR(250),
	@Password AS NVARCHAR(250)
AS BEGIN
	UPDATE [dbo].[Users] SET 
		[Username] = @Username,
		[Password] = @Password
	WHERE Id = @IdUser;
END
GO

/* Delete */
CREATE PROCEDURE [dbo].[DeleteUser]
	@IdUser AS INT
AS BEGIN 
	DELETE u1 FROM [dbo].[Users] u1
	WHERE u1.Id = @IdUser
END
GO

/* ********************* Person ********************* */

/* List */
CREATE PROCEDURE [dbo].[ListPerson]
	-- @IdPerson AS INT
AS BEGIN
	-- Person
	SELECT * FROM [dbo].[Person] p1
	-- Users
	LEFT JOIN [dbo].[Users] u1
	ON p1.UserId = u1.Id
	-- Pictures
	LEFT JOIN [dbo].[Pictures] p2
	ON p1.PictureId = p2.Id
	-- Contacts
	LEFT JOIN [dbo].[Contacts] c2
	ON p1.ContactId = c2.Id
	-- Addresses
	LEFT JOIN [dbo].[Addresses] a1
	ON p1.AddressId = a1.Id
END
GO

/* Detail */
CREATE PROCEDURE [dbo].[GetPerson]
	@IdPerson AS INT
AS BEGIN
	SELECT 
		-- Person
		p1.Id,
		p1.FirstName,
		p1.LastName,
		p1.Genre,
		p1.Age,
		p1.Birthday,
		-- Users
		u1.Username,
		u1.[Password],
		-- Address
		p1.AddressId,
		-- Pictures
		p2.Id,
		p2.[Tag],
		p2.[Path],
		-- Contacts
		c2.Id,
		c2.Email,
		c2.Mobile	
	FROM Person p1
	-- Users
	LEFT JOIN [dbo].[Users] u1
	ON p1.UserId = u1.Id
	-- Pictures
	LEFT JOIN [dbo].[Pictures] p2
	ON p1.PictureId = p2.Id
	-- Contacts
	LEFT JOIN [dbo].[Contacts] c2
	ON p1.ContactId = c2.Id
	-- Addresses
	LEFT JOIN [dbo].[Addresses] a1
	ON p1.AddressId = a1.Id
	WHERE p1.Id = @IdPerson
END
GO

/* Create */
CREATE PROCEDURE [dbo].[PostPerson]	
	-- Pictures
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@PictureId AS INT,
	-- Contacts
	@Email AS NVARCHAR(250),
	@Mobile AS NVARCHAR(250),
	@ContactId AS INT,
	-- Addresses
	@Country AS NVARCHAR(250),
	@States AS NVARCHAR(250),
	@City AS NVARCHAR(250),
	@Neighborhoods AS NVARCHAR(250),
	@AddressId AS INT,
	-- Person
	@FirstName AS NVARCHAR(250),
	@LastName AS NVARCHAR(250),
	@Genre AS NVARCHAR(250),
	@Birthday AS DATE,
	@Age AS INT

AS BEGIN
	-- Pictures
	INSERT INTO [dbo].[Pictures]([Tag], [Path]) 
	VALUES (@Tag, @Path);
	-- Contacts
	INSERT INTO [dbo].[Contacts]([Email], [Mobile]) 
	VALUES (@Email, @Mobile);
	-- Addresses
	INSERT INTO [dbo].[Addresses]([Country], [States], [City], [Neighborhoods]) 
	VALUES (@Country, @States, @City, @Neighborhoods);
	-- Person
	INSERT INTO [dbo].[Person]([FirstName], [LastName], [Age], [Genre], [Birthday], [UserId], [PictureId], [AddressId], [ContactId])
	VALUES (@FirstName, @LastName, @Age, @Genre, Convert(DATE, @Birthday), 
	(SELECT MAX(u1.Id) FROM [dbo].[Users] u1),
	(SELECT MAX(p1.Id) FROM [dbo].[Pictures] p1),
	(SELECT MAX(c1.Id) FROM [dbo].[Contacts] c1),
	(SELECT MAX(a1.Id) FROM [dbo].[Addresses] a1));
END
GO

/* Update */
CREATE PROCEDURE [dbo].[PutPerson]
	@IdPerson AS INT,
	-- Pictures
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@PictureId AS INT,
	-- Contacts
	@Email AS NVARCHAR(250),
	@Mobile AS NVARCHAR(250),
	@ContactId AS INT,
	-- Addresses
	@Country AS NVARCHAR(250),
	@States AS NVARCHAR(250),
	@City AS NVARCHAR(250),
	@Neighborhoods AS NVARCHAR(250),
	@AddressId AS INT,
	-- Users
	@UserId AS INT,
	-- Person
	@FirstName AS NVARCHAR(250),
	@LastName AS NVARCHAR(250),
	@Genre AS NVARCHAR(250),
	@Birthday AS DATE,
	@Age AS INT

AS BEGIN
	-- Pictures
	UPDATE [dbo].[Pictures] SET 
		[Tag] = @Tag,
		[Path] = @Path
	WHERE Id = @PictureId;
	
	-- Contacts
	UPDATE [dbo].[Contacts] SET
		Email = @Email,
		Mobile = @Mobile 
	WHERE Id = @ContactId;

	-- Addresses
	UPDATE  [dbo].[Addresses] SET 
		[Country] = @Country,
		[States] = @States,
		[City] = @City,
		[Neighborhoods] = @Neighborhoods
	WHERE Id = @AddressId;

	-- Person
	UPDATE [dbo].[Person] SET 
		FirstName = @FirstName,
		LastName = @LastName,
		Age = @Age,
		Genre = @Genre,
		Birthday = Convert(DATE, @Birthday),
		PictureId = @PictureId,
		UserId = @UserId,
		AddressId = @AddressId, 
		ContactId = @ContactId
	WHERE Id = @IdPerson;
END
GO

/* Delete */
CREATE PROCEDURE [dbo].[DeletePerson]	
	@IdPerson AS INT
AS BEGIN 
	DELETE p1 FROM [dbo].[Person] p1
	-- Users
	LEFT JOIN [dbo].[Users] u1
	ON p1.UserId = u1.Id
	-- Pictures
	LEFT JOIN [dbo].[Pictures] p2
	ON p1.PictureId = p2.Id
	-- Contacts
	LEFT JOIN [dbo].[Contacts] c2
	ON p1.ContactId = c2.Id
	-- Addresses
	LEFT JOIN [dbo].[Addresses] a1
	ON p1.AddressId = a1.Id
	WHERE p1.Id = @IdPerson
END
GO


/* ********************* Pet ********************* */

/* List */
CREATE PROCEDURE [dbo].[ListPet]
	-- @IdPerson AS INT
AS BEGIN
	-- Person
	SELECT * FROM [dbo].[Pet] p1
	-- Image
	LEFT JOIN [dbo].[Images] i1
	ON p1.ImageId = i1.Id
	-- Health
	LEFT JOIN [dbo].[Health] h1
	ON p1.HealthId = h1.Id	
END
GO

/* Detail */
CREATE PROCEDURE [dbo].[GetPet]
	@IdPet AS INT
AS BEGIN
	SELECT 	
		-- Pictures
		i1.[Id],
		i1.[Tag],
		i1.[Path],
		-- Health
		h1.[Status],		
		-- Pet
		p1.[Id],
		p1.[Name],
		p1.[Type],
		p1.[Age],
		p1.[Birthday],
		p1.[Genre]
	FROM Pet p1
	-- Image
	LEFT JOIN [dbo].[Images] i1
	ON p1.ImageId = i1.Id
	-- Health
	LEFT JOIN [dbo].[Health] h1
	ON p1.HealthId = h1.Id
	WHERE p1.Id = @IdPet
END
GO

/* Create */
CREATE PROCEDURE [dbo].[PostPet]
	-- Images
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@ImageId AS INT,
	-- Health
	@Status AS NVARCHAR(250),
	@HealthId AS INT,
	-- Pet
	@Name AS NVARCHAR(250),
	@Type AS NVARCHAR(250),
	@Genre AS NVARCHAR(250),
	@Birthday AS DATE,
	@Age AS INT,
	-- Person
	@PersonId AS INT

AS BEGIN
	-- Images
	INSERT INTO [dbo].[Images]([Tag], [Path]) 
	VALUES (@Tag, @Path);
	-- Contacts
	INSERT INTO [dbo].[Health]([Status]) 
	VALUES (@Status);	
		-- Pet
	INSERT INTO [dbo].[Pet]([Name], [Type], [Age], [Genre], [Birthday], [ImageId], [HealthId], [PersonId])
	VALUES (@Name, @Type, @Age, @Genre, Convert(DATE, @Birthday), 
	(SELECT MAX(i1.Id) FROM [dbo].[Images] i1),
	(SELECT MAX(h1.Id) FROM [dbo].[Health] h1),
	(SELECT MAX(p1.Id) FROM [dbo].[Person] p1))
END
GO

/* Update */
CREATE PROCEDURE [dbo].[PutPet]
	@IdPet AS INT,
	-- Images
	@Tag AS NVARCHAR(255),
	@Path AS NVARCHAR(255),
	@ImageId AS INT,
	-- Health
	@Status AS NVARCHAR(250),
	@HealthId AS INT,
	-- Pet
	@Name AS NVARCHAR(250),
	@Type AS NVARCHAR(250),
	@Genre AS NVARCHAR(250),
	@Birthday AS DATE,
	@Age AS INT,
	-- Person
	@PersonId AS INT

AS BEGIN
	-- Images
	UPDATE [dbo].[Images] SET 
		[Tag] = @Tag,
		[Path] = @Path
	WHERE Id = @ImageId;
	
	-- Health
	UPDATE [dbo].[Health] SET
		[Status] = @Status
	WHERE Id = @HealthId;	
	
	-- Pet
	UPDATE [dbo].[Pet] SET 
		[Name] = @Name,
		[Type] = @Type,
		Age = @Age,
		Genre = @Genre,
		Birthday = Convert(DATE, @Birthday),
		ImageId = @ImageId,
		HealthId = @HealthId,
		PersonId = @PersonId
	WHERE Id = @IdPet;
END
GO

/* Delete */
CREATE PROCEDURE [dbo].[DeletePet]	
	@IdPet AS INT
AS BEGIN 
	DELETE p1 FROM Pet p1
	-- Image
	LEFT JOIN [dbo].[Images] i1
	ON p1.ImageId = i1.Id
	-- Health
	LEFT JOIN [dbo].[Health] h1
	ON p1.HealthId = h1.Id	
	WHERE p1.Id = @IdPet
END
GO


/* ********************* Schedules ********************* */

/* List */
CREATE PROCEDURE [dbo].[ListSchedule]
AS BEGIN
	SELECT * FROM [dbo].[Schedules] s1;
END
GO

/* Detail */
CREATE PROCEDURE [dbo].[GetSchedule]
	@IdSchedule AS INT
AS BEGIN
	SELECT
		s1.Id,
		s1.[Services],
		s1.[Date],
		s1.[Time],
		s1.[PersonId]
	FROM [dbo].[Schedules] s1
	WHERE s1.Id = @IdSchedule
END
GO

/* Create */
CREATE PROCEDURE [dbo].[PostSchedule]	
	-- Users
	@Services AS NVARCHAR(250),
	@Date AS DATE,
	@Time TIME,
	@PersonId AS INT
AS BEGIN
	-- Schedules
	INSERT INTO [dbo].[Schedules]([Services], [Date], [Time], [PersonId]) 
	VALUES (@Services, Convert(DATE, @Date), @Time, @PersonId);
END
GO

/* Update */
CREATE PROCEDURE [dbo].[PutSchedule]
	@IdSchedule AS INT,
	@Services AS NVARCHAR(250),
	@Date AS DATE,
	@Time TIME,
	@PersonId AS INT
AS BEGIN
	-- Schedules
	UPDATE  [dbo].[Schedules] SET 
		[Services] = @Services,
		[Date] = @Date,
		[Time] = @Time,
		[PersonId] = @PersonId
	WHERE Id = @IdSchedule;	
END
GO

/* Delete */
CREATE PROCEDURE [dbo].[DeleteSchedule]
	@IdSchedule AS INT
AS BEGIN 
	DELETE s1 FROM [dbo].[Schedules] s1
	WHERE s1.Id = @IdSchedule
END
GO

/* ********************* User ********************* */

/* List */

EXEC [dbo].[ListUser];

/* Details */

EXEC [dbo].[GetUser] @IdUser = 1;

/* Create */

EXEC [dbo].[PostUser] @Username = 'root@local.com', 
@Password = '$2a$11$Y/cVg.Xx8m6HSOqkqFThWeG5bNz5d6OnOlDGdpuZlyigy3fGtEEEG';

/* Update */

EXEC [dbo].[PutUser] @IdUser = 1, @Username = 'root1@local.com',
@Password = '$2a$11$lbfOHPz2buHUN5VnvAkv5Omq9TfQ1jQIxuhLhZsFlUA3Mux9bl746';

/* Delete */

EXEC [dbo].[DeleteUser] @IdUser = 1;


/* ********************* Person ********************* */

/* List */

EXEC [dbo].[ListPerson];

/* Details */

EXEC [dbo].[GetPerson] @IdPerson = 1;

/* Create */

EXEC [dbo].[PostPerson] @Tag = 'MyPicturePerson', @Path = '../Pictures/my_picture_person.png', 
	@Email = 'luiz@siqueira.psk', @Mobile = '21975918265', 
	@Country = 'Brasil', @States = 'Rio de Janeiro', @City = 'Rio de Janeiro', @Neighborhoods = 'Leme',
	@Firstname = 'Luiz', @Lastname = 'Siqueira', @Genre = 'Male', @Age = '31', @Birthday = '1990-01-28',
	@PictureId = 1, @ContactId = 1, @AddressId = 1;

/* Update */

EXEC [dbo].[PutPerson] @IdPerson = 1, @Tag = 'MyPicturePerson', @Path = '../Pictures/my_picture_person.png', 
	@Email = 'luiz@siqueira.psk', @Mobile = '21975918265', 
	@Country = 'Brasil', @States = 'Rio de Janeiro', @City = 'Rio de Janeiro', @Neighborhoods = 'Leme',
	@Firstname = 'Luiz', @Lastname = 'Siqueira', @Genre = 'Male', @Age = '31', @Birthday = '1990-01-28',
	@PictureId = 1, @UserId = 1, @ContactId = 1, @AddressId = 1;

/* Delete */

EXEC [dbo].[DeletePerson] @IdPerson = 1;


/* ********************* Pet ********************* */

/* List */

EXEC [dbo].[ListPet];

/* Details */

EXEC [dbo].[GetPet] @IdPet = 1;


/* Create */

EXEC [dbo].[PostPet] @Tag = 'my_picture_pet', @Path = '../Pictures/my_picture_pet.png', @Status = 'Bad', @Name = 'Negao', @Type = 'Mendes', @Genre = 'M', @Age = '10', @Birthday = '2002-11-20', 
	@ImageId = 1,  @HealthId = 1, @PersonId = 1; 

/* Update */

EXEC [dbo].[PutPet] @IdPet = 1, @Tag = 'SelfPet', @Path = '../Pictures/my_picture_pet.png',
	@Status = 'Bad', @Name = 'Negao', @Type = 'Mendes', @Genre = 'M', @Age = '10', @Birthday = '2002-11-20', 
	@ImageId = 1,  @HealthId = 1, @PersonId = 1;

/* Delete */

EXEC [dbo].[DeletePet] @IdPet = 1;


/* ********************* Schedules ********************* */

/* List */

EXEC [dbo].[ListSchedule];

/* Details */

EXEC [dbo].[GetSchedule] @IdSchedule = 1;

/* Create */

EXEC [dbo].[PostSchedule] @Services = 'Banho', @Date = '2022-06-12', @Time = '10:00:00.0123456', @PersonId = 1;

/* Update */

EXEC [dbo].[PutSchedule] @IdSchedule = 1, @Services = 'Banho', @Date = '2022-06-12', @Time = '10:00:00.0123456',  @PersonId = 1;

/* Delete */

EXEC [dbo].[DeleteSchedule] @IdSchedule = 1;