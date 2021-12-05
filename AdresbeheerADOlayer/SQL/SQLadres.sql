CREATE TABLE [dbo].[adres]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [straatID] INT NOT NULL, 
    [huisnummer] NVARCHAR(10) NOT NULL, 
    [appartementnummer] NVARCHAR(25) NULL, 
    [busnummer] NVARCHAR(25) NULL, 
    [xcoord] float NOT NULL,
    [ycoord] float NOT NULL,
    [postcode] int NOT null,
	CONSTRAINT [FK_adres_straat] FOREIGN KEY([straatID]) REFERENCES [dbo].[straat] ([Id])
)
