USE [db_a8ec2d_zedicus52001]
GO
/****** Object:  Table [dbo].[Adresses]    Script Date: 27.10.2022 14:45:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](90) NOT NULL,
	[City] [nvarchar](90) NOT NULL,
	[Street] [nvarchar](90) NOT NULL,
	[House_Number] [nvarchar](10) NOT NULL,
	[Full_Adress] [nvarchar](250) NULL,
 CONSTRAINT [PK_Adresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Descriptions]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Descriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](2500) NOT NULL,
 CONSTRAINT [PK_Descriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
	[LoginDataId] [int] NOT NULL,
	[Salary] [money] NOT NULL,
	[Avatar] [nvarchar](110) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeesRoles]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeesRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DescriptionId] [int] NOT NULL,
	[UserRoleId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeesRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FIOs]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FIOs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](70) NOT NULL,
	[Last_Name] [nvarchar](70) NOT NULL,
	[Patronymic] [nvarchar](70) NOT NULL,
 CONSTRAINT [PK_FIOs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Path] [nvarchar](110) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Importances]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Importances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DescriptionId] [int] NOT NULL,
 CONSTRAINT [PK_Importances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginData]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](25) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_LoginData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FIO_Id] [int] NOT NULL,
	[Adress_Id] [int] NOT NULL,
	[Birthday] [date] NOT NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone_Numbers]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone_Numbers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Phone_Number] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Phone_Numbers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description_Id] [int] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskConditions]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskConditions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description_Id] [int] NULL,
 CONSTRAINT [PK_TaskConditions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[DescriptionId] [int] NOT NULL,
	[TaskConditionId] [int] NOT NULL,
	[ImportanceId] [int] NOT NULL,
	[TermId] [int] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Terms]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Terms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ToComplete] [datetime] NOT NULL,
 CONSTRAINT [PK_Terms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 27.10.2022 14:45:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Adresses] ON 

INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (1, N'Україна', N'Одеса', N'П.Орлика', N'33', N'Україна Одеська область, місто Одеса, пл. П. Орлика, 33')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (2, N'Україна', N'Кропивницький', N'Львівська', N'5', N'Україна Кіровоградська область, місто Кропивницький, пров. Львівська, 05')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (3, N'Україна', N'Луцьк', N'Хрещатик', N'93', N'Україна Волинська область, місто Луцьк, просп. Хрещатик, 93')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (4, N'Україна', N'Вінниця', N'Б.Грінченка', N'79', N'Україна Вінницька область, місто Вінниця, пров. Б. Грінченка, 79')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (5, N'Україна', N'Черкаси', N'Фізкультури', N'44', N'Україна Черкаська область, місто Черкаси, просп. Фізкультури, 44')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (6, N'Україна', N'Чернігів', N'Лесі Українки', N'32', N'Україна ернігівська область, місто Чернігів, просп. Лесі Українки, 32')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (7, N'Україна', N'Житомир', N'Мельникова', N'44', N'Україна Житомирська область, місто Житомир, пров. Мельникова, 44')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (8, N'Україна', N'Миколаїв', N'М.Коцюбинського', N'94', N'Україна Миколаївська область, місто Миколаїв, вул. М. Коцюбинського, 94')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (9, N'Україна', N'Полтава', N'І.Франка', N'45', N'Україна Полтавська область, місто Полтава, вул. І. Франкa, 45')
INSERT [dbo].[Adresses] ([Id], [Country], [City], [Street], [House_Number], [Full_Adress]) VALUES (10, N'Україна', N'Донецьк', N'Лесі Українки', N'97', N'Україна Донецька область, місто Донецьк, пл. Лесі Українки, 97')
SET IDENTITY_INSERT [dbo].[Adresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Descriptions] ON 

INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (2, N'FixFlex', N'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.

Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante.

Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a libero. Fusce vulputate eleifend sapien. Vestibulum purus quam, scelerisque ut, mollis sed, nonummy id, metus. Nullam accumsan lorem in dui. Cras ultricies mi eu turpis hendrerit fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In ac dui quis mi consectetuer lacinia. Nam pretium turpis et arcu. Duis arcu tortor, suscipit eget, imperdiet nec, imperdiet iaculis, ipsum. Sed aliquam ultrices mauris. Integer ante arcu, accumsan a, consectetuer eget, posuere ut, mauris. Praesent adipiscing. Phasellus ullamcorper ipsum rutrum nunc. Nunc nonummy metus. Vestibulum volutpat pretium libero. Cras id dui. Aenean ut eros et nisl sagittis vestibulum. Nullam nulla eros, ultricies sit amet, nonummy id, imperdiet feugiat, pede. Sed lectus. Donec mollis hendrerit risus. Phasellus nec sem in justo pellentesque facilisis. Etiam imperdiet imperdiet orci')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (3, N'Asoka', N'Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.

Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur? At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.

Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. ')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (4, N'Complete', N'This task is completely ready')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (5, N'Failed', N'This task was overdue or not done')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (6, N'In process', N'This task has been undertaken but not yet completed')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (7, N'Important', N'This is an important task, it is better to do it soon')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (8, N'Secondary', N'Better to do, but not very important')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (9, N'Unimportant', N'This task is best done last')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (10, N'Junior Developer', N'Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there live the blind texts.

Separated they live in Bookmarksgrove right at the coast of the Semantics, a large language ocean. A small river named Duden flows by their place and supplies it with the necessary regelialia.

It is a paradisematic country, in which roasted parts of sentences fly into your mouth. Even the all-powerful Pointing has no control about the blind texts it is an almost unorthographic lif ')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (11, N'Middle Developer ', N'Li Europan lingues es membres del sam familie. Lor separat existentie es un myth. Por scientie, musica, sport etc, litot Europa usa li sam vocabular.

Li lingues differe solmen in li grammatica, li pronunciation e li plu commun vocabules.

Omnicos directe al desirabilite de un nov lingua franca: On refusa continuar payar custosi traductores. At solmen va esser necessi far uniform grammatica, pronunciation e plu sommun paroles. Ma quande lingues coalesce, li grammatica del resultant lingue es plu s ')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (12, N'Senior Developer', N'But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness.

No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful.

Nor again is there anyone who loves or. ')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (13, N'Team Lead', N'The European languages are members of the same family. Their separate existence is a myth.

For science, music, sport, etc, Europe uses the same vocabulary. The languages only differ in their grammar, their pronunciation and their most common words.

Everyone realizes why a new common language would be desirable: one could refuse to pay expensive translators. To achieve this, it would be necessary to have uniform grammar, pronunciation and more common words. If several languages coalesce, the gram ')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (14, N'DB Admin', N'Can interact with the database(CRUD)')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (15, N'Yellow-throated sandgrouse', N'Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (16, N'Gelada baboon', N'Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Proin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl. Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (17, N'Eurasian hoopoe', N'Aenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (18, N'Wallaby', N'Nunc nisl. Duis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus. Duis at velit eu est congue elementum. In hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (19, N'Red meerkat', N'Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis. Fusce posuere felis sed lacus. Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl. Nunc rhoncus dui vel sem. Sed sagittis. Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci. Nullam molestie nibh in lectus. Pellentesque at nulla. Suspendisse potenti. Cras in purus eu magna vulputate luctus.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (20, N'Bat', N'Integer ac neque. Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus. In sagittis dui vel nisl. Duis ac nibh. Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus. Suspendisse potenti.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (21, N'Long-tailed jaeger', N'Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (22, N'Red hartebeest', N'Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis. Fusce posuere felis sed lacus. Morbi sem mauris, laoreet ut, rhoncus aliquet, pulvinar sed, nisl. Nunc rhoncus dui vel sem. Sed sagittis. Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci. Nullam molestie nibh in lectus.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (23, N'Hanuman langur', N'Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus. Curabitur at ipsum ac tellus semper interdum. Mauris ullamcorper purus sit amet nulla. Quisque arcu libero, rutrum ac, lobortis vel, dapibus at, diam.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (24, N'Green heron', N'Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus. Phasellus in felis. Donec semper sapien a libero. Nam dui.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (25, N'Meerkat', N'Aliquam erat volutpat. In congue. Etiam justo. Etiam pretium iaculis justo. In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus. Nulla ut erat id mauris vulputate elementum.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (26, N'Levaillant''s barbet', N'Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (27, N'Hornbill', N'In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem. Duis aliquam convallis nunc. Proin at turpis a pede posuere nonummy.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (28, N'Cormorant', N'Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum. Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est. Phasellus sit amet erat.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (29, N'Malagasy', N'Aenean auctor gravida sem. Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (30, N'Weaver', N'Aenean auctor gravida sem. Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio. Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (31, N'eland', N'Vestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat. In congue. Etiam justo.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (32, N'Chestnut weaver', N'Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero. Nullam sit amet turpis elementum ligula vehicula consequat. Morbi a ipsum.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (33, N'Oriental short-clawed otter', N'Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Etiam vel augue.')
INSERT [dbo].[Descriptions] ([Id], [Title], [Description]) VALUES (34, N'Feathertail glider', N'Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus. In sagittis dui vel nisl.')
SET IDENTITY_INSERT [dbo].[Descriptions] OFF
GO
SET IDENTITY_INSERT [dbo].[Emails] ON 

INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (1, 1, N'ckeavenyb@blogs.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (2, 1, N'nmadgina@mediafire.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (3, 2, N'arutherfoordk@etsy.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (4, 3, N'gfriattl@msn.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (5, 4, N'asemoninm@marketwatch.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (6, 4, N'dcliftn@cnbc.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (7, 5, N'gphilipeauxo@columbia.edu')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (8, 6, N'aairdriev@bloglovin.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (9, 7, N'djaeggiy@deliciousdays.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (10, 8, N'lstegers10@technorati.com')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (11, 9, N'jcarpenter13@chicagotribune.co')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (12, 10, N'mmalecky14@so-net.ne.jp')
INSERT [dbo].[Emails] ([Id], [PersonId], [Email]) VALUES (13, 10, N'hdooley16@ehow.com')
SET IDENTITY_INSERT [dbo].[Emails] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (1, 1, 1, 7, 1, 15000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (2, 2, 1, 8, 2, 15550.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (3, 3, 2, 9, 3, 20000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (4, 4, 2, 10, 4, 22000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (5, 5, 3, 11, 5, 21000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (6, 6, 3, 12, 6, 14000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (7, 7, 1, 13, 7, 45000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (8, 8, 4, 15, 8, 18000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (9, 9, 4, 18, 9, 35000.0000, NULL)
INSERT [dbo].[Employees] ([Id], [PersonId], [RoleId], [TaskId], [LoginDataId], [Salary], [Avatar]) VALUES (10, 10, 5, 16, 10, 24000.0000, NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeesRoles] ON 

INSERT [dbo].[EmployeesRoles] ([Id], [DescriptionId], [UserRoleId]) VALUES (1, 10, 1)
INSERT [dbo].[EmployeesRoles] ([Id], [DescriptionId], [UserRoleId]) VALUES (2, 11, 1)
INSERT [dbo].[EmployeesRoles] ([Id], [DescriptionId], [UserRoleId]) VALUES (3, 12, 6)
INSERT [dbo].[EmployeesRoles] ([Id], [DescriptionId], [UserRoleId]) VALUES (4, 13, 6)
INSERT [dbo].[EmployeesRoles] ([Id], [DescriptionId], [UserRoleId]) VALUES (5, 14, 7)
SET IDENTITY_INSERT [dbo].[EmployeesRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[FIOs] ON 

INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (1, N'Фауст', N'Копиленко', N'Драганович')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (2, N'Арсенія', N'Дергач', N'Ігорівна')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (3, N'Полель', N'Уляницький', N'Арсенович')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (4, N'Цецілія', N'Папроцька', N'Устимівна')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (5, N'Живорід', N'Овсієнко', N'Остапович')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (6, N'Єлизавета', N'Маценко', N'Костянтинівна')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (7, N'Ходота', N'Пилип''юк', N'Олегович')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (8, N'Світовида', N'Демчишин', N'Охримівна')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (9, N'Звенимир', N'Уляницький', N'Юхимович')
INSERT [dbo].[FIOs] ([Id], [First_Name], [Last_Name], [Patronymic]) VALUES (10, N'Циганок', N'Олександр', N'Станіславович')
SET IDENTITY_INSERT [dbo].[FIOs] OFF
GO
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([Id], [ProjectId], [Path]) VALUES (1, 4, N'some path')
INSERT [dbo].[Images] ([Id], [ProjectId], [Path]) VALUES (2, 4, N'some path')
INSERT [dbo].[Images] ([Id], [ProjectId], [Path]) VALUES (3, 4, N'some path')
INSERT [dbo].[Images] ([Id], [ProjectId], [Path]) VALUES (4, 5, N'some path')
INSERT [dbo].[Images] ([Id], [ProjectId], [Path]) VALUES (5, 5, N'some path')
SET IDENTITY_INSERT [dbo].[Images] OFF
GO
SET IDENTITY_INSERT [dbo].[Importances] ON 

INSERT [dbo].[Importances] ([Id], [DescriptionId]) VALUES (1, 7)
INSERT [dbo].[Importances] ([Id], [DescriptionId]) VALUES (2, 8)
INSERT [dbo].[Importances] ([Id], [DescriptionId]) VALUES (3, 9)
SET IDENTITY_INSERT [dbo].[Importances] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginData] ON 

INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (1, N'tavesque0', N'xJTCsLQsPIf7')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (2, N'scorthes3', N'VjrAGdDIp1o9')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (3, N'vcuerdale5', N'g4gvnBe')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (4, N'kthaine6', N'GUWhBuuHN')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (5, N'mwaker7', N'yn7mqHOey')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (6, N'rporcher8', N'364F6Jjp')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (7, N'gbothie9', N'HitVeUoKxz')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (8, N'jbarstowka', N'CeMxjbtf6TZ')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (9, N'qsatyfordb', N'Xw5l6GYHETSf')
INSERT [dbo].[LoginData] ([Id], [Login], [Password]) VALUES (10, N'zedicus', N'qwerty')
SET IDENTITY_INSERT [dbo].[LoginData] OFF
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 

INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (1, 3, 5, CAST(N'1989-10-05' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (2, 1, 2, CAST(N'1994-02-04' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (3, 2, 3, CAST(N'1998-01-05' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (4, 7, 7, CAST(N'1996-10-08' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (5, 9, 8, CAST(N'1989-04-12' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (6, 6, 9, CAST(N'1996-05-16' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (7, 5, 10, CAST(N'1990-09-21' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (8, 4, 1, CAST(N'1999-04-19' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (9, 10, 4, CAST(N'2000-01-02' AS Date))
INSERT [dbo].[Persons] ([Id], [FIO_Id], [Adress_Id], [Birthday]) VALUES (10, 8, 6, CAST(N'2001-12-27' AS Date))
SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
SET IDENTITY_INSERT [dbo].[Phone_Numbers] ON 

INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (2, 1, N'+3805886114878')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (3, 2, N'+3801964813786')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (4, 2, N'+3808862458342')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (5, 3, N'+3801244928620')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (6, 4, N'+3801407085788')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (7, 5, N'+3806769738441')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (8, 5, N'+3801846731891')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (9, 6, N'+3805919795868')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (10, 7, N'+3804656983715')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (11, 8, N'+3809893252985')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (12, 8, N'+3801765242419')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (13, 9, N'+3803097779995')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (14, 10, N'+3803006962426')
INSERT [dbo].[Phone_Numbers] ([Id], [PersonId], [Phone_Number]) VALUES (15, 10, N'+3804216257796')
SET IDENTITY_INSERT [dbo].[Phone_Numbers] OFF
GO
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([Id], [Description_Id]) VALUES (4, 2)
INSERT [dbo].[Projects] ([Id], [Description_Id]) VALUES (5, 3)
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskConditions] ON 

INSERT [dbo].[TaskConditions] ([Id], [Description_Id]) VALUES (1, 4)
INSERT [dbo].[TaskConditions] ([Id], [Description_Id]) VALUES (2, 5)
INSERT [dbo].[TaskConditions] ([Id], [Description_Id]) VALUES (3, 6)
SET IDENTITY_INSERT [dbo].[TaskConditions] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (7, 4, 14, 2, 2, 1)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (8, 4, 15, 2, 2, 2)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (9, 4, 16, 2, 2, 3)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (10, 4, 17, 2, 2, 4)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (11, 4, 18, 2, 2, 5)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (12, 4, 19, 2, 2, 6)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (13, 4, 20, 2, 2, 7)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (14, 5, 21, 2, 2, 8)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (15, 5, 22, 2, 2, 9)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (16, 5, 23, 2, 2, 10)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (17, 4, 24, 2, 1, 11)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (18, 4, 25, 2, 1, 12)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (19, 5, 26, 2, 1, 13)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (20, 5, 27, 2, 3, 14)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (21, 5, 28, 2, 3, 15)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (22, 5, 29, 2, 3, 16)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (23, 5, 30, 2, 3, 17)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (24, 5, 31, 2, 1, 18)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (25, 5, 32, 2, 2, 19)
INSERT [dbo].[Tasks] ([Id], [ProjectId], [DescriptionId], [TaskConditionId], [ImportanceId], [TermId]) VALUES (26, 4, 33, 2, 2, 20)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[Terms] ON 

INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (1, CAST(N'2022-12-12T10:03:18.000' AS DateTime), CAST(N'2023-05-31T19:56:10.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (2, CAST(N'2022-12-02T22:18:59.000' AS DateTime), CAST(N'2023-12-06T09:32:36.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (3, CAST(N'2022-11-17T04:52:35.000' AS DateTime), CAST(N'2023-08-08T21:36:54.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (4, CAST(N'2022-11-13T09:14:44.000' AS DateTime), CAST(N'2023-05-11T07:39:31.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (5, CAST(N'2022-11-09T15:27:55.000' AS DateTime), CAST(N'2023-11-30T22:36:57.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (6, CAST(N'2022-10-28T22:02:22.000' AS DateTime), CAST(N'2023-09-12T21:50:57.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (7, CAST(N'2022-12-21T06:56:10.000' AS DateTime), CAST(N'2023-12-18T11:29:25.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (8, CAST(N'2022-12-01T23:26:59.000' AS DateTime), CAST(N'2023-05-22T18:42:46.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (9, CAST(N'2022-11-02T23:48:16.000' AS DateTime), CAST(N'2023-09-25T09:46:36.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (10, CAST(N'2022-10-31T02:28:58.000' AS DateTime), CAST(N'2023-01-13T07:53:53.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (11, CAST(N'2022-11-15T04:30:36.000' AS DateTime), CAST(N'2023-09-14T11:37:11.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (12, CAST(N'2022-12-19T07:20:54.000' AS DateTime), CAST(N'2023-10-17T02:05:05.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (13, CAST(N'2022-12-06T07:02:27.000' AS DateTime), CAST(N'2023-02-05T14:51:44.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (14, CAST(N'2022-12-03T04:30:19.000' AS DateTime), CAST(N'2023-08-29T06:33:59.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (15, CAST(N'2022-12-20T16:06:51.000' AS DateTime), CAST(N'2022-12-27T22:26:39.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (16, CAST(N'2022-12-08T13:44:48.000' AS DateTime), CAST(N'2023-09-15T06:37:18.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (17, CAST(N'2022-12-20T19:36:00.000' AS DateTime), CAST(N'2023-04-30T10:49:25.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (18, CAST(N'2022-12-10T09:31:13.000' AS DateTime), CAST(N'2023-03-14T05:22:21.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (19, CAST(N'2022-11-05T00:05:20.000' AS DateTime), CAST(N'2023-07-19T11:04:29.000' AS DateTime))
INSERT [dbo].[Terms] ([Id], [CreationDate], [ToComplete]) VALUES (20, CAST(N'2022-11-30T18:22:29.000' AS DateTime), CAST(N'2023-12-02T13:11:50.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Terms] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersRoles] ON 

INSERT [dbo].[UsersRoles] ([Id], [Name]) VALUES (1, N'User')
INSERT [dbo].[UsersRoles] ([Id], [Name]) VALUES (6, N'User+')
INSERT [dbo].[UsersRoles] ([Id], [Name]) VALUES (7, N'Admin')
SET IDENTITY_INSERT [dbo].[UsersRoles] OFF
GO
/****** Object:  Index [IX_Employees]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [IX_Employees] UNIQUE NONCLUSTERED 
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_2]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [IX_Employees_2] UNIQUE NONCLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_3]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [IX_Employees_3] UNIQUE NONCLUSTERED 
(
	[LoginDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeesRoles]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[EmployeesRoles] ADD  CONSTRAINT [IX_EmployeesRoles] UNIQUE NONCLUSTERED 
(
	[DescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Importances]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Importances] ADD  CONSTRAINT [IX_Importances] UNIQUE NONCLUSTERED 
(
	[DescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Persons]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [IX_Persons] UNIQUE NONCLUSTERED 
(
	[FIO_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Persons_1]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [IX_Persons_1] UNIQUE NONCLUSTERED 
(
	[Adress_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Projects] ADD  CONSTRAINT [IX_Projects] UNIQUE NONCLUSTERED 
(
	[Description_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TaskConditions]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[TaskConditions] ADD  CONSTRAINT [IX_TaskConditions] UNIQUE NONCLUSTERED 
(
	[Description_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tasks_1]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [IX_Tasks_1] UNIQUE NONCLUSTERED 
(
	[DescriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tasks_4]    Script Date: 27.10.2022 14:46:05 ******/
