const feedBack = {
    Sucsess: 'تم الحفظ بنجاح'
}
const DestroyDataTable = () => {
    var table = $('.dataTable').DataTable();
    table.clear();
    table.destroy();
}

const DataTableWithFilter = (pagesize=10) => {
    $('table[id]').filter(function () {
        return (/DataTables/i).test($(this).attr('id'));
    }).dataTable({
        "order": [],
        layout: {
            topStart: {
                buttons: ["csv", "excel", "pdf", "print"]
            }
        },
        "pageLength": pagesize,
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
}

$(window).load(function () {
    // Animate loader off screen
    $(".se-pre-con").fadeOut("slow");
});

$(document).ready(function () {
    $('.date').datepicker({
        dateFormat: "DD, MM d, yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "1940:2050"
    });
    $('.smallDate').datepicker({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "1940:2050"
    });

    $('[data-toggle="tooltip"]').tooltip();

    const params = new URLSearchParams(window.location.search)
    if (params.has('success')) {
        var alert = document.getElementById('feedback')
        alert.classList.add('show');
        alert.innerHTML = feedBack.Sucsess;
        alert.innerHTML += "<button type='button' class='btn-close'" +
            "data-bs-dismiss='alert'"
            + "aria-label='Close' ></button >";
    }
        
    
    $('.dataTableWithPrint').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'print'
        ],
        "order": [],
        layout: {
            topStart: {
                buttons: ["csv", "excel", "pdf", "print"]
            }
        },
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

    var table = $('.dataTable').DataTable({
      
        "order": [],
        layout: {
            topStart: {
                buttons:["csv","excel","pdf","print"]
            }
        },
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

    //$('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
    //    if (!$(this).next().hasClass('show')) {
    //        $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
    //    }
    //    var $subMenu = $(this).next(".dropdown-menu");
    //    $subMenu.toggleClass('show');


    //    $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
    //        $('.dropdown-submenu .show').removeClass("show");
    //    });


    //    return false;
    //});

});

