/**
 * Validación de sesión
 */
if (!localStorage.getItem("isLoggedIn")) {
    window.location.href = "login.html";
}

// URL base de la API de productos
const apiUrl = "http://localhost:5196/api/Products";

// Página actual y tamaño de página (cantidad de productos por página)
let currentPage = 1;
const pageSize = 5;

/**
 * Asigna los eventos principales de la interfaz:
 * - Buscar productos
 * - Navegar entre páginas
 * - Crear un nuevo producto
 */
document.getElementById("btnSearch").addEventListener("click", searchProducts);
document.getElementById("prevPage").addEventListener("click", prevPage);
document.getElementById("nextPage").addEventListener("click", nextPage);
document.getElementById("createForm").addEventListener("submit", createProduct);

// Carga inicial de productos al abrir la aplicación
loadProducts();

/**
 * Carga los productos según la página actual y el tamaño definido.
 * Llama a la función getProductsPaginated() para obtener los datos desde la API.
 */
async function loadProducts() {
    await getProductsPaginated(currentPage, pageSize);
}

/**
 * Obtiene los productos de la API con paginación y los muestra en la tabla.
 * Usa la función genérica apiRequest() para manejar la solicitud.
 * 
 * @param {number} page - Número de página actual.
 * @param {number} size - Cantidad de productos por página.
 */
async function getProductsPaginated(page, size) {
    const tableBody = document.getElementById("productsTableBody");
    const pageInfo = document.getElementById("pageInfo");
    tableBody.innerHTML = "";

    try {
        const data = await apiRequest(`/GetProductsPagination?Page=${page}&PageSize=${size}`, "GET");

        if (!data.procesoCorrecto || !data.respuesta || data.respuesta.length === 0) {
            alert(data.descripcion || "No se encontraron productos.");
            return;
        }

        data.respuesta.forEach(p => {
            const row = `
                <tr>
                    <td>${sanitizeHTML(p.nameProduct)}</td>
                    <td>${sanitizeHTML(p.sku)}</td>
                    <td>${p.quantity}</td>
                    <td>${p.priceSale}</td>
                    <td>${p.priceCost ?? "-"}</td>
                    <td>${new Date(p.dateModify).toLocaleString()}</td>
                    <td>
                        <button class="edit-btn" 
                            onclick="editProduct(${p.id}, '${sanitizeHTML(p.nameProduct)}', '${sanitizeHTML(p.sku)}', ${p.quantity}, ${p.priceSale}, ${p.priceCost ?? 0})">
                            ✏️ Editar
                        </button>
                        <button class="delete-btn" onclick="deleteProduct(${p.id})">🗑️ Eliminar</button>
                    </td>
                </tr>`;
            tableBody.innerHTML += row;
        });

        pageInfo.textContent = `Página ${currentPage}`;

    } catch (error) {
        console.error("Error al cargar productos:", error);
        alert("Error al conectarse con el servidor.");
    }
}

/**
 * Busca productos por nombre o SKU en la base de datos.
 * Limpia la tabla antes de mostrar los resultados y muestra alertas en caso de error.
 */
async function searchProducts() {
    const searchTerm = document.getElementById("searchInput").value.trim();
    const tableBody = document.getElementById("productsTableBody");
    const pageInfo = document.getElementById("pageInfo");

    tableBody.innerHTML = "";
    pageInfo.textContent = "";

    if (!searchTerm) {
        alert("Por favor ingresa un nombre o SKU para buscar.");
        return;
    }

    try {
        const data = await apiRequest(`/GetProductsByNameOrCode?SearchTerm=${encodeURIComponent(searchTerm)}`, "GET");
        if (!data.procesoCorrecto || !data.respuesta) {
            alert(data.descripcion || "No se encontraron productos.");
            return;
        }

        data.respuesta.forEach(p => {
            const row = `
                <tr>
                    <td>${sanitizeHTML(p.nameProduct)}</td>
                    <td>${sanitizeHTML(p.sku)}</td>
                    <td>${p.quantity}</td>
                    <td>${p.priceSale}</td>
                    <td>${p.priceCost ?? "-"}</td>
                    <td>${new Date(p.dateModify).toLocaleString()}</td>
                    <td>
                        <button class="edit-btn" 
                            onclick="editProduct(${p.id}, '${sanitizeHTML(p.nameProduct)}', '${sanitizeHTML(p.sku)}', ${p.quantity}, ${p.priceSale}, ${p.priceCost ?? 0})">
                            ✏️ Editar
                        </button>
                        <button class="delete-btn" onclick="deleteProduct(${p.id})">🗑️ Eliminar</button>
                    </td>
                </tr>`;
            tableBody.innerHTML += row;
        });

    } catch (error) {
        console.error("Error al buscar productos:", error);
        alert("Error al conectarse con el servidor.");
    }
}

