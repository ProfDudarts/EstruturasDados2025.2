#include <stdio.h>
#include <stdlib.h>
#include "lista_duplamente_encadeada.h"

ListaDuplamenteEncadeada* criar_lista_dupla() {
    ListaDuplamenteEncadeada* lista = (ListaDuplamenteEncadeada*)malloc(sizeof(ListaDuplamenteEncadeada));
    if (lista == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }
    lista->cabeca = NULL;
    lista->cauda = NULL;
    lista->tamanho = 0;
    return lista;
}

void inserir_inicio_dupla(ListaDuplamenteEncadeada* lista, int dado) {
    NoListaDupla* novo_no = (NoListaDupla*)malloc(sizeof(NoListaDupla));
    if (novo_no == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }

    novo_no->dado = dado;
    novo_no->anterior = NULL;
    novo_no->proximo = lista->cabeca;

    if (lista->cabeca != NULL) {
        lista->cabeca->anterior = novo_no;
    }
    else {
        lista->cauda = novo_no; // Lista estava vazia
    }

    lista->cabeca = novo_no;
    lista->tamanho++;
}

void inserir_fim_dupla(ListaDuplamenteEncadeada* lista, int dado) {
    NoListaDupla* novo_no = (NoListaDupla*)malloc(sizeof(NoListaDupla));
    if (novo_no == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }

    novo_no->dado = dado;
    novo_no->proximo = NULL;
    novo_no->anterior = lista->cauda;

    if (lista->cauda != NULL) {
        lista->cauda->proximo = novo_no;
    }
    else {
        lista->cabeca = novo_no; // Lista estava vazia
    }

    lista->cauda = novo_no;
    lista->tamanho++;
}

void inserir_posicao_dupla(ListaDuplamenteEncadeada* lista, int dado, int posicao) {
    if (posicao < 0 || posicao > lista->tamanho) {
        fprintf(stderr, "Erro: Posição inválida\n");
        return;
    }

    if (posicao == 0) {
        inserir_inicio_dupla(lista, dado);
        return;
    }

    if (posicao == lista->tamanho) {
        inserir_fim_dupla(lista, dado);
        return;
    }

    NoListaDupla* novo_no = (NoListaDupla*)malloc(sizeof(NoListaDupla));
    if (novo_no == NULL) {
        fprintf(stderr, "Erro: Falha na alocação de memória\n");
        exit(1);
    }
    novo_no->dado = dado;

    NoListaDupla* atual = lista->cabeca;
    for (int i = 0; i < posicao; i++) {
        atual = atual->proximo;
    }

    novo_no->proximo = atual;
    novo_no->anterior = atual->anterior;
    atual->anterior->proximo = novo_no;
    atual->anterior = novo_no;
    lista->tamanho++;
}

int remover_inicio_dupla(ListaDuplamenteEncadeada* lista) {
    if (lista_dupla_vazia(lista)) {
        fprintf(stderr, "Erro: Lista vazia\n");
        exit(1);
    }

    NoListaDupla* no_a_remover = lista->cabeca;
    int dado_retornado = no_a_remover->dado;

    lista->cabeca = lista->cabeca->proximo;
    if (lista->cabeca != NULL) {
        lista->cabeca->anterior = NULL;
    }
    else {
        lista->cauda = NULL; // Lista ficou vazia
    }

    free(no_a_remover);
    lista->tamanho--;
    return dado_retornado;
}

int remover_fim_dupla(ListaDuplamenteEncadeada* lista) {
    if (lista_dupla_vazia(lista)) {
        fprintf(stderr, "Erro: Lista vazia\n");
        exit(1);
    }

    NoListaDupla* no_a_remover = lista->cauda;
    int dado_retornado = no_a_remover->dado;

    lista->cauda = lista->cauda->anterior;
    if (lista->cauda != NULL) {
        lista->cauda->proximo = NULL;
    }
    else {
        lista->cabeca = NULL; // Lista ficou vazia
    }

    free(no_a_remover);
    lista->tamanho--;
    return dado_retornado;
}

int remover_posicao_dupla(ListaDuplamenteEncadeada* lista, int posicao) {
    if (posicao < 0 || posicao >= lista->tamanho) {
        fprintf(stderr, "Erro: Posição inválida\n");
        exit(1);
    }

    if (posicao == 0) {
        return remover_inicio_dupla(lista);
    }

    if (posicao == lista->tamanho - 1) {
        return remover_fim_dupla(lista);
    }

    NoListaDupla* atual = lista->cabeca;
    for (int i = 0; i < posicao; i++) {
        atual = atual->proximo;
    }

    int dado_retornado = atual->dado;
    atual->anterior->proximo = atual->proximo;
    atual->proximo->anterior = atual->anterior;
    free(atual);
    lista->tamanho--;
    return dado_retornado;
}

int buscar_dupla(ListaDuplamenteEncadeada* lista, int dado) {
    NoListaDupla* atual = lista->cabeca;
    int posicao = 0;

    while (atual != NULL) {
        if (atual->dado == dado) {
            return posicao;
        }
        atual = atual->proximo;
        posicao++;
    }

    return -1; // Não encontrado
}

int lista_dupla_vazia(ListaDuplamenteEncadeada* lista) {
    return lista->cabeca == NULL;
}

int obter_tamanho_dupla(ListaDuplamenteEncadeada* lista) {
    return lista->tamanho;
}

void imprimir_lista_dupla(ListaDuplamenteEncadeada* lista) {
    printf("Lista Dupla: [");
    NoListaDupla* atual = lista->cabeca;
    while (atual != NULL) {
        printf("%d", atual->dado);
        if (atual->proximo != NULL) {
            printf(", ");
        }
        atual = atual->proximo;
    }
    printf("]\n");
}

void imprimir_lista_dupla_reversa(ListaDuplamenteEncadeada* lista) {
    printf("Lista Dupla (reversa): [");
    NoListaDupla* atual = lista->cauda;
    while (atual != NULL) {
        printf("%d", atual->dado);
        if (atual->anterior != NULL) {
            printf(", ");
        }
        atual = atual->anterior;
    }
    printf("]\n");
}

void deletar_lista_dupla(ListaDuplamenteEncadeada* lista) {
    NoListaDupla* atual = lista->cabeca;
    NoListaDupla* proximo_no;

    while (atual != NULL) {
        proximo_no = atual->proximo;
        free(atual);
        atual = proximo_no;
    }

    free(lista);
}