USE [ClearXchangeDB]
GO
/*building the function and table if necesery - will be running at start only*/
DECLARE	@return_value Int

EXEC	@return_value = [dbo].[CreateFunctionsAndTable]

SELECT	@return_value as 'Return Value'

GO
