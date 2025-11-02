#include "ArvoreBinaria.hpp"

int main() {
    ArvoreBinaria arvore;

    arvore.inserir(8);
    arvore.inserir(3);
    arvore.inserir(10);
    arvore.inserir(1);
    arvore.inserir(6);
    arvore.inserir(14);
    arvore.inserir(4);
    arvore.inserir(7);
    arvore.inserir(13);

    arvore.emOrdem();
    arvore.preOrdem();
    arvore.posOrdem();

    return 0;
}
