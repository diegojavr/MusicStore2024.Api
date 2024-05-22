SET IDENTITY_INSERT [dbo].[Genre] ON 
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (1, N'Rock', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (2, N'Rock Blues', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (4, N'K-Pop', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (5, N'Salsa', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (6, N'Bachata', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (7, N'Reggeaton', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (8, N'Pop', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (9, N'Metal', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (10, N'Instrumental', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (11, N'Punk', CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Genre] ([Id], [Name], [CreationDate], [ModifiedDate], [Status]) VALUES (12, N'Electronica', CAST(N'2024-04-22T16:46:43.2544266' AS DateTime2), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Genre] OFF
GO
SET IDENTITY_INSERT [dbo].[Concert] ON 
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (1, N'AC/DC', N'Concierto benefico para los refugiados', N'Estadio de River Plate', CAST(299.80 AS Decimal(11, 2)), 1, CAST(N'2023-06-09T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/ACDC.jpg', 5000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (2, N'Avril Lavigne', N'Concierto en conmemoracion de su vigesimo aniversario', N'Guadalajara, MX', CAST(340.00 AS Decimal(11, 2)), 1, CAST(N'2024-10-15T21:00:00.000' AS DateTime), N'https://musictore24.blob.core.windows.net/images/avril.jpg', 5000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), CAST(N'2024-04-22T16:30:21.8979580' AS DateTime2), 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (3, N'Bon Jovi', N'Concierto de Vivo por el Rock, festival de Lima', N'Estadio Monumental', CAST(180.00 AS Decimal(11, 2)), 1, CAST(N'2023-06-30T22:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/bonjovi.jpeg', 3000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (4, N'Britney Spears', N'Concierto de Pop para los noventeros', N'Wimbledon', CAST(455.66 AS Decimal(11, 2)), 8, CAST(N'2023-07-08T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/britney.jpeg', 1000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (5, N'Freddy Mercury', N'Concierto de la legendaria banda Queen', N'Anfield, UK', CAST(200.00 AS Decimal(11, 2)), 1, CAST(N'2023-07-20T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/freddy.jpg', 120, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (6, N'Gilberto Santa Rosa', N'La salsa romántica nunca pasará de moda', N'Puerto Rico', CAST(250.00 AS Decimal(11, 2)), 5, CAST(N'2023-07-08T10:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/gilbertosantarosa.jpg', 1000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (7, N'Green Day', N'Green Day nos demuestra que a pesar de los años, sigue siendo relevante', N'Madison Square Garden, NY', CAST(200.00 AS Decimal(11, 2)), 11, CAST(N'2023-07-21T09:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/greenday.jpg', 5000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (8, N'Adele', N'Concierto para celebrar los grammys ganados', N'Wimbledon', CAST(300.00 AS Decimal(11, 2)), 8, CAST(N'2023-06-30T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/Adele.jpeg', 1000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (9, N'Blackpink', N'Blackpink por primera vez dará su concierto en latinoamerica', N'Coachela', CAST(100.00 AS Decimal(11, 2)), 4, CAST(N'2023-06-10T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/blackpink.jpg', 500, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (10, N'Skillet', N'Skillet demostrará todo su despliegue artístico en un evento especial para los fans', N'New York', CAST(245.00 AS Decimal(11, 2)), 1, CAST(N'2023-07-29T10:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/skillet.jpeg', 5000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (11, N'The Piano Guys', N'Si te gusta la música instrumental, este es tu concierto ideal, no te lo pierdas!', N'Atlanta', CAST(349.99 AS Decimal(11, 2)), 8, CAST(N'2023-11-02T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/PianoGuys.jpeg', 2000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (12, N'Antonio Cartagena', N'Concierto de Salsa en vivo', N'Lima', CAST(100.00 AS Decimal(11, 2)), 5, CAST(N'2023-08-23T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/antoniocartagena.jpeg', 400, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (13, N'Arch Enemy', N'Concierto de Metal melódico para los fanáticos', N'Melbourne', CAST(179.80 AS Decimal(11, 2)), 9, CAST(N'2023-06-10T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/archenemy.jpg', 1000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (14, N'Blink 182', N'Para los fans del punk clásico de los 90', N'Piura, PE', CAST(59.20 AS Decimal(11, 2)), 11, CAST(N'2023-08-14T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/blink182.jpg', 2000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (15, N'Boney M', N'Una de las mejores bandas de los 70''s', N'Moscu, Rusia', CAST(300.00 AS Decimal(11, 2)), 8, CAST(N'2023-09-20T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/boneym.jpeg', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (16, N'BTS', N'La banda que a todas las chicas vuelve locas', N'CMDX', CAST(340.00 AS Decimal(11, 2)), 4, CAST(N'2023-10-31T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/bts.webp', 40000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (17, N'Daddy Yankee', N'La ultima gira mundial del legendario Daddy Yankee', N'San Jose, CR', CAST(299.90 AS Decimal(11, 2)), 7, CAST(N'2023-09-05T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/daddyyankee.webp', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (18, N'Don Omar', N'Este concierto dará mucho que hablar, por primera vez en Bogotá', N'Bogota', CAST(100.00 AS Decimal(11, 2)), 7, CAST(N'2023-06-23T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/donomar.jpg', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (19, N'Ed Sheeran', N'Un concierto romántico a todo dar', N'Londres, UK', CAST(340.00 AS Decimal(11, 2)), 8, CAST(N'2023-08-18T10:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/edsheeran.jpg', 20000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (20, N'Frankie Ruiz', N'El concierto mas esperado por toda latinoamerica unida', N'Quito, Ecuador', CAST(150.00 AS Decimal(11, 2)), 5, CAST(N'2023-08-01T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/frankieruiz.jpg', 5000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (21, N'G-IDLE', N'Para los fanáticos del K-POP llega la banda femenina mas exitosa', N'Seul, Corea del Sur', CAST(200.00 AS Decimal(11, 2)), 4, CAST(N'2023-07-28T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/gidle.jpeg', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (22, N'Juan Luis Guerra', N'El maximo exponente de la bachata en la musica latina', N'Varadero, Cuba', CAST(299.90 AS Decimal(11, 2)), 6, CAST(N'2023-07-12T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/juanluisguerra.jpg', 20000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (23, N'Katy Perry', N'Una de las mas grandes estrellas del pop actual', N'Monumental, Lima', CAST(780.00 AS Decimal(11, 2)), 8, CAST(N'2023-08-05T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/kattyperry.jpg', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (24, N'KISS', N'La legendaria banda del Glam Metal y el Glam Rock se presentan en su ultima gira internacional en sus mas de 40 años de existencia', N'Melbourne', CAST(750.00 AS Decimal(11, 2)), 1, CAST(N'2023-07-08T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/KISS.jpg', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (25, N'Lindsey Sterling', N'La talentosa Lindsey nos sorprenderá con la mezcla de la música instrumental con el pop moderno', N'Wisconsin', CAST(180.00 AS Decimal(11, 2)), 10, CAST(N'2023-07-15T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/lindeysterling.webp', 5000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (26, N'Mägo de Oz', N'Una de las mejores bandas del rock en español, nos deleitan con su presentación llena de efectos especiales', N'Madrid, España', CAST(199.00 AS Decimal(11, 2)), 1, CAST(N'2023-08-28T18:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/magodeoz.jpg', 20000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (27, N'Marc Anthony', N'Como parte de su gira musical, Marc Anthony ha compilado la mayor parte de sus éxitos en este concierto imperdible', N'Santiago, Chile', CAST(230.00 AS Decimal(11, 2)), 5, CAST(N'2023-06-30T10:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/marcanthony.jpg', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (28, N'Nightwish', N'Nightwish ha demostrado una entrega sin precedentes en el rock nórdico, con  un potente sonido y una intepretación maravillosa de Floor Jansen', N'Estocolmo, Suecia', CAST(499.00 AS Decimal(11, 2)), 9, CAST(N'2023-09-02T21:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/nightwish.jpg', 8500, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (29, N'Oscar de Leon', N'El faraón de la salsa en su gira mundial por toda América', N'Sao Paulo, Brasil', CAST(230.00 AS Decimal(11, 2)), 5, CAST(N'2023-12-10T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/oscardeleon.jpg', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (30, N'Papa Roach', N'La banda Papa Roach hace su aparición en el viejo continente con un repertorio cargado de pura adrenalina', N'Ottery St. Catchpole, UK', CAST(600.00 AS Decimal(11, 2)), 1, CAST(N'2023-12-03T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/paparoach.jpg', 20000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (31, N'Red Hot Chilli Peppers', N'La banda que conquistó los 2000 vuelve a delitarnos con su potente música y sus letras llenas de adrenalina y poesía', N'San Francisco, CA', CAST(360.00 AS Decimal(11, 2)), 1, CAST(N'2023-07-12T20:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/redhotchillipapers.webp', 8500, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (32, N'Red Velvet', N'El quinteto surcoreano hará vibrar a sus fans con sus melodías pegajosas y sus coreografías perfectamente sincronizadas', N'Guadalajara, MX', CAST(400.00 AS Decimal(11, 2)), 4, CAST(N'2024-10-09T19:00:00.000' AS DateTime), N'http://localhost/imagenesmusicstore/redvelvet.jpg', 20000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), CAST(N'2024-04-18T17:19:29.4895567' AS DateTime2), 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (33, N'Romeo Santos', N'Romeo conquistará a sus fans con su repertorio de canciones románticas', N'San Salvador, El Salvador', CAST(780.00 AS Decimal(11, 2)), 6, CAST(N'2023-07-28T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/romeosantos.jpg', 15000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (34, N'Shania Twain', N'Shania siempre se ha caracterizado por cautivar a su público con sus melodias únicas del country, siempre destacando su belleza y su carisma ', N'Miami, FL', CAST(600.00 AS Decimal(11, 2)), 8, CAST(N'2023-08-26T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/shaniatwain.jpg', 20000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (35, N'Taylor Davis', N'Si lo tuyo es la música instrumental, déjate llevar por estas bellas melodías', N'New Mexico', CAST(150.00 AS Decimal(11, 2)), 10, CAST(N'2023-05-28T09:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/taylordavis.jpeg', 1000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (36, N'Victor Manuelle', N'La salsa no sería lo mismo sin Victor Manuelle, su estilo unico lo deja muy en claro en cada concierto', N'Mendoza, ARG', CAST(350.00 AS Decimal(11, 2)), 5, CAST(N'2023-09-18T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/victormauelle.jpg', 15000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (37, N'ZZ Top', N'El Rock blues sigue siendo relevante con una de las bandas mas legendarias del género', N'Palo Alto, CA', CAST(360.00 AS Decimal(11, 2)), 2, CAST(N'2023-07-12T17:00:00.000' AS DateTime), N'https://musicstoreimages.blob.core.windows.net/blazormusicstore/zztop.webp', 10000, 0, CAST(N'2023-07-17T10:33:55.8500000' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (38, N'Empire Music Festival 2024', N'Evento de musica multigenero', N'Finca El Jocotillo', CAST(845.00 AS Decimal(11, 2)), 7, CAST(N'2024-04-26T16:00:00.000' AS DateTime), N'http://localhost/imagenesmusicstore/emf2024.jpg', 12000, 0, CAST(N'2024-04-17T15:51:36.3229337' AS DateTime2), CAST(N'2024-04-18T16:16:56.3524945' AS DateTime2), 1)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (39, N'Road to Ultra 2025 Guatemala', N'RTU', N'Cayala', CAST(850.00 AS Decimal(11, 2)), 12, CAST(N'2024-11-04T18:00:00.000' AS DateTime), N'https://musictore24.blob.core.windows.net/images/rtu.jpg', 12000, 0, CAST(N'2024-04-22T16:47:42.5504246' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (40, N'Road to Ultra 2025 Guatemala', N'RTU', N'Cayala', CAST(850.00 AS Decimal(11, 2)), 12, CAST(N'2024-11-04T18:00:00.000' AS DateTime), N'https://musictore24.blob.core.windows.net/images/rtu.jpg', 12000, 0, CAST(N'2024-04-22T16:52:58.8709128' AS DateTime2), NULL, 0)
GO
INSERT [dbo].[Concert] ([Id], [Title], [Description], [Place], [UnitPrice], [GenreId], [DateEvent], [ImageUrl], [TicketsQuantity], [Finalized], [CreationDate], [ModifiedDate], [Status]) VALUES (41, N'Road to Ultra 2025 Guatemala', N'RTU', N'Cayala', CAST(850.00 AS Decimal(11, 2)), 12, CAST(N'2024-11-04T18:00:00.000' AS DateTime), N'https://musictore24.blob.core.windows.net/images/rtu.jpg', 12000, 0, CAST(N'2024-04-22T16:53:34.7052809' AS DateTime2), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Concert] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([Id], [Email], [FullName], [CreationDate], [ModifiedDate], [Status]) VALUES (1, N'dgarcias@outlook.com', N'Diego Garcia', CAST(N'2024-05-02T18:23:51.8919082' AS DateTime2), NULL, 1)
GO
INSERT [dbo].[Customer] ([Id], [Email], [FullName], [CreationDate], [ModifiedDate], [Status]) VALUES (2, N'dummy@gmail.com', N'Dummy User', CAST(N'2024-05-02T18:47:22.3066916' AS DateTime2), NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Sale] ON 
GO
INSERT [dbo].[Sale] ([Id], [CustomerId], [ConcertId], [SaleDate], [OperationNumber], [Total], [Quantity], [CreationDate], [ModifiedDate], [Status]) VALUES (1, 1, 2, CAST(N'2024-05-09' AS Date), N'00001', CAST(340.00 AS Decimal(11, 2)), 1, CAST(N'2024-05-09T12:20:55.6898653' AS DateTime2), CAST(N'2024-05-09T12:20:55.7834203' AS DateTime2), 1)
GO
INSERT [dbo].[Sale] ([Id], [CustomerId], [ConcertId], [SaleDate], [OperationNumber], [Total], [Quantity], [CreationDate], [ModifiedDate], [Status]) VALUES (2, 1, 38, CAST(N'2024-05-09' AS Date), N'00002', CAST(3380.00 AS Decimal(11, 2)), 4, CAST(N'2024-05-09T18:31:31.9768255' AS DateTime2), CAST(N'2024-05-09T18:31:32.0801952' AS DateTime2), 1)
GO
INSERT [dbo].[Sale] ([Id], [CustomerId], [ConcertId], [SaleDate], [OperationNumber], [Total], [Quantity], [CreationDate], [ModifiedDate], [Status]) VALUES (3, 1, 39, CAST(N'2024-05-17' AS Date), N'00003', CAST(1700.00 AS Decimal(11, 2)), 2, CAST(N'2024-05-17T16:26:59.1198413' AS DateTime2), CAST(N'2024-05-17T16:26:59.1513373' AS DateTime2), 1)
GO
INSERT [dbo].[Sale] ([Id], [CustomerId], [ConcertId], [SaleDate], [OperationNumber], [Total], [Quantity], [CreationDate], [ModifiedDate], [Status]) VALUES (4, 1, 39, CAST(N'2024-05-17' AS Date), N'00004', CAST(5950.00 AS Decimal(11, 2)), 7, CAST(N'2024-05-17T16:27:06.8681346' AS DateTime2), CAST(N'2024-05-17T16:27:06.8744052' AS DateTime2), 1)
GO
INSERT [dbo].[Sale] ([Id], [CustomerId], [ConcertId], [SaleDate], [OperationNumber], [Total], [Quantity], [CreationDate], [ModifiedDate], [Status]) VALUES (5, 1, 39, CAST(N'2024-05-17' AS Date), N'00005', CAST(850.00 AS Decimal(11, 2)), 1, CAST(N'2024-05-17T16:27:10.5706052' AS DateTime2), CAST(N'2024-05-17T16:27:10.5761274' AS DateTime2), 1)
GO
SET IDENTITY_INSERT [dbo].[Sale] OFF
GO
