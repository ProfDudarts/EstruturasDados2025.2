#ifndef ARVORERUBRONEGRA_HPP
#define ARVORERUBRONEGRA_HPP

#include <iostream>

// Enum para representar as cores dos nós
enum Cor { VERMELHO, PRETO };

struct No {
    int dado;
    Cor cor;
    No *pai, *esquerdo, *direito;

    // Construtor do Nó
    No(int dado) : dado(dado), cor(VERMELHO), pai(nullptr), esquerdo(nullptr), direito(nullptr) {}
};

class ArvoreRubroNegra {
private:
    No* raiz;

    // Funções auxiliares privadas
    void rotacionarEsquerda(No* x);
    void rotacionarDireita(No* y);
    void corrigirInsercao(No* z);
    void emOrdemRec(No* no);
    No* buscarRec(No* no, int valor);
    void liberarMemoria(No* no);

public:
    ArvoreRubroNegra();
    ~ArvoreRubroNegra();

    void inserir(int valor);
    No* buscar(int valor);
    void emOrdem();
};

#endif 