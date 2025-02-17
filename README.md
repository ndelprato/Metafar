# Documentación de API REST Saldos

## Proposito del documento

El propósito de este documento es brindar información de los endpoints del API REST, asi como también generar la imágen docker y su posterior ejecución

## Alcance

Este documento abarca la descripción funcional y técnica del uso de los endpoints, así como la información generada previamente para poder realizar las pruebas necesarios

Se utilizó una arquitecto Clean separando en capas los niveles de la API.

#### Levantar imagen docket y base de datos

### Imagen docker

Para poder levantar la proyecto es necesario posicionarse en la ubicacion /Metafar/Dockerfile y ejecutar el siguiente comando
docker-compose up --build.
La información de la base de datos se mantiene persistente aunque se apague la imagen.

### Base de datos

Debido a la imposibilidad de inicializar la base de datos con los valores predeterminados al levantar la imagen docker, se deja registrado el paso a paso para poder correr el script manualmente y así poder ejecutar las pruebas.

Desde el VSCODE instalamos el plugin de SQL SERVER, hacemos click en nueva conexión y cargamos los siguientes datos

SERVER NAME = localhost
USERNAME = sa
PASSWORD= YourStrong!Passw0rd

Vamos al apartado de configuración avanzada y cargamos el puerto 1433
AL hacer click en conectar aceptamos el certificado de confianza
Una vez que estamos conectados, hacemos click derecho sobre el servidor al que nos conectamos y ponemos nueva consulta.
Abrimos el archivo init.sql que se encuentra en la raiz del proyecto y procedemos a copiar todo su contenido y ejecutamos el script, de esta manera vamos a tener la base de datos con información suficiente para realizar las pruebas


### Descripcion Funcional

#### Diagrama entidad relación de las tablas generadas

![Flow](img/metafar.jpg)



#### Endpoints

### Swagger
Para acceder al swagger, cuando levantamos la imagen dockerizada simplemente ponemos en el explorador

http://localhost:8080/swagger/index.html

### Login /api/usuario/Login

Se encuentran precargados diferentes usuarios para realizar las pruebas necesarias de los endpoint

El password de todos los usuarios/tarjetas es "admin"
Despues el usuario/tarjeta es el siguiente:
123
1234
12345
123456

Completando tarjeta y password obtenemos al respuesta del login.
Copiamos integramente el token que fue generado. Vamos arriba a la derecha del swagger, en Authorize, al hacer click se nos despliega una ventana en la cual vamos a escribir lo siguiente:

Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0IiwidW5pcXVlX25hbWUiOiIxMjM0NTYiLCJuYmYiOjE3Mzk3NjgyODgsImV4cCI6MTczOTg1NDY4OCwiaWF0IjoxNzM5NzY4Mjg4fQ.jqi_BYz7ICgynZtZv-rDKbAK_lwqilAk_Dvv_LQAg8I

Luego de la palabra Bearer dejamos un espacio y pegamos como se ve arriba el token que nos devolvió el endpoint para poder estar autenticado y utilizar los enpoints

### Saldo /api/saldo/{tarjeta}

Ingresando el número de tarjeta nos devuelve toda la información disponible respecto al saldo de la misma
Respuesta:
{
  "id": 4,
  "tarjeta": "123456",
  "monto": "3500",
  "fecha": "2025-02-17T05:46:15.9447556"
}

### Retiro /api/saldo/{tarjeta}

Este endpoint del tipo PATCH nos permite realizar el retiro de dinero de la tarjeta cargada, actualizando el saldo y generando el registro correspondiente en la tabla Operaciones


### Operaciones /api/operaciones/{tarjeta}

Ingresando el número de tarjeta se puede obtener de manera paginada todas las operaciones realizadas por la misma

Respuesta:


  "totalPages": 2,
  "totalItems": 19,
  "pageNumber": 1,
  "pageSize": 10,
  "items": [
    {
      "operacion": "Extracción",
      "montoInicial": "15000",
      "montoFinal": "14500",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.9099475"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "14500",
      "montoFinal": "14000",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.9147396"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "14000",
      "montoFinal": "13500",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.9147396"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "13500",
      "montoFinal": "12000",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.9147396"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "12000",
      "montoFinal": "11500",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.9147396"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "11500",
      "montoFinal": "11000",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.919723"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "11000",
      "montoFinal": "10500",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.919723"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "10500",
      "montoFinal": "10000",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.919723"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "10000",
      "montoFinal": "9500",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.919723"
    },
    {
      "operacion": "Extracción",
      "montoInicial": "9500",
      "montoFinal": "8000",
      "tarjeta": "123456",
      "fecha": "2025-02-17T05:46:15.9248056"
    }
  ]
}

#### Repositorio GITHUB

https://github.com/ndelprato/Metafar



