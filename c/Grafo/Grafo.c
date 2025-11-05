#include <stdio.h>
#include <stdlib.h>
#include "Grafo.h"

#include "../Fila/fila.h"
#include "../Pilha/pilha.h"

Grafo* criar_grafo(int num_vertices) {
    Grafo* grafo = (Grafo*)malloc(sizeof(Grafo));
    if (grafo == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória para o grafo\n");
        exit(1);
    }

    grafo->num_vertices = num_vertices;

    grafo->listas_adj = (ListaEncadeada**)malloc(num_vertices * sizeof(ListaEncadeada*));
    if (grafo->listas_adj == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória para as listas de adjacência\n");
        exit(1);
    }

    for (int i = 0; i < num_vertices; i++) {
        grafo->listas_adj[i] = criar_lista();
    }

    return grafo;
}

void deletar_grafo(Grafo* grafo) {
    if (grafo == NULL) return;

    for (int i = 0; i < grafo->num_vertices; i++) {
        deletar_lista(grafo->listas_adj[i]);
    }

    free(grafo->listas_adj);
    free(grafo);
}

void adicionar_aresta(Grafo* grafo, int origem, int destino) {
    if (origem < 0 || origem >= grafo->num_vertices || destino < 0 || destino >= grafo->num_vertices) {
        fprintf(stderr, "Erro: Vértice de origem ou destino inválido\n");
        return;
    }

    inserir_inicio(grafo->listas_adj[origem], destino);

    inserir_inicio(grafo->listas_adj[destino], origem);
}

void imprimir_grafo(Grafo* grafo) {
    printf("--- Grafo (Lista de Adjacência) ---\n");
    for (int i = 0; i < grafo->num_vertices; i++) {
        printf("Vértice %d: ", i);
        imprimir_lista(grafo->listas_adj[i]);
    }
    printf("----------------------------------\n");
}


void BFS(Grafo* grafo, int vertice_inicio) {
    if (grafo == NULL || vertice_inicio < 0 || vertice_inicio >= grafo->num_vertices) {
        fprintf(stderr, "Erro: vértice inicial inválido para BFS\n");
        return;
    }

    printf("Iniciando BFS a partir do vértice %d:\n", vertice_inicio);

    int* visitados = (int*)calloc(grafo->num_vertices, sizeof(int));
    if (visitados == NULL) exit(1);

    Fila* fila = criar_fila();

    visitados[vertice_inicio] = 1;
    enfileirar(fila, vertice_inicio);
    printf("Visitando: %d\n", vertice_inicio);

    while (!fila_vazia(fila)) {
        int vertice_atual = desenfileirar(fila);

        NoLista* vizinho = grafo->listas_adj[vertice_atual]->cabeca;

        while (vizinho != NULL) {
            int vizinho_dado = vizinho->dado;
            if (visitados[vizinho_dado] == 0) {
                visitados[vizinho_dado] = 1;
                enfileirar(fila, vizinho_dado);
                printf("Visitando: %d\n", vizinho_dado);
            }
            vizinho = vizinho->proximo;
        }
    }

    printf("Fim do BFS.\n");
    deletar_fila(fila);
    free(visitados);
}

void DFS(Grafo* grafo, int vertice_inicio) {
    if (grafo == NULL || vertice_inicio < 0 || vertice_inicio >= grafo->num_vertices) {
        fprintf(stderr, "Erro: vértice inicial inválido para DFS\n");
        return;
    }

    printf("Iniciando DFS a partir do vértice %d:\n", vertice_inicio);

    int* visitados = (int*)calloc(grafo->num_vertices, sizeof(int));
    if (visitados == NULL) exit(1);

    Pilha* pilha = criar_pilha();

    empilhar(pilha, vertice_inicio);

    while (!pilha_vazia(pilha)) {
        int vertice_atual = desempilhar(pilha);

        if (visitados[vertice_atual] == 0) {
            visitados[vertice_atual] = 1;
            printf("Visitando: %d\n", vertice_atual);

            NoLista* vizinho = grafo->listas_adj[vertice_atual]->cabeca;
            while (vizinho != NULL) {
                if (visitados[vizinho->dado] == 0) {
                    empilhar(pilha, vizinho->dado);
                }
                vizinho = vizinho->proximo;
            }
        }
    }

    printf("Fim do DFS.\n");
    deletar_pilha(pilha);
    free(visitados);
}