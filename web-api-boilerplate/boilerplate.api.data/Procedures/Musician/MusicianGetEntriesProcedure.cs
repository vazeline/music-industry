using boilerplate.api.core.Models;
using boilerplate.api.data.Models;

namespace boilerplate.api.data.Procedures
{
    [Procedure]
    public static class MusicianGetEntriesProcedure
    {
        public static string Name => "procMusicianGetEntries";
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
    m.[{nameof(Musician.Id)}] AS [{nameof(MusicianReportModel.Id)}],
    m.[{nameof(Musician.Name)}] AS [{nameof(MusicianReportModel.Name)}],
    m.[{nameof(Musician.DateCreated)}] AS [{nameof(MusicianReportModel.DateCreated)}],
    m.[{nameof(Musician.DateModified)}] AS [{nameof(MusicianReportModel.DateModified)}]
    FROM [{MusicianExtension.TABLE_NAME}] m
    WHERE {ProcedureParams.Id} IS NULL OR m.[{nameof(Musician.Id)}] = {ProcedureParams.Id}
    ORDER BY m.[{nameof(Musician.Id)}]
    OFFSET {ProcedureParams.Offset} ROWS FETCH NEXT {ProcedureParams.Limit} ROWS ONLY
    
    IF {ProcedureParams.Id} IS NULL
    BEGIN
        SELECT COUNT(*)
        FROM [{MusicianExtension.TABLE_NAME}] m
    END
END";
    }
}
