# music-industry

Goal: the goal of this challenge is to create a Music Industry Contacts Management .NET Core application composed of API and UI parts and SQL database.


Task:

Design an application composed of 2 solutions:

.NET Core WebAPI - an API directly interacting with the database 

The application should use the MSSQL Server database.

You can use EF Core, Dapper or ADO.NET - the key point is the effectiveness and reliability of the final solution.

It is preferable though, that the EF-core code-first approach is taken

We'd prefer a RESTful API design.

ASP.NET Core MVC WebApp - UI application

Database requirements:

The database should contain the following entities:

MusicLabel 

Id (int, PK)

Name (string, length 50, not null, default value "")

DTC - date created (DateTimeOffset, with default value)

DTU - date updated (DateTimeOffset, with default value)

Musician

Id (int, PK)

Name (string, length 50, not null, default value "")

DTC - date created (DateTimeOffset, with default value)

DTU - date updated (DateTimeOffset, with default value)

Platform

Id (int, PK)

Name (string, length 50, not null, default value "")

DTC - date created (DateTimeOffset, with default value)

DTU - date updated (DateTimeOffset, with default value)

Contact:

Id (int, PK)

FirstName (string, length 50, not null, default value "")

LastName (string, length 50, not null, default value "")

Title (string, length 50, not null, default value "")

Company (string, length 150, not null, default value "")

Email (string, length 250, not null, default value "")

PhoneCell (string, length 50, not null, default value "")

PhoneBusiness (string, length 50, not null, default value "")

Fax (string, length 50, not null, default value "")

AddressLine1 (string, length 50, not null, default value "")

AddressLine2 (string, length 50, not null, default value "")

City (string, length 50, not null, default value "")

State (string, length 50, not null, default value "")

Zip (string, length 15, not null, default value "")

IsActive (bool)

DateCreated (DateTimeOffset, with default value)

DateModified (DateTimeOffset, with default value)

MusicLabelContacts:

Id (int, PK)

DateCreated (DateTimeOffset)

MusicLabelId (int, FK)

ContactId (int, FK)

MusicianContacts:

Id (int, PK)

DateCreated (DateTimeOffset)

MusicianId (int, FK)

ContactId (int, FK)

PlatformContacts:

Id (int, PK)

DateCreated (DateTimeOffset)

PlatformId (int, FK)

ContactId (int, FK)

Notes:

Each contact can have an unlimited count of relations to Music labels, musicians, and platforms. But at least one relation is required when creating a new contact (it is expected to use batch operations to modify data)

Contact???s First name and Last name are required fields

At least one of the following must be specified when creating a contact: Email, Mobile, Office, Fax

UI requirements:

The UI should contain screens for listing, creating/editing contacts.

1) Contacts list:
![8acfe45f-e859-4381-b244-d3d45343625e](https://user-images.githubusercontent.com/3456280/182712438-0a551c83-f673-4fbe-b57b-8bb2300f5848.png)

2) Create Contact: 
![31140ff8-6242-4ee2-ac0e-ac4d4eaed60b](https://user-images.githubusercontent.com/3456280/182712359-f2f060c0-9e31-426c-a01e-138904cd4e41.png)

3) Edit Contact:

![193f6cb0-aba1-41cf-b7fa-e68ee49a68a0](https://user-images.githubusercontent.com/3456280/182712009-662cb657-8cfa-47e9-ab1f-015e2d836ae9.png)

Code review: 

API:
1) MusicianContacts/MusicLabelContacts/PlatformContacts
- ?????????? ???????????? ???? ?????????????????????????? ??????????????????????, ???????? Id ???????????? ???????? ?????????????????? ????????????

2) ContactStore.GetEntry
- ?????? ???????????????? ???? NULL ?????? cmodel
- ???? ???????????????????? ?????? ?? ???????????????? cmodel.MusicianContacts
- ???? ?????????????????????? ?????????????? ?????????????? ???? ???????????????? ?????????????? ?? SQL ???????????????? (???????????????? ?????????? ?? ???????????? ????????????????????????)
- ???????????????????????? ?????????????????? ContactGetEntriesProcedure ???????????????????? ???????????????????? ???????????? ?????? ?????????????? ???????????????? (PlatformCount/MusicLabelCount/MusicianCount)

3) ContactGetEntriesProcedure
- ???? ?????????????????????? ?????????????? ?????????????? ???? ???????????????? ?????????????? ?? SQL ???????????????? (???????????????? ?????????? ?? ???????????? ????????????????????????)

4) ContactStore.UpdateEntry
- ???????????????? ?????????????? "var mappedEntry = MapUpdateModel<ContactUpdateModel, int>(request) as Contact;", ?????????? Generic, ???????? ?????? ???????????? ?????? ?????????????????
- ???????????? ???????????????????? ???????? "var existingEntry = await Context.Contacts.FindAsync(mappedEntry.Id).ConfigureAwait(false) as Contact;"

5) ContactService.GetReferenceEntries
- ???????????? ?????????????? ?????????? ?? ??????, ?????????? SQL ?????????????????? ???????????? ????????????????????????????

6) BaseStore.GetEntries
- ???????????????????????? ??????????????????

7) ContactGetRefEntriesProcedure
- ???? ?????????????????????? ?????????????? ?????????????? ???? ???????????????? ?????????????? ?? SQL ???????????????? (???????????????? ?????????? ?? ?????????? ????????????????????????)
- ?????????? ???????????????????? ?? ???????????

8) ?????????????????? ContactGetEntriesProcedure ???????????????????? ???????????????????? ???????????? ?????? ???????????????????? ??????????????????????????

UI:
1) web-ui-boilerplate\nuget.config
- ???????????????????? ?????????????????????? ?????????????????? ???????? ?? ???????????????? ?????????????????? (???????????????? ?? ???????????? ???? ???????????? ????????????)

2) ContactController.GetEntries
- ???????????????????????? ???????????????????????? ???????????? ???????????????? ?????? ?????????????????? (_pagingAppSettings.DefaultDropdownLimit)

3) ContactEntryViewModelBase.Validate
- ?????????????????????? ?????????????????? ?????? "Phone business" (Office, "At least one of the following must be specified when creating a contact: Email, Mobile, Office, Fax"), ???????????? ?????????? ?????????????????????? ??????????

4) ContactService.GetContactItems
- ???? ???????????????????????????? ???????????? ???????????? ??????

5) ContactGetEntriesViewModel.GetTableViewModel
- ???????????????????????????? ???????? MusicLabelCount/MusicianCount/PlatformCount

6) CreateEntry.cshtml/UpdateEntry.cshtml
- ???????? State ?????????????????????? ?????????????????? ?????????? (???? ?????????????? ????????????????)
- ???? ???????????????????????? ?????????????????? ?????????????? ?????????? ?????????? ???????????????????? ?????????????? ?? ???????????????????? ???????????????????? ??????????
- ???????????????????? ?????????????????? ?????????????????? ???? ?????????????? ???? ?????????????????????????? ??????????????
- ???????????????????? ?????????????????? ?????????????????? ???? ?????????????? ???????????????????????????? ???????????? ????????????????????

