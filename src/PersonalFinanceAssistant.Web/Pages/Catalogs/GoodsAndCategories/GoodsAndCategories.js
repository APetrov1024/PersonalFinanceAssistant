"use strict";

const busyManager = new BusyManager();

let selectedCategoryId = null;
let selectedRow = null;

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
    dataTreeChildIndent: 25,
    columns: [
        //{ width: 150 }, // branch element
        { field: 'name',  },
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
    selectCategoryRow(row);
})

function selectCategoryRow(row) {
    let selectedRow = categoriesTable.getSelectedRows()[0];
    if (selectedRow != row) {
        if (selectedRow) selectedRow.deselect();
        row.select();
    }
}

categoriesTable.on("dataTreeRowExpanded", async function (row, level) {
    selectCategoryRow(row);
    let data = row.getData();
    if (data._children.length > 0) return;
    let parentId = row.getData().id;
    var subs = await loadCategories(parentId, false);
    subs.forEach(x => {
        row.addTreeChild(x);
    });
});

categoriesTable.on("rowSelected", function (row) {
    selectedCategoryId = row.getData().id;
    selectedRow = row;
});

categoriesTable.on('tableBuilt', async function () {
    await categoriesTable.setData(await loadCategories(null, true));
    selectEmptyCategory();
})

function selectEmptyCategory() {
    let emptyCategoryRow = categoriesTable.getRows().find(x => x.getData().id == null );
    selectCategoryRow(emptyCategoryRow);
}

goodsTable.on('tableBuilt', async function () {
    goodsTable.setData(await loadGoods(null));
})

async function loadCategories(parentId, addEmpty) {
    busyManager.startOperation();
    let result = await personalFinanceAssistant.catalogs.goodCategories.getList({ parentId: parentId })
        .catch(err => handleError(err));
    busyManager.endOperation();
    result.forEach(x => {
        if (x.hasChilds) x._children = [];
    })
    if (addEmpty) result.unshift({ id: null, name: 'Без категории'});
    return result;
}

async function loadGoods(categoryId) {
    busyManager.startOperation();
    let result = await personalFinanceAssistant.catalogs.goods.getList({ categoryId: categoryId })
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
    editCategoryModal.open({ parentCategoryId: selectedCategoryId });
})

categoriesTable.on("rowDblClick", function (e, row) {
    let data = row.getData();
    editCategoryModal.open({ id: data.id, parentCategoryId: data.parentCategoryId });
});

function editCategoryClicked(cell) {
    let data = cell.getRow().getData();
    editCategoryModal.open({ id: data.id, parentCategoryId: data.parentCategoryId });
}

async function deleteCategoryClicked(cell) {
    if (await abp.message.confirm('', 'Вы уверены?')) {
        busyManager.startOperation();
        let row = cell.getRow();
        let id = row.getData().id;
        personalFinanceAssistant.catalogs.goodCategories.delete(id)
            .then(() => {
                selectEmptyCategory();
                row.delete();
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
    let response = result.response;
    if (!response.parentCategoryId) {
        categoriesTable.updateOrAddRow(response.id, response);
    }
    else {
        if (result.action === 'update') {
            let affectedRow;
            if (selectedRow.getData().id == response.id) {
                affectedRow = selectedRow;
            }
            else {
                affectedRow = selectedRow.getTreeChildren().find(x => x.getData().id == response.id);
            }
            // костыль: похоже на баг табулятора. табулятор пытается обратиться к вложенным строкам (зачем?), а их нет
            //TODO хорошо бы разобраться что именно происходит в табуляторе и если это баг, то отправить репорт
            let hasNoChilds = !affectedRow.getData()._children
            if (hasNoChilds) affectedRow.addTreeChild({});
            affectedRow.update(response);
            if (hasNoChilds) affectedRow.getTreeChildren()[0].delete();
        }
        if (result.action === 'create') {
            selectedRow.addTreeChild(response);
            selectedRow.treeExpand();
        }
    }
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