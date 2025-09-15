#include <stdio.h>
#include <stdlib.h>
#include "pilha.h"

Pilha* criar_pilha() {
    Pilha* pilha = (Pilha*)malloc(sizeof(Pilha));
    if (pilha == NULL) {
        exit(1);
    }
    pilha->topo = NULL;
    return pilha;
}

void empilhar(Pilha* pilha, int dado) {
    NoPilha* novo_no = (NoPilha*)malloc(sizeof(NoPilha));
    if (novo_no == NULL) {
        fprintf(stderr, "Error: Memory allocation failed\n");
        exit(1);
    }
    novo_no->dado = dado;
    novo_no->proximo = pilha->topo;
    pilha->topo = novo_no;
}

int desempilhar(Pilha* pilha) {
    if (pilha_vazia(pilha)) {
        fprintf(stderr, "Erro: Pilha vazia.\n");
        exit(1);
    }
    NoPilha* no_a_remover = pilha->topo;
    int dado_retornado = no_a_remover->dado;
    pilha->topo = pilha->topo->proximo;
    free(no_a_remover);
    return dado_retornado;
}

int pilha_vazia(Pilha* pilha) {
    return pilha->topo == NULL;
}

void deletar_pilha(Pilha* pilha) {
    NoPilha* no_atual = pilha->topo;
    NoPilha* proximo_no;
    while (no_atual != NULL) {
        proximo_no = no_atual->proximo;
        free(no_atual);
        no_atual = proximo_no;
    }
    free(pilha);
}