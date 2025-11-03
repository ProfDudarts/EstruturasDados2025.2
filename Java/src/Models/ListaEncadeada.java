package Models;
 
// Nó da lista encadeada simples
class No<T> {
    T valor;
    No<T> proximo;

    public No(T valor) {
        this.valor = valor;
        this.proximo = null;
    }
}

// Lista encadeada simples
public class ListaEncadeada<T> {
    private No<T> inicio;

    public ListaEncadeada() {
        this.inicio = null;
    }

    // Inserir no final
    public void inserir(T valor) {
        No<T> novo = new No<>(valor);
        if (inicio == null) {
            inicio = novo;
        } else {
            No<T> atual = inicio;
            while (atual.proximo != null) {
                atual = atual.proximo;
            }
            atual.proximo = novo;
        }
    }

    // Remover por valor
    public boolean remover(T valor) {
        if (inicio == null)
            throw new RuntimeException("A Lista Encadeada está vazia.");

        if (inicio.valor.equals(valor)) {
            inicio = inicio.proximo;
            return true;
        }

        No<T> atual = inicio;
        while (atual.proximo != null && !atual.proximo.valor.equals(valor)) {
            atual = atual.proximo;
        }
        if (atual.proximo != null) {
            atual.proximo = atual.proximo.proximo;
            return true;
        }

        return false;
    }

    // Listar elementos
    public void listar() {
        No<T> atual = inicio;
        while (atual != null) {
            System.out.print(atual.valor + " -> ");
            atual = atual.proximo;
        }
        System.out.println("null");
    }

    public boolean contem(T valor) {
        No<T> atual = inicio;
        while (atual != null) {
            if (atual.valor.equals(valor)) return true;
            atual = atual.proximo;
        }
        return false;
    }

    public boolean estaVazia() {
        return inicio == null;
    }

    public int tamanho() {
        int count = 0;
        No<T> atual = inicio;
        while (atual != null) {
            count++;
            atual = atual.proximo;
        }
        return count;
    }

    public static void info() {
        System.out.println("Lista Encadeada → Nós ligados por ponteiros simples.");
        System.out.println("Vantagem: inserção e remoção rápidas.");
        System.out.println("Limitação: acesso sequencial, não é indexada.\n");
    }
}



