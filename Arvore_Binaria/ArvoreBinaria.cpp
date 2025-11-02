#include "ArvoreBinaria.hpp"

ArvoreBinaria::ArvoreBinaria() : raiz(nullptr) {}

ArvoreBinaria::~ArvoreBinaria() {
    liberarMemoria(raiz);
}

void ArvoreBinaria::liberarMemoria(No* no) {
    if (no != nullptr) {
        liberarMemoria(no->esquerdo);
        liberarMemoria(no->direito);
        delete no;
    }
}

void ArvoreBinaria::inserirRec(No*& no, int valor) {
    if (no == nullptr) {
        no = new No(valor);
    } else if (valor < no->valor) {
        inserirRec(no->esquerdo, valor);
    } else {
        inserirRec(no->direito, valor);
    }
}

void ArvoreBinaria::inserir(int valor) {
    inserirRec(raiz, valor);
}

void ArvoreBinaria::emOrdemRec(No* no) {
    if (no != nullptr) {
        emOrdemRec(no->esquerdo);
        std::cout << no->valor << " ";
        emOrdemRec(no->direito);
    }
}

void ArvoreBinaria::preOrdemRec(No* no) {
    if (no != nullptr) {
        std::cout << no->valor << " ";
        preOrdemRec(no->esquerdo);
        preOrdemRec(no->direito);
    }
}

void ArvoreBinaria::posOrdemRec(No* no) {
    if (no != nullptr) {
        posOrdemRec(no->esquerdo);
        posOrdemRec(no->direito);
        std::cout << no->valor << " ";
    }
}

void ArvoreBinaria::emOrdem() {
    std::cout << "Em ordem: ";
    emOrdemRec(raiz);
    std::cout << std::endl;
}

void ArvoreBinaria::preOrdem() {
    std::cout << "Pré-ordem: ";
    preOrdemRec(raiz);
    std::cout << std::endl;
}

void ArvoreBinaria::posOrdem() {
    std::cout << "Pós-ordem: ";
    posOrdemRec(raiz);
    std::cout << std::endl;
}
