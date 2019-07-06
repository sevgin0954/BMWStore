$(function () {
    var menuItems = $('#admin-menu>li>a');
    let currentActionName = getActionNameFromUrl();

    for (let i = 0; i < menuItems.length; i++) {
        let currentMenuItem = $(menuItems[i]);
        let currentItemNameAttr = currentMenuItem.attr('data-name');

        if (currentActionName === currentItemNameAttr) {
            currentMenuItem.addClass('btn-light-blue');
        }
        else {
            currentMenuItem.removeClass('btn-light-blue');
        }
    }

    function getActionNameFromUrl() {
        let urlParts = document.URL.split("/").slice(-2);

        let actionNameIndex = urlParts.length - 1;
        let actionName = urlParts[actionNameIndex];
        while (actionName === '') {
            actionNameIndex--;
            actionName = urlParts[actionNameIndex];
        }

        return actionName;
    }
});