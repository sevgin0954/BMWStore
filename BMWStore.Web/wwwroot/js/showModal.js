$(function () {
    const showBtns = $('[data-toggle="modal"]');
    
    showBtns.on('click', function (e) {
        const clickedBtn = e.currentTarget;
        const dataTargetValue = $(clickedBtn).attr('data-target');
        
        const elementToShow = $(dataTargetValue);
        elementToShow.modal('show');
    });
})