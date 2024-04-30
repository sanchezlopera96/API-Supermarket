# API-Supermarket
 
API-Supermarket es una API construida con .NET Core 6.0 que proporciona funcionalidades de gestion de productos utilizando SQL Server como base de datos.

## Requisitos

- .NET Core 6.0 SDK
- SQL Server (local o remoto)

## Instalación

1. Clona este repositorio en tu máquina local:

2. Configura tu conexión a la base de datos SQL Server en `appsettings.json`:
 
 {
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu_servidor;Database=tu_basededatos ;User ID=tu_usuario;Password=tu_clave;Persist Security Info=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AuthenticationService": {
    "UrlBase": "tu_api_autenticacion",
    "User": "tu_usuario",
    "Password": "tu_clave",
    "Secret": "tu_secret",
    "TipoAutenticacion": 0,
    "Issuer": "tu_aplicacion",
    "Audience": "tu_usser_name"
  },
  "EmailSettings": {
    "SenderEmail": "tu_correo",
    "Password": "tu_clave"
  },
  "AllowedHosts": "*"
}