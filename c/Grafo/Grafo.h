#ifndef GRAFO_H
#define GRAFO_H

#include "../ListaEncadeada/lista_encadeada.h"

typedef struct {
    int num_vertices;
    ListaEncadeada** listas_adj;
} Grafo;

Grafo* criar_grafo(int num_vertices);

void deletar_grafo(Grafo* grafo);

void adicionar_aresta(Grafo* grafo, int origem, int destino);

void imprimir_grafo(Grafo* grafo);

void BFS(Grafo* grafo, int vertice_inicio);

void DFS(Grafo* grafo, int vertice_inicio);

#endif