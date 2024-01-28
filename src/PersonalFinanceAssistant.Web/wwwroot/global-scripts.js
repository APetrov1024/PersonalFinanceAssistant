"use strict";

const L = abp.localization.localize;

function calculateContentHeight() {
    let docHeight = jQuery(window).innerHeight();
    let headHeight = jQuery('#main-navbar').outerHeight(true);
    let toolbarHeight = jQuery('#PageHeader').outerHeight(true);
    let footerHeight = jQuery('.page-footer').outerHeight(true);
    return docHeight - headHeight - toolbarHeight - footerHeight;
}


function handleError(error) {
    let message = 'Ошибка'
    if (error instanceof Error) {
        message = error.message ?? message
    }
    else if (_.isString(error) && error.length > 0) {
        message = error;
    }
    else if (typeof error === 'object') {
        if (error.responseJSON) {
            message = error.responseJSON?.error?.message ?? message;
        }
        else if (_.isString(error.responseText) && error.responseText.length > 0) {
            message = error.responseText;
        }
    }
    abp.notify.error(message);
};