window.onload = ListaJugadores();

function ListaJugadores()
{
    $.ajax({
        url: '../../Jugadores/ListaJugadores',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function(listadoJugadores){
            $("#ModalClubes").modal("hide");
            LimpiarModal();
            
            let tabla = ``

            $.each(listadoJugadores, function(index, jugadores){

                tabla += `
                <tr>
                    <td>${jugadores.nombreJugador}</td>
                    <td>${jugadores.nombreClub}</td>
                    <td>${jugadores.posicion}</td>
                    <td>${jugadores.edad}</td>
                    <td>${jugadores.nacionalidad}</td>
                    <td>${jugadores.ingreso}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="ModalEditar(${jugadores.jugadorID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="ValidarEliminacion(${jugadores.jugadorID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("tbody-jugadores").innerHTML = tabla;                                
        },
        error: function(xhr, status){
            console.log('Problemas al cargar la tabla');
        }
    });
}

function LimpiarModal(){
    document.getElementById("JugadorID").value = 0;
    document.getElementById("ClubID").value = 0;
    document.getElementById("Posicion").value = 0;
    document.getElementById("Edad").value = "";
    document.getElementById("Ingreso").value = "";
    document.getElementById("NombreJugador").value = "";
    document.getElementById("Nacionalidad").value = "";
}

function NuevoJugador(){
    $("#tituloModal").text("Nuevo Jugador");
}

function GuardarJugador(){
    let jugadorID = document.getElementById("JugadorID").value;
    let clubID = document.getElementById("ClubID").value;
    let nombreJugador = document.getElementById("NombreJugador").value;
    let posicion = document.getElementById("Posicion").value;
    let edad = document.getElementById("Edad").value;
    let nacionalidad = document.getElementById("Nacionalidad").value;
    let ingreso = document.getElementById("Ingreso").value;


    $.ajax({
        url: '../../Jugadores/GuardarJugador',
        data: {
            clubID : clubID,
            jugadorID : jugadorID,
            nombreJugador : nombreJugador,
            posicion : posicion,
            edad : edad,
            nacionalidad : nacionalidad,
            ingreso : ingreso
        },
        type: 'POST',
        dataType: 'json',
        success: function(resultado){
            if(resultado != "") {
                alert(resultado)
            }
            ListaJugadores();
        },
        error: function(xhr, status){
            console.log('Problemas al guardar Jugador');
        },
    });
}

function ModalEditar(jugadorID){
    $.ajax({
        url: '../../Jugadores/ListaJugadores',
        data: { jugadorID : jugadorID },
        type: 'POST',
        dataType: 'json',
        success: function(listadoJugadores){
        let listadoJugador = listadoJugadores[0];
            
            document.getElementById("JugadorID").value = jugadorID
            $("#tituloModal").text("Editar Jugador");
            document.getElementById("ClubID").value = listadoJugador.ClubID;
            document.getElementById("NombreJugador").value = listadoJugador.nombreJugador;
            document.getElementById("Posicion").value = listadoJugador.posicion;
            document.getElementById("Nacionalidad").value = listadoJugador.nacionalidad;
            document.getElementById("Edad").value = listadoJugador.edad;
            document.getElementById("Ingreso").value = listadoJugador.ingreso;
            $("#ModalJugadores").modal("show");
        },
        error: function(xhr, status){
            console.log('Problemas al cargar Jugador');
        }
    });
}

function ValidarEliminacion(jugadorID)
{
    var elimina = confirm("¿Esta seguro que desea eliminar?");
    if(elimina == true)
        {
            EliminarJugador(jugadorID);
        }
}

function EliminarJugador(jugadorID){
    $.ajax({
        url: '../../Jugadores/EliminarJugador',
        data: { jugadorID: jugadorID },
        type: 'POST',
        dataType: 'json',
        success: function(EliminarJugador){
            ListaJugadores()
        },
        error: function(xhr, status){
            console.log('Problemas al eliminar Jugador');
        }
    });
}