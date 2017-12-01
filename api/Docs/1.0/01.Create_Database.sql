-----------------------------------------------------创建数据库脚本开始-----------------------------------------------------
EXECUTE ('CREATE DATABASE WIKI
ON 
( NAME = WIKI_dat,
    FILENAME = ''D:\data\WIKI.mdf'',
    SIZE = 50,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10 )
LOG ON
( NAME = WIKI_log,
    FILENAME = ''D:\data\WIKI.ldf'',
    SIZE = 10,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10 )'
);
GO
ALTER DATABASE WIKI COLLATE Chinese_PRC_CI_AS;
GO
--创建帐号
CREATE LOGIN wiki WITH PASSWORD = 'unBDWvL2SlpR5Y_fROB' ,DEFAULT_DATABASE = WIKI,CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
GO

USE [WIKI];

/****** Object:  User [wiki]    Script Date: 2017/11/30 9:29:00 ******/
CREATE USER [wiki] FOR LOGIN [wiki] WITH DEFAULT_SCHEMA=[dbo];
EXEC sp_addrolemember N'db_owner', N'wiki'
Go

-----------------------------------------------------创建数据库脚本结束-----------------------------------------------------