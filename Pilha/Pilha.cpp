#include <iostream>
#include "Pilha.hpp"

Pilha::Pilha() {
    topo = nullptr;
}

Pilha::~Pilha() {
    while (!estaVazia()) {
        pop();
    }
}

void Pilha::push(int valor) {
    No* novoNo = new No;
    novoNo->dado = valor;
    novoNo->proximo = topo;
    topo = novoNo;
}

int Pilha::pop() {
    if (estaVazia()) {
        std::cerr << "Pilha vazia!\n";
        return -1;
    }
    No* temp = topo;
    int valor = topo->dado;
    topo = topo->proximo;
    delete temp;
    return valor;
}

int Pilha::peek() {
    if (estaVazia()) {
        std::cerr << "Pilha vazia!\n";
        return -1;
    }
    return topo->dado;
}

bool Pilha::estaVazia() {
    return topo == nullptr;
}
