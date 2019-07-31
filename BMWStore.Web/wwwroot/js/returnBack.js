$(function () {
    const cancelBtn = $('[cancelBtn]');

    cancelBtn.on('click', function () {
        window.history.back()
    })
})