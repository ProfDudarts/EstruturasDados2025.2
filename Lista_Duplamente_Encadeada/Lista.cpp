#include "Lista.hpp"

ListaDuplamenteEncadeada::ListaDuplamenteEncadeada() : cabeca(nullptr), cauda(nullptr) {}

ListaDuplamenteEncadeada::~ListaDuplamenteEncadeada() {
    No* atual = cabeca;
    while (atual) {
        No* proximo = atual->proximo;
        delete atual;
        atual = proximo;
    }
}

void ListaDuplamenteEncadeada::inserir_no_final(int valor) {
    No* novo = new No(valor);
    if (!cabeca) {
        cabeca = cauda = novo;
    } else {
        cauda->proximo = novo;
        novo->anterior = cauda;
        cauda = novo;
    }
}

void ListaDuplamenteEncadeada::inserir_no_inicio(int valor) {
    No* novo = new No(valor);
    if (!cabeca) {
        cabeca = cauda = novo;
    } else {
        novo->proximo = cabeca;
        cabeca->anterior = novo;
        cabeca = novo;
    }
}

void ListaDuplamenteEncadeada::remover(int valor) {
    No* atual = cabeca;
    while (atual && atual->dado != valor) {
        atual = atual->proximo;
    }
    if (!atual) return; 
    if (atual == cabeca) cabeca = cabeca->proximo;
    if (atual == cauda) cauda = cauda->anterior;

    if (atual->anterior) atual->anterior->proximo = atual->proximo;
    if (atual->proximo) atual->proximo->anterior = atual->anterior;

    delete atual;
}

void ListaDuplamenteEncadeada::mostrar_para_frente() {
    No* temp = cabeca;
    while (temp) {
        cout << temp->dado << " ";
        temp = temp->proximo;
    }
    cout << endl;
}

void ListaDuplamenteEncadeada::mostrar_para_tras() {
    No* temp = cauda;
    while (temp) {
        cout << temp->dado << " ";
        temp = temp->anterior;
    }
    cout << endl;
}
