USE [PhoneBook] 
GO 

CREATE TABLE Users (
    [Full name] VARCHAR(40),
    [Phone number] VARCHAR(15) PRIMARY KEY,
    [Birth date] DATE
);
GO

INSERT INTO [dbo].[Users]
           ([Full name]
           ,[Phone number]
           ,[Birth date])
     VALUES
           ('Monika Balèiûtë','863254687','2000-12-13')
GO

INSERT INTO [dbo].[Users]
           ([Full name]
           ,[Phone number]
           ,[Birth date])
     VALUES
           ('Lukas Vanagas','+370 65495276','1990-05-01')
GO

INSERT INTO [dbo].[Users]
           ([Full name]
           ,[Phone number]
           ,[Birth date])
     VALUES
           ('Raminta Marija Balaitë','+370 12345689','2003-08-26');
GO

INSERT INTO [dbo].[Users]
           ([Full name]
           ,[Phone number]
           ,[Birth date])
     VALUES
           ('John Niv','+44 20 7123 456','2000-07-20');
GO

INSERT INTO [dbo].[Users]
           ([Full name]
           ,[Phone number]
           ,[Birth date])
     VALUES
           ('Saulius Balsys','863254654','2004-02-16');
GO

INSERT INTO [dbo].[Users]
           ([Full name]
           ,[Phone number]
           ,[Birth date])
     VALUES
           ('Gabija Dainaitë','863254354','2008-12-25');