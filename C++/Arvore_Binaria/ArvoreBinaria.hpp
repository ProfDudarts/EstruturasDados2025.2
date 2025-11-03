#ifndef ARVOREBINARIA_HPP
#define ARVOREBINARIA_HPP

#include <iostream>

struct No {
    int valor;
    No* esquerdo;
    No* direito;

    No(int v) : valor(v), esquerdo(nullptr), direito(nullptr) {}
};

class ArvoreBinaria {
private:
    No* raiz;

    void inserirRec(No*& no, int valor);
    void emOrdemRec(No* no);
    void preOrdemRec(No* no);
    void posOrdemRec(No* no);
    void liberarMemoria(No* no);

public:
    ArvoreBinaria();
    ~ArvoreBinaria();

    void inserir(int valor);
    void emOrdem();
    void preOrdem();
    void posOrdem();
};

#endif
