/**
 * formatterParams: {
 *  buttons:[
 *      {
 *          iconClass: класс иконки из fontawesome
 *          btnClass: класс кнопки для ее идентификации
 *          condition: необязательная функция для проверки отображать ли кнопку в строке. на вход cell component, на выход boolean. 
 *                     если не указывать, то кнопка отображается в каждой строке
 *      },
 *      ...
 *  ]
 * }
 * @param {any} cell
 * @param {any} formatterParams
 * @param {any} onRendered
 */
function toolsFormatter(cell, formatterParams, onRendered) {
    if (formatterParams && formatterParams.buttons) {
        let html = '<div class="tabulator-tools-cell">';
        formatterParams.buttons.forEach(function (btn) {
            if (typeof btn.condition !== 'function' || btn.condition(cell)) {
                html += '<button type="button" class="tabulator-tools-btn ' + btn.btnClass + '">';
                html += '<i class="' + btn.iconClass + '" ></i>';
                html += '</button > ';
            }
        });
        html += '</div>';
        return html;
    } else {
        throw 'Buttons definition is missed in toolsFormatter params';
    };
}

/**
 *  buttons - описание кнопок (см. toolsFormatter)
 *  handlers - объект-словарь обработчиков клика по кнопкам. ключ - класс кнопки из buttons, значение - function(cell)
 * @param {any} buttons
 * @param {any} handlers
 */
function ToolsColumn(buttons, handlers, params) {
    this.title = '';
    this.field = '';
    this.width = params?.width ?? '' + buttons.length * 1.5 + 'em';
    this.resizable = false;
    this.frozen = true;
    this.headerSort = false;
    this.formatter = toolsFormatter;
    this.formatterParams = { buttons: buttons };
    this.cellClick = function (e, cell) {
        let button = e.target.closest('button');
        if (button) {
            let classes = button.classList;
            classes.forEach(function (cl) {
                if (handlers[cl]) {
                    handlers[cl](cell);
                };
            });
        }
    };
}
