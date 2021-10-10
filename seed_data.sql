USE [IdentityServer]
GO
SET IDENTITY_INSERT [dbo].[Scopes] ON 
GO
INSERT [dbo].[Scopes] ([Id], [Scope]) VALUES (1, N'SendSms')
GO
INSERT [dbo].[Scopes] ([Id], [Scope]) VALUES (2, N'SendEmail')
GO
INSERT [dbo].[Scopes] ([Id], [Scope]) VALUES (3, N'SendOrder')
GO
SET IDENTITY_INSERT [dbo].[Scopes] OFF
GO
SET IDENTITY_INSERT [dbo].[ClientApplications] ON 
GO
INSERT [dbo].[ClientApplications] ([Id], [ClientName], [ClientIdentifier], [AllowedGrantType], [CreationDate], [ClientSecret]) VALUES (1, N'gateway api user', N'3X=nN334v?Sgu$S', N'password', CAST(N'2020-08-01T12:57:28.693' AS DateTime), N'1554db43-3015-47a8-a748-55bd76b6af48')
GO
INSERT [dbo].[ClientApplications] ([Id], [ClientName], [ClientIdentifier], [AllowedGrantType], [CreationDate], [ClientSecret]) VALUES (4, N'gateway api application', N'S&^J=asbd73', N'client_credentials', CAST(N'2020-08-01T18:14:54.390' AS DateTime), N'2B00E525-72D9-4310-B7FD-D828AD6F30AE')
GO
SET IDENTITY_INSERT [dbo].[ClientApplications] OFF
GO
INSERT [dbo].[ClientAppsAllowedScopes] ([ClientId], [AllowedScopeId]) VALUES (1, 1)
GO
INSERT [dbo].[ClientAppsAllowedScopes] ([ClientId], [AllowedScopeId]) VALUES (1, 2)
GO
INSERT [dbo].[ClientAppsAllowedScopes] ([ClientId], [AllowedScopeId]) VALUES (4, 1)
GO
INSERT [dbo].[ClientAppsAllowedScopes] ([ClientId], [AllowedScopeId]) VALUES (4, 2)
GO
SET IDENTITY_INSERT [dbo].[ApiResources] ON 
GO
INSERT [dbo].[ApiResources] ([Id], [Name], [Description], [SecretKey]) VALUES (3, N'mailgatewayapi', N'mail gateway api', N'asdasdasuhdiasbidasbidbasidbiasbidasbidbasidbaisd@*#&@$HG*RBWBF*BCSBVDSD')
GO
INSERT [dbo].[ApiResources] ([Id], [Name], [Description], [SecretKey]) VALUES (5, N'Test', N'Test test', N'asdasfnasijdnaisndiasndinasidnasd')
GO
SET IDENTITY_INSERT [dbo].[ApiResources] OFF
GO
INSERT [dbo].[ApiResourceScopes] ([ApiResourceId], [ScopeId]) VALUES (3, 1)
GO
INSERT [dbo].[ApiResourceScopes] ([ApiResourceId], [ScopeId]) VALUES (3, 2)
GO
INSERT [dbo].[ApiResourceScopes] ([ApiResourceId], [ScopeId]) VALUES (5, 3)
GO
INSERT [dbo].[Users] ([UserId], [Username], [IsActive], [FirstName], [LastName], [Password], [PasswordSalt]) VALUES (N'1de86746-b9e7-488e-9b78-779eb232f239', N'sjafarianm', 1, N'Saeed', N'Jafarian', N'ABlML8KF4jMtbYgDe9pPrjT+sEJHk5QmrOWGixqE+QjXOjoT9VZzHJvhDp4Ozf1EiEqs6NMGLDILgDPCZN+gRP8OASv6fLRHnRPZQf4518rtTFd6boxJL2EJYSZkxUE70YoH+Pn5mG58d34LPzERlGG2u0kUimqehfcclXqUIKfwRr7r', N'RUNTNUIAAAAATw8UN/PV2VqLXCE7L68fMRuh4u1CgDIz5XHPYgXYkGwlS+grSW2zvh6ykA2iwxQk3wk+Zuc4ztSBBpr/yg/cL5cAMskDbdERsUP10aYjYAdzYKszuJvzwc5ZoUKNIsHSgmrjnoSRVASy27o4WjKwSu7+Zje7d8gmRRxIjoo/CA7PXCc=')
GO
