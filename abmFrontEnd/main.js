const listUsersContainer = document.getElementById('listUsers');


async function listarUsuarios() {

    const getUsers = 'http://localhost:5021/api/usuarios/obtenerUsuarios';

    const response = await fetch(getUsers);
    const users = await response.json()
    console.log(users)

    let tableBodyContent = '';
    users.data.forEach(user => {
        tableBodyContent += `
                <tr>
                    <th scope="row">${user.usuarioId}</th>
                    <td>${user.nombre || 'N/A'}</td>
                    <td>${user.apellido || 'N/A'}</td>
                    <td>${user.email || 'No disponible'}</td>
                    <td>${user.dni || 'No disponible'}</td>
                    <td>
                    <button class="btn btn-danger">Eliminar</button>
                    </td>
                </tr>
            `;
    });
    listUsersContainer.innerHTML = tableBodyContent;

}
listarUsuarios()