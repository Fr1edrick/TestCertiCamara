# TestCertiCamara
Prueba de Selección
## instalación
Ejecutar el script de base de datos que se encuentra en la carpeta de adicionales y que se llama "scriptDDBBLog.sql"
Clonar el proyecto con el convencional comando en git de Clone y proceder a su ejecución en local

## Tecnologías
1. SQL Server - localDB
2. .Net Core 5 (c# 9)
3. Entity Framework 5
4. AutoMapper 11
5. Open Api (Swagger)

## Arquitectura del API
Patron Repositorio con Unidad de trabajo

## Grupos de Endpoints
### HistoryQueryLog
* /api/v1/HistoryQueryLog/GetById/{id} : Para recuperar un registro de log por id
* /api/v1/HistoryQueryLog/GetTotalQueries : Para recuperar el numero de consultas que se ha hecho al servicio externo
* /api/v1/HistoryQueryLog : Consultar y generar registro en el log de consultas al servicio externo

### TransactionZone
* /v1/api/TransactionalZone/GetProducts : Para obtener los productos y sus detalles ( Se iba a usar para al elegir el producto saber el precio de la cuota
* /v1​/api​/TransactionalZone​/PayProduct : Para realizar la transacción de pago y con las condiciones indicadas en el requerimiento.

Payload de ejemplo:
```javascript
{
  "name": "Cecilia Restrepo",
  "cardTarget": "81A1C783-19C3-4839-9450-7155C358756B",
  "cardExpire": "2022-01-06",
  "quatityQuote": 80000,
  "email": "cecilia.restrepo@test.com",
  "idProduct": "2E8ACB76-7CF2-47FE-8585-08BADC441502"
}
```