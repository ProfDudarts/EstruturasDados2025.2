#include <iostream>
#include "ListaEncadeada.h"

int main() {
    ListaEncadeada minhaLista;

    std::cout << "Inserindo 10, 20 e 30 no inicio..." << std::endl;
    minhaLista.inserirNoInicio(10);
    minhaLista.inserirNoInicio(20);
    minhaLista.inserirNoInicio(30);
    minhaLista.imprimir();

    std::cout << "\nRemovendo o valor 20..." << std::endl;
    minhaLista.remover(20);
    minhaLista.imprimir();

    std::cout << "\nRemovendo o valor 30 (a cabeca)..." << std::endl;
    minhaLista.remover(30);
    minhaLista.imprimir();

    return 0;
}