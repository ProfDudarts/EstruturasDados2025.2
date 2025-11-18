#include "Lista.hpp"


int main() {
    ListaDuplamenteEncadeada lista;

    lista.inserir_no_final(10);
    lista.inserir_no_final(20);
    lista.inserir_no_final(30);

    cout << "Lista para frente: ";
    lista.mostrar_para_frente();

    cout << "Lista para tras: ";
    lista.mostrar_para_tras();

    lista.inserir_no_inicio(5);
    cout << "Depois de inserir no inicio: ";
    lista.mostrar_para_frente();

    lista.remover(20);
    cout << "Depois de remover 20: ";
    lista.mostrar_para_frente();

    return 0;
}