/**
 * Avanza a la siguiente página de productos.
 * Incrementa el número de página actual y recarga la lista.
 */
function nextPage() {
    currentPage++;
    loadProducts();
}

/**
 * Retrocede a la página anterior de productos.
 * Solo decrementa si la página actual es mayor que 1.
 */
function prevPage() {
    if (currentPage > 1) {
        currentPage--;
        loadProducts();
    }
}

/**
 * Elimina un producto del sistema mediante una solicitud DELETE.
 * Solicita confirmación al usuario antes de ejecutar la acción.
 *
 * @param {number} id - Identificador único del producto a eliminar.
 */
async function deleteProduct(id) {
    if (!confirm("¿Seguro que deseas eliminar este producto?")) return;

    try {
        const data = await apiRequest("/DeleteProduct", "DELETE", { idProduct: id });

        if (!data.procesoCorrecto) {
            alert(data.descripcion || "No se pudo eliminar el producto.");
            return;
        }

        alert("Producto eliminado correctamente.");
        loadProducts();

    } catch (error) {
        console.error("Error al eliminar producto:", error);
        alert("Error al conectarse con el servidor.");
    }
}

/**
 * Envía una solicitud al backend para crear un nuevo producto.
 * Valida los datos del formulario, realiza la petición HTTP POST y actualiza la tabla de productos.
 *
 * @param {Event} e - Evento del formulario que se previene para evitar el comportamiento por defecto.
 */
async function createProduct(e) {
    e.preventDefault();

    const nameProduct = document.getElementById("nameProduct").value.trim();
    const sku = document.getElementById("sku").value.trim();
    const quantity = parseInt(document.getElementById("quantity").value);
    const priceSale = parseFloat(document.getElementById("priceSale").value);
    const priceCost = parseFloat(document.getElementById("priceCost").value) || 0;

    const isValid = validateProduct({ nameProduct, sku, quantity, priceSale, priceCost });
    if (!isValid) return;

    try {
        const data = await apiRequest("/CreateProduct", "POST", {
            nameProduct,
            sku,
            quantity,
            priceSale,
            priceCost
        });

        if (!data.procesoCorrecto) {
            alert(data.descripcion || "Error al crear el producto.");
            return;
        }

        alert("Producto creado correctamente.");
        document.getElementById("createForm").reset();
        loadProducts();

    } catch (error) {
        console.error("Error al crear producto:", error);
        alert("Error al conectarse con el servidor.");
    }
}

/**
 * Valida los datos ingresados de un producto antes de enviarlos al servidor.
 * Se asegura de que los campos obligatorios estén completos y que los valores numéricos sean válidos.
 *
 * @param {Object} product - Objeto que contiene los valores del producto a validar.
 * @param {string} product.nameProduct - Nombre del producto.
 * @param {string} product.sku - Código SKU del producto.
 * @param {number} product.quantity - Cantidad disponible.
 * @param {number} product.priceSale - Precio de venta.
 * @param {number} product.priceCost - Precio de costo (opcional).
 * @returns {boolean} - Retorna true si la validación es exitosa, false si falla.
 */
function validateProduct({ nameProduct, sku, quantity, priceSale, priceCost }) {
    if (!nameProduct || !sku) {
        alert("Por favor completa todos los campos obligatorios.");
        return false;
    }

    if (quantity <= 0) {
        alert("La cantidad debe ser mayor a 0.");
        return false;
    }

    if (priceSale <= 0) {
        alert("El precio de venta debe ser mayor a 0.");
        return false;
    }

    if (priceCost < 0) {
        alert("El precio de costo no puede ser negativo.");
        return false;
    }

    return true;
}

/**
 * Carga los datos de un producto en el formulario para su edición.
 * Cambia el título y los botones del formulario para reflejar el modo de edición.
 *
 * @param {number} id - Identificador del producto a editar.
 * @param {string} nameProduct - Nombre actual del producto.
 * @param {string} sku - Código SKU actual del producto.
 * @param {number} quantity - Cantidad disponible del producto.
 * @param {number} priceSale - Precio de venta actual.
 * @param {number} priceCost - Precio de costo actual.
 */
