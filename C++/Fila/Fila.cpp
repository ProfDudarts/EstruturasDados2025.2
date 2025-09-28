#include <iostream>
#include "Fila.hpp"

Fila::Fila() {
    frente = tras = nullptr;
}

Fila::~Fila() {
    while (!estaVazia()) {
        desenfileirar();
    }
}

void Fila::enfileirar(int valor) {
    NoF* novoNo = new NoF;
    novoNo->dado = valor;
    novoNo->proximo = nullptr;

    if (estaVazia()) {
        frente = tras = novoNo;
    } else {
        tras->proximo = novoNo;
        tras = novoNo;
    }
}

int Fila::desenfileirar() {
    if (estaVazia()) {
        std::cerr << "Fila vazia!\n";
        return -1;
    }
    NoF* temp = frente;
    int valor = frente->dado;
    frente = frente->proximo;
    delete temp;
    if (frente == nullptr) tras = nullptr;
    return valor;
}

int Fila::peek() {
    if (estaVazia()) {
        std::cerr << "Fila vazia!\n";
        return -1;
    }
    return frente->dado;
}

bool Fila::estaVazia() {
    return frente == nullptr;
}
