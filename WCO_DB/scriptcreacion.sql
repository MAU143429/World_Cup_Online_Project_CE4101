USE [master]
GO
/****** Object:  Database [WCODB]    Script Date: 17/11/2022 11:24:32 ******/
CREATE DATABASE [WCODB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WCODB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WCODB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WCODB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\WCODB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WCODB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WCODB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WCODB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WCODB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WCODB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WCODB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WCODB] SET ARITHABORT OFF 
GO
ALTER DATABASE [WCODB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WCODB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WCODB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WCODB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WCODB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WCODB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WCODB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WCODB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WCODB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WCODB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WCODB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WCODB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WCODB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WCODB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WCODB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WCODB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WCODB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WCODB] SET RECOVERY FULL 
GO
ALTER DATABASE [WCODB] SET  MULTI_USER 
GO
ALTER DATABASE [WCODB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WCODB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WCODB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WCODB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WCODB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WCODB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'WCODB', N'ON'
GO
ALTER DATABASE [WCODB] SET QUERY_STORE = OFF
GO
USE [WCODB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[nickname] [varchar](30) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[lastName] [varchar](30) NOT NULL,
	[birthdate] [varchar](30) NOT NULL,
	[country] [varchar](128) NOT NULL,
	[password] [varchar](128) NOT NULL,
	[isAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[nickname] ASC,
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bracket]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bracket](
	[b_id] [int] IDENTITY(0,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[tournamentId] [varchar](6) NOT NULL,
 CONSTRAINT [PK_Bracket] PRIMARY KEY CLUSTERED 
(
	[b_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[g_id] [varchar](12) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[tournament_id] [varchar](6) NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[g_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Match]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Match](
	[m_id] [int] IDENTITY(0,1) NOT NULL,
	[startTime] [varchar](50) NOT NULL,
	[date] [varchar](50) NOT NULL,
	[venue] [varchar](50) NOT NULL,
	[scoreT1] [int] NULL,
	[scoreT2] [int] NULL,
	[bracket_id] [int] NOT NULL,
 CONSTRAINT [PK_Match] PRIMARY KEY CLUSTERED 
(
	[m_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Match_Team]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Match_Team](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[team_id] [int] NOT NULL,
	[match_id] [int] NOT NULL,
 CONSTRAINT [PK_Match_Team] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[p_id] [int] IDENTITY(0,1) NOT NULL,
	[name] [varchar](50) NULL,
	[team_id] [int] NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[p_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prediction]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prediction](
	[pr_id] [int] IDENTITY(0,1) NOT NULL,
	[goalsT1] [int] NULL,
	[goalsT2] [int] NULL,
	[winner] [int] NULL,
	[points] [float] NULL,
	[player_id] [int] NOT NULL,
	[acc_nick] [varchar](30) NOT NULL,
	[acc_email] [varchar](50) NOT NULL,
	[match_id] [int] NOT NULL,
 CONSTRAINT [PK_Prediction] PRIMARY KEY CLUSTERED 
(
	[pr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scores_Assists]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scores_Assists](
	[player_id] [int] NOT NULL,
	[prediction_id] [int] NOT NULL,
	[assists] [int] NULL,
	[goals] [int] NULL,
 CONSTRAINT [PK_Scores_Assists] PRIMARY KEY CLUSTERED 
(
	[player_id] ASC,
	[prediction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[te_id] [int] IDENTITY(0,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[type] [varchar](50) NOT NULL,
	[tournamentId] [varchar](6) NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[te_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tournament]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tournament](
	[to_id] [varchar](6) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[startDate] [varchar](50) NOT NULL,
	[endDate] [varchar](50) NOT NULL,
	[description] [varchar](1100) NOT NULL,
	[type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tournament] PRIMARY KEY CLUSTERED 
(
	[to_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tournament_Account_S]    Script Date: 17/11/2022 11:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tournament_Account_S](
	[t_id] [varchar](6) NOT NULL,
	[acc_nick] [varchar](30) NOT NULL,
	[acc_email] [varchar](50) NOT NULL,
	[points] [float] NULL,
	[group_id] [varchar](12) NULL,
 CONSTRAINT [PK_Tournament_Account_S] PRIMARY KEY CLUSTERED 
(
	[t_id] ASC,
	[acc_nick] ASC,
	[acc_email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bracket]  WITH CHECK ADD  CONSTRAINT [FK_Bracket_Tournament] FOREIGN KEY([tournamentId])
REFERENCES [dbo].[Tournament] ([to_id])
GO
ALTER TABLE [dbo].[Bracket] CHECK CONSTRAINT [FK_Bracket_Tournament]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Tournament] FOREIGN KEY([tournament_id])
REFERENCES [dbo].[Tournament] ([to_id])
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Tournament]
GO
ALTER TABLE [dbo].[Match]  WITH CHECK ADD  CONSTRAINT [FK_Match_Bracket] FOREIGN KEY([bracket_id])
REFERENCES [dbo].[Bracket] ([b_id])
GO
ALTER TABLE [dbo].[Match] CHECK CONSTRAINT [FK_Match_Bracket]
GO
ALTER TABLE [dbo].[Match_Team]  WITH CHECK ADD  CONSTRAINT [FK_Match_Team_Match] FOREIGN KEY([match_id])
REFERENCES [dbo].[Match] ([m_id])
GO
ALTER TABLE [dbo].[Match_Team] CHECK CONSTRAINT [FK_Match_Team_Match]
GO
ALTER TABLE [dbo].[Match_Team]  WITH CHECK ADD  CONSTRAINT [FK_Match_Team_Team] FOREIGN KEY([team_id])
REFERENCES [dbo].[Team] ([te_id])
GO
ALTER TABLE [dbo].[Match_Team] CHECK CONSTRAINT [FK_Match_Team_Team]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Player_Team] FOREIGN KEY([team_id])
REFERENCES [dbo].[Team] ([te_id])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Team]
GO
ALTER TABLE [dbo].[Prediction]  WITH CHECK ADD  CONSTRAINT [FK_Prediction_Account] FOREIGN KEY([acc_nick], [acc_email])
REFERENCES [dbo].[Account] ([nickname], [email])
GO
ALTER TABLE [dbo].[Prediction] CHECK CONSTRAINT [FK_Prediction_Account]
GO
ALTER TABLE [dbo].[Prediction]  WITH CHECK ADD  CONSTRAINT [FK_Prediction_Match] FOREIGN KEY([match_id])
REFERENCES [dbo].[Match] ([m_id])
GO
ALTER TABLE [dbo].[Prediction] CHECK CONSTRAINT [FK_Prediction_Match]
GO
ALTER TABLE [dbo].[Prediction]  WITH CHECK ADD  CONSTRAINT [FK_Prediction_Player] FOREIGN KEY([player_id])
REFERENCES [dbo].[Player] ([p_id])
GO
ALTER TABLE [dbo].[Prediction] CHECK CONSTRAINT [FK_Prediction_Player]
GO
ALTER TABLE [dbo].[Scores_Assists]  WITH CHECK ADD  CONSTRAINT [FK_Scores_Assists_Player] FOREIGN KEY([player_id])
REFERENCES [dbo].[Player] ([p_id])
GO
ALTER TABLE [dbo].[Scores_Assists] CHECK CONSTRAINT [FK_Scores_Assists_Player]
GO
ALTER TABLE [dbo].[Scores_Assists]  WITH CHECK ADD  CONSTRAINT [FK_Scores_Assists_Prediction] FOREIGN KEY([prediction_id])
REFERENCES [dbo].[Prediction] ([pr_id])
GO
ALTER TABLE [dbo].[Scores_Assists] CHECK CONSTRAINT [FK_Scores_Assists_Prediction]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Tournament] FOREIGN KEY([tournamentId])
REFERENCES [dbo].[Tournament] ([to_id])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Tournament]
GO
ALTER TABLE [dbo].[Tournament_Account_S]  WITH CHECK ADD  CONSTRAINT [FK_Tournament_Account_S_Account] FOREIGN KEY([acc_nick], [acc_email])
REFERENCES [dbo].[Account] ([nickname], [email])
GO
ALTER TABLE [dbo].[Tournament_Account_S] CHECK CONSTRAINT [FK_Tournament_Account_S_Account]
GO
ALTER TABLE [dbo].[Tournament_Account_S]  WITH CHECK ADD  CONSTRAINT [FK_Tournament_Account_S_Tournament] FOREIGN KEY([t_id])
REFERENCES [dbo].[Tournament] ([to_id])
GO
ALTER TABLE [dbo].[Tournament_Account_S] CHECK CONSTRAINT [FK_Tournament_Account_S_Tournament]
GO
USE [master]
GO
ALTER DATABASE [WCODB] SET  READ_WRITE 
GO
