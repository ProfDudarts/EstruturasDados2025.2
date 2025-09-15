#include <stdio.h>
#include "Pilha/pilha.h"
#include "Fila/fila.h"

int main() {
    printf("--- Testando a Pilha (LIFO) ---\n");
    Pilha* minha_pilha = criar_pilha();
    empilhar(minha_pilha, 10);
    empilhar(minha_pilha, 20);
    empilhar(minha_pilha, 30);
    while (!pilha_vazia(minha_pilha)) {
        printf("Saiu da pilha: %d\n", desempilhar(minha_pilha));
    }
    deletar_pilha(minha_pilha);
    printf("\n");

    printf("--- Testando a Fila (FIFO) ---\n");
    Fila* minha_fila = criar_fila();
    enfileirar(minha_fila, 10);
    enfileirar(minha_fila, 20);
    enfileirar(minha_fila, 30);
    while (!fila_vazia(minha_fila)) {
        printf("Saiu da fila: %d\n", desenfileirar(minha_fila));
    }
    deletar_fila(minha_fila);

    return 0;
}