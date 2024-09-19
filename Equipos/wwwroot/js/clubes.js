window.onload = ListadoClubes();

function ListadoClubes()
{
    $.ajax({
        url: '../../Clubes/ListadoClubes',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function(listadoClubes){
            $("#ModalClubes").modal("hide");
            LimpiarModal();
            
            let tabla = ``

            $.each(listadoClubes, function(index, clubes){

                tabla += `
                <tr>
                    <td>${clubes.nombreClub}</td>
                    <td>${clubes.pais}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="ModalEditar(${clubes.clubID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="ValidarEliminacion(${clubes.clubID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("tbody-clubes").innerHTML = tabla;                                
        },
        error: function(xhr, status){
            console.log('Problemas al cargar la tabla');
        }
    });
}

function LimpiarModal(){
    document.getElementById("ClubID").value = 0;
    document.getElementById("NombreClub").value = "";
    document.getElementById("Pais").value = "";
}

function NuevoClub(){
    $("#tituloModal").text("Nuevo Club");
}

function GuardarClub(){
    let clubID = document.getElementById("ClubID").value;
    let nombreClub = document.getElementById("NombreClub").value;
    let pais = document.getElementById("Pais").value;


    $.ajax({
        url: '../../Clubes/GuardarClub',
        data: {
            clubID : clubID,
            nombreClub : nombreClub,
            pais : pais
        },
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if(resultado != "") {
                alert(resultado)
            }
            ListadoClubes();
        },
        error: function(xhr, status){
            console.log('Problemas al guardar Club');
        },
    });
}

function ModalEditar(clubID){
    $.ajax({
        url: '../../Clubes/ListadoClubes',
        data: { clubID : clubID },
        type: 'POST',
        dataType: 'json',
        success: function(listadoClubes){
            listadoClub = listadoClubes[0];
            
            document.getElementById("ClubID").value = clubID
            $("#tituloModal").text("Editar Club");
            document.getElementById("NombreClub").value = listadoClub.nombreClub;
            document.getElementById("Pais").value = listadoClub.pais;
            $("#ModalClubes").modal("show");
        },
        error: function(xhr, status){
            console.log('Problemas al cargar Club');
        }
    });
}

function ValidarEliminacion(clubID)
{
    var elimina = confirm("¿Esta seguro que desea eliminar?");
    if(elimina == true)
        {
            EliminarEmpleado(clubID);
        }
}

function EliminarEmpleado(clubID){
    $.ajax({
        url: '../../Clubes/EliminarClub',
        data: { clubID: clubID },
        type: 'POST',
        dataType: 'json',
        success: function(EliminarClub){
            ListadoClubes()
        },
        error: function(xhr, status){
            console.log('Problemas al eliminar Club');
        }
    });
}