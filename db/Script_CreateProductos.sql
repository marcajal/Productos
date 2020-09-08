
CREATE TABLE Producto (
	ID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	Nombre VARCHAR (255) NOT NULL,
	IdMarca INT NOT NULL,
	Costo DECIMAL (10, 2) NOT NULL,
	Precio DECIMAL (10, 2) NOT NULL
	)

GO

CREATE TABLE Marca (
					Id INT NOT NULL PRIMARY KEY,
					Nombre VARCHAR (255) NOT NULL 
	)
GO

INSERT INTO Marca(Id, Nombre)
		VALUES(1,'Marca1')
GO

INSERT INTO Producto(Nombre, IdMarca, Costo ,Precio)
		VALUES('Nombre1',1,10,20)