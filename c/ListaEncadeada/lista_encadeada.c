#include <stdio.h>
#include <stdlib.h>
#include "lista_encadeada.h"

ListaEncadeada* criar_lista() {
    ListaEncadeada* lista = (ListaEncadeada*)malloc(sizeof(ListaEncadeada));
    if (lista == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }
    lista->cabeca = NULL;
    lista->tamanho = 0;
    return lista;
}

void inserir_inicio(ListaEncadeada* lista, int dado) {
    NoLista* novo_no = (NoLista*)malloc(sizeof(NoLista));
    if (novo_no == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }
    novo_no->dado = dado;
    novo_no->proximo = lista->cabeca;
    lista->cabeca = novo_no;
    lista->tamanho++;
}

void inserir_fim(ListaEncadeada* lista, int dado) {
    NoLista* novo_no = (NoLista*)malloc(sizeof(NoLista));
    if (novo_no == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }
    novo_no->dado = dado;
    novo_no->proximo = NULL;
    
    if (lista->cabeca == NULL) {
        lista->cabeca = novo_no;
    } else {
        NoLista* atual = lista->cabeca;
        while (atual->proximo != NULL) {
            atual = atual->proximo;
        }
        atual->proximo = novo_no;
    }
    lista->tamanho++;
}

void inserir_posicao(ListaEncadeada* lista, int dado, int posicao) {
    if (posicao < 0 || posicao > lista->tamanho) {
        fprintf(stderr, "Erro: Posição inválida\n");
        return;
    }
    
    if (posicao == 0) {
        inserir_inicio(lista, dado);
        return;
    }
    
    NoLista* novo_no = (NoLista*)malloc(sizeof(NoLista));
    if (novo_no == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }
    novo_no->dado = dado;
    
    NoLista* atual = lista->cabeca;
    for (int i = 0; i < posicao - 1; i++) {
        atual = atual->proximo;
    }
    
    novo_no->proximo = atual->proximo;
    atual->proximo = novo_no;
    lista->tamanho++;
}

int remover_inicio(ListaEncadeada* lista) {
    if (lista_vazia(lista)) {
        fprintf(stderr, "Erro: Lista vazia\n");
        exit(1);
    }
    
    NoLista* no_a_remover = lista->cabeca;
    int dado_retornado = no_a_remover->dado;
    lista->cabeca = lista->cabeca->proximo;
    free(no_a_remover);
    lista->tamanho--;
    return dado_retornado;
}

int remover_fim(ListaEncadeada* lista) {
    if (lista_vazia(lista)) {
        fprintf(stderr, "Erro: Lista vazia\n");
        exit(1);
    }
    
    if (lista->cabeca->proximo == NULL) {
        return remover_inicio(lista);
    }
    
    NoLista* atual = lista->cabeca;
    while (atual->proximo->proximo != NULL) {
        atual = atual->proximo;
    }
    
    int dado_retornado = atual->proximo->dado;
    free(atual->proximo);
    atual->proximo = NULL;
    lista->tamanho--;
    return dado_retornado;
}

int remover_posicao(ListaEncadeada* lista, int posicao) {
    if (posicao < 0 || posicao >= lista->tamanho) {
        fprintf(stderr, "Erro: Posição inválida\n");
        exit(1);
    }
    
    if (posicao == 0) {
        return remover_inicio(lista);
    }
    
    NoLista* atual = lista->cabeca;
    for (int i = 0; i < posicao - 1; i++) {
        atual = atual->proximo;
    }
    
    NoLista* no_a_remover = atual->proximo;
    int dado_retornado = no_a_remover->dado;
    atual->proximo = no_a_remover->proximo;
    free(no_a_remover);
    lista->tamanho--;
    return dado_retornado;
}

int buscar(ListaEncadeada* lista, int dado) {
    NoLista* atual = lista->cabeca;
    int posicao = 0;
    
    while (atual != NULL) {
        if (atual->dado == dado) {
            return posicao;
        }
        atual = atual->proximo;
        posicao++;
    }
    
    return -1;
}

int lista_vazia(ListaEncadeada* lista) {
    return lista->cabeca == NULL;
}

int obter_tamanho(ListaEncadeada* lista) {
    return lista->tamanho;
}

void imprimir_lista(ListaEncadeada* lista) {
    printf("Lista: [");
    NoLista* atual = lista->cabeca;
    while (atual != NULL) {
        printf("%d", atual->dado);
        if (atual->proximo != NULL) {
            printf(", ");
        }
        atual = atual->proximo;
    }
    printf("]\n");
}

void deletar_lista(ListaEncadeada* lista) {
    NoLista* atual = lista->cabeca;
    NoLista* proximo_no;
    
    while (atual != NULL) {
        proximo_no = atual->proximo;
        free(atual);
        atual = proximo_no;
    }
    
    free(lista);
}


