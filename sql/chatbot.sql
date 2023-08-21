CREATE TABLE AspNetUsers(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(250),
    AccessFailedCount INT,
    ConcurrencyStamp NVARCHAR(MAX),
    FirstName VARCHAR(250),
    LastName VARCHAR(250),
    Email NVARCHAR(250),
    EmailConfirmed BIT,
    LockoutEnabled BIT,
    PhoneNumberConfirmed BIT,
    TwoFactorEnabled BIT,
    LockoutEnd DATETIMEOFFSET,
    NormalizedEmail NVARCHAR(250),
    NormalizedUserName NVARCHAR(250),
    PasswordHash NVARCHAR(MAX),
    PhoneNumber  NVARCHAR(50),
    SecurityStamp NVARCHAR(250),
)
CREATE TABLE AspNetRoles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ConcurrencyStamp NVARCHAR(MAX),
    [Name] NVARCHAR(250),
    NormalizedName NVARCHAR(250)
)

CREATE TABLE AspNetRoleClaims (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ConcurrencyStamp NVARCHAR(MAX),
    [ClaimType] NVARCHAR(250),
    [ClaimValue] NVARCHAR(250),
    RoleId INT
)

CREATE TABLE AspNetUserClaims (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ConcurrencyStamp NVARCHAR(MAX),
    [ClaimType] NVARCHAR(250),
    [ClaimValue] NVARCHAR(250),
    UserId INT
)

CREATE TABLE AspNetUserLogins (
    [ProviderDisplayName] NVARCHAR(MAX),
    [LoginProvider] NVARCHAR(250),
    [ProviderKey] NVARCHAR(250),
    UserId INT,
    PRIMARY KEY (LoginProvider, ProviderKey)
)

CREATE TABLE AspNetUserRoles (
    UserId INT,
    RoleId INT,
    PRIMARY KEY (UserId, RoleId)
)

CREATE TABLE AspNetUserTokens (
    UserId INT,
    [LoginProvider] NVARCHAR(250),
    [Name] NVARCHAR(250),
    [Value] NVARCHAR(250),
    PRIMARY KEY (UserId, LoginProvider, Name)
)

CREATE TABLE ChatBotMessage (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Message] VARCHAR(MAX),
    CreatedAt DATETIMEOFFSET,
    UserId INT FOREIGN KEY REFERENCES AspNetUsers(Id)
)