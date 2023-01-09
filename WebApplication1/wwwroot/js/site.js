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

$(function () {
    $('#summernote').summernote({
        placeholder: 'Wpisz treść tutaj',
        tabsize: 2,
        height: 400,
        hint: {
            mentions: ['FirstName', 'LastName', 'Email', 'Area', 'Age', 'Address', 'PostCode', 'City', 'FullName','BirthDate'],
            match: /\B$(\w*)$/,
            search: function (keyword, callback) {
                callback($.grep(this.mentions, function (item) {
                    return item.indexOf(keyword) == 0;
                }));
            },
            content: function (item) {
                return item + '$';
            }
        }
    });
});

$(document).ready(function () {
    $('#postcode-input').inputmask();
    $('#date-input').inputmask();
});


$(document).ready(function () {
    
    $('#dataTable').DataTable({

        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'fr>>" +
             "<'row'<'col-sm-12't>>" +
             "<'row'<'col-sm-12 col-md-4'i><'col-sm-12 col-md-8'p>>",

        colReorder: true,
        pagingType: "full_numbers",
        lengthMenu: [10, 25, 50, 75, 100],
        lengthChange: true,

        buttons: [
            {
                extend: 'copy',
                text: 'Kopiuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'csv',
                text: 'CSV',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'pdf',
                text: 'PDF',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'print',
                text: 'Drukuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'colvis',
                text: 'Wyświetlane kolumny'
            }
        ],
        language: {
            buttons: {
                copySuccess: {
                    1: "Skopiowano rekord do schowka",
                    _: "Skopiowano %d rekordów do schowka"
                },
                copyTitle: 'Skopiowano do schowka'
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            },
            paginate: {
                first: "Pierwsza",
                previous: "Poprzednia",
                next: "Następna",
                last: "Ostatnia"
            },
            lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
            search: "Szukaj:",
            decimal: "",
            emptyTable: "Brak rekordów",
            info: "Wyświetlono _START_ - _END_ z _TOTAL_ rekordów",
            infoEmpty: "Wyświetlono  0 - 0 z 0 rekordów",
            infoFiltered: "(przeszukano _MAX_ rekordów)",
            infoPostFix: "",
            thousands: ",",
            lengthMenu: "Show _MENU_ entries",
            loadingRecords: "Loading...",
            processing: "",
            zeroRecords: "Brak dopasowań"
        }
    });

    $('#dataTable1').DataTable({
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'fr>>" +
            "<'row'<'col-sm-12't>>" +
            "<'row'<'col-sm-12 col-md-4'i><'col-sm-12 col-md-8'p>>",
        colReorder: true,
        pagingType: "full_numbers",
        lengthMenu: [10, 25, 50, 75, 100],
        lengthChange: true,
        

        buttons: [
            {
                extend: 'copy',
                text: 'Kopiuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'csv',
                text: 'CSV',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'pdf',
                text: 'PDF',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'print',
                text: 'Drukuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'colvis',
                text: 'Wyświetlane kolumny'
            }
        ],
        language: {
            buttons: {
                copySuccess: {
                    1: "Skopiowano rekord do schowka",
                    _: "Skopiowano %d rekordów do schowka"
                },
                copyTitle: 'Skopiowano do schowka'
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            },
            paginate: {
                first: "Pierwsza",
                previous: "Poprzednia",
                next: "Następna",
                last: "Ostatnia"
            },
            lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
            search: "Szukaj:",
            decimal: "",
            emptyTable: "Brak rekordów",
            info: "Wyświetlono _START_ - _END_ z _TOTAL_ rekordów",
            infoEmpty: "Wyświetlono  0 - 0 z 0 rekordów",
            infoFiltered: "(przeszukano _MAX_ rekordów)",
            infoPostFix: "",
            thousands: ",",
            lengthMenu: "Show _MENU_ entries",
            loadingRecords: "Loading...",
            processing: "",
            zeroRecords: "Brak dopasowań"
        }
    });

    $('#dataTable2').DataTable({
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'fr>>" +
            "<'row'<'col-sm-12't>>" +
            "<'row'<'col-sm-12 col-md-4'i><'col-sm-12 col-md-8'p>>",
        colReorder: true,
        pagingType: "full_numbers",
        lengthMenu: [10, 25, 50, 75, 100],
        lengthChange: true,


        buttons: [
            {
                extend: 'copy',
                text: 'Kopiuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'csv',
                text: 'CSV',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'pdf',
                text: 'PDF',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'print',
                text: 'Drukuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'colvis',
                text: 'Wyświetlane kolumny'
            }
        ],
        language: {
            buttons: {
                copySuccess: {
                    1: "Skopiowano rekord do schowka",
                    _: "Skopiowano %d rekordów do schowka"
                },
                copyTitle: 'Skopiowano do schowka'
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            },
            paginate: {
                first: "Pierwsza",
                previous: "Poprzednia",
                next: "Następna",
                last: "Ostatnia"
            },
            lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
            search: "Szukaj:",
            decimal: "",
            emptyTable: "Brak rekordów",
            info: "Wyświetlono _START_ - _END_ z _TOTAL_ rekordów",
            infoEmpty: "Wyświetlono  0 - 0 z 0 rekordów",
            infoFiltered: "(przeszukano _MAX_ rekordów)",
            infoPostFix: "",
            thousands: ",",
            lengthMenu: "Show _MENU_ entries",
            loadingRecords: "Loading...",
            processing: "",
            zeroRecords: "Brak dopasowań"
        }
    });
    $('#dataTable4').DataTable({
        dom: "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'fr>>" +
            "<'row'<'col-sm-12't>>" +
            "<'row'<'col-sm-12 col-md-4'i><'col-sm-12 col-md-8'p>>",
        colReorder: true,
        pagingType: "full_numbers",
        lengthMenu: [10, 25, 50, 75, 100],
        lengthChange: true,


        buttons: [
            {
                extend: 'copy',
                text: 'Kopiuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'csv',
                text: 'CSV',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'pdf',
                text: 'PDF',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'print',
                text: 'Drukuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'colvis',
                text: 'Wyświetlane kolumny'
            }
        ],
        language: {
            buttons: {
                copySuccess: {
                    1: "Skopiowano rekord do schowka",
                    _: "Skopiowano %d rekordów do schowka"
                },
                copyTitle: 'Skopiowano do schowka'
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            },
            paginate: {
                first: "Pierwsza",
                previous: "Poprzednia",
                next: "Następna",
                last: "Ostatnia"
            },
            lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
            search: "Szukaj:",
            decimal: "",
            emptyTable: "Brak rekordów",
            info: "Wyświetlono _START_ - _END_ z _TOTAL_ rekordów",
            infoEmpty: "Wyświetlono  0 - 0 z 0 rekordów",
            infoFiltered: "(przeszukano _MAX_ rekordów)",
            infoPostFix: "",
            thousands: ",",
            lengthMenu: "Show _MENU_ entries",
            loadingRecords: "Loading...",
            processing: "",
            zeroRecords: "Brak dopasowań"
        }
    });

    $('#dataTableFilters').DataTable({
        
        dom: "<'row'<'col-sm-12'Q>>" +
             "<'row'<'col-sm-12'P>>" +
             "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'fr>>" +
             "<'row'<'col-sm-12't>>" +
             "<'row'<'col-sm-12 col-md-4'i><'col-sm-12 col-md-8'p>>",
           
        colReorder: true,
        pagingType: "full_numbers",
        lengthMenu: [10, 25, 50, 75, 100],
        lengthChange: true,
        searchPanes: {
            cascadePanes: true,
            initCollapsed: true
        },
        columnDefs: [
            {                
                targets: 0,
                orderable: false,
                searchable: false
            },
            {
                targets: [5],
                searchPanes: {
                    show: true,

                },
                orderable: true,
                searchable: true
            },
            {
                targets: [7],
                searchPanes: {
                    show: true,
                    
                },
                orderable: true,
                searchable: true,
            },
            {
                targets: [9],
                searchPanes: {
                    show: true,
                    
                },
                orderable: true,
                searchable: true                
            },
        ],

        searchBuilder: {
            columns: [1, 2, 4, 5, 6, 7, 8, 9]
        },

        buttons: [
            {
                extend: 'copy',
                text: 'Kopiuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'csv',
                text: 'CSV',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'pdf',
                text: 'PDF',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'print',
                text: 'Drukuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'colvis',
                text: 'Wyświetlane kolumny'
            }
        ],
        language: {
            buttons: {
                copySuccess: {
                    1: "Skopiowano rekord do schowka",
                    _: "Skopiowano %d rekordów do schowka"
                },
                copyTitle: 'Skopiowano do schowka'
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            },
            paginate: {
                first: "Pierwsza",
                previous: "Poprzednia",
                next: "Następna",
                last: "Ostatnia"
            },
            lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
            search: "Szukaj:",
            decimal: "",
            emptyTable: "Brak rekordów",
            info: "Wyświetlono _START_ - _END_ z _TOTAL_ rekordów",
            infoEmpty: "Wyświetlono  0 - 0 z 0 rekordów",
            infoFiltered: "(przeszukano _MAX_ rekordów)",
            infoPostFix: "",
            thousands: ",",
            lengthMenu: "Show _MENU_ entries",
            loadingRecords: "Loading...",
            processing: "",
            zeroRecords: "Brak dopasowań"
        }



    });

    $('#dataTableFiltersLogs').DataTable({

        dom: "<'row'<'col-sm-12'Q>>" +
            "<'row'<'col-sm-12'P>>" +
            "<'row'<'col-sm-12 col-md-6'B><'col-sm-12 col-md-6'fr>>" +
            "<'row'<'col-sm-12't>>" +
            "<'row'<'col-sm-12 col-md-4'i><'col-sm-12 col-md-8'p>>",

        colReorder: true,
        pagingType: "full_numbers",
        lengthMenu: [10, 25, 50, 75, 100],
        lengthChange: true,
        searchPanes: {
            cascadePanes: false,
            initCollapsed: true
        },
        
        buttons: [
            {
                extend: 'copy',
                text: 'Kopiuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'csv',
                text: 'CSV',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'pdf',
                text: 'PDF',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'print',
                text: 'Drukuj',
                exportOptions: {
                    columns: ':visible :not(#actionColumn)'
                }
            },
            {
                extend: 'colvis',
                text: 'Wyświetlane kolumny'
            }
        ],
        language: {
            buttons: {
                copySuccess: {
                    1: "Skopiowano rekord do schowka",
                    _: "Skopiowano %d rekordów do schowka"
                },
                copyTitle: 'Skopiowano do schowka'
            },
            aria: {
                sortAscending: ": activer pour trier la colonne par ordre croissant",
                sortDescending: ": activer pour trier la colonne par ordre décroissant"
            },
            paginate: {
                first: "Pierwsza",
                previous: "Poprzednia",
                next: "Następna",
                last: "Ostatnia"
            },
            lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
            search: "Szukaj:",
            decimal: "",
            emptyTable: "Brak rekordów",
            info: "Wyświetlono _START_ - _END_ z _TOTAL_ rekordów",
            infoEmpty: "Wyświetlono  0 - 0 z 0 rekordów",
            infoFiltered: "(przeszukano _MAX_ rekordów)",
            infoPostFix: "",
            thousands: ",",
            lengthMenu: "Show _MENU_ entries",
            loadingRecords: "Loading...",
            processing: "",
            zeroRecords: "Brak dopasowań"
        }



    });
    

   
});




