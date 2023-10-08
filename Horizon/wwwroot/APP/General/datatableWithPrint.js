$(document).ready(function () {
   /* $(win.document.body).css('direction', 'rtl');*/
    $('.dataTableWithPrint').DataTable({
        dom: 'Bfrtip',
        buttons: [{
            extend: 'print',
            customize: function (win) {
                $(win.document.body).css('direction', 'rtl');
            }
            
        },
            'copy', 'excel'
        ],
        direction:'rtl',
        "order": [],
        "language": {
            "sProcessing": "جارٍ التحميل...",
            "sLengthMenu": "أظهر _MENU_ مدخلات",
            "sZeroRecords": "لم يعثر على أية سجلات",
            "sInfo": "إظهار _START_ إلى _END_ من أصل _TOTAL_ مدخل",
            "sInfoEmpty": "يعرض 0 إلى 0 من أصل 0 سجل",
            "sInfoFiltered": "(منتقاة من مجموع _MAX_ مُدخل)",
            "sInfoPostFix": "",
            "sSearch": "ابحث:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "الأول",
                "sPrevious": "السابق",
                "sNext": "التالي",
                "sLast": "الأخير"
            }
        }
    });
});