function addEventForAddingPictures(addBtnSelector, inputSelector, outputSelector) {
    const addBtn = $(addBtnSelector);
    addBtn.on('click', function () {
        const input = $(inputSelector);
        let isInputChanged = false;

        input.click();

        input.on('change', function () {
            if (isInputChanged == false) {
                const displayInput = $(outputSelector);

                const inputFiles = input[0].files;
                const inputValue = Array.from(inputFiles).map(f => f.name + ' ');
                $(displayInput).val(inputValue);

                isInputChanged = true;
            }
        });
    });
}