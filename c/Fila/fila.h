#ifndef FILA_H
#define FILA_H

typedef struct NoFila {
    int dado;
    struct NoFila* proximo;
} NoFila;

typedef struct {
    NoFila* inicio;
    NoFila* fim;
} Fila;

Fila* criar_fila();
void enfileirar(Fila* fila, int dado);
int desenfileirar(Fila* fila);
int fila_vazia(Fila* fila);
void deletar_fila(Fila* fila);

#endif