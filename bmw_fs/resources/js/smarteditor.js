var editor_object = [];
nhn.husky.EZCreator.createInIFrame({
    oAppRef: editor_object,
    elPlaceHolder: 'contents' ,
    sSkinURI: "/resources/se/SmartEditor2Skin.html", 
    htParams : {
        bUseToolbar : true,             	 // 툴바 사용 여부 (true:사용/ false:사용하지 않음)
        bUseVerticalResizer : true,     // 입력창 크기 조절바 사용 여부 (true:사용/ false:사용하지 않음)
        bUseModeChanger : true, 		  // 모드 탭(Editor | HTML | TEXT) 사용 여부 (true:사용/ false:사용하지 않음)
    }
});