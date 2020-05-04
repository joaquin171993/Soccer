var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {

    dataTable = $("#tblTeams").DataTable({
        "ajax": {
            "url": "/admin/teams/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "30%" },
            {
                "data": "logoPath",
                "render": function (imagen) {
                    if (imagen != null) {
                        var imagen = imagen.substring(1);
                    } else {
                        imagen = "/images/Teams/blanco.jpg";
                    }                
                    return `<img src="${imagen}" style="width:100px;height:100px;max-width: 100%; height: auto; text-align:center"/>`;
                }, "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <table class="table table-bordered">
                                    <tbody>
                                    <tr>
                                        <td scope="row">
                                            <a href='/Admin/Teams/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; width:50px;'>
                                                <i class='fas fa-edit'></i> 
                                            </a> 
                                        </td>
                                        <td>  
                                            <a onclick=Delete("/Admin/Teams/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer; width:50px;'>
                                                <i class='fas fa-ban'></i> 
                                            </a>
                                        </td>
                                        <td>
                                            <a href='/Admin/Activos/Teams/${data}' class='btn btn-info text-white' style='cursor:pointer; width:50px;'>
                                                <i class='fas fa-info'></i> 
                                             </a> 
                                        </td>
                                    </tr>
                                 </tbody>
                                </table>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}


function Delete(url) {
    swal({
        title: "¿Está seguro que desea anular el registro?",
        text: "En caso de anularlo puede volver a recuperarlo en la opción Configuración de Registros",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, anularlo!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}