package Models;

// Nó da lista duplamente encadeada
class NoDuplo<T> {
    T valor;
    NoDuplo<T> proximo;
    NoDuplo<T> anterior;

    public NoDuplo(T valor) {
        this.valor = valor;
        this.proximo = null;
        this.anterior = null;
    }
}

// Lista duplamente encadeada
public class ListaDuplamenteEncadeada<T> {
    private NoDuplo<T> inicio;
    private NoDuplo<T> fim;

    public ListaDuplamenteEncadeada() {
        this.inicio = null;
        this.fim = null;
    }

    // Inserir no final
    public void inserir(T valor) {
        NoDuplo<T> novo = new NoDuplo<>(valor);
        if (inicio == null) {
            inicio = fim = novo;
        } else {
            fim.proximo = novo;
            novo.anterior = fim;
            fim = novo;
        }
    }

    // Remover por valor
    public boolean remover(T valor) {
        if (inicio == null)
            throw new RuntimeException("A Lista Duplamente Encadeada está vazia.");

        NoDuplo<T> atual = inicio;
        while (atual != null && !atual.valor.equals(valor)) {
            atual = atual.proximo;
        }
        if (atual == null) return false; // não achou

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

        return true;
    }

    // Listar do início ao fim
    public void listarFrente() {
        NoDuplo<T> atual = inicio;
        while (atual != null) {
            System.out.print(atual.valor + (atual.proximo != null ? " <-> " : ""));
            atual = atual.proximo;
        }
        System.out.println();
    }

    // Listar do fim ao início
    public void listarTras() {
        NoDuplo<T> atual = fim;
        while (atual != null) {
            System.out.print(atual.valor + (atual.anterior != null ? " <-> " : ""));
            atual = atual.anterior;
        }
        System.out.println();
    }

    public boolean contem(T valor) {
        NoDuplo<T> atual = inicio;
        while (atual != null) {
            if (atual.valor.equals(valor))
                return true;
            atual = atual.proximo;
        }
        return false;
    }

    public boolean estaVazia() {
        return inicio == null;
    }

    public int tamanho() {
        int count = 0;
        NoDuplo<T> atual = inicio;
        while (atual != null) {
            count++;
            atual = atual.proximo;
        }
        return count;
    }

    public static void info() {
        System.out.println("Lista Duplamente Encadeada → Cada nó aponta para o anterior e o próximo.");
        System.out.println("Vantagem: navegação nos dois sentidos.");
        System.out.println("Limitação: usa mais memória por ter dois ponteiros.\n");
    }
}

