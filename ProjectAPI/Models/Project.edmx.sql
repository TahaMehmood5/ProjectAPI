
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/27/2023 17:24:00
-- Generated from EDMX file: E:\ProjectAPI\ProjectAPI\ProjectAPI\Models\Project.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DisciplineCommiteeAssistant];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Appeal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Appeal];
GO
IF OBJECT_ID(N'[dbo].[AssignCommittee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssignCommittee];
GO
IF OBJECT_ID(N'[dbo].[AssignPunishment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssignPunishment];
GO
IF OBJECT_ID(N'[dbo].[Attendance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendance];
GO
IF OBJECT_ID(N'[dbo].[Case]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Case];
GO
IF OBJECT_ID(N'[dbo].[Committee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Committee];
GO
IF OBJECT_ID(N'[dbo].[Meeting]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Meeting];
GO
IF OBJECT_ID(N'[dbo].[Punishment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Punishment];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Appeals'
CREATE TABLE [dbo].[Appeals] (
    [a_id] int  NOT NULL,
    [c_id] int  NULL,
    [St_comments] varchar(max)  NULL,
    [Dr_comments] varchar(max)  NULL,
    [datetime] datetime  NULL
);
GO

-- Creating table 'AssignCommittees'
CREATE TABLE [dbo].[AssignCommittees] (
    [ac_id] int IDENTITY(1,1) NOT NULL,
    [t_id] int  NULL,
    [com_id] int  NULL
);
GO

-- Creating table 'AssignPunishments'
CREATE TABLE [dbo].[AssignPunishments] (
    [ap_id] int IDENTITY(1,1) NOT NULL,
    [st_id] int  NULL,
    [com_id] int  NULL
);
GO

-- Creating table 'Attendances'
CREATE TABLE [dbo].[Attendances] (
    [atd_id] int IDENTITY(1,1) NOT NULL,
    [c_id] int  NULL,
    [datetime] datetime  NULL
);
GO

-- Creating table 'Cases'
CREATE TABLE [dbo].[Cases] (
    [c_id] int IDENTITY(1,1) NOT NULL,
    [t_id] int  NULL,
    [p_id] int  NULL,
    [rb_id] int  NULL,
    [st_id] int  NULL,
    [datetime] datetime  NULL,
    [description] varchar(max)  NULL,
    [image] varchar(50)  NULL,
    [viol_date] datetime  NULL,
    [com_id] int  NULL
);
GO

-- Creating table 'Committees'
CREATE TABLE [dbo].[Committees] (
    [com_id] int IDENTITY(1,1) NOT NULL,
    [title] varchar(50)  NULL
);
GO

-- Creating table 'Meetings'
CREATE TABLE [dbo].[Meetings] (
    [m_id] int IDENTITY(1,1) NOT NULL,
    [c_id] int  NULL,
    [meetingtime] datetime  NULL,
    [venue] varchar(20)  NULL
);
GO

-- Creating table 'Punishments'
CREATE TABLE [dbo].[Punishments] (
    [p_id] int IDENTITY(1,1) NOT NULL,
    [title] varchar(20)  NULL,
    [isfine] int  NULL,
    [no_of_days] int  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [u_id] int IDENTITY(1,1) NOT NULL,
    [username] varchar(50)  NULL,
    [password] varchar(50)  NULL,
    [name] varchar(50)  NULL,
    [image] varchar(50)  NULL,
    [usertype] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [a_id] in table 'Appeals'
ALTER TABLE [dbo].[Appeals]
ADD CONSTRAINT [PK_Appeals]
    PRIMARY KEY CLUSTERED ([a_id] ASC);
GO

-- Creating primary key on [ac_id] in table 'AssignCommittees'
ALTER TABLE [dbo].[AssignCommittees]
ADD CONSTRAINT [PK_AssignCommittees]
    PRIMARY KEY CLUSTERED ([ac_id] ASC);
GO

-- Creating primary key on [ap_id] in table 'AssignPunishments'
ALTER TABLE [dbo].[AssignPunishments]
ADD CONSTRAINT [PK_AssignPunishments]
    PRIMARY KEY CLUSTERED ([ap_id] ASC);
GO

-- Creating primary key on [atd_id] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [PK_Attendances]
    PRIMARY KEY CLUSTERED ([atd_id] ASC);
GO

-- Creating primary key on [c_id] in table 'Cases'
ALTER TABLE [dbo].[Cases]
ADD CONSTRAINT [PK_Cases]
    PRIMARY KEY CLUSTERED ([c_id] ASC);
GO

-- Creating primary key on [com_id] in table 'Committees'
ALTER TABLE [dbo].[Committees]
ADD CONSTRAINT [PK_Committees]
    PRIMARY KEY CLUSTERED ([com_id] ASC);
GO

-- Creating primary key on [m_id] in table 'Meetings'
ALTER TABLE [dbo].[Meetings]
ADD CONSTRAINT [PK_Meetings]
    PRIMARY KEY CLUSTERED ([m_id] ASC);
GO

-- Creating primary key on [p_id] in table 'Punishments'
ALTER TABLE [dbo].[Punishments]
ADD CONSTRAINT [PK_Punishments]
    PRIMARY KEY CLUSTERED ([p_id] ASC);
GO

-- Creating primary key on [u_id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([u_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------