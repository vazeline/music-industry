using boilerplate.api.core.Models;
using boilerplate.api.data.Models;

namespace boilerplate.api.data.Procedures
{
    [Procedure]
    public static class ContactGetRefEntriesProcedure
    {
        public static string Name => "procContactGetRefEntries";
        public static int Version => 8;
        public static string Text => $@"
/* version={Version} */
CREATE PROCEDURE [{Name}]
    {ProcedureParams.AllowDirtyRead} BIT,
    {ProcedureParams.Offset} INT = 0,
    {ProcedureParams.Limit} INT = 1,
    {ProcedureParams.Filter} VARCHAR = ''
AS
BEGIN
    SET NOCOUNT ON;
    IF {ProcedureParams.AllowDirtyRead} = 1
    BEGIN
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    END

	IF @filter is null
		SET @filter = ''

SELECT Id, Name, Type
FROM (
    SELECT
    p.[{nameof(Platform.Id)}] AS [Id],
    p.[{nameof(Platform.Name)}] AS [Name],
    1 AS [Type]
    FROM [{PlatformExtension.TABLE_NAME}] p
    
UNION ALL    
    SELECT
    ml.[{nameof(MusicLabel.Id)}] AS [Id],
    ml.[{nameof(MusicLabel.Name)}] AS [Name],
    2 AS [Type]
    FROM [{MusicLabelExtension.TABLE_NAME}] ml
    
UNION ALL
    SELECT
    m.[{nameof(Musician.Id)}] AS [Id],
    m.[{nameof(Musician.Name)}] AS [Name],
    3 AS [Type]
    FROM [{MusicianExtension.TABLE_NAME}] m
) a
    WHERE Name LIKE '%'+{ProcedureParams.Filter}+'%'
    ORDER BY [Id]
    OFFSET 0 ROWS FETCH NEXT {ProcedureParams.Limit} ROWS ONLY
    
END";
    }
}
