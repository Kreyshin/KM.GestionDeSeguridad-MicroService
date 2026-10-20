# Login

## Modelo de acceso

### Origen del modelo

La base de SQL Express **`KM_GSeguridad`** (esquema `dbo`) es la referencia. Fue la verdad original; hoy es una referencia real que necesita ajustes, porque en varios puntos está detrás de lo decidido. Las diferencias deliberadas quedan anotadas abajo.

No confundirla con `Karma.Systems` de SQL Express, que es otro modelo con esquemas `KM_Core` y `KM_Seguridad`.

### Roles

Los roles son **agnósticos al sistema**. Se definen una vez y se reutilizan.

El motivo es la reutilización: si el rol perteneciera a un sistema, "Administrador" habría que crearlo una vez por sistema, con los mismos permisos duplicados en cada uno, y se desincronizarían.

Lo que acota a un rol son sus permisos, no su definición.

> Diferencia con SQL Server: ahí `Tm_Rol` no tiene sistema — coincide. En `Karma.Systems` sí lo tiene; ese modelo no se sigue.

### Permisos

- Todo permiso pertenece a **un sistema** (`id_sistema`, obligatorio).
- Un permiso puede pertenecer a **un módulo** (`id_modulo`, opcional) o ser transversal al sistema.
- La relación rol–permiso **no lleva sistema**: ya lo trae el permiso. Duplicarlo solo permite que los dos valores discrepen.

Un mismo rol puede tener permisos de varios sistemas. Al entrar a uno, solo aplican los de ese sistema. Por eso un rol **no** significa lo mismo en todas partes: su significado efectivo lo determina el sistema por el que se entra.

Dentro de un sistema, un rol puede tener permisos distintos en módulos distintos.

### Asignación

`tm_usuarios_roles_sistemas` dice dónde opera cada usuario y con qué rol.

- **Un único rol por usuario, sistema y contexto.** Lo garantiza una restricción única sobre `(usuario, sistema, organización, empresa, sucursal)`; el rol no forma parte de la llave.
- Un usuario **sí** puede operar en varias empresas o sucursales de la misma organización —es el caso de un administrador o un jefe—, con un solo rol en cada una.
- La asignación tiene **vigencia**: `f_fecha_inicio` y `f_fecha_fin`. Permite accesos temporales y, sobre todo, conservar el rastro en vez de borrar la fila.

> Diferencia con SQL Server: su llave primaria es `(ID_Usuario, ID_Rol, ID_Sistema)`, que permite varios roles por sistema, y no tiene columnas de contexto ni de vigencia. Aquí se decidió lo contrario, a propósito.

### Tres niveles de acceso

Ninguno puede dar más que el anterior:

1. **Lo contratado** — qué módulos tiene la organización (`tm_organizaciones_modulos`). Es el techo.
2. **Lo asignado** — qué módulos y opciones ve el usuario (`tm_modulos_usuarios`, `tm_opciones_usuarios`).
3. **Lo permitido** — qué puede hacer dentro, por rol y por excepciones de usuario.

El rol **no** arrastra módulos. Si lo hiciera, chocaría con el nivel 1: dos clientes con el mismo rol verían lo mismo aunque uno no haya contratado ese módulo.

### Permisos efectivos

Los del rol son la base. Una excepción del usuario los sobrescribe, y **una denegación explícita prevalece** sobre el permiso del rol: es la única forma de quitarle algo puntual a una persona sin sacarla del rol.

Se resuelven al final, cuando ya hay rol y contexto, porque dependen de los tres.

## Flujo

Validaciones en orden. Cada una corta con su propio motivo, y el usuario debe poder distinguirlos:

| Situación | Código | HTTP |
|---|---|---|
| Falta usuario, clave o código de sistema | `AUTH.SOLICITUD_INCOMPLETA` | 400 |
| Usuario o clave incorrectos | `AUTH.CREDENCIAL_INVALIDA` | 401 |
| El usuario existe pero está desactivado | `AUTH.USUARIO_INACTIVO` | 401 |
| El código de sistema no existe o está inactivo | `AUTH.SISTEMA_NO_DISPONIBLE` | 401 |
| Sin rol activo y vigente en ese sistema | `AUTH.SIN_ACCESO_AL_SISTEMA` | 401 |
| Opera en varios contextos y debe elegir | `AUTH.CONTEXTO_REQUERIDO` | 409 |
| Autenticado | `AUTH.OK` | 200 |

**Que no diga "clave incorrecta" cuando el problema es otro.** Un usuario que se sabe su clave y recibe ese mensaje genera un ticket de soporte que no debería existir.

### Reglas del flujo

- **El login no elige por el usuario.** Si hay más de un contexto, devuelve `AUTH.CONTEXTO_REQUERIDO` y no emite token. Entrar con un contexto arbitrario, en silencio, es peor que fallar.
- **El token se emite ya amarrado** a un rol y un contexto. No existe un token "sin contexto" que después elige. Cambiar de rol o de empresa emite un token nuevo; no se muta el que se tiene.
- **La clave se compara dentro de la base**, con pgcrypto. Su resumen nunca sale de ahí.
- **El token de renovación** se genera en la aplicación; a la base solo viaja su resumen SHA-256.

### Separación de endpoints

`/token` **solo** emite tokens. Siempre devuelve token o error, nunca una lista de opciones. Un endpoint que a veces devuelve una cosa y a veces otra es un endpoint haciendo dos trabajos.

Cuando haga falta elegir contexto, la lista la entrega un endpoint aparte. Ese endpoint **también autentica**, así que necesita la misma limitación de intentos que el login.

## Pendiente

- El endpoint que entrega los contextos disponibles. Hasta que exista, un usuario con más de uno queda bloqueado con `AUTH.CONTEXTO_REQUERIDO`.
- Que el menú y los permisos respeten el nivel 1: si la organización deja de tener contratado un módulo, debe dejar de verse aunque siga asignado al usuario.
- El script `002` sigue creando el modelo viejo; una instalación desde cero lo crea y luego lo migra con `010` y `012`.
