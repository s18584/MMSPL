// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    console.log("Gotowe");

    $('#CreateUser').click(function () {
        console.log("Gotowe click");
        event.preventDefault();
        $.ajax({
            type: 'POST',
            url: '/UserManagement/CreateUser',
            success: function (output) {
                $('#CreateUserModalContent').html(output);
                $('#CreateUserModal').modal('show')//now its working
                
            },
            error: function (output) {
                alert("fail");
            }
        });
    });
});

$(function () {
    $('#AllUserTable').on('click', 'tbody tr', function (event) {
        $(this).addClass('table-active').siblings().removeClass('table-active');


    });

    $('#AllDocTypesTable').on('click', 'tbody tr', function (event) {
        $(this).addClass('table-active').siblings().removeClass('table-active');


    });

    $('#btnRowClick').click(function (e) {
        var rows = getHighlightRow();
        if (rows != undefined) {
            alert(rows.attr('id'));
        }
    });
});


$(function () {
    $('#UpdateUser').click(function () {
        var value = $(".table-active td:first").html();

        event.preventDefault();
        $.ajax({
            type: 'GET',
            url: '/UserManagement/EditUser/' + value,
            success: function (output) {
                $('#CreateUserModalContent').html(output);
                $('#CreateUserModal').modal('show')//now its working

            },
            error: function (output) {
                alert("fail");
            }
        });
        
    });
});

$(function () {
    console.log("Gotowe");

    $('#CreateDocType').click(function () {
        console.log("Gotowe click");
        event.preventDefault();
        $.ajax({
            type: 'GET',
            url: '/DocTypes/Create',
            success: function (output) {
                $('#DocTypeModalContent').html(output);
                $('#DocTypeModal').modal('show')//now its working

            },
            error: function (output) {
                alert("fail");
            }
        });
    });
});


$(function () {
    $('#EditDocType').click(function () {
        var value = $(".table-active td:first").html();

        event.preventDefault();
        $.ajax({
            type: 'GET',
            url: '/DocTypes/Edit/' + value,
            success: function (output) {
                $('#DocTypeModalContent').html(output);
                $('#DocTypeModal').modal('show')//now its working

            },
            error: function (output) {
                alert("fail");
            }
        });

    });
});


$(function () {
    $('#CreateCostType').click(function () {
        
        event.preventDefault();
        $.ajax({
            type: 'GET',
            url: '/CostTypes/Create',
            success: function (output) {
                $('#CostTypeModalContent').html(output);
                $('#CostTypeModal').modal('show')//now its working

            },
            error: function (output) {
                alert("fail");
            }
        });

    });
});


$(function () {
    $('#EditCostType').click(function () {
        var value = $(".table-active td:first").html();

        event.preventDefault();
        $.ajax({
            type: 'GET',
            url: '/CostTypes/Edit/' + value,
            success: function (output) {
                $('#CostTypeModalContent').html(output);
                $('#CostTypeModal').modal('show')//now its working

            },
            error: function (output) {
                alert("fail");
            }
        });

    });
});

$(function () {
    $('#DetailsCostType').click(function () {
        var value = $(".table-active td:first").html();

        event.preventDefault();
        $.ajax({
            type: 'GET',
            url: '/CostTypes/Details/' + value,
            success: function (output) {
                $('#CostTypeModalContent').html(output);
                $('#CostTypeModal').modal('show')//now its working

            },
            error: function (output) {
                alert("fail");
            }
        });

    });
});
