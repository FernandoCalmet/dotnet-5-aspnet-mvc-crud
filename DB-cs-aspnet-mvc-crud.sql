/****** Developed by: Fernando Calmet Ramirez <fercalmet@gmail.com> ******/
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'cs-aspnet-mvc-crud')
BEGIN
	CREATE DATABASE [cs-aspnet-mvc-crud]
END
GO
USE [cs-aspnet-mvc-crud]
GO
/****** Object:  Table [dbo].[module]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[module_category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [nvarchar](100) NULL,
 CONSTRAINT [PK_module_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[module]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[module](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [nvarchar](100) NULL,
	[module_category_id] [int] NOT NULL,
 CONSTRAINT [PK_module] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[note]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[note](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](100) NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_note] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[task]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[task](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](100) NULL,
	[status] [int] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_task] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[task_note]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[task_note](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[task_id] [int] NOT NULL,
	[note_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_task_note] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](16) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[email_confirmed] [bit] NULL,
	[password_hash] [nvarchar](MAX) NOT NULL,
	[security_stamp] [nvarchar](MAX) NULL,
	[two_factor_enabled] [bit] NOT NULL,
	[lockout_end_date_utc] [datetime] NULL,
	[lockout_enabled] [bit] NOT NULL,
	[access_failed_count] [int] NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[picture] [varbinary](MAX) NULL,
	[birthdate] [date] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[user_position_id] [int] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_action]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_action](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[module_id] [int] NOT NULL,
 CONSTRAINT [PK_user_action] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_permission]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_permission](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_position_id] [int] NOT NULL,
	[user_action_id] [int] NOT NULL,
 CONSTRAINT [PK_user_permission] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_position]    Script Date: 18/10/2021 13:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_position](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](100) NULL,
 CONSTRAINT [PK_user_position] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[task_note]  WITH CHECK ADD  CONSTRAINT [FK_task_note_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[task_note] CHECK CONSTRAINT [FK_task_note_user]
GO

ALTER TABLE [dbo].[task_note]  WITH CHECK ADD  CONSTRAINT [FK_task_note_note] FOREIGN KEY([note_id])
REFERENCES [dbo].[note] ([id])
GO
ALTER TABLE [dbo].[task_note] CHECK CONSTRAINT [FK_task_note_note]
GO

ALTER TABLE [dbo].[task_note]  WITH CHECK ADD  CONSTRAINT [FK_task_note_task] FOREIGN KEY([task_id])
REFERENCES [dbo].[task] ([id])
GO
ALTER TABLE [dbo].[task_note] CHECK CONSTRAINT [FK_task_note_task]
GO

ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_user_position] FOREIGN KEY([user_position_id])
REFERENCES [dbo].[user_position] ([id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_user_position]
GO

ALTER TABLE [dbo].[user_action]  WITH CHECK ADD  CONSTRAINT [FK_user_action_module] FOREIGN KEY([module_id])
REFERENCES [dbo].[module] ([id])
GO
ALTER TABLE [dbo].[user_action] CHECK CONSTRAINT [FK_user_action_module]
GO

ALTER TABLE [dbo].[user_permission]  WITH CHECK ADD  CONSTRAINT [FK_user_permission_user_action] FOREIGN KEY([user_action_id])
REFERENCES [dbo].[user_action] ([id])
GO
ALTER TABLE [dbo].[user_permission] CHECK CONSTRAINT [FK_user_permission_user_action]
GO

ALTER TABLE [dbo].[user_permission]  WITH CHECK ADD  CONSTRAINT [FK_user_permission_user_position] FOREIGN KEY([user_position_id])
REFERENCES [dbo].[user_position] ([id])
GO
ALTER TABLE [dbo].[user_permission] CHECK CONSTRAINT [FK_user_permission_user_position]
GO

ALTER TABLE [dbo].[module]  WITH CHECK ADD  CONSTRAINT [FK_module_module_category] FOREIGN KEY([module_category_id])
REFERENCES [dbo].[module_category] ([id])
GO
ALTER TABLE [dbo].[module] CHECK CONSTRAINT [FK_module_module_category]
GO