const listUsersContainer = document.getElementById('listUsers'); 

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
                    <td><button type="button" class="btn btn-danger">Eliminar</button></td>
                    <td> <button type="button" class="btn btn-primary">Editar</button></td>
                </tr>
            `;
    });
    listUsersContainer.innerHTML = tableBodyContent;
}

// DOM
listarUsuarios()