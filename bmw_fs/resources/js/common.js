$(function () {    
    $(".add_img_area").find(".file_area").on("change", ".input_file", function (e) {
        var $parent = $(this).closest("div");
        var val = $(this).val();
        var valTit = val.split("\\");
        var valTittext = valTit[valTit.length - 1]
        if (val != "") {
            $parent.find(".file_src").text(valTittext).addClass("on");
        };
    });    

    $('.search_text').keyup(function (e) {
        if (e.keyCode === 13) {
            searchList();
        }
    });
    $('#sdate').change(function () {
        $('#edate').attr('min', $(this).val());
    });
    $('#edate').change(function () {
        $('#sdate').attr('max', $(this).val());
    });
});
function searchList() {
    $('#page').val(1);
    $('#content form').submit();
};

function lnbSet(mainIdx, subIdx) {
    $('.menu_list li').removeClass('on');
    if (subIdx !== 0) {
        $('.menu_list>li:nth-child(' + mainIdx + ')').find('li:nth-child(' + subIdx + ')').addClass('on');
    } else {
        $('.menu_list>li:nth-child(' + mainIdx + ')').addClass('on');
    }
};

function addFileBtn(target) {
    var fileWrapper = $(target).closest('div');
    var fileName = fileWrapper.find('input[type="file"]').attr('name');
    var fileIdxs = fileWrapper.find('input[type="hidden"]').attr('name');
    var html = '';
    html += '<div class="file-wrapper">';
    html += '<input type="text" class="file_text" value="" readonly="readonly" />';
    html += '<span class="btn_area file_area">';
    html += '<button type="button" class="btn file_btn">파일 선택</button>';
    html += '<input type="file" name="' + fileName + '" class="file_input" />';
    html += '</span>';
    html += '<input type="hidden" name="' + fileIdxs+'" value="" />';
    html += '</div>';
    var appendTarget = $(target).parents('td').find('.file-wrapper:last');
    appendTarget.after(html);
};
