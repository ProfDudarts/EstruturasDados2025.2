#include <iostream>
#include "Fila.hpp"

int main() {
    Fila fila;

    std::cout << "=== Teste da Fila ===\n";

    fila.enfileirar(1);
    fila.enfileirar(2);
    fila.enfileirar(3);

    std::cout << "Frente da fila: " << fila.peek() << "\n";

    while (!fila.estaVazia()) {
        std::cout << "Desenfileira: " << fila.desenfileirar() << "\n";
    }

    return 0;
}
