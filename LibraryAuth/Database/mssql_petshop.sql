-- Create database
CREATE DATABASE Petshop;

-- Drop database
DROP DATABASE Petshop;

-- Use Database
USE Petshop;


----------------------------------------------------------
				  /* CREATE TABLES */
----------------------------------------------------------



-------------------------- SELECT ------------------------

-- Person
SELECT * FROM Person;
SELECT * FROM Pictures;
SELECT * FROM Contacts;
SELECT * FROM Addresses;

-- Pet
SELECT * FROM Pet;
SELECT * FROM Images;
SELECT * FROM Health;
SELECT * FROM Schedules;

------------------------------ DROP ------------------------

-- Person
DROP TABLE Person;
DROP TABLE Pictures;
DROP TABLE Contacts;
DROP TABLE Addresses;

-- Pet
DROP TABLE Pet;
DROP TABLE Images;
DROP TABLE Health;
DROP TABLE Schedules;

------------------------- Person -------------------------


-- Creating Pictures

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Pictures' and type in (N'U'))

CREATE TABLE [dbo].[Pictures](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Tag] NVARCHAR(255) NULL,
	[Path] NVARCHAR(255) NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
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
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
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
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);
ELSE
	PRINT 'Addresses - Exists this table !!!'
GO

-- Creating Person

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Person' and type in (N'U'))

CREATE TABLE [dbo].[Person] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[FirstName] NVARCHAR(250) NOT NULL,
	[LastName] NVARCHAR(250) NOT NULL,
	[Age] INT NOT NULL,
	[Genre] NVARCHAR(250) NOT NULL,
	[Birthday] DATE NOT NULL,
	[PictureId] INT NOT NULL,
	[AddressId] INT NOT NULL,
	[ContactId] INT NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (PictureId) REFERENCES [dbo].[Pictures](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (ContactId) REFERENCES [dbo].[Contacts](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (AddressId) REFERENCES [dbo].[Addresses](Id) ON UPDATE CASCADE ON DELETE CASCADE
);
ELSE
	PRINT 'Person - Exists this table !!!'
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

-- Creating Schedules

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Schedules' and type in (N'U'))

CREATE TABLE [dbo].[Schedules](
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Services] NVARCHAR(255) NOT NULL,
	[Date] DATETIME NOT NULL,
	[Time] DATETIME NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
);
ELSE
	PRINT 'Schedules - Exists this table !!!'
GO

-- Creating Pet

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME=N'Pet' and type in (N'U'))

