#ifndef ARVORE_BINARIA_H
#define ARVORE_BINARIA_H

typedef struct NoArvore {
    int valor;
    struct NoArvore* esquerda;
    struct NoArvore* direita;
} NoArvore;

typedef struct {
    NoArvore* raiz;
} ArvoreBinaria;


ArvoreBinaria* criar_arvore();

void deletar_arvore(ArvoreBinaria* arvore);

void inserir_valor(ArvoreBinaria* arvore, int valor);

void imprimir_em_ordem(ArvoreBinaria* arvore);

int buscar_valor(ArvoreBinaria* arvore, int valor);

#endif