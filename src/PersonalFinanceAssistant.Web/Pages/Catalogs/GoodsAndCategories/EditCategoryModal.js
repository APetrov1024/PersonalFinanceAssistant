"use strict";

abp.modals.EditCategoryModal = function () {
    const busyManager = new BusyManager(); 
    let _modalManager;
    let _args;

    function initModal(modalmanager, args) {
        _modalManager = modalmanager;
        _args = args;

        modalmanager.getModal().find('button:submit').on('click', function (e) {
            e.preventDefault()
            SubmitData();
        });
    }

    return {
        initModal: initModal,
    };

    async function SubmitData() {
        const dto = {
            name: document.querySelector('#VM_Name').value,
            parentCategoryId: _args.parentCategoryId,
        }

        busyManager.startOperation();
        let isSuccess = true;
        let response;
        if (_args.id) {
            response = await personalFinanceAssistant.catalogs.goodCategories.update(_args.id, dto)
                .catch(err => {
                    handleError(err);
                    isSuccess = false;
                });
        }
        else {
            response = await personalFinanceAssistant.catalogs.goodCategories.create(dto)
                .catch(err => {
                    handleError(err);
                    isSuccess = false;
                });
        }
        if (isSuccess) {
            onDataSubmited(response);
        }
    }

    function onDataSubmited(result) {
        abp.notify.success('Данные сохранены');
        _modalManager.setResult(result);
        busyManager.endOperation();
        _modalManager.close();
    }
}