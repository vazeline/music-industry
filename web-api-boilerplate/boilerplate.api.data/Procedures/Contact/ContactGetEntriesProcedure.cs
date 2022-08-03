using boilerplate.api.core.Models;
using boilerplate.api.data.Models;

namespace boilerplate.api.data.Procedures
{
    [Procedure]
    public static class ContactGetEntriesProcedure
    {
        public static string Name => "procContactGetEntries";
        public static int Version => 7;
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
    p.[{nameof(Contact.Id)}] AS [{nameof(ContactReportModel.Id)}],
    p.[{nameof(Contact.FirstName)}] AS [{nameof(ContactReportModel.FirstName)}],
    p.[{nameof(Contact.DateCreated)}] AS [{nameof(ContactReportModel.DateCreated)}],
    p.[{nameof(Contact.DateModified)}] AS [{nameof(ContactReportModel.DateModified)}],
    p.[{nameof(Contact.LastName)}] AS [{nameof(ContactReportModel.LastName)}],
    p.[{nameof(Contact.Email)}] AS [{nameof(ContactReportModel.Email)}],
    p.[{nameof(Contact.AddressLine1)}] AS [{nameof(ContactReportModel.AddressLine1)}],
    p.[{nameof(Contact.AddressLine2)}] AS [{nameof(ContactReportModel.AddressLine2)}],
    p.[{nameof(Contact.Fax)}] AS [{nameof(ContactReportModel.Fax)}],
    p.[{nameof(Contact.PhoneBusiness)}] AS [{nameof(ContactReportModel.PhoneBusiness)}],
    p.[{nameof(Contact.PhoneCell)}] AS [{nameof(ContactReportModel.PhoneCell)}],
    p.[{nameof(Contact.State)}] AS [{nameof(ContactReportModel.State)}],
    p.[{nameof(Contact.Title)}] AS [{nameof(ContactReportModel.Title)}],
    p.[{nameof(Contact.City)}] AS [{nameof(ContactReportModel.City)}],
    p.[{nameof(Contact.Company)}] AS [{nameof(ContactReportModel.Company)}],
    p.[{nameof(Contact.Zip)}] AS [{nameof(ContactReportModel.Zip)}],
    p.[{nameof(Contact.IsActive)}] AS [{nameof(ContactReportModel.IsActive)}],
    (select count(*) FROM [PlatformContacts] where ContactId=p.{nameof(Contact.Id)}) PlatformCount,
    (select count(*) FROM [MusicLabelContacts] where ContactId=p.{nameof(Contact.Id)}) MusicLabelCount,
    (select count(*) FROM [MusicianContacts] where ContactId=p.{nameof(Contact.Id)}) MusicianCount
    FROM [{ContactExtension.TABLE_NAME}] p
    WHERE {ProcedureParams.Id} IS NULL OR p.[{nameof(Contact.Id)}] = {ProcedureParams.Id}
    ORDER BY p.[{nameof(Contact.Id)}]
    OFFSET {ProcedureParams.Offset} ROWS FETCH NEXT {ProcedureParams.Limit} ROWS ONLY
    
    IF {ProcedureParams.Id} IS NULL
    BEGIN
        SELECT COUNT(*)
        FROM [{ContactExtension.TABLE_NAME}] p
    END
END";
    }
}
