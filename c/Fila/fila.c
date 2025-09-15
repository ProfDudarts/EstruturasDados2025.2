#include <stdio.h>
#include <stdlib.h>
#include "fila.h"

Fila* criar_fila() {
    Fila* fila = (Fila*)malloc(sizeof(Fila));
    if (fila == NULL) {
        exit(1);
    }
    fila->inicio = NULL;
    fila->fim = NULL;
    return fila;
}

void enfileirar(Fila* fila, int dado) {
    NoFila* novo_no = (NoFila*)malloc(sizeof(NoFila));
    if (novo_no == NULL) {
        exit(1);
    }
    novo_no->dado = dado;
    novo_no->proximo = NULL;

    if (fila_vazia(fila)) {
        fila->inicio = novo_no;
    }
    else {
        fila->fim->proximo = novo_no;
    }
    fila->fim = novo_no;
}

int desenfileirar(Fila* fila) {
    if (fila_vazia(fila)) {
        fprintf(stderr, "Erro: Fila vazia.\n");
        exit(1);
    }
    NoFila* no_a_remover = fila->inicio;
    int dado_retornado = no_a_remover->dado;
    fila->inicio = fila->inicio->proximo;
    if (fila->inicio == NULL) {
        fila->fim = NULL;
    }
    free(no_a_remover);
    return dado_retornado;
}

int fila_vazia(Fila* fila) {
    return fila->inicio == NULL;
}

void deletar_fila(Fila* fila) {
    NoFila* no_atual = fila->inicio;
    NoFila* proximo_no;
    while (no_atual != NULL) {
        proximo_no = no_atual->proximo;
        free(no_atual);
        no_atual = proximo_no;
    }
    free(fila);
}