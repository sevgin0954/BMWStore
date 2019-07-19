function removeTrFromTable(deleteBtnAttr, trDeleteAttr, deleteUrl) {
    const deleteBtns = $(deleteBtnAttr);

    deleteBtns.on('click', function (e) {
        const deleteBtn = $(e.currentTarget);
        const id = deleteBtn.attr(trDeleteAttr);
        
        makePostRequest(deleteUrl, id, function () {
            $(`tr[${trDeleteAttr}*='${id}']`).remove();
        });
    });
}