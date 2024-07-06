USE [PhoneBook] 
GO 
CREATE PROCEDURE GetAllUsers
AS
BEGIN
    SELECT [Full name], [Birth date], [Phone number] FROM Users; 
END;
GO
CREATE PROCEDURE AddUser
    (@FullName VARCHAR(40),
    @BirthDate DATE,
    @PhoneNumber VARCHAR(15))
AS
BEGIN
    INSERT INTO Users([Full name], [Birth date], [Phone number]) VALUES(@FullName, @BirthDate, @PhoneNumber);
END;
GO
CREATE PROCEDURE EditUser
    (@OriginalPhoneNumber VARCHAR(15),
    @FullName VARCHAR(40),
    @BirthDate DATE,
    @PhoneNumber VARCHAR(15))
AS
BEGIN
    UPDATE Users
    SET
        [Full name] = ISNULL(@FullName, [Full name]),
        [Birth date] = ISNULL(@BirthDate, [Birth date]),
        [Phone number] = ISNULL(@PhoneNumber, [Phone number])
    WHERE
        [Phone number] = @OriginalPhoneNumber;
END;
GO
CREATE PROCEDURE DeleteUser
    @PhoneNumber VARCHAR(15)
AS
BEGIN
   DELETE FROM Users WHERE [Phone number] = @PhoneNumber;
END;