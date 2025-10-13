// pessoa 1 
#include <stdio.h>
#include <stdlib.h>

struct No {
    int valor;
    struct No* esquerda;
    struct No* direita;
};

struct No* criarNo(int valor) {
    struct No* novoNo = (struct No*)malloc(sizeof(struct No));
    novoNo->valor = valor;
    novoNo->esquerda = NULL;
    novoNo->direita = NULL;
    return novoNo;
}
// pessoa 2 
void inserir_iterativo(struct No** raiz, int valor) {
    struct No* novoNo = criarNo(valor);

    if (*raiz == NULL) {
        *raiz = novoNo;
        return;
    }

    struct No* atual = *raiz;
    struct No* pai = NULL;

    while (atual != NULL) {
        pai = atual;
        if (valor < atual->valor) {
            atual = atual->esquerda;
        } else if (valor > atual->valor) {
            atual = atual->direita;
        } else {
            free(novoNo);
            return;
        }
    }

    if (valor < pai->valor) {
        pai->esquerda = novoNo;
    } else {
        pai->direita = novoNo;
    }
}