$(function () {

    $('body').on('change', '.file_input', function () {
        var text = $(this).val();
        $(this).parents('.file-wrapper').find('.file_text').val(text.substring(text.lastIndexOf('\\') + 1, text.length));
    });

    $('.search_text').keyup(function (e) {
        if (e.keyCode == 13) {
            searchList();
        }
    });
});
function searchList() {
    $('#page').val(1);
    $('form').submit();
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

function deleteFile(obj) {
    if (!confirm('파일을 삭제하시겠습니까?')) return;
    $obj = $(obj);
    $fileKey = $obj.attr('data-key');
    $csrf = $obj.attr('data-csrf');
    $.ajax({
        url: '/files/fileDelete.do',
        data: { 'key': $fileKey, '_csrf': $csrf },
        dataType: 'json',
        timeout: 10000,
        cache: false,
        type: 'POST',
        beforeSend: function () {
            //$.blockUI(_BLOCKUI_OPTION);
        },
        complete: function () {
            //$.unblockUI();
        },
        error: function (x, e) {
            alert('요청하신 작업을 수행하던 중 예상치 않게 중지되었습니다.\n\n다시 시도해 주십시오.');
        },
        success: function (data) {
            if (data.status == -1) {
                alert('파일이 존재하지 않습니다.');
                return;
            }
            $parentObject = $obj.closest('td');
            $fileCount = $parentObject.find('.file-wrapper').length;
            $targetObject = $obj.closest('.file-wrapper');
            $hasAddBtn = $targetObject.find('.add_btn').length;
            if ($fileCount == 1 || $hasAddBtn == 1) {
                $targetObject.find('.file_text').val('');
                $targetObject.find('input[type=hidden]').val('');
            } else {
                $targetObject.remove();
            }
            return;
        }
    });
};