function editProduct(id, nameProduct, sku, quantity, priceSale, priceCost) {
    document.getElementById("idProduct").value = id;
    document.getElementById("nameProduct").value = nameProduct;
    document.getElementById("sku").value = sku;
    document.getElementById("quantity").value = quantity;
    document.getElementById("priceSale").value = priceSale;
    document.getElementById("priceCost").value = priceCost;

    document.getElementById("formTitle").textContent = "Editar producto";
    document.getElementById("btnCreate").style.display = "none";
    document.getElementById("btnUpdate").style.display = "inline-block";
    document.getElementById("btnCancel").style.display = "inline-block";
}

/**
 * Actualiza un producto existente mediante la API.
 * Valida los campos antes de enviar la solicitud y muestra mensajes según el resultado.
 */
async function updateProduct() {
    const idProduct = parseInt(document.getElementById("idProduct").value);
    const nameProduct = document.getElementById("nameProduct").value.trim();
    const sku = document.getElementById("sku").value.trim();
    const quantity = parseInt(document.getElementById("quantity").value);
    const priceSale = parseFloat(document.getElementById("priceSale").value);
    const priceCost = parseFloat(document.getElementById("priceCost").value) || 0;

    const isValid = validateProduct({ nameProduct, sku, quantity, priceSale, priceCost });
    if (!isValid) return;

    try {
        const data = await apiRequest("/UpdateProduct", "PUT", {
            id: idProduct,
            nameProduct,
            sku,
            quantity,
            priceSale,
            priceCost
        });

        if (!data.procesoCorrecto) {
            alert(data.descripcion || "Error al actualizar producto.");
            return;
        }

        alert("Producto actualizado correctamente.");
        resetForm();
        loadProducts();

    } catch (error) {
        console.error("Error al actualizar producto:", error);
        alert("Error al conectarse con el servidor.");
    }
}

/**
 * Realiza una solicitud HTTP genérica a la API.
 * @param {string} endpoint - Ruta del endpoint (por ejemplo, "/UpdateProduct").
 * @param {string} method - Método HTTP (GET, POST, PUT, DELETE).
 * @param {Object} [body=null] - Cuerpo de la solicitud en formato JSON.
 * @returns {Promise<Object>} - Retorna la respuesta parseada en JSON.
 * @throws {Error} - Si ocurre un error HTTP o de conexión.
 */
async function apiRequest(endpoint, method, body = null) {
    const options = {
        method,
        headers: { "Content-Type": "application/json" }
    };

    if (body) options.body = JSON.stringify(body);

    const response = await fetch(`${apiUrl}${endpoint}`, options);

    if (!response.ok) {
        throw new Error(`Error HTTP: ${response.status}`);
    }

    const data = await response.json();
    return data;
}

/**
 * Restablece el formulario a su estado inicial.
 * Limpia los campos, oculta los botones de actualización/cancelación
 * y vuelve a mostrar el botón de creación.
 */
function resetForm() {
    document.getElementById("createForm").reset();
    document.getElementById("idProduct").value = "";
    document.getElementById("formTitle").textContent = "Crear nuevo producto";
    document.getElementById("btnCreate").style.display = "inline-block";
    document.getElementById("btnUpdate").style.display = "none";
    document.getElementById("btnCancel").style.display = "none";
}

/**
 * Sanitiza una cadena de texto para prevenir ataques XSS.
 * Convierte caracteres especiales en texto seguro antes de renderizarlos en el HTML.
 * @param {string} str - Texto a sanitizar.
 * @returns {string} - Texto limpio y seguro.
 */
function sanitizeHTML(str) {
    const div = document.createElement("div");
    div.textContent = str;
    return div.innerHTML;
}

/**
 * Cierra la sesión del usuario.
 * Elimina la marca de sesión en el almacenamiento local (localStorage)
 * y redirige al usuario a la pantalla de inicio de sesión.
 */
document.getElementById("logoutBtn").addEventListener("click", () => {
    localStorage.removeItem("isLoggedIn");
    window.location.href = "login.html";
});

/**
 * Asigna los eventos principales del formulario de productos.
 * - Crear: Envía un nuevo producto a la API.
 * - Actualizar: Modifica un producto existente.
 * - Cancelar: Restaura el formulario sin guardar cambios.
 */
document.getElementById("createForm").addEventListener("submit", createProduct);
document.getElementById("btnUpdate").addEventListener("click", updateProduct);
document.getElementById("btnCancel").addEventListener("click", resetForm);


