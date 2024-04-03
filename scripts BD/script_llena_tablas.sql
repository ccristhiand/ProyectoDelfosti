INSERT INTO Estado VALUES('Por atender')
INSERT INTO Estado VALUES('En proceso')
INSERT INTO Estado VALUES('En delivery')
INSERT INTO Estado VALUES('Recibido')

INSERT INTO Rol VALUES('Encargado')
INSERT INTO Rol VALUES('Vendedor')
INSERT INTO Rol VALUES('Delivery')
INSERT INTO Rol VALUES('Repartidor')

INSERT INTO TipoProducto VALUES('Electrodomésticos')
INSERT INTO TipoProducto VALUES('Productos para el hogar')
INSERT INTO TipoProducto VALUES('Moda y accesorios')
INSERT INTO TipoProducto VALUES('Salud y belleza')
INSERT INTO TipoProducto VALUES('Alimentos y bebidas')
INSERT INTO TipoProducto VALUES('Juguetes y juegos')

INSERT INTO UnidadMedida VALUES('unidad')
INSERT INTO UnidadMedida VALUES('docena')
INSERT INTO UnidadMedida VALUES('kg')
INSERT INTO UnidadMedida VALUES('litros')
INSERT INTO UnidadMedida VALUES('cajax100')

INSERT INTO Usuario VALUES('Cristhian','ccristhiand@gmail.com','123456','927064045','',1)
INSERT INTO Usuario VALUES('Carlos','carlos@gmail.com','123456','985647523','',2)
INSERT INTO Usuario VALUES('Maria','maria@gmail.com','123456','987968457','',3)
INSERT INTO Usuario VALUES('Alejandro','alejandro@gmail.com','123456','985647523','',4)
INSERT INTO Usuario VALUES('Pedro','pedro@gmail.com','123456','945324567','',2)
INSERT INTO Usuario VALUES('Luis','luis@gmail.com','123456','965234875','',4)

INSERT INTO Producto VALUES('Ajedrez',6,10000,'juego,competitivo,familiar,mesa,estrategia',20.50,1)
INSERT INTO Producto VALUES('Lapiceros XYZ',2,1000,'lapicero,escribir,colegio,universidad',60.80,2)
INSERT INTO Producto VALUES('Lavadora',1,600,'electrodomestico,ropa,lavadora',1500.00,1)
INSERT INTO Producto VALUES('SACO DE ARROZ X50',6,3000,'arroz,comida',90.00,3)
INSERT INTO Producto VALUES('CUBITOS',5,10000,'juego',400.00,5)

INSERT INTO Pedido VALUES(GETDATE(),NULL,NULL,NULL,2,4,1,NULL,NULL,NULL,NULL)
INSERT INTO Pedido VALUES(GETDATE(),NULL,NULL,NULL,5,6,1,NULL,NULL,NULL,NULL)
INSERT INTO Pedido VALUES(GETDATE(),NULL,NULL,NULL,2,6,1,NULL,NULL,NULL,NULL)
INSERT INTO Pedido VALUES(GETDATE(),NULL,NULL,NULL,5,4,1,NULL,NULL,NULL,NULL)


