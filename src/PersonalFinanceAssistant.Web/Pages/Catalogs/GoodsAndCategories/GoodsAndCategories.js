"use strict";

const busyManager = new BusyManager();

const goodsTable = new Tabulator('.goods-table', {
    height: calculateContentHeight() - $('.goods-panel-header').outerHeight() - 25,
    layout: 'fitColumns',
    columns: [
        { field: 'name', title: 'Наименование' },
    ],
})

const categoriesTable = new Tabulator('.categories-table', {
    height: calculateContentHeight() - $('.goods-panel-header').outerHeight() - 25,
    layout: 'fitColumns',
    dataTree: true,
    columns: [
        { field: 'name',  },
    ],
})

categoriesTable.on('tableBuilt', async function () {
    categoriesTable.setData(await loadCategories(null));
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

const editCategoryModal = new abp.ModalManager({
    viewUrl: abp.appPath + 'Catalogs/GoodsAndCategories/EditCategoryModal',
    modalClass: 'EditCategoryModal',
});

document.querySelector('.add-category-btn').addEventListener('click', function () {
    editCategoryModal.open();
})

editCategoryModal.onResult(function (result) {
    categoriesTable.addRow(result);
})
