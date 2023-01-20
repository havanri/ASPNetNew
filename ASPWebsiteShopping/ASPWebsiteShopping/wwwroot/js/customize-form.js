$(function () {
/*    $('.select2').select2()*/

    //Initialize Select2 Elements

    //chủng loại
    $(".js-species-select").select2({
        tags: true,
        tokenSeparators: [',']
    });
    //select-option-dropdown 
    $('.js-example-basic-single').select2();

    //tag
    $("#Tags").select2({
        tags: true,
        tokenSeparators: [',']
    });
    //datatable
    $("#example1").DataTable({
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#example2').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
    //
});