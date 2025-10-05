# Prueba Técnica – Javier Azaid Romero Vera

Este proyecto consiste en una **aplicación web funcional (backend + frontend)** para la gestión de productos en un almacén, desarrollada como parte de una prueba técnica para el puesto de **Desarrollador Jr**.

## Tecnologías Utilizadas

### Backend
- **.NET 8 (ASP.NET Core Web API)**
- **Entity Framework Core**
- **SQL Server (LocalDB)**
- **Swagger** para documentación de la API

### Frontend
- **HTML5**, **CSS3**, **JavaScript Puro (Vanilla JS)**
- **Fetch API** para consumo de servicios
- Validaciones nativas con **HTML5** y del lado cliente en **JS**

---

##  Estructura del Proyecto

```
Prueba_Tecnica_Desarrollador/
│
├── PruebaTecnica_JavierAzaid/          # Proyecto Backend (.NET Web API)
│   ├── Controllers/                    # Controladores (API REST)
│   ├── Business/                       # Lógica de negocio
│   ├── Data/                           # Contexto de base de datos (EF Core)
│   ├── Models/                         # Entidades del modelo
│   ├── Dto/                            # Objetos de transferencia de datos
│   ├── Migrations/                     # Migraciones de Entity Framework Core
│   └── Program.cs                      # Configuración principal
│
└── Frontend/                           # Archivos del cliente web
    ├── index.html                      # Pantalla principal (CRUD)
    ├── login.html                      # Pantalla de inicio de sesión
    ├── script.js                       # Lógica CRUD del frontend
    ├── login.js                        # Lógica del inicio de sesión
    └── styles.css                      # Estilos básicos
```

---

## Requisitos del Sistema

- Visual Studio 2022 o superior  
- .NET 8 SDK  
- SQL Server Express o LocalDB  
- VS Code
- Navegador moderno (Chrome, Edge, Firefox)

---

## Configuración de la Base de Datos

La base de datos se maneja mediante **Entity Framework Core Migrations**.

### 1️⃣ Crear la base de datos

1. Abre el proyecto backend en Visual Studio (PruebaTecnica_JavierAzaid.sln).
2. Abre la Consola del Administrador de Paquetes (Herramientas → Administrador de paquetes NuGet → Consola).
3. Ejecuta el siguiente comando:

Update-Database

4. Esto generará automáticamente la base de datos PruebaTecnicaDB con la tabla Products.

### 2️⃣ Archivos de migración incluidos
Los scripts generados se encuentran en:

/PruebaTecnica_JavierAzaid/Migrations/


## ▶️ Ejecución del Proyecto

### 🔹 1. Ejecutar el Backend

1. Abre la solución PruebaTecnica_JavierAzaid.sln en Visual Studio.
2. En la barra superior, selecciona el perfil http (no https).
3. Presiona Ctrl + F5 o haz clic en Iniciar sin depuración.

El backend se ejecutará en: http://localhost:5196

⚠️ Asegúrate de **usar HTTP**, no HTTPS, ya que el frontend se sirve localmente.

Podrás probar este endpoints en Swagger:
http://localhost:5196/swagger


### 🔹 2. Ejecutar el Frontend

No es necesario usar un servidor adicional.
Opción 1️⃣ — Abrir directamente

1. Asegúrate de que el backend esté ejecutándose en la ruta:
http://localhost:5196

2. Abre el archivo login.html directamente desde el explorador de archivos (doble clic).
3. Esto abrirá una URL similar a:
file:///C:/Users/tu_usuario/Desktop/Prueba_Tecnica/Frontend/login.html

4. Inicia sesión con las credenciales de prueba (ver abajo).
5. Accederás al panel principal (index.html) donde podrás crear, buscar, editar y eliminar productos.


Opción 2️⃣ — Servir con VS Code (opcional)

Si prefieres probar el frontend desde un servidor local (por ejemplo, para evitar restricciones del navegador):

1. Abre la carpeta del frontend en Visual Studio Code.
2. Ejecuta en la terminal el comando: 
npx serve
2. Elige la ruta del proyecto cuando te lo solicite.
3. Abre en el navegador la dirección local que indique (por ejemplo):
http://localhost:3000

4. Asegúrate de que el backend siga activo en:
http://localhost:5196


## 🧰 Credenciales de Prueba

| Usuario | Contraseña |
|----------|-------------|
| admin    | 1234        |

---

## 🔐 Seguridad Implementada

- ✅ ORM con Entity Framework Core (sin SQL Injection)
- ✅ Validaciones del lado **servidor** (`[Required]`, `[StringLength]`, `[Range]`)
- ✅ Validaciones del lado **cliente** (HTML5 y JS)
- ✅ Sanitización de texto al renderizar (`sanitizeHTML()`)
- ✅ CORS configurado correctamente
- ✅ Manejo centralizado de errores y mensajes claros
- 🔸 (Opcional) CSRF y autenticación con JWT no implementados (no requeridos en esta prueba)

---

## 🧩 Funcionalidades

- **Crear productos** con validaciones completas.
- **Ver productos** con paginación.
- **Buscar productos** por nombre o SKU.
- **Editar productos** con precarga de datos.
- **Eliminar productos** con confirmación.
- **Login básico** sin JWT (control mediante `localStorage`).
- **Protección XSS** mediante sanitización de contenido.

---

## 🧱 Endpoints Principales

| Método | Endpoint | Descripción |
|---------|-----------|-------------|
| GET | `/api/Products/GetProductsPagination` | Obtiene productos paginados |
| GET | `/api/Products/GetProductsByNameOrCode` | Busca productos por nombre o SKU |
| POST | `/api/Products/CreateProduct` | Crea un nuevo producto |
| PUT | `/api/Products/UpdateProduct` | Actualiza un producto existente |
| DELETE | `/api/Products/DeleteProduct` | Elimina un producto por ID |
| POST | `/api/Auth/Login` | Inicia sesión con credenciales básicas |

---

## 🧠 Autor

**Javier Azaid Romero Vera**  
Desarrollador .NET | Prueba Técnica – 2025  

