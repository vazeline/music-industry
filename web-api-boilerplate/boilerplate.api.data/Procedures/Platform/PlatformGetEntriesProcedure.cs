using boilerplate.api.core.Models;
using boilerplate.api.data.Models;

namespace boilerplate.api.data.Procedures
{
    [Procedure]
    public static class PlatformGetEntriesProcedure
    {
        public static string Name => "procPlatformGetEntries";
        public static int Version => 3;
        public static string Text => $@"
/* version={Version} */
CREATE PROCEDURE [{Name}]
    {ProcedureParams.AllowDirtyRead} BIT,
    {ProcedureParams.Offset} INT = 0,
    {ProcedureParams.Limit} INT = 1,
    {ProcedureParams.Id} INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF {ProcedureParams.AllowDirtyRead} = 1
    BEGIN
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    END

    SELECT
    p.[{nameof(Platform.Id)}] AS [{nameof(PlatformReportModel.Id)}],
    p.[{nameof(Platform.Name)}] AS [{nameof(PlatformReportModel.Name)}],
    p.[{nameof(Platform.DateCreated)}] AS [{nameof(PlatformReportModel.DateCreated)}],
    p.[{nameof(Platform.DateModified)}] AS [{nameof(PlatformReportModel.DateModified)}]
    FROM [{PlatformExtension.TABLE_NAME}] p
    WHERE {ProcedureParams.Id} IS NULL OR p.[{nameof(Platform.Id)}] = {ProcedureParams.Id}
    ORDER BY p.[{nameof(Platform.Id)}]
    OFFSET {ProcedureParams.Offset} ROWS FETCH NEXT {ProcedureParams.Limit} ROWS ONLY
    
    IF {ProcedureParams.Id} IS NULL
    BEGIN
        SELECT COUNT(*)
        FROM [{PlatformExtension.TABLE_NAME}] p
    END
END";
    }
}
