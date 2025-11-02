#include <stdio.h>
#include "Pilha/pilha.h"
#include "Fila/fila.h"
#include "ListaEncadeada/lista_encadeada.h"
#include "ListaDuplamenteEncadeada/lista_duplamente_encadeada.h"
#include "ArvoreBinaria/arvore_binaria.h"

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
    printf("\n");

    printf("--- Testando a Lista Encadeada ---\n");
    ListaEncadeada* minha_lista = criar_lista();
    
    inserir_inicio(minha_lista, 10);
    inserir_fim(minha_lista, 30);
    inserir_posicao(minha_lista, 20, 1);
    printf("Após inserções: ");
    imprimir_lista(minha_lista);
    printf("Tamanho: %d\n", obter_tamanho(minha_lista));
    
    int pos = buscar(minha_lista, 20);
    printf("Elemento 20 encontrado na posição: %d\n", pos);
    
    printf("Removido do início: %d\n", remover_inicio(minha_lista));
    printf("Removido do fim: %d\n", remover_fim(minha_lista));
    printf("Após remoções: ");
    imprimir_lista(minha_lista);
    
    deletar_lista(minha_lista);
    printf("\n");

    printf("--- Testando a Lista Duplamente Encadeada ---\n");
    ListaDuplamenteEncadeada* minha_lista_dupla = criar_lista_dupla();
    
    inserir_inicio_dupla(minha_lista_dupla, 10);
    inserir_fim_dupla(minha_lista_dupla, 30);
    inserir_posicao_dupla(minha_lista_dupla, 20, 1);
    inserir_fim_dupla(minha_lista_dupla, 40);
    printf("Após inserções: ");
    imprimir_lista_dupla(minha_lista_dupla);
    printf("Lista reversa: ");
    imprimir_lista_dupla_reversa(minha_lista_dupla);
    printf("Tamanho: %d\n", obter_tamanho_dupla(minha_lista_dupla));
    
    int pos_dupla = buscar_dupla(minha_lista_dupla, 20);
    printf("Elemento 20 encontrado na posição: %d\n", pos_dupla);
    
    printf("Removido do início: %d\n", remover_inicio_dupla(minha_lista_dupla));
    printf("Removido do fim: %d\n", remover_fim_dupla(minha_lista_dupla));
    printf("Removido da posição 0: %d\n", remover_posicao_dupla(minha_lista_dupla, 0));
    printf("Após remoções: ");
    imprimir_lista_dupla(minha_lista_dupla);
    
    deletar_lista_dupla(minha_lista_dupla);

    printf("\n--- Testando a Árvore Binária de Busca ---\n");
    ArvoreBinaria* minha_arvore = criar_arvore();

    printf("Inserindo valores: 50, 30, 70, 20, 40\n");
    inserir_valor(minha_arvore, 50);
    inserir_valor(minha_arvore, 30);
    inserir_valor(minha_arvore, 70);
    inserir_valor(minha_arvore, 20);
    inserir_valor(minha_arvore, 40);

    printf("Percurso Em Ordem: ");
    imprimir_em_ordem(minha_arvore);

    printf("Buscando valor 40: %s\n", buscar_valor(minha_arvore, 40) ? "Encontrado" : "Não Encontrado");
    printf("Buscando valor 99: %s\n", buscar_valor(minha_arvore, 99) ? "Encontrado" : "Não Encontrado");

    deletar_arvore(minha_arvore);
    printf("Árvore deletada.\n");

    return 0;
}