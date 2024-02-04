"use strict";

const busyManager = new BusyManager();

let selectedCategoryId = null;

const goodsTable = new Tabulator('.goods-table', {
    height: getTableHeight(),
    layout: 'fitColumns',
    columns: [
        new ToolsColumn(
            [
                { btnClass: 'btn-edit', iconClass: 'fas fa-edit' },
                { btnClass: 'btn-delete', iconClass: 'fas fa-trash' },
            ],
            {
                'btn-edit': editGoodClicked,
                'btn-delete': deleteGoodClicked
            }
        ),
        { field: 'name', title: 'Наименование' },
    ],
})

const categoriesTable = new Tabulator('.categories-table', {
    height: getTableHeight(),
    layout: 'fitColumns',
    dataTree: true,
    columns: [
        new ToolsColumn(
            [
                { btnClass: 'btn-edit', iconClass: 'fas fa-edit', condition: (cell) => cell.getData().id != null, },
                { btnClass: 'btn-delete', iconClass: 'fas fa-trash', condition: (cell) => cell.getData().id != null, },
            ],
            {
                'btn-edit': editCategoryClicked,
                'btn-delete': deleteCategoryClicked
            },
            {
                width: '10em',
            }
        ),
        { field: 'name',  },
    ],
})

function getTableHeight() {
    return calculateContentHeight() - $('.goods-panel-header').outerHeight() - 25;
}

window.addEventListener('resize', function (event) {
    categoriesTable.setHeight(getTableHeight())
    categoriesTable.redraw();
    goodsTable.setHeight(getTableHeight())
    goodsTable.redraw();
});

categoriesTable.on('rowClick', function (e, row) {
    if (e.target.closest('button')) return;
    let selectedRow = categoriesTable.getRows().find(x => x.getData().id === selectedCategoryId);
    if (selectedRow != row) {
        selectedRow.deselect();
        row.select();
    }
})

categoriesTable.on("rowSelected", function (row) {
    selectedCategoryId = row.getData().id;
});

categoriesTable.on('tableBuilt', async function () {
    await categoriesTable.setData(await loadCategories(null));
    categoriesTable.getRows().find(x => x.getData().id === selectedCategoryId).select();
})

goodsTable.on('tableBuilt', async function () {
    goodsTable.setData(await loadGoods(null));
})

async function loadCategories(parentId) {
    busyManager.startOperation();
    var result = await personalFinanceAssistant.catalogs.goodCategories.getList({ parentId: parentId })
        .catch(err => handleError(err));
    busyManager.endOperation();
    result.unshift({ id: null, name: 'Без категории'});
    return result;
}

async function loadGoods(categoryId) {
    busyManager.startOperation();
    var result = await personalFinanceAssistant.catalogs.goods.getList({ categoryId: categoryId })
        .catch(err => handleError(err));
    busyManager.endOperation();
    return result;
}

const editCategoryModal = commonEditModal({
    viewUrl: abp.appPath + 'Catalogs/GoodsAndCategories/EditCategoryModal',
    modalClass: 'EditCategoryModal',
    appService: personalFinanceAssistant.catalogs.goodCategories,
    collectData: function (modalManager, args) {
        const $modal = modalManager.getModal();
        const dto = {
            name: $modal.find('#VM_Name').val(),
            parentCategoryId: args.parentCategoryId,
        };
        //debugger;
        return dto;
    },
});

document.querySelector('.add-category-btn').addEventListener('click', function () {
    editCategoryModal.open();
})

categoriesTable.on("rowDblClick", function (e, row) {
    editCategoryModal.open({ id: row.getData().id });
});

function editCategoryClicked(cell) {
    editCategoryModal.open({ id: cell.getRow().getData().id });
}

async function deleteCategoryClicked(cell) {
    if (await abp.message.confirm('', 'Вы уверены?')) {
        busyManager.startOperation();
        let id = cell.getRow().getData().id;
        personalFinanceAssistant.catalogs.goodCategories.delete(id)
            .then(() => {
                categoriesTable.deleteRow(id);
                abp.notify.success('Категория удалена');
                busyManager.endOperation()
            })
            .catch(err => {
                handleError(err);
                busyManager.endOperation()
            });
    }
}

editCategoryModal.onResult(function (result) {
    categoriesTable.updateOrAddRow(result.id, result);
})


const editGoodModal = commonEditModal({
    viewUrl: abp.appPath + 'Catalogs/GoodsAndCategories/EditGoodModal',
    modalClass: 'EditGoodModal',
    appService: personalFinanceAssistant.catalogs.goods,
    collectData: function (modalManager, args) {
        const $modal = modalManager.getModal();
        const dto = {
            name: $modal.find('#VM_Name').val(),
            parentCategoryId: args.parentCategoryId,
        };
        debugger;
        return dto;
    },
});

document.querySelector('.add-good-btn').addEventListener('click', function () {
    editGoodModal.open();
})

goodsTable.on("rowDblClick", function (e, row) {
    editGoodModal.open({ id: row.getData().id });
});

function editGoodClicked(cell) {
    editGoodModal.open({ id: cell.getRow().getData().id });
}

async function deleteGoodClicked(cell) {
    if (await abp.message.confirm('', 'Вы уверены?')) {
        busyManager.startOperation();
        let id = cell.getRow().getData().id;
        personalFinanceAssistant.catalogs.goods.delete(cell.getRow().getData().id)
            .then(() => {
                goodsTable.deleteRow(id);
                abp.notify.success('Товар удален');
                busyManager.endOperation()
            })
            .catch(err => {
                handleError(err);
                busyManager.endOperation()
            });
    }
}