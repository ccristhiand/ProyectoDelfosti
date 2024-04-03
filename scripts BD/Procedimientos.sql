
CREATE OR ALTER PROC SP_ADD_PEDIDO(
	@idVendedor INT ,
	@idRepartidor INT
)AS BEGIN 
	IF EXISTS(SELECT * FROM usuario where idRol=2 and id=@idVendedor)
	BEGIN
		IF EXISTS(SELECT * FROM usuario where idRol=4 and id=@idRepartidor)
		BEGIN
			INSERT INTO Pedido VALUES(GETDATE(),NULL,NULL,NULL,@idVendedor,@idRepartidor,1,0,0,0,0)
			SELECT 'Pedido agregado con exito'  AS result
		END
		ELSE
		BEGIN
			SELECT 'El usuario no tienen el Rol de Repartidor o no existe' AS result
		END
	END
	ELSE
	BEGIN
		SELECT 'El usuario no tienen el Rol de Vendedor o no existe' AS result
		
	END
	
END


CREATE OR ALTER PROC SP_UPDATE_STATE_PEDIDO(
	@Estado INT,
	@numeroPedido INT
)AS BEGIN
	IF EXISTS(SELECT TOP 1 * from Pedido where numeroPedido=@numeroPedido)
	BEGIN
		IF @Estado =1
		BEGIN
			UPDATE Pedido SET
			fechaPedido=GETDATE(),
			fechaRecepcion=NULL,	
			fechaDespacho=NULL,	
			fechaEntrega=NULL
			WHERE numeroPedido=@numeroPedido
			SELECT 'Actualizado con exito' as Result
		END

		IF @Estado =2
		BEGIN
			 UPDATE Pedido SET
			 fechaRecepcion=GETDATE(),	
			 fechaDespacho=NULL,	
			 fechaEntrega=NULL
			 WHERE numeroPedido=@numeroPedido AND fechaPedido  IS NOT NULL
			 SELECT 'Actualizado con exito' as Result
		END

		IF @Estado =3
		BEGIN
			UPDATE Pedido SET
			 fechaDespacho=GETDATE(),
			 fechaEntrega=NULL
			 WHERE numeroPedido=@numeroPedido AND fechaPedido IS NOT NULL AND  fechaRecepcion  IS NOT NULL
			 SELECT 'Actualizado con exito' as Result
		END
		IF @Estado =4
		BEGIN
			 UPDATE Pedido SET
			 fechaEntrega=GETDATE()
			 WHERE numeroPedido=@numeroPedido AND fechaPedido  IS NOT NULL AND  fechaRecepcion  IS NOT NULL AND fechaDespacho  IS NOT NULL
			SELECT 'Actualizado con exito' as Result
		END
	END
	ELSE
	BEGIN
		SELECT 'El pedido no exite' as Result
	END
END

SP_UPDATE_STATE_PEDIDO 2,20000


CREATE OR ALTER PROC SP_LOGIN(
	@correo varchar(200),
	@password varchar(100)
)
AS BEGIN
	DECLARE @pass VARCHAR(MAX)=NULL
	IF EXISTS (select [password] from [usuario] where correo=@correo)
	BEGIN
		SET @pass=(SELECT TOP 1 [password] FROM usuario WHERE correo=@correo)
		IF @pass=@password
		BEGIN
			SELECT TOP 1
			U.id AS idUsuario,
			R.id AS idRol,
			U.nombre AS nombre,
			R.nombre AS rol,
			U.correo AS correo,
			U.telefono AS telefono,
			U.puesto AS puesto,
			'Inicio de sesion Exitoso' AS result
			FROM dbo.usuario U INNER JOIN 
			Rol R ON U.idRol =R.id 
			WHERE U.correo=@correo 
		END
		ELSE
			SELECT 'Password incorrecto' AS result
		END
	ELSE
		SELECT 'El correo no existe' AS result
END

