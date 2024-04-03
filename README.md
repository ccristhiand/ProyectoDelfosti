# ProyectoDelfosti

## Tabla de contenido
1. [informacion Genral](#info-general)
2. [Technologies](#technologies)
3. [Funcionamiento](#funcionamiento)
4. [Installation](#installation)

### info general
***
Sistema para la empresa XYZ de venta de productos básicos

## Technologies
***
A list of technologies used within the project:
* [Technologie .Net]()Version 7.0
* [Technologie Sql Server]()Version 2022 conexion: "Data Source=SQL5113.site4now.net;Initial Catalog=db_aa7348_proyectodelfosti;User Id=db_aa7348_proyectodelfosti_admin;Password=delfosti123"
* [Library JWT]() Version 7.0.17
* [Library Dapper]() Version 2.1.35
* [Library SqlClient]() Version 4.8.6

## Funcionamiento
***
en el sistemas tenemos 3 EndPoints

usuario (login(verbo:post))-> Se genera el token____________________Acceso Roles (todos) 
pedido  (post(verbo:post)) -> Se agrega el pedido y su detalle______Acceso Roles (Encargado,Vendedor)
pedido  (patch(verbo:patch))-> Se actualiza el estado del pedido_____Acceso Roles (Encargado,Delivery,Repartidor)

Para poder iniciar sesion ingresar con el correo seguido del password
correo: ccristhiand@gmail.com	password: 123456        Rol:Encargado
correo: carlos@gmail.com	    password: 123456        Rol:Vendedor
correo: maria@gmail.com	        password: 123456        Rol:Delivery
correo: alejandro@gmail.com	    password: 123456        Rol:Repartidor
correo: pedro@gmail.com	        password: 123456        Rol:Vendedor
correo: luis@gmail.com	        password: 123456        Rol:Repartidor

nos generara el Bearer Token el cual podemos usarlo en Postman, o en el mismo swagger 

ejemplo json del uso del pedido(post):
{
  "idVendedor": 2,
  "idRepartidor": 4,
  "productosPedidos": [
    {
      "sku": 1000,
      "cant": 200
    },
{
      "sku": 1001,
      "cant": 40
    },
{
      "sku": 1002,
      "cant": 18
    },
{
      "sku": 1003,
      "cant": 50
    }
  ]
}

podemos consultar la bd que esta en la nube para realizar las consultas especificas.


## Installation
***
$ git clone ccristhiand/ProyectoDelfosti
$ open proyect in Visual Studio
$ start.


