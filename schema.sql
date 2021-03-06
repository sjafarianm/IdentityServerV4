USE [IdentityServer]
GO
/****** Object:  Table [dbo].[ApiResources]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiResources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[SecretKey] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_ApiResources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApiResourceScopes]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiResourceScopes](
	[ApiResourceId] [int] NOT NULL,
	[ScopeId] [int] NOT NULL,
 CONSTRAINT [PK_ApiResourceScopes] PRIMARY KEY CLUSTERED 
(
	[ApiResourceId] ASC,
	[ScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientApplications]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientApplications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientName] [varchar](100) NOT NULL,
	[ClientIdentifier] [varchar](100) NOT NULL,
	[AllowedGrantType] [varchar](100) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ClientSecret] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_ClientApplications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientAppsAllowedScopes]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientAppsAllowedScopes](
	[ClientId] [int] NOT NULL,
	[AllowedScopeId] [int] NOT NULL,
 CONSTRAINT [PK_ClientAppsAllowedScopes_1] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC,
	[AllowedScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scopes]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scopes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Scope] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Scopes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FirstName] [nvarchar](250) NOT NULL,
	[LastName] [nvarchar](400) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[PasswordSalt] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserScopes]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserScopes](
	[UserId] [uniqueidentifier] NOT NULL,
	[ScopeId] [int] NOT NULL,
 CONSTRAINT [PK_UserScopes] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientApplications] ADD  CONSTRAINT [DF_ClientApplications_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[ClientApplications] ADD  CONSTRAINT [DF_ClientApplications_ClientSecret]  DEFAULT (newid()) FOR [ClientSecret]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ApiResourceScopes]  WITH CHECK ADD  CONSTRAINT [FK_ApiResourceScopes_ApiResources] FOREIGN KEY([ApiResourceId])
REFERENCES [dbo].[ApiResources] ([Id])
GO
ALTER TABLE [dbo].[ApiResourceScopes] CHECK CONSTRAINT [FK_ApiResourceScopes_ApiResources]
GO
ALTER TABLE [dbo].[ApiResourceScopes]  WITH CHECK ADD  CONSTRAINT [FK_ApiResourceScopes_Scopes] FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scopes] ([Id])
GO
ALTER TABLE [dbo].[ApiResourceScopes] CHECK CONSTRAINT [FK_ApiResourceScopes_Scopes]
GO
ALTER TABLE [dbo].[ClientAppsAllowedScopes]  WITH CHECK ADD  CONSTRAINT [FK_ClientAppsAllowedScopes_ClientApplications] FOREIGN KEY([ClientId])
REFERENCES [dbo].[ClientApplications] ([Id])
GO
ALTER TABLE [dbo].[ClientAppsAllowedScopes] CHECK CONSTRAINT [FK_ClientAppsAllowedScopes_ClientApplications]
GO
ALTER TABLE [dbo].[ClientAppsAllowedScopes]  WITH CHECK ADD  CONSTRAINT [FK_ClientAppsAllowedScopes_Scopes] FOREIGN KEY([AllowedScopeId])
REFERENCES [dbo].[Scopes] ([Id])
GO
ALTER TABLE [dbo].[ClientAppsAllowedScopes] CHECK CONSTRAINT [FK_ClientAppsAllowedScopes_Scopes]
GO
ALTER TABLE [dbo].[UserScopes]  WITH CHECK ADD  CONSTRAINT [FK_UserScopes_Scopes] FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scopes] ([Id])
GO
ALTER TABLE [dbo].[UserScopes] CHECK CONSTRAINT [FK_UserScopes_Scopes]
GO
ALTER TABLE [dbo].[UserScopes]  WITH CHECK ADD  CONSTRAINT [FK_UserScopes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserScopes] CHECK CONSTRAINT [FK_UserScopes_Users]
GO
/****** Object:  StoredProcedure [dbo].[ApiResource_GetAll]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[ApiResource_GetAll]
	
AS
BEGIN
	select ar.*, s.Id as ScopeId,s.Scope as Scope from ApiResources ar inner join ApiResourceScopes ars on ar.Id=ars.ApiResourceId inner join Scopes s on s.Id=ars.ScopeId
END
GO
/****** Object:  StoredProcedure [dbo].[ClientApplications_GetAll]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ClientApplications_GetAll]
	
AS
BEGIN
	select ca.*, s.Id as ScopeId,s.Scope as Scope from ClientApplications ca inner join ClientAppsAllowedScopes caas on ca.Id=caas.ClientId inner join Scopes s on s.Id=caas.AllowedScopeId
END
GO
/****** Object:  StoredProcedure [dbo].[Scopes_GetAll]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Scopes_GetAll]
	
AS
BEGIN
	Select * from Scopes
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Add]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_Add]
	@Username varchar(256),
	@UserId uniqueidentifier,
	@FirstName varchar(250),
	@LastName varchar(400),
	@Password varchar(1000),
	@PasswordSalt varchar(1000)
AS
BEGIN
	insert into Users(Username,UserId,FirstName,LastName,[Password],PasswordSalt) values (@Username,@UserId,@FirstName,@LastName,@Password,@PasswordSalt)
END
GO
/****** Object:  StoredProcedure [dbo].[Users_DeleteByUserId]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_DeleteByUserId]
	@userId uniqueidentifier
AS
BEGIN
	delete Users where UserId=@userId
END
GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserByUserId]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserByUserId]
	@userId uniqueidentifier
AS
BEGIN
	select * from Users u where u.UserId=@userId
END
GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserByUsername]    Script Date: 10/9/2021 3:08:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Users_GetUserByUsername]
	@username varchar(256)
AS
BEGIN
	select * from Users u where u.Username=@username
END
GO
