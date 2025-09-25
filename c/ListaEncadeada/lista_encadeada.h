#ifndef LISTA_ENCADEADA_H
#define LISTA_ENCADEADA_H

typedef struct NoLista {
    int dado;
    struct NoLista* proximo;
} NoLista;

typedef struct {
    NoLista* cabeca;
    int tamanho;
} ListaEncadeada;

ListaEncadeada* criar_lista();
void inserir_inicio(ListaEncadeada* lista, int dado);
void inserir_fim(ListaEncadeada* lista, int dado);
void inserir_posicao(ListaEncadeada* lista, int dado, int posicao);
int remover_inicio(ListaEncadeada* lista);
int remover_fim(ListaEncadeada* lista);
int remover_posicao(ListaEncadeada* lista, int posicao);
int buscar(ListaEncadeada* lista, int dado);
int lista_vazia(ListaEncadeada* lista);
int obter_tamanho(ListaEncadeada* lista);
void imprimir_lista(ListaEncadeada* lista);
void deletar_lista(ListaEncadeada* lista);

#endif
