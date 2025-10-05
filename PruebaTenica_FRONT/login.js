// URL base del endpoint de autenticación
const apiUrl = "http://localhost:5196/api/Auth/Login";

/**
 * Asigna el evento de envío del formulario de inicio de sesión.
 * Cuando el usuario envía el formulario, se evita el comportamiento por defecto
 * y se ejecuta la solicitud HTTP al backend para validar las credenciales.
 */
document.getElementById("loginForm").addEventListener("submit", async function (e) {
  e.preventDefault();

  const username = document.getElementById("username").value.trim();
  const password = document.getElementById("password").value.trim();
 
  try {
    const response = await fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password })
    });

    const data = await response.json();

    if (response.ok && data.procesoCorrecto) {
      localStorage.setItem("isLoggedIn", "true");
      window.location.href = "index.html";
    } else {
      alert(data.mensaje || "Credenciales incorrectas.");
    }
  } catch (error) {
    console.error("Error al iniciar sesión:", error);
    alert("Error al conectarse con el servidor.");
  }
});
