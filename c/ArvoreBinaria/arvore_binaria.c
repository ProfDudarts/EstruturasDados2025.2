#include <stdio.h>
#include <stdlib.h>
#include "arvore_binaria.h"

NoArvore* criar_no(int valor) {
    NoArvore* novoNo = (NoArvore*)malloc(sizeof(NoArvore));
    novoNo->valor = valor;
    novoNo->esquerda = NULL;
    novoNo->direita = NULL;
    return novoNo;
}

void inserir_iterativo_helper(NoArvore** raiz, int valor) {
    NoArvore* novoNo = criar_no(valor);

    if (*raiz == NULL) {
        *raiz = novoNo;
        return;
    }

    NoArvore* atual = *raiz;
    NoArvore* pai = NULL;

    while (atual != NULL) {
        pai = atual;
        if (valor < atual->valor) {
            atual = atual->esquerda;
        }
        else if (valor > atual->valor) {
            atual = atual->direita;
        }
        else {
            free(novoNo);
            return;
        }
    }

    if (valor < pai->valor) {
        pai->esquerda = novoNo;
    }
    else {
        pai->direita = novoNo;
    }
}

void em_ordem_helper(NoArvore* raiz) {
    if (raiz != NULL) {
        em_ordem_helper(raiz->esquerda);
        printf("%d ", raiz->valor);
        em_ordem_helper(raiz->direita);
    }
}
void deletar_nos_recursivo(NoArvore* raiz) {
    if (raiz != NULL) {
        deletar_nos_recursivo(raiz->esquerda);
        deletar_nos_recursivo(raiz->direita);
        free(raiz);
    }
}

ArvoreBinaria* criar_arvore() {
    ArvoreBinaria* arvore = (ArvoreBinaria*)malloc(sizeof(ArvoreBinaria));
    if (arvore == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória para a árvore\n");
        exit(1);
    }
    arvore->raiz = NULL;
    return arvore;
}

void deletar_arvore(ArvoreBinaria* arvore) {
    if (arvore == NULL) return;
    deletar_nos_recursivo(arvore->raiz);
    free(arvore);
}

void inserir_valor(ArvoreBinaria* arvore, int valor) {
    if (arvore == NULL) return;
    inserir_iterativo_helper(&(arvore->raiz), valor);
}

void imprimir_em_ordem(ArvoreBinaria* arvore) {
    if (arvore == NULL || arvore->raiz == NULL) {
        printf("Árvore vazia.\n");
        return;
    }
    em_ordem_helper(arvore->raiz);
    printf("\n");
}

int buscar_valor(ArvoreBinaria* arvore, int valor) {
    if (arvore == NULL) return 0;

    NoArvore* atual = arvore->raiz;
    while (atual != NULL) {
        if (valor == atual->valor) {
            return 1;
        }
        else if (valor < atual->valor) {
            atual = atual->esquerda;
        }
        else {
            atual = atual->direita;
        }
    }
    return 0;
}