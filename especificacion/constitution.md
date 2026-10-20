# Constitución

Reglas no negociables. Mandan sobre cualquier criterio propio de Claude.

Este documento es **global**: aplica a `KM.GestionSeguridad.MS` y a cualquier otro microservicio que comparta esta estructura de proyectos. Lo específico de un microservicio no va aquí.

## 1. Stack

- C# sobre .NET 8, desarrollado en Visual Studio 2026.
- PostgreSQL como motor de base de datos.
- SOLID y clean code.
- Solución segmentada en proyectos, con inyección de dependencias.

## 2. Acceso a datos

- **Todo acceso a datos se hace mediante procedimientos almacenados.** Sin excepción.
- El motivo es evitar inyección SQL.

### Códigos de error

- Cada procedimiento almacenado señala lo que ocurrió con un código simbólico con el formato **`AREA.CAUSA`**, en mayúsculas. Ejemplos: `AUTH.CREDENCIAL_INVALIDA`, `ROL.NOMBRE_DUPLICADO`.
- El código se registra en el catálogo del microservicio **al escribir el procedimiento**, no después.
- Un código publicado no se renombra: puede haber clientes reaccionando a él.

### Nomenclatura de la base de datos

Prefijos de objeto:

| Prefijo | Objeto | Ejemplo |
|---|---|---|
| `tm_` | Tabla maestra | `tm_usuarios` |
| `tt_` | Tabla transaccional | `tt_conexiones` |
| `sp_` | Procedimiento almacenado | `sp_autenticar_usuario` |
| `fn_` | Función | `fn_validar_contexto` |

Prefijos de columna, según el tipo de dato:

| Prefijo | Tipo | Ejemplo |
|---|---|---|
| `id_` | Identificador o clave foránea | `id_usuario` |
| `c_` | Texto | `c_nombre` |
| `n_` | Numérico | `n_orden` |
| `b_` | Booleano | `b_activo` |
| `f_` | Fecha | `f_fecha_creacion` |

Los nombres van en minúsculas, y las tablas en plural.

Prefijos de parámetro en procedimientos y funciones. Al prefijo de tipo se le antepone la dirección: `i` de entrada, `o` de salida.

| Prefijo | Significado | Ejemplo |
|---|---|---|
| `ic_` | Entrada, texto | `ic_usuario` |
| `in_` | Entrada, numérico | `in_intentos` |
| `ib_` | Entrada, booleano | `ib_activo` |
| `if_` | Entrada, fecha | `if_fecha_corte` |
| `iid_` | Entrada, identificador | `iid_usuario` |
| `oc_`, `on_`, `ob_`, `of_`, `oid_` | Lo mismo, de salida | `oc_mensaje` |

El motivo es que al leer el código C# que invoca el procedimiento se vea de inmediato qué entra y qué sale.

### Documentación de la base de datos

La documentación tiene que ser **consultable**, no solo legible: alimenta el catálogo de conocimiento de Karma. Por eso no se usan comentarios `--`, que no se pueden consultar, sino `COMMENT ON`, que guarda la descripción en el catálogo de PostgreSQL y se lee con SQL.

- **Todo procedimiento y toda función** llevan `COMMENT ON` describiendo qué hacen, qué reciben y qué devuelven.
- **Toda columna de toda tabla** lleva `COMMENT ON`, por descriptiva que parezca.
- Si una columna guarda **valores codificados** —letras o números que representan una palabra, como `a` de activo o `anu` de anulado— el comentario enumera cada valor y su significado. Sin excepción.

## 3. Responsabilidad de cada proyecto

`<Prefijo>` son las iniciales del microservicio: `GS` para Gestión de Seguridad, otras para los demás.

### <Prefijo>.Api

- Contiene los controladores. Los endpoints son el canal que recibe y devuelve un mensaje; nada más.
- Contiene `Program.cs`, donde se configura la validación del token además de su generación.
- Los controladores se agrupan en carpetas y siguen una nomenclatura que agilice ubicarlos.
- Es el único proyecto que obtiene la cadena de conexión. Lee `appsettings` y las variables de entorno, y entrega la cadena ya resuelta a la infraestructura al registrarla en el contenedor. Ningún otro proyecto lee la configuración por su cuenta.

### <Prefijo>.Aplicacion

- Contiene la lógica de negocio, agrupada en carpetas.
- Casos de uso, DTOs e interfaces de los casos de uso, para que `<Prefijo>.Api` los invoque.
- Desde aquí se llama a las interfaces de repositorio.

### <Prefijo>.Dominio

- Entidades y DTOs de lo que devuelve un procedimiento almacenado.
- Interfaces de los repositorios.
- Se usan `record` o clases según convenga.

### <Prefijo>.Infraestructura

- Acceso a datos e implementación de las interfaces de repositorio.
- Es donde se invocan los procedimientos almacenados.

### <Prefijo>.Logging

- Control y configuración de logs. Por ahora solo la configuración.

### <Prefijo>.Api.Tests

