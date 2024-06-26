$((function (e) {
    $("#basic-datatable").DataTable({
        language: {
            searchPlaceholder: "Search...",
            sSearch: ""
        }
    }), $("#responsive-datatable").DataTable({
        scrollX: "100%",
        language: {
            searchPlaceholder: "Search...",
            sSearch: ""
        }
    }), (a = $("#file-datatable").DataTable({
        buttons: ["copy", "excel", "pdf", "print"],
        scrollX: "100%",
        language: {
            searchPlaceholder: "Search...",
            sSearch: ""
        }
    })).buttons().container().appendTo("#file-datatable_wrapper .col-md-6:eq(0)");
    var a = $("#delete-datatable").DataTable({
        language: {
            searchPlaceholder: "Search...",
            sSearch: ""
        }
    });
    $("#delete-datatable tbody").on("click", "tr", (function () {
        $(this).hasClass("selected") ? $(this).removeClass("selected") : (a.$("tr.selected").removeClass("selected"), $(this).addClass("selected"))
    })), $("#button").click((function () {
        a.row(".selected").remove().draw(!1)
    })), $(".select2").select2({
        minimumResultsForSearch: 1 / 0
    })
}));