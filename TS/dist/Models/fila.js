"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Fila = void 0;
const no_js_1 = require("./no.js");
class Fila {
    constructor() {
        this.primeiro = null;
        this.ultimo = null;
        this.tamanho = 0;
    }
    tamanho_fila() {
        return this.tamanho;
    }
    vazio() {
        return this.tamanho === 0;
    }
    inserir(elemento) {
        const novoNo = new no_js_1.No(elemento);
        if (this.ultimo === null) {
            this.primeiro = novoNo;
            this.ultimo = novoNo;
        }
        else {
            this.ultimo.proximo = novoNo;
            this.ultimo = novoNo;
        }
        this.tamanho++;
    }
    apagar() {
        if (this.vazio()) {
            return 'Fila vazia';
        }
        const noRemovido = this.primeiro;
        if (noRemovido) {
            this.primeiro = noRemovido.proximo;
            this.tamanho--;
            if (this.primeiro === null) {
                this.ultimo = null;
            }
            return noRemovido.dado;
        }
        return 'Erro inesperado na fila';
    }
    frente() {
        if (this.vazio()) {
            return 'Fila vazia';
        }
        return this.primeiro.dado;
    }
    listar() {
        if (this.vazio()) {
            return 'Fila vazia';
        }
        const elementos = [];
        let p = this.primeiro;
        while (p) {
            elementos.push(String(p.dado));
            p = p.proximo;
        }
        return elementos.join(' -> ');
    }
}
exports.Fila = Fila;
//# sourceMappingURL=fila.js.map