// Nó da lista encadeada simples
class No {
    int valor;
    No proximo;

    public No(int valor) {
        this.valor = valor;
        this.proximo = null;
    }
}

// Lista encadeada simples
class ListaEncadeada {
    private No inicio;

    public ListaEncadeada() {
        this.inicio = null;
    }

    // Inserir no final
    public void inserir(int valor) {
        No novo = new No(valor);
        if (inicio == null) {
            inicio = novo;
        } else {
            No atual = inicio;
            while (atual.proximo != null) {
                atual = atual.proximo;
            }
            atual.proximo = novo;
        }
    }

    // Remover por valor
    public void remover(int valor) {
        if (inicio == null) return;

        if (inicio.valor == valor) {
            inicio = inicio.proximo;
            return;
        }

        No atual = inicio;
        while (atual.proximo != null && atual.proximo.valor != valor) {
            atual = atual.proximo;
        }
        if (atual.proximo != null) {
            atual.proximo = atual.proximo.proximo;
        }
    }

    // Listar elementos
    public void listar() {
        No atual = inicio;
        while (atual != null) {
            System.out.print(atual.valor + " -> ");
            atual = atual.proximo;
        }
        System.out.println("null");
    }
}


// Nó da lista duplamente encadeada
class NoDuplo {
    int valor;
    NoDuplo proximo;
    NoDuplo anterior;

    public NoDuplo(int valor) {
        this.valor = valor;
        this.proximo = null;
        this.anterior = null;
    }
}

// Lista duplamente encadeada
class ListaDuplamenteEncadeada {
    private NoDuplo inicio;
    private NoDuplo fim;

    public ListaDuplamenteEncadeada() {
        this.inicio = null;
        this.fim = null;
    }

    // Inserir no final
    public void inserir(int valor) {
        NoDuplo novo = new NoDuplo(valor);
        if (inicio == null) {
            inicio = fim = novo;
        } else {
            fim.proximo = novo;
            novo.anterior = fim;
            fim = novo;
        }
    }

    // Remover por valor
    public void remover(int valor) {
        NoDuplo atual = inicio;
        while (atual != null && atual.valor != valor) {
            atual = atual.proximo;
        }
        if (atual == null) return; // não achou

        if (atual.anterior != null) {
            atual.anterior.proximo = atual.proximo;
        } else {
            inicio = atual.proximo; // removendo o primeiro
        }

        if (atual.proximo != null) {
            atual.proximo.anterior = atual.anterior;
        } else {
            fim = atual.anterior; // removendo o último
        }
    }

    // Listar do início ao fim
    public void listarFrente() {
        NoDuplo atual = inicio;
        while (atual != null) {
            System.out.print(atual.valor + " <-> ");
            atual = atual.proximo;
        }
        System.out.println("null");
    }

    // Listar do fim ao início
    public void listarTras() {
        NoDuplo atual = fim;
        while (atual != null) {
            System.out.print(atual.valor + " <-> ");
            atual = atual.anterior;
        }
        System.out.println("null");
    }
}

