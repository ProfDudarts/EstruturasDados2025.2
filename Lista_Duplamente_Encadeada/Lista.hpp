#ifndef LISTADUPLAMENTEENCADEADA_HPP
#define LISTADUPLAMENTEENCADEADA_HPP

#include <iostream>
using namespace std;

struct No {
    int dado;
    No* anterior;
    No* proximo;

    No(int valor) : dado(valor), anterior(nullptr), proximo(nullptr) {}
};

class ListaDuplamenteEncadeada {
private:
    No* cabeca;
    No* cauda;

public:
    ListaDuplamenteEncadeada();
    ~ListaDuplamenteEncadeada();

    void inserir_no_final(int valor);
    void inserir_no_inicio(int valor);
    void remover(int valor);
    void mostrar_para_frente();
    void mostrar_para_tras();
};

#endif
