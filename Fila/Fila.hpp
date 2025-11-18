#ifndef FILA_HPP
#define FILA_HPP

struct NoF {
    int dado;
    NoF* proximo;
};

class Fila {
private:
    NoF* frente;
    NoF* tras;
public:
    Fila();
    ~Fila();
    void enfileirar(int valor);
    int desenfileirar();
    int peek();
    bool estaVazia();
};

#endif
