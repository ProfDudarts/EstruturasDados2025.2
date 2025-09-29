#ifndef LISTA_DUPLAMENTE_ENCADEADA_H
#define LISTA_DUPLAMENTE_ENCADEADA_H

typedef struct NoListaDupla {
    int dado;
    struct NoListaDupla* proximo;
    struct NoListaDupla* anterior;
} NoListaDupla;

typedef struct {
    NoListaDupla* cabeca;
    NoListaDupla* cauda;
    int tamanho;
} ListaDuplamenteEncadeada;

// Funções básicas
ListaDuplamenteEncadeada* criar_lista_dupla();
void inserir_inicio_dupla(ListaDuplamenteEncadeada* lista, int dado);
void inserir_fim_dupla(ListaDuplamenteEncadeada* lista, int dado);
void inserir_posicao_dupla(ListaDuplamenteEncadeada* lista, int dado, int posicao);
int remover_inicio_dupla(ListaDuplamenteEncadeada* lista);
int remover_fim_dupla(ListaDuplamenteEncadeada* lista);
int remover_posicao_dupla(ListaDuplamenteEncadeada* lista, int posicao);
int buscar_dupla(ListaDuplamenteEncadeada* lista, int dado);
int lista_dupla_vazia(ListaDuplamenteEncadeada* lista);
int obter_tamanho_dupla(ListaDuplamenteEncadeada* lista);
void imprimir_lista_dupla(ListaDuplamenteEncadeada* lista);
void imprimir_lista_dupla_reversa(ListaDuplamenteEncadeada* lista);
void deletar_lista_dupla(ListaDuplamenteEncadeada* lista);

#endif