- Pruebas de toda la solución, agrupadas por carpeta.

## 4. DTOs en dos niveles

Un DTO de API no puede arrastrar la nomenclatura de las tablas ni de sus campos. Por eso la data pasa por dos representaciones:

1. **DTO de base de datos** — lo que devuelve el procedimiento almacenado, con la nomenclatura de la BD.
2. **DTO de API** — lo que consume el cliente, con nomenclatura propia.

La transformación ocurre entre ambos, en los dos sentidos.

## 5. Contrato de respuesta

Toda respuesta viaja en `SingleResponse<T>` o `ListResponse<T>`, que heredan de `RepositoryResult`. Los campos que importan:

| Campo | Para qué sirve |
|---|---|
| `StatusCode` | Estado HTTP de la operación. |
| `Codigo` | Código simbólico `AREA.CAUSA`: la causa concreta. |
| `StatusMessage` | Mensaje de estado. **Para el log y la depuración, no para mostrar al usuario.** |
| `ErrorCode`, `ErrorMessage` | Detalle técnico del fallo. Nunca llegan al consumidor. |

El caso de uso fija el `StatusCode` y el `Codigo`. El controlador traduce el `StatusCode` al estado HTTP de la respuesta.

### Qué consume el front

Las dos cosas, pero en capas distintas:

- **El estado HTTP lo atiende el interceptor global.** Es la reacción transversal, igual en toda la aplicación: 401 limpia la sesión y manda al login, 500 muestra un error genérico. Se escribe una vez.
- **El `Codigo` lo atiende la pantalla.** Solo cuando necesita comportarse distinto según la causa. `AUTH.CREDENCIAL_INVALIDA`, `AUTH.USUARIO_INACTIVO` y `AUTH.SIN_ACCESO_AL_SISTEMA` son los tres un 401, pero no se le dicen al usuario igual.

**El front nunca interpreta `StatusMessage`.** Es texto en español pensado para depurar; si la pantalla lo muestra tal cual, cambiar una redacción o traducir obliga a tocar el backend.

Regla práctica: si la reacción es la misma en toda la aplicación, va por HTTP. Si cambia según el caso puntual, va por `Codigo`.

## 6. Convención de nombres

El objetivo es poder ubicar un tema en todas sus capas de inmediato, y poder señalarlo en una conversación sin ambigüedad.

**Tema primero, tipo al final.** El tema abre el nombre para que al buscarlo salgan juntas todas sus capas. Si el tipo fuera prefijo, los archivos se agruparían por tipo y se perdería el rastro del tema.

Los sufijos son cortos a propósito: nombres largos dejan de ser legibles. Lo que importa es que sean **siempre los mismos**.

| Tipo | Sufijo | Ejemplo |
|---|---|---|
| Controlador | `Controller` | `RolController.cs` |
| Caso de uso | `CU` | `RolCrearCU.cs` |
| Interfaz de caso de uso | `I…CU` | `IRolCrearCU.cs` |
| DTO de entrada de API | `RQ` | `RolCrearRQ.cs` |
| DTO de salida de API | `RS` | `RolCrearRS.cs` |
| DTO que devuelve un procedimiento almacenado | `DB` | `RolDB.cs` |
| Interfaz de repositorio | `I…Repo` | `IRolRepo.cs` |
| Implementación de repositorio | `Repo` | `RolRepo.cs` |
| Prueba | `Tests` | `RolCrearTests.cs` |

Además, **cada capa agrupa por tema en carpetas** con el mismo nombre:

```
<Prefijo>.Api/Controllers/Rol/RolController.cs
<Prefijo>.Aplicacion/Rol/RolCrearCU.cs
<Prefijo>.Aplicacion/Rol/Dtos/RolCrearRQ.cs
<Prefijo>.Aplicacion/Rol/Dtos/RolCrearRS.cs
<Prefijo>.Dominio/Rol/RolDB.cs
<Prefijo>.Dominio/Rol/IRolRepo.cs
<Prefijo>.Infraestructura/Rol/RolRepo.cs
<Prefijo>.Api.Tests/Rol/RolCrearTests.cs
```

Un sufijo nuevo no se inventa sobre la marcha: se agrega a esta tabla primero.

### Documentación del código

Toda clase, interfaz y método público lleva comentario XML (`///`), por el mismo motivo que en la base: la documentación se extrae y alimenta el catálogo de conocimiento de Karma.

## 7. Cómo trabaja Claude

- Enfocarse en la tarea pedida, sea pequeña o grande, y terminarla.
- Al terminar, **Kreyshin decide** si prueba o si Claude continúa.
- Claude mantiene una lista de lo que queda **pendiente de revisión** por Kreyshin.
- **Los commits los ordena Kreyshin.** Claude no commitea por iniciativa propia.

## 8. Cuando algo no está especificado

- Claude indica el bloqueante de forma corta y concisa.
- Da su recomendación, igual de corta y clara.
- **Kreyshin decide** si se aplica.
- Cuando sea posible, se debate el motivo de esa elección antes de aplicarla.
