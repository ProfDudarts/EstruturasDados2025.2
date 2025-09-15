#ifndef PILHA_H
#define PILHA_H

typedef struct NoPilha {
    int dado;
    struct NoPilha* proximo;
} NoPilha;

typedef struct {
    NoPilha* topo;
} Pilha;

Pilha* criar_pilha();
void empilhar(Pilha* pilha, int dado);
int desempilhar(Pilha* pilha);
int pilha_vazia(Pilha* pilha);
void deletar_pilha(Pilha* pilha);

#endif