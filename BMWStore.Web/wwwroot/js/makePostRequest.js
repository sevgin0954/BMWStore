function makePostRequest(url, id, successCallback) {
    $.ajax({
        url: url,
        data: { id: id },
        method: 'post',
        success: function (data) {
            successCallback();
        }
    });
}