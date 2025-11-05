#include <iostream>
#include "Pilha.hpp"

int main() {
    Pilha pilha;

    std::cout << "=== Teste da Pilha ===\n";

    pilha.push(10);
    pilha.push(20);
    pilha.push(30);

    std::cout << "Topo da pilha: " << pilha.peek() << "\n";

    while (!pilha.estaVazia()) {
        std::cout << "Pop: " << pilha.pop() << "\n";
    }

    return 0;
}