ALTER TABLE [dbo].[Tasks] ADD  CONSTRAINT [IX_Tasks_4] UNIQUE NONCLUSTERED 
(
	[TermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Emails]  WITH CHECK ADD  CONSTRAINT [FK_Emails_Persons] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Emails] CHECK CONSTRAINT [FK_Emails_Persons]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeesRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[EmployeesRoles] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_EmployeesRoles]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_LoginData] FOREIGN KEY([LoginDataId])
REFERENCES [dbo].[LoginData] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_LoginData]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Persons] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Persons]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Tasks]
GO
ALTER TABLE [dbo].[EmployeesRoles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeesRoles_Descriptions] FOREIGN KEY([DescriptionId])
REFERENCES [dbo].[Descriptions] ([Id])
GO
ALTER TABLE [dbo].[EmployeesRoles] CHECK CONSTRAINT [FK_EmployeesRoles_Descriptions]
GO
ALTER TABLE [dbo].[EmployeesRoles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeesRoles_UsersRoles] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UsersRoles] ([Id])
GO
ALTER TABLE [dbo].[EmployeesRoles] CHECK CONSTRAINT [FK_EmployeesRoles_UsersRoles]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Projects]
GO
ALTER TABLE [dbo].[Importances]  WITH CHECK ADD  CONSTRAINT [FK_Importances_Descriptions] FOREIGN KEY([DescriptionId])
REFERENCES [dbo].[Descriptions] ([Id])
GO
ALTER TABLE [dbo].[Importances] CHECK CONSTRAINT [FK_Importances_Descriptions]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Adresses] FOREIGN KEY([Adress_Id])
REFERENCES [dbo].[Adresses] ([Id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Adresses]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_FIOs] FOREIGN KEY([FIO_Id])
REFERENCES [dbo].[FIOs] ([Id])
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_FIOs]
GO
ALTER TABLE [dbo].[Phone_Numbers]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Numbers_Persons] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Phone_Numbers] CHECK CONSTRAINT [FK_Phone_Numbers_Persons]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Descriptions] FOREIGN KEY([Description_Id])
REFERENCES [dbo].[Descriptions] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Descriptions]
GO
ALTER TABLE [dbo].[TaskConditions]  WITH CHECK ADD  CONSTRAINT [FK_TaskConditions_Descriptions] FOREIGN KEY([Description_Id])
REFERENCES [dbo].[Descriptions] ([Id])
GO
ALTER TABLE [dbo].[TaskConditions] CHECK CONSTRAINT [FK_TaskConditions_Descriptions]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Descriptions] FOREIGN KEY([DescriptionId])
REFERENCES [dbo].[Descriptions] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Descriptions]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Importances] FOREIGN KEY([ImportanceId])
REFERENCES [dbo].[Importances] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Importances]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projects]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_TaskConditions] FOREIGN KEY([TaskConditionId])
REFERENCES [dbo].[TaskConditions] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_TaskConditions]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Terms] FOREIGN KEY([TermId])
REFERENCES [dbo].[Terms] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Terms]
GO
