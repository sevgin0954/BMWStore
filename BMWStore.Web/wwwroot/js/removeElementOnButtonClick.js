function removeElementOnButtonClick(btnEventStarterAttr, elementToDeleteAttr, deleteUrl) {
    const deleteBtns = $(`[${btnEventStarterAttr}]`);

    deleteBtns.on('click', function (e) {
        const deleteBtn = $(e.currentTarget);
        
        const id = deleteBtn.attr(btnEventStarterAttr);
        
        makePostRequest(deleteUrl, id, function () {
            const elementToDelete = $(`[${elementToDeleteAttr}*='${id}']`);
            elementToDelete.remove();
        });
    });
}

function makePostRequest(url, id, successCallback) {
    $.ajax({
        url: url,
        data: { id: id },
        method: 'post',
        success: function () {
            successCallback();
        }
    });
}