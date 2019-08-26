function clickBtnOnBtnClick(clickEventStarter, btnToClickSelector) {
    const eventStarter = $(clickEventStarter);

    eventStarter.on('click', function () {
        const btnToClick = $(btnToClickSelector);
        btnToClick.click();
    })
}