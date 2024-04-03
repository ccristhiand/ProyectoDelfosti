
CREATE TABLE Estado(
	id INT IDENTITY PRIMARY KEY,
	nombre VARCHAR(30)
);

CREATE TABLE Rol(
	id INT IDENTITY PRIMARY KEY,
	nombre VARCHAR(30)
);

CREATE TABLE TipoProducto(
	id INT IDENTITY PRIMARY KEY,
	nombre VARCHAR(30)
);

CREATE TABLE UnidadMedida(
	id INT IDENTITY PRIMARY KEY,
	nombre VARCHAR(30)
);

CREATE TABLE Usuario (
	id INT IDENTITY PRIMARY KEY,
	nombre VARCHAR(30),
	correo VARCHAR(120),
	[password] VARCHAR(MAX), 
	telefono VARCHAR(20),
	puesto VARCHAR(50),
	idRol INT FOREIGN KEY REFERENCES Rol(id) 
);

CREATE TABLE Producto(
	sku INT IDENTITY(1000,1) PRIMARY KEY,
	nombre VARCHAR(30),
	idTipoProducto INT FOREIGN KEY REFERENCES TipoProducto(id),
	stock int,
	etiquetas VARCHAR(MAX),
	precio NUMERIC(10,2),
	idUnidadMedida INT FOREIGN KEY REFERENCES UnidadMedida(id) 
);

CREATE TABLE Pedido(
	numeroPedido INT IDENTITY(20000,1) PRIMARY KEY,
	fechaPedido DATETIME,
	fechaRecepcion DATETIME,
	fechaDespacho DATETIME,
	fechaEntrega DATETIME,
	idVendedor INT FOREIGN KEY REFERENCES  Usuario(id) ,
	idRepartidor INT FOREIGN KEY REFERENCES Usuario(id),
	idEstado INT FOREIGN KEY REFERENCES Estado(id),

	Subtotal numeric(10,2),
	IGV numeric(10,2),
	Descuento numeric(10,2),
	total numeric(10,2)
);
CREATE TABLE DetallePedido(
	id INT IDENTITY PRIMARY KEY ,
	numeroPedido INT FOREIGN KEY REFERENCES Pedido(numeroPedido),
	idproducto INT FOREIGN KEY REFERENCES Producto(sku),
	cantidad int,
	total numeric(10,2),
	fechaRegistro Datetime
)



