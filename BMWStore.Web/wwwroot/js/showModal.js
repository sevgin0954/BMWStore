$(function () {
    const showBtns = $('[data-toggle="modal"]');
    
    showBtns.on('click', function (e) {
        const clickedBtn = e.currentTarget;
        const dataTargetValue = $(clickedBtn).attr('data-target');
        console.log(dataTargetValue)
        const elementToShow = $(dataTargetValue);
        elementToShow.modal('show');
    });
})