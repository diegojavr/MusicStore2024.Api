IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227221343_v1.0.0')
BEGIN
    CREATE TABLE [Genre] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Genre] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227221343_v1.0.0')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240227221343_v1.0.0', N'7.0.18');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227232509_v2.0.0')
BEGIN
    ALTER TABLE [Genre] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227232509_v2.0.0')
BEGIN
    ALTER TABLE [Genre] ADD [ModifiedDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227232509_v2.0.0')
BEGIN
    ALTER TABLE [Genre] ADD [Status] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227232509_v2.0.0')
BEGIN
    CREATE TABLE [Concert] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [Place] nvarchar(80) NOT NULL,
        [UnitPrice] decimal(11,2) NOT NULL,
        [GenreId] int NOT NULL,
        [DateEvent] datetime NOT NULL,
        [ImageUrl] varchar(1000) NULL,
        [TicketsQuantity] int NOT NULL,
        [Finalized] bit NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_Concert] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Concert_Genre_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227232509_v2.0.0')
BEGIN
    CREATE INDEX [IX_Concert_GenreId] ON [Concert] ([GenreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240227232509_v2.0.0')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240227232509_v2.0.0', N'7.0.18');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240417181503_v3.0.0')
BEGIN
    CREATE TABLE [Customer] (
        [Id] int NOT NULL IDENTITY,
        [Email] varchar(200) NOT NULL,
        [FullName] nvarchar(200) NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_Customer] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240417181503_v3.0.0')
BEGIN
    CREATE TABLE [Sale] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [ConcertId] int NOT NULL,
        [SaleDate] date NOT NULL DEFAULT (GETDATE()),
        [OperationNumber] varchar(20) NOT NULL,
        [Total] decimal(11,2) NOT NULL,
        [Quantity] smallint NOT NULL,
        [CreationDate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NULL,
        [Status] bit NOT NULL,
        CONSTRAINT [PK_Sale] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Sale_Concert_ConcertId] FOREIGN KEY ([ConcertId]) REFERENCES [Concert] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Sale_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240417181503_v3.0.0')
BEGIN
    CREATE INDEX [IX_Sale_ConcertId] ON [Sale] ([ConcertId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240417181503_v3.0.0')
BEGIN
    CREATE INDEX [IX_Sale_CustomerId] ON [Sale] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240417181503_v3.0.0')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240417181503_v3.0.0', N'7.0.18');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [Rol] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Rol] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [Usuario] (
        [Id] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [Age] int NOT NULL,
        [DocumentType] int NOT NULL,
        [DocumentNumber] nvarchar(100) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [UsuarioRol] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [NormalizedName] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_UsuarioRol] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_Rol_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Rol] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_Rol_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Rol] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_Usuario_UserId] FOREIGN KEY ([UserId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Rol] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    CREATE INDEX [EmailIndex] ON [Usuario] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Usuario] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240423005204_v4.0.0')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240423005204_v4.0.0', N'7.0.18');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240510172341_SqlReportes')
BEGIN
    CREATE PROCEDURE uspReportSales (@DateStart DATE, @DateEnd DATE)
                    as
                    BEGIN
                    	SELECT 
                    		C.Title ConcertName,
                    		SUM(S.Total) AS Total
                    	FROM Sale S(NOLOCK)
                    	INNER JOIN Concert C (NOLOCK) ON S.ConcertId = C.Id
                    	AND C.[Status] =1
                    	AND S.SaleDate BETWEEN @DateStart AND @DateEnd
                    	GROUP BY C.Title
                    	ORDER BY 2 DESC


                    END
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240510172341_SqlReportes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240510172341_SqlReportes', N'7.0.18');
END;
GO

COMMIT;
GO

