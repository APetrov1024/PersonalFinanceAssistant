"use strict";

/**
 * Решает проблему, когда индикатор вызывается для нескольких
 * асинхронных операций и должен быть убран после того, как завершатся все.
 * Является singleton.
 */
class BusyManager {
    constructor() {
        if (BusyManager.#instance) {
            return BusyManager.#instance;
        }
        BusyManager.#instance = this;
    }

    static #instance = null;
    #count = 0;

    #show = () => abp.ui.setBusy();
    #hide = () => abp.ui.clearBusy();

    startOperation = function () {
        this.#count++;
        this.#show();
    }

    endOperation = function () {
        if (this.#count > 0) {
            this.#count--;
            if (this.#count == 0) this.#hide();
        }
    }

    reset = function () {
        this.#count = 0;
        this.#hide();
    }
}