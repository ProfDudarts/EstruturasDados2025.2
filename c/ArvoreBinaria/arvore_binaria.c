#include <stdio.h>
#include <stdlib.h>

struct No {
    int valor;
    struct No* esquerda;
    struct No* direita;
};

struct No* criarNo(int valor) {
    struct No* novoNo = (struct No*)malloc(sizeof(struct No));
    novoNo->valor = valor;
    novoNo->esquerda = NULL;
    novoNo->direita = NULL;
    return novoNo;
}

void inserir_iterativo(struct No** raiz, int valor) {
    struct No* novoNo = criarNo(valor);

    if (*raiz == NULL) {
        *raiz = novoNo;
        return;
    }

    struct No* atual = *raiz;
    struct No* pai = NULL;

    while (atual != NULL) {
        pai = atual;
        if (valor < atual->valor) {
            atual = atual->esquerda;
        } else if (valor > atual->valor) {
            atual = atual->direita;
        } else {
            free(novoNo);
            return;
        }
    }

    if (valor < pai->valor) {
        pai->esquerda = novoNo;
    } else {
        pai->direita = novoNo;
    }
}

void emOrdem(struct No* raiz) {
    if (raiz != NULL) {
        emOrdem(raiz->esquerda);
        printf("%d ", raiz->valor);
        emOrdem(raiz->direita);
    }
}

void liberarArvore(struct No* raiz) {
    if (raiz != NULL) {
        liberarArvore(raiz->esquerda);
        liberarArvore(raiz->direita);
        free(raiz);
    }
}
int main() {
    struct No* raiz = NULL;

    printf("Inserindo valores com a função ITERATIVA...\n");
    inserir_iterativo(&raiz, 50);
    inserir_iterativo(&raiz, 30);
    inserir_iterativo(&raiz, 70);
    inserir_iterativo(&raiz, 20);
    inserir_iterativo(&raiz, 40);

    printf("Valores da árvore em ordem:\n");
    emOrdem(raiz);
    printf("\n");
    
    liberarArvore(raiz);
    return 0;
}