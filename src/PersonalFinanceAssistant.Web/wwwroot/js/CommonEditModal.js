"use strict";

const commonEditModal = function (params) {
    if (!_.isString(params.viewUrl) || params.viewUrl.length === 0) throw new Error('Invalid viewUrl value');
    const viewUrl = params.viewUrl;

    if (!_.isString(params.modalClass) || params.modalClass.length === 0) throw new Error('Invalid modalClass value');
    const modalClass = params.modalClass;

    if (!params.appService) throw new Error('Application service is not set');
    const createMethod = _.isString(params.createMethod) && params.createMethod.length > 0 ? params.appService[params.createMethod] : params.appService['create'];
    if (!_.isFunction(createMethod)) throw new Error('The application service must implement createMethod');
    const updateMethod = _.isString(params.updateMethod) && params.updateMethod.length > 0 ? params.appService[params.updateMethod] : params.appService['update'];
    if (!_.isFunction(updateMethod)) throw new Error('The application service must implement updateMethod');

    const collectData = _.isFunction(params.collectData) ? params.collectData : () => { };
    const validateData = _.isFunction(params.validateData) ? params.validateData : () => true;

    abp.modals[params.modalClass] = function () {
        return {
            initModal: initModal,
        };    
    }

    return new abp.ModalManager({
        viewUrl: viewUrl,
        modalClass: modalClass,
    });

    function initModal(modalManager, args) {
        modalManager.getModal().find('button:submit').on('click', function (e) {
            e.preventDefault()
            submitData(modalManager, args);
        });
    }

    async function submitData(modalManager, args) {
        if (!validateData()) return;
        const dto = collectData(modalManager, args);
        const busyManager = new BusyManager(); 
        busyManager.startOperation();
        const onError = (err) => {
            handleError(err);
            busyManager.endOperation();
        };
        if (args.id) {
            await updateMethod(args.id, dto)
                .then(response => {
                    onDataSubmited(response, 'update', modalManager);
                })
                .catch(err => onError(err));
        }
        else {
            await createMethod(dto)
                .then(response => {
                    onDataSubmited(response, 'create', modalManager);
                })
                .catch(err => onError(err));
        }
    }

    function onDataSubmited(response, action, modalManager) {
        abp.notify.success('Данные сохранены');
        modalManager.setResult({
            response: response,
            action: action,
        });
        const busyManager = new BusyManager(); 
        busyManager.endOperation();
        modalManager.close();
    }
}