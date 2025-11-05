#include "ArvoreRubroNegra.hpp"
#include <iostream>

int main() {
    ArvoreRubroNegra arvore;

    std::cout << "Inserindo valores: 10, 20, 30, 15, 25, 5" << std::endl;
    arvore.inserir(10);
    arvore.inserir(20);
    arvore.inserir(30);
    arvore.inserir(15);
    arvore.inserir(25);
    arvore.inserir(5);

    arvore.emOrdem();

    int valorBusca = 15;
    if (arvore.buscar(valorBusca) != nullptr) {
        std::cout << "Valor " << valorBusca << " encontrado na arvore." << std::endl;
    } else {
        std::cout << "Valor " << valorBusca << " nao encontrado na arvore." << std::endl;
    }

    valorBusca = 100;
    if (arvore.buscar(valorBusca) != nullptr) {
        std::cout << "Valor " << valorBusca << " encontrado na arvore." << std::endl;
    } else {
        std::cout << "Valor " << valorBusca << " nao encontrado na arvore." << std::endl;
    }

    return 0;
}