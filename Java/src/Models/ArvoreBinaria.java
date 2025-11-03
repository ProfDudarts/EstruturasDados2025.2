package Models;

public class ArvoreBinaria {
    private class No {
        int valor;
        No esquerda;
        No direita;

        No(int valor) {
            this.valor = valor;
            esquerda = direita = null;
        }
    }

    private No raiz;

    public void inserir(int valor) {
        raiz = inserirRecursivo(raiz, valor);
    }

    private No inserirRecursivo(No atual, int valor) {
        if (atual == null) {
            return new No(valor);
        }

        if (valor < atual.valor) {
            atual.esquerda = inserirRecursivo(atual.esquerda, valor);
        } else if (valor > atual.valor) {
            atual.direita = inserirRecursivo(atual.direita, valor);
        }

        return atual;
    }

    public void emOrdem() {
        emOrdemRecursivo(raiz);
        System.out.println();
    }

    private void emOrdemRecursivo(No atual) {
        if (atual != null) {
            emOrdemRecursivo(atual.esquerda);
            System.out.print(atual.valor + " ");
            emOrdemRecursivo(atual.direita);
        }
    }

    public boolean buscar(int valor) {
        return buscarRecursivo(raiz, valor);
    }

    private boolean buscarRecursivo(No atual, int valor) {
        if (atual == null) {
            return false;
        }
        if (valor == atual.valor) {
            return true;
        }
        return valor < atual.valor
                ? buscarRecursivo(atual.esquerda, valor)
                : buscarRecursivo(atual.direita, valor);
    }

    public static void info() {
        System.out.println("Árvore Binária de Busca → organiza dados hierarquicamente.");
        System.out.println("Vantagem: busca rápida em dados ordenados.");
        System.out.println("Limitação: pode ficar desbalanceada, perdendo eficiência.\n");
    }
}