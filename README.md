# Product API

Una API RESTful para la gestión de productos, creada con ASP.NET Core.  
Este proyecto tiene como objetivo **practicar la implementación de Middleware de manejo de errores***.

## 📝 Descripción General

- API de gestión de productos.  
- Implementa los principales verbos HTTP:
  - `GET /tasks` → traer todos los productos
  - `GET /tasks/{id}` → traer los productos por ID
  - `POST /tasks` → crear producto
  - `PUT /tasks/{id}` → actualizar producto
  - `DELETE /tasks/{id}` → eliminar producto

### 🛡️ Middleware de manejo de errores  

Este middleware se encarga de **capturar las excepciones no controladas** que ocurran durante la ejecución de la API.  

- Registra el error en los logs mediante `ILogger`.  
- Devuelve una **respuesta estándar en formato JSON** con:  
  - `success: false`  
  - `message`: mensaje genérico de error para el cliente  
  - `detail`: detalle técnico del error (útil en desarrollo).  
