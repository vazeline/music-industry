using boilerplate.api.core.Models;
using boilerplate.api.data.Models;

namespace boilerplate.api.data.Procedures
{
    [Procedure]
    public static class MusicLabelGetEntriesProcedure
    {
        public static string Name => "procMusicLabelGetEntries";
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
    ml.[{nameof(MusicLabel.Id)}] AS [{nameof(MusicLabelReportModel.Id)}],
    ml.[{nameof(MusicLabel.Name)}] AS [{nameof(MusicLabelReportModel.Name)}],
    ml.[{nameof(MusicLabel.DateCreated)}] AS [{nameof(MusicLabelReportModel.DateCreated)}],
    ml.[{nameof(MusicLabel.DateModified)}] AS [{nameof(MusicLabelReportModel.DateModified)}]
    FROM [{MusicLabelExtension.TABLE_NAME}] ml
    WHERE {ProcedureParams.Id} IS NULL OR ml.[{nameof(MusicLabel.Id)}] = {ProcedureParams.Id}
    ORDER BY ml.[{nameof(MusicLabel.Id)}]
    OFFSET {ProcedureParams.Offset} ROWS FETCH NEXT {ProcedureParams.Limit} ROWS ONLY
    
    IF {ProcedureParams.Id} IS NULL
    BEGIN
        SELECT COUNT(*)
        FROM [{MusicLabelExtension.TABLE_NAME}] ml
    END
END";
    }
}
