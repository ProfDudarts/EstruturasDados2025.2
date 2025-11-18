#ifndef LISTAENCADEADA_H
#define LISTAENCADEADA_H

struct No {
    int dado;
    No* proximo;
    No(int valor);
};

class ListaEncadeada {
private:
    No* cabeca;

public:
    ListaEncadeada();
    ~ListaEncadeada();

    void inserirNoInicio(int valor);
    void imprimir();
    void remover(int valor);
};

#endif