CREATE TABLE [dbo].[Pet] (
	[Id] INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(250) NOT NULL,
	[Type] NVARCHAR(250) NOT NULL,
	[Genre] NVARCHAR(250) NOT NULL,
	[Age] INT NOT NULL,
	[Birthday] DATE NOT NULL,
	[PersonId] INT NOT NULL,
	[ImageId] INT NOT NULL,
	[HealthId] INT NOT NULL,
	[ScheduleId] INT NOT NULL,
	Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    Updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
	FOREIGN KEY (PersonId) REFERENCES [dbo].[Person](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (ImageId) REFERENCES [dbo].[Images](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (HealthId) REFERENCES [dbo].[Health](Id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (ScheduleId) REFERENCES [dbo].[Schedules](Id) ON UPDATE CASCADE ON DELETE CASCADE,
);
ELSE
	PRINT 'Pet - Exists this table !!!'
GO

----------------------------------------------------------
			  /* Crud Procedure Operation */
----------------------------------------------------------


/* ********************* Person ********************* */

/* List */
CREATE PROCEDURE [dbo].[ListPerson]
	-- @IdPerson AS INT
AS BEGIN
	-- Person
	SELECT * FROM [dbo].[Person] p1
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
		p1.PictureId,
		p1.ContactId,
		p1.AddressId,		
		-- Pictures
		p2.Id,
		p2.[Tag],
		p2.[Path],
		-- Contacts
		c2.Id,
		c2.Email,
		c2.Mobile,
		-- Address
		a1.Country,
		a1.States,
		a1.City,
		a1.Neighborhoods
	FROM [dbo].[Person] p1
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
	INSERT INTO [dbo].[Person]([FirstName], [LastName], [Age], [Genre], [Birthday], [PictureId], [AddressId], [ContactId])
	VALUES (@FirstName, @LastName, @Age, @Genre, Convert(DATE, @Birthday), 
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
	-- Schedules
	LEFT JOIN [dbo].[Schedules] s1
	ON p1.ScheduleId = s1.Id
END
GO

/* Detail */
CREATE PROCEDURE [dbo].[GetPet]
	@IdPet AS INT
AS BEGIN
	SELECT 
		-- Pet
		p1.[Id],
		p1.[Name],
		p1.[Type],
		p1.[Age],
		p1.[Birthday],
		p1.[Genre],
		p1.[PersonId],
		-- Images
		i1.[Id],
		i1.[Tag],
		i1.[Path],
		-- Health
		h1.[Id],
		h1.[Status],		
		-- Schedules
		s1.[Id],
		s1.[Services],
		s1.[Date],
		s1.[Time]
	-- Person
	FROM [dbo].[Pet] p1
	-- Image
	LEFT JOIN [dbo].[Images] i1
	ON p1.ImageId = i1.Id
	-- Health
	LEFT JOIN [dbo].[Health] h1
	ON p1.HealthId = h1.Id	
	-- Schedules
	LEFT JOIN [dbo].[Schedules] s1
	ON p1.ScheduleId = s1.Id
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
	-- Services
	@Services AS NVARCHAR(250),
	@Date AS DATE,
	@Time AS TIME,
	@ScheduleId AS INT,
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
	-- Schedules
	INSERT INTO [dbo].[Schedules]([Services], [Date], [Time]) 
	VALUES (@Services, Convert(DATE, @Date), @Time);
	-- Pet
	INSERT INTO [dbo].[Pet]([Name], [Type], [Age], [Genre], [Birthday], [ImageId], [HealthId], [ScheduleId], [PersonId])
	VALUES (@Name, @Type, @Age, @Genre, Convert(DATE, @Birthday), 
	(SELECT MAX(i1.Id) FROM [dbo].[Images] i1),
	(SELECT MAX(h1.Id) FROM [dbo].[Health] h1),
	(SELECT MAX(s1.Id) FROM [dbo].[Schedules] s1),
	(SELECT MAX(p1.Id) FROM [dbo].[Person] p1 WHERE Id = @PersonId));
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
	-- Services
	@Services AS NVARCHAR(250),
	@Date AS DATE,
	@Time TIME,
	@ScheduleId AS INT,
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

	-- Schedules
	UPDATE  [dbo].[Schedules] SET 
		[Services] = @Services,
		[Date] = @Date,
		[Time] = @Time
	WHERE Id = @ScheduleId;	
	
	-- Pet
	UPDATE [dbo].[Pet] SET 
		[Name] = @Name,
		[Type] = @Type,
		Age = @Age,
		Genre = @Genre,
		Birthday = Convert(DATE, @Birthday),
		ImageId = @ImageId,
		HealthId = @HealthId,
		ScheduleId = @ScheduleId,
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
	-- Schedules
	LEFT JOIN [dbo].[Schedules] s1
	ON p1.ScheduleId = s1.Id	
	WHERE p1.Id = @IdPet
END
GO

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
	@PictureId = 1, @ContactId = 1, @AddressId = 1;

/* Delete */

EXEC [dbo].[DeletePerson] @IdPerson = 1;


/* ********************* Pet ********************* */

/* List */

EXEC [dbo].[ListPet];

/* Details */

EXEC [dbo].[GetPet] @IdPet = 1;

/* Create */

EXEC [dbo].[PostPet] @Tag = 'my_picture_pet', @Path = '../Pictures/my_picture_pet.png',
	@Status = 'Bad', @Services = 'Banho', @Date = '2022-06-12', @Time = '10:00:00.0123456', 
	@Name = 'Negao', @Type = 'Mendes', @Genre = 'M', @Age = '10', @Birthday = '2002-11-20', 
	@ImageId = 1,  @HealthId = 1, @PersonId = 1, @ScheduleId = 1; 

/* Update */

EXEC [dbo].[PutPet] @IdPet = 1, @Tag = 'SelfPet', @Path = '../Pictures/my_picture_pet.png',
	@Status = 'Bad', @Services = 'Banho', @Date = '2022-06-12', @Time = '10:00:00.0123456', 
	@Name = 'Negao', @Type = 'Mendes', @Genre = 'M', @Age = '10', @Birthday = '2002-11-20', 
	@ImageId = 1,  @HealthId = 1, @PersonId = 1, @ScheduleId = 1; 

/* Delete */

EXEC [dbo].[DeletePet] @IdPet = 1;
