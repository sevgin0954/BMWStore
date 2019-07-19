function removeElementOnButtonClick(btnAttr, elementToDeleteAttr, deleteUrl) {
    const deleteBtns = $(btnAttr);

    deleteBtns.on('click', function (e) {
        const deleteBtn = $(e.currentTarget);
        // TODO: REFACETOR FOR BTN TO DONT NEED TO HAVE elementToDeleteAttr
        const id = deleteBtn.attr(elementToDeleteAttr);
        
        makePostRequest(deleteUrl, id, function () {
            $(`[${elementToDeleteAttr}*='${id}']`).remove();
        });
    });
}