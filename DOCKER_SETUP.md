# 🐳 Docker Setup - GS.Api

## ✅ Archivos Creados

```
✅ Dockerfile              - Multi-stage build para .NET 8
✅ docker-compose.yml      - Orquestación del contenedor
✅ .dockerignore           - Optimización de build
```

## 🚀 Cómo Usar

### 1️⃣ Construir la Imagen
```powershell
docker build -t km-gestion-seguridad-api:latest .
```

### 2️⃣ Ejecutar con Docker Compose
```powershell
# Iniciar el contenedor
docker-compose up -d

# Ver que está corriendo
docker ps

# Ver logs
docker-compose logs -f
```

### 3️⃣ Acceder a la API
```
API:      http://localhost:5000
Swagger:  http://localhost:5000/swagger/index.html
```

## 🛑 Detener el Contenedor
```powershell
docker-compose down
```

## 📝 Detalles de Configuración

| Parámetro | Valor |
|-----------|-------|
| **Imagen Base** | mcr.microsoft.com/dotnet/aspnet:8.0 |
| **Puerto** | 5000 (host) → 80 (contenedor) |
| **Ambiente** | Production |
| **Volúmenes** | ./Logs, ./Assets |
| **Health Check** | Cada 30 segundos |
| **Restart** | unless-stopped |

## 🔧 Dockerfile

Usa **multi-stage build** para:
- ✅ Compilar en etapa de build
- ✅ Publicar optimizado en Release
- ✅ Ejecutar en imagen runtime (más pequeña)

**Tamaño final estimado:** ~350 MB

## 🌐 docker-compose.yml

Características:
- ✅ Build automático desde Dockerfile
- ✅ Mapeo de puertos 5000:80
- ✅ Volúmenes persistentes para Logs y Assets
- ✅ Health checks integrados
- ✅ Auto-restart si falla
- ✅ Red privada configurada

## 💡 Comandos Útiles

```powershell
# Construir sin usar cache
docker build --no-cache -t km-gestion-seguridad-api:latest .

# Ver imágenes creadas
docker images

# Ver procesos en contenedor
docker exec -it km-gestion-seguridad-api ps aux

# Entrar en la shell del contenedor
docker exec -it km-gestion-seguridad-api bash

# Ver logs completos
docker logs km-gestion-seguridad-api

# Listar contenedores
docker ps -a
```

## ✨ ¡Listo!

Tu API GS.Api está completamente dockerizada y lista para desplegar.

```
docker-compose up -d
```

¡Eso es todo! 🚀
