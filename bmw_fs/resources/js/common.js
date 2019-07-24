$(function () {  
    $(".add_img_area").find(".file_area").on("change", ".input_file", function (e) {
        var $parent = $(this).closest("div");        
        var val = $(this).val();
        var valTit = val.split("\\");
        var valTittext = valTit[valTit.length - 1];
       
        if (val != "") {
            $parent.find(".file_src").text(valTittext).addClass("on");
            $parent.find('.btn_file_txt').hide();//하나의 파일에 대해 삭제 및 파일찾기 trigger가 필요할 때 파일추가 버튼에 .btn_file_txt 클래스 추가
            $parent.find('.del_file_txt').show();//하나의 파일에 대해 삭제 및 파일찾기 trigger가 필요할 때 삭제 버튼에 .btn_file_txt 클래스 추가
        } else {
            $parent.find(".file_src").text('').addClass("on");
            $parent.find('.btn_file_txt').show();//하나의 파일에 대해 삭제 및 파일찾기 trigger가 필요할 때 파일추가 버튼에 .btn_file_txt 클래스 추가
            $parent.find('.del_file_txt').hide();//하나의 파일에 대해 삭제 및 파일찾기 trigger가 필요할 때 삭제 버튼에 .btn_file_txt 클래스 추가
        };
    });    

    
    $(document).on('click', '.del_file_txt', function () { //file delete button click
        $(this).hide();
        $(this).siblings('.file_src').text('');
        $(this).siblings('.btn_file_txt').show();
        $inputFile = $(this).siblings('input[type="hidden"]');
        $inputFile.val($inputFile.val() * -1);
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

function lnbSet(className) {
    $('.menu_list li').removeClass('on');
    $(className).addClass('on');
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

function validationFileTypeSingle(inputFileDoc, typeArray) {
    var $file = $(inputFileDoc);
    var $filePath = $file.val();
    var $fileType = $filePath.substring($filePath.lastIndexOf('.') + 1, $filePath.length);   
    if (typeArray.indexOf($fileType.toLowerCase()) == -1) {
        $file.val('');
        $file.siblings('.file_src').text('');
        alert('첨부 가능한 파일 형식은 ' + typeArray+' 입니다.');
        return false;
    } else {
        return true;
    }
};

function validationFileType(typeArray) {
    var fileFlag = true;
    $('input[type="file"]').each(function (idx, item) {
        if ($(item).val() != '' && !validationFileTypeSingle($(item), typeArray)) {
            fileFlag = false;
            return false;
        }
    });
    return fileFlag;
};