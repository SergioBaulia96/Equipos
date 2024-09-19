window.onload = ListadoInformes();

function ListadoInformes() {
    let buscarDesde = document.getElementById("BuscarDesde").value;
    let buscarHasta = document.getElementById("BuscarHasta").value;

    $.ajax({
        url: '../../Jugadores/ListadoInformes',
        data: {
            id: null,
            buscarDesde: buscarDesde,
            buscarHasta: buscarHasta
        },
        type: 'POST',
        dataType: 'json',
        success: function (clubesJugadoresMostrar){
            let contenidoTabla = ``;
            let agrupadoPorTipo = {};

            $.each(clubesJugadoresMostrar, function (index, club) {
                // Agrupar por TipoclubDescripcion
                if (!agrupadoPorTipo[club.nombreClub]) {
                    agrupadoPorTipo[club.nombreClub] = [];
                }
                agrupadoPorTipo[club.nombreClub].push(club);
            });

            // Construir el contenido de la tabla
            for (let club in agrupadoPorTipo) {
                contenidoTabla += `
                <tr>
                    <td>${club}</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                `;

                $.each(agrupadoPorTipo[club], function (index, club) {
                    contenidoTabla += `
                    <tr>
                        <td></td>
                        <td>${club.nombreJugador}</td>
                        <td>${club.posicion}</td>
                        <td>${club.edad}</td>
                        <td>${club.nacionalidad}</td>
                        <td>${club.ingreso}</td>
                    </tr>
                    `
                });
        }
        document.getElementById("tbody-informeclubes").innerHTML = contenidoTabla;
    },
    error: function (xhr, status) {
        alert('Disculpe, existió un problema al procesar la solicitud.');
    }
    });
}