--SP_LOGIN 'ccristhiand@gmail.com','123456'


CREATE OR ALTER PROC SP_ADD_DETALLE_PEDIDO(
	@numeroPedido INT,
	@idproducto INT,
	@cantidad INT
)AS BEGIN
	DECLARE @nombreProducto VARCHAR(200)=(SELECT nombre FROM Producto where  sku=@idproducto )

	IF EXISTS(SELECT * FROM Producto WHERE stock>@cantidad)
	BEGIN
		DECLARE @precio NUMERIC(10,2)=(SELECT precio FROM Producto where  sku=@idproducto )
		
		INSERT INTO DetallePedido VALUES(@numeroPedido,@idproducto,@cantidad,@cantidad*@precio,GETDATE())
		
		--RESTAR UNIDADES 
		UPDATE Producto set stock=stock-@cantidad where sku=@idproducto
		SELECT 'Producto '+@nombreProducto+' Agregado con Exito' as result

		--CALCULO DEL TOTAL DEL PEDIDO
		DECLARE @SUBTOTAL_NUEVO NUMERIC(10,2) =(select sum(total) from DetallePedido where numeroPedido=@numeroPedido)
		DECLARE @IGV_NUEVO NUMERIC (10,2)=@SUBTOTAL_NUEVO*0.18
		DECLARE @DESCUENTO_NUEVO NUMERIC(10,2)=0-- DESCUENTOS PORSIACASO
		DECLARE @TOTAL_NUEVO NUMERIC(10,2)=@SUBTOTAL_NUEVO+@IGV_NUEVO-@DESCUENTO_NUEVO

		UPDATE Pedido SET subtotal=@SUBTOTAL_NUEVO, IGV=@IGV_NUEVO,Descuento=@DESCUENTO_NUEVO,total=@TOTAL_NUEVO
		where numeroPedido=@numeroPedido
	END
	ELSE
	BEGIN
		SELECT 'Producto '+@nombreProducto+' Sin stock' as result
	END
END


CREATE OR ALTER PROC SP_LISTAR_USUARIOS(
	@rol VARCHAR (40)
)AS BEGIN
	SELECT  U.id AS idPersona,
			U.correo AS Nombre,
			U.telefono AS tepefono,
			U.puesto AS puesto,
			U.idRol AS idRol,
			R.nombre As Rol
	FROM Usuario U INNER JOIN Rol R ON U.idRol=r.id where r.nombre like @rol
END

CREATE OR ALTER PROC SP_LISTAR_PRODUCTOS(
	@SKU INT
)AS BEGIN
	SELECT P.sku,
		   P.nombre,
		   P.stock,
		   P.etiquetas,
		   P.precio,
		   p.idTipoProducto,
		   p.idUnidadMedida,
		   T.nombre AS tipo,
		   U.nombre AS unidadMedida
	
	FROM Producto P INNER JOIN TipoProducto T ON P.idTipoProducto=T.id 
					INNER JOIN UnidadMedida U ON P.idUnidadMedida=U.id
	where P.sku like @SKU
END

CREATE OR ALTER PROC SP_LISTAR_DETALLE_PEDIDO(
	@pedido INT 
)AS BEGIN
	SELECT 
	P.numeroPedido,
	P.fechaPedido,
	P.fechaRecepcion,
	P.fechaDespacho,
	P.fechaEntrega,
	PR.sku,
	PR.nombre AS NombreProducto,
	D.cantidad,
	PR.precio,
	D.total,
	UV.nombre AS vendedor,
	UR.nombre AS repartidor
	FROM Pedido P INNER JOIN DetallePedido D ON P.numeroPedido=D.numeroPedido
	INNER JOIN Producto PR  ON D.idproducto=PR.sku
	INNER JOIN Usuario UV ON P.idVendedor=UV.id
	INNER JOIN Usuario UR ON P.idRepartidor=UR.id
	WHERE P.numeroPedido LIKE @PEDIDO
END


