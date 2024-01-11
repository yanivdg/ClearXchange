/*validate ID number*/
CREATE FUNCTION dbo.is_valid_israeli_id(@id_number NVARCHAR(9))
RETURNS BIT AS
BEGIN
  IF LEN(@id_number) != 9
    RETURN 0

  DECLARE @total INT = 0, @digit INT, @doubled INT
  DECLARE @i INT = 1
  WHILE @i <= 9
    BEGIN
      SET @digit = CAST(SUBSTRING(@id_number, @i, 1) AS INT)
      IF @i % 2 = 0
        BEGIN
          SET @doubled = @digit * 2
          SET @total = @total + IIF(@doubled < 10, @doubled, 1 + @doubled % 10)
        END
      ELSE
        BEGIN
          SET @total = @total + @digit
        END
      SET @i = @i + 1
    END
  RETURN IIF(@total % 10 = 0, 1, 0)
END

/*validate numberic string*/ 
CREATE FUNCTION dbo.IsNumeric(@str NVARCHAR(255))
RETURNS BIT
AS
BEGIN
  DECLARE @result BIT
  IF PATINDEX('%[^0-9]%', @str) > 0
    SET @result = 0
  ELSE
    SET @result = 1
  RETURN @result
END

/*validate email address*/
CREATE FUNCTION dbo.is_valid_email(@email NVARCHAR(255))
RETURNS BIT AS
BEGIN
  DECLARE @result BIT
  IF @email LIKE '%_@__%.__%'
    SET @result = 1
  ELSE
    SET @result = 0
  RETURN @result
END


/*creating users table*/
CREATE TABLE Users (
  IDNumber NVARCHAR(9) NOT NULL CHECK (dbo.is_valid_israeli_id(IDNumber) = 1),
  Name NVARCHAR(255) NOT NULL,
  Email NVARCHAR(255) CHECK (dbo.is_valid_email(Email) = 1),
  DateOfBirth DATE NOT NULL CHECK (DateOfBirth <= GETDATE() AND DateOfBirth >= '1900-01-01'),
  Gender NVARCHAR(20) CHECK (Gender IN ('Male', 'Female', 'Other')),
  Telephone NVARCHAR(20) CHECK (dbo.IsNumeric(Telephone) = 1),
  PRIMARY KEY (IDNumber)
);







