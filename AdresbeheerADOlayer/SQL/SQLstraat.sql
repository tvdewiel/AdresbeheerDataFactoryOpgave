CREATE TABLE [dbo].[straat]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [straatnaam] NVARCHAR(250) NULL, 
    [NIScode] INT NOT NULL,
	CONSTRAINT [FK_straat_gemeente] FOREIGN KEY([NIScode]) REFERENCES [dbo].[gemeente] ([NIScode])
)
