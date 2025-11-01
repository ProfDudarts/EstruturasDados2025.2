#include "ListaEncadeada.h"
#include <iostream>

No::No(int valor) {
    dado = valor;
    proximo = nullptr;
}

ListaEncadeada::ListaEncadeada() {
    cabeca = nullptr;
}

ListaEncadeada::~ListaEncadeada() {
    No* atual = cabeca;
    while (atual != nullptr) {
        No* proximoNo = atual->proximo;
        delete atual;
        atual = proximoNo;
    }
}

void ListaEncadeada::inserirNoInicio(int valor) {
    No* novoNo = new No(valor);
    novoNo->proximo = cabeca;
    cabeca = novoNo;
}

void ListaEncadeada::imprimir() {
    if (cabeca == nullptr) {
        std::cout << "A lista está vazia." << std::endl;
        return;
    }

    No* atual = cabeca;
    std::cout << "Lista: ";
    while (atual != nullptr) {
        std::cout << atual->dado << " -> ";
        atual = atual->proximo;
    }
    std::cout << "nullptr" << std::endl;
}

void ListaEncadeada::remover(int valor) {
    No* atual = cabeca;
    No* anterior = nullptr;

    if (atual != nullptr && atual->dado == valor) {
        cabeca = atual->proximo;
        delete atual;
        return;
    }

    while (atual != nullptr && atual->dado != valor) {
        anterior = atual;
        atual = atual->proximo;
    }

    if (atual == nullptr) {
        return;
    }

    anterior->proximo = atual->proximo;
    delete atual;
}