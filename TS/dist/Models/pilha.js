"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Pilha = void 0;
const no_js_1 = require("./no.js");
class Pilha {
    constructor() {
        this.topo = null;
        this.tamanho = 0;
    }
    inserir(elemento) {
        const novoNo = new no_js_1.No(elemento);
        novoNo.proximo = this.topo;
        this.topo = novoNo;
        this.tamanho++;
    }
    apagar() {
        if (this.empty()) {
            return 'pilha vazia';
        }
        const noApagado = this.topo;
        if (noApagado) {
            this.topo = noApagado.proximo;
            this.tamanho--;
            return noApagado.dado;
        }
        return "Erro";
    }
    topo_valor() {
        if (this.topo === null) {
            return 'pilha vazia';
        }
        return this.topo.dado;
    }
    tamanho_pilha() {
        return this.tamanho;
    }
    empty() {
        return this.tamanho === 0;
    }
    listar() {
        if (this.tamanho === 0) {
            return 'Pilha vazia';
        }
        let s = '';
        let p = this.topo;
        while (p) {
            s += `${p.dado}\n`;
            p = p.proximo;
        }
        return s;
    }
}
exports.Pilha = Pilha;
//# sourceMappingURL=pilha.js.map