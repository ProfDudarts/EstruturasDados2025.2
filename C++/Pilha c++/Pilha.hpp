#ifndef PILHA_HPP
#define PILHA_HPP

struct No {
    int dado;
    No* proximo;
};

class Pilha {
private:
    No* topo;
public:
    Pilha();
    ~Pilha();
    void push(int valor);
    int pop();
    int peek();
    bool estaVazia();
};

#endif
