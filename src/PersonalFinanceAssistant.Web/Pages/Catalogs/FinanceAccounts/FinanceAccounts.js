"use strict";

const busyManager = new BusyManager();

const accountsTable = new Tabulator('.accounts-table', {
    height: calculateContentHeight(),
    layout: 'fitColumns',
    ajaxURL: true,
    ajaxRequestFunc: accountsQuery,
    columns: [
        { field: 'name', title: 'Наименование' },
        { field: 'currencyName', title: 'Валюта', width: '15%' },
        { field: 'isDefault', title: 'Счет по умолчанию', formatter:'tickCross', width: '15%' },
        new ToolsColumn(
            [
                { btnClass: 'btn-edit', iconClass: 'fas fa-edit' },
                { btnClass: 'btn-delete', iconClass: 'fas fa-trash' },
            ],
            {
                'btn-edit': editAccountClicked,
                'btn-delete': deleteAccountClicked
            },
            {
                width: '5em',
            }
        ),
    ],
})

window.addEventListener('resize', function (event) {
    accountsTable.setHeight(calculateContentHeight())
    accountsTable.redraw();
});

function accountsQuery(url, config, params) {
    return new Promise(function (resolve, reject) {
        personalFinanceAssistant.catalogs.financeAccounts.getList()
            .done(function (result) {
                resolve(result);
            })
            .catch(err => handleError(err));
    });
}

async function deleteAccountClicked(cell) {
    if (await abp.message.confirm('', 'Вы уверены?')) {
        busyManager.startOperation();
        let row = cell.getRow();
        let id = row.getData().id;
        personalFinanceAssistant.catalogs.financeAccounts.delete(id)
            .then(() => {
                row.delete();
                abp.notify.success('Счет удален');
                busyManager.endOperation()
            })
            .catch(err => {
                handleError(err);
                busyManager.endOperation()
            });
    }
}

const editAccountModal = commonEditModal({
    viewUrl: abp.appPath + 'Catalogs/FinanceAccounts/EditFinanceAccountModal',
    modalClass: 'EditFinanceAccountModal',
    appService: personalFinanceAssistant.catalogs.financeAccounts,
    collectData: function (modalManager, args) {
        const $modal = modalManager.getModal();
        const dto = {
            name: $modal.find('#VM_Name').val(),
            isDefault: $modal.find('#VM_IsDefault').prop('checked'),
            currencyId: $modal.find('#VM_CurrencyId').val(),
        };
        return dto;
    },
});

document.querySelector('.add-account-btn').addEventListener('click', function () {
    editAccountModal.open();
})

accountsTable.on("rowDblClick", function (e, row) {
    editAccountModal.open({ id: row.getData().id });
});

function editAccountClicked(cell) {
    let data = cell.getRow().getData();
    editAccountModal.open({ id: data.id });
}

editAccountModal.onResult(function (result) {
    let response = result.response;
    accountsTable.updateOrAddRow(response.id, response);
})