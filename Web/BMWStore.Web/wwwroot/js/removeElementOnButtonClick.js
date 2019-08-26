function removeElementOnButtonClick(btnEventStarterAttr, elementToDeleteAttr, deleteUrl, antiForgeryToken) {
    const deleteBtns = $(`[${btnEventStarterAttr}]`);

    deleteBtns.on('click', function (e) {
        const deleteBtn = $(e.currentTarget);
        
        const id = deleteBtn.attr(btnEventStarterAttr);
        
        makePostRequest(id, function () {
            const elementToDelete = $(`[${elementToDeleteAttr}*='${id}']`);
            elementToDelete.remove();
        });
    });

    function makePostRequest(id, successCallback) {
        $.ajax({
            url: deleteUrl,
            data: { id: id, __RequestVerificationToken: antiForgeryToken },
            method: 'post',
            success: function () {
                successCallback();
            }
        });
    }
}