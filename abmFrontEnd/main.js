const listUsersContainer = document.getElementById('listUsers');
const ModalEliminar = document.getElementById('ModalEliminar')
const ModalCrear = document.getElementById('ModalCrear')
const ModalEditar = document.getElementById('ModalEditar')


let modEliminar = new bootstrap.Modal(ModalEliminar);
let idUsuario = null;
let modCrear = new bootstrap.Modal(ModalCrear);
let modEditar = new bootstrap.Modal(ModalEditar);

async function listarUsuarios() {
    const response = await fetch('http://localhost:5021/api/usuarios/obtenerUsuarios');
    const users = await response.json()
    console.log('resultado consolelog:', users)

    let tableBodyContent = '';
    users.forEach(user => {
        tableBodyContent += `
                <tr>
                    <th scope="row" data-id= ${user.usuarioId}>${user.usuarioId}</th>
                    <td>${user.nombre || 'N/A'}</td>
                    <td>${user.apellido || 'N/A'}</td>
                    <td>${user.email || 'No disponible'}</td>
                    <td>${user.dni || 'No disponible'}</td>
                    <td><button type="button" class="btn btn-danger" onclick="AbrirModalEliminar(${user.usuarioId})">Eliminar</button></td>
                    <td> <button type="button" class="btn btn-primary" onclick="AbrirModalEditar(${user.usuarioId})">Editar</button></td>
                </tr>
            `;
    });
    listUsersContainer.innerHTML = tableBodyContent;
}

async function AbrirModalEliminar(id) {
    idUsuario = id;

    const mostrarDatos = document.getElementById("info-usuario")
    mostrarDatos.textContent = `¿Seguro desea eliminar al usuario de Id = ${idUsuario}?`;

    modEliminar.show();
}

async function ConfirmarEliminarUsuario() {
    const response = await fetch(`http://localhost:5021/api/usuarios/eliminarUsuario/${idUsuario}`, { method: 'DELETE' });
    const user = await response.json()
    console.log('resultado consolelog:', user)

    modEliminar.hide();
    listarUsuarios();
}

async function AbrirModalCrear() {
    document.getElementById('nombre').value = '';
    document.getElementById('apellido').value = '';
    document.getElementById('password').value = '';
    document.getElementById('email').value = '';
    document.getElementById('dni').value = '';
    modCrear.show()
}

async function AgregarNuevoUsuario() {
    const nombre = document.getElementById('nombre').value;
    const apellido = document.getElementById('apellido').value;
    const password = document.getElementById('password').value;
    const email = document.getElementById('email').value;
    const dni = parseInt(document.getElementById('dni').value);

    const alertBox = document.getElementById('alert-error');
    alertBox.classList.add('d-none');

    if (!nombre && !apellido && !password && !email && !dni) {
        alertBox.textContent = 'Es necesario completar los datos';
        alertBox.classList.remove('d-none');
        alertBox.classList.replace('alert-danger', 'alert-warning')
        return;
    }

    const response = await fetch(`http://localhost:5021/api/usuarios/crearUsuario`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            nombre: nombre,
            apellido: apellido,
            password: password,
            email: email,
            dni: dni
        })
    });

    const mensaje = await response.text() //devuelve un mensaje de texto
    console.log('resultado consolelog:', mensaje)

    if (mensaje.includes('correctamente')) {
        modCrear.hide();
        listarUsuarios();
    } else {
        alertBox.textContent = mensaje;
        alertBox.classList.remove('d-none');
    }
}

async function AbrirModalEditar(id) {
    idUsuario = id;

    const response = await fetch('http://localhost:5021/api/usuarios/obtenerUsuarios');
    const users = await response.json()
    console.log('resultado consolelog:', users)

    let usuarioSelec = users.find(u => u.usuarioId === idUsuario);
    console.log('resultado consolelog:', usuarioSelec)

    document.getElementById('nombre-edit').value = usuarioSelec.nombre;
    document.getElementById('apellido-edit').value = usuarioSelec.apellido;
    document.getElementById('password-edit').value = usuarioSelec.password;
    document.getElementById('email-edit').value = usuarioSelec.email;
    document.getElementById('dni-edit').value = usuarioSelec.dni;

    modEditar.show()
}

async function EditarUsuario() {
    const nombre = document.getElementById('nombre-edit').value;
    const apellido = document.getElementById('apellido-edit').value;
    const password = document.getElementById('password-edit').value;
    const email = document.getElementById('email-edit').value;
    const dni = parseInt(document.getElementById('dni-edit').value);

    const alertBoxEdit = document.getElementById('alert-edit-error');
    alertBoxEdit.classList.add('d-none');

    if (!nombre && !apellido && !password && !email && !dni) {
        alertBoxEdit.textContent = 'Es necesario completar los datos';
        alertBoxEdit.classList.remove('d-none');
        alertBoxEdit.classList.replace('alert-danger', 'alert-warning')
        return;
    }

    const response = await fetch(`http://localhost:5021/api/usuarios/editarUsuario/${idUsuario}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            nombre: nombre,
            apellido: apellido,
            password: password,
            email: email,
            dni: dni
        })
    });

    const mensaje = await response.text();
    console.log('resultado consolelog:', mensaje)

    if (mensaje.includes('correctamente')) {
        modEditar.hide();
        listarUsuarios();
    } else {
        alertBoxEdit.textContent = mensaje;
        alertBoxEdit.classList.remove('d-none');
    }
}

listarUsuarios();