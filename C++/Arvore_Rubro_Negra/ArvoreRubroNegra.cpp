#include "ArvoreRubroNegra.hpp"

// Construtor
ArvoreRubroNegra::ArvoreRubroNegra() : raiz(nullptr) {}

// Destrutor
ArvoreRubroNegra::~ArvoreRubroNegra() {
    liberarMemoria(raiz);
}

// Libera a memória alocada para os nós da árvore
void ArvoreRubroNegra::liberarMemoria(No* no) {
    if (no != nullptr) {
        liberarMemoria(no->esquerdo);
        liberarMemoria(no->direito);
        delete no;
    }
}

// Rotação à esquerda
void ArvoreRubroNegra::rotacionarEsquerda(No* x) {
    No* y = x->direito;
    x->direito = y->esquerdo;
    if (y->esquerdo != nullptr) {
        y->esquerdo->pai = x;
    }
    y->pai = x->pai;
    if (x->pai == nullptr) {
        raiz = y;
    } else if (x == x->pai->esquerdo) {
        x->pai->esquerdo = y;
    } else {
        x->pai->direito = y;
    }
    y->esquerdo = x;
    x->pai = y;
}

// Rotação à direita
void ArvoreRubroNegra::rotacionarDireita(No* y) {
    No* x = y->esquerdo;
    y->esquerdo = x->direito;
    if (x->direito != nullptr) {
        x->direito->pai = y;
    }
    x->pai = y->pai;
    if (y->pai == nullptr) {
        raiz = x;
    } else if (y == y->pai->esquerdo) {
        y->pai->esquerdo = x;
    } else {
        y->pai->direito = x;
    }
    x->direito = y;
    y->pai = x;
}

// Corrige a árvore após a inserção para manter as propriedades da árvore rubro-negra
void ArvoreRubroNegra::corrigirInsercao(No* z) {
    while (z->pai != nullptr && z->pai->cor == VERMELHO) {
        if (z->pai == z->pai->pai->esquerdo) {
            No* y = z->pai->pai->direito; // Tio de z
            if (y != nullptr && y->cor == VERMELHO) {
                // Caso 1: Tio é vermelho
                z->pai->cor = PRETO;
                y->cor = PRETO;
                z->pai->pai->cor = VERMELHO;
                z = z->pai->pai;
            } else {
                if (z == z->pai->direito) {
                    // Caso 2: Tio é preto e z é filho à direita
                    z = z->pai;
                    rotacionarEsquerda(z);
                }
                // Caso 3: Tio é preto e z é filho à esquerda
                z->pai->cor = PRETO;
                z->pai->pai->cor = VERMELHO;
                rotacionarDireita(z->pai->pai);
            }
        } else {
            // Simétrico ao if acima
            No* y = z->pai->pai->esquerdo; // Tio de z
            if (y != nullptr && y->cor == VERMELHO) {
                // Caso 1
                z->pai->cor = PRETO;
                y->cor = PRETO;
                z->pai->pai->cor = VERMELHO;
                z = z->pai->pai;
            } else {
                if (z == z->pai->esquerdo) {
                    // Caso 2
                    z = z->pai;
                    rotacionarDireita(z);
                }
                // Caso 3
                z->pai->cor = PRETO;
                z->pai->pai->cor = VERMELHO;
                rotacionarEsquerda(z->pai->pai);
            }
        }
    }
    raiz->cor = PRETO;
}

// Insere um novo valor na árvore
void ArvoreRubroNegra::inserir(int valor) {
    No* z = new No(valor);
    No* y = nullptr;
    No* x = raiz;

    while (x != nullptr) {
        y = x;
        if (z->dado < x->dado) {
            x = x->esquerdo;
        } else {
            x = x->direito;
        }
    }

    z->pai = y;
    if (y == nullptr) {
        raiz = z;
    } else if (z->dado < y->dado) {
        y->esquerdo = z;
    } else {
        y->direito = z;
    }

    corrigirInsercao(z);
}

// Busca um valor na árvore (função pública)
No* ArvoreRubroNegra::buscar(int valor) {
    return buscarRec(raiz, valor);
}

// Função recursiva para buscar um valor
No* ArvoreRubroNegra::buscarRec(No* no, int valor) {
    if (no == nullptr || no->dado == valor) {
        return no;
    }
    if (valor < no->dado) {
        return buscarRec(no->esquerdo, valor);
    }
    return buscarRec(no->direito, valor);
}

// Travessia em ordem (função pública)
void ArvoreRubroNegra::emOrdem() {
    std::cout << "Em ordem: ";
    emOrdemRec(raiz);
    std::cout << std::endl;
}

// Função recursiva para a travessia em ordem
void ArvoreRubroNegra::emOrdemRec(No* no) {
    if (no != nullptr) {
        emOrdemRec(no->esquerdo);
        std::cout << no->dado << "(" << (no->cor == VERMELHO ? "V" : "P") << ") ";
        emOrdemRec(no->direito);
    }
}