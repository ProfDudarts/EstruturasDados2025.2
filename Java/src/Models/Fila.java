package Models;

public class Fila<T> {
    private java.util.LinkedList<T> elementos = new java.util.LinkedList<>();

    // adiciona no final
    public void enqueue(T valor) {
        elementos.addLast(valor);
    }

    // remove do início
    public T dequeue() {
        if (isEmpty()) throw new RuntimeException("Fila vazia!");
        return elementos.removeFirst();
    }

    // olha o primeiro
    public T peek() {
        if (isEmpty()) throw new RuntimeException("Fila vazia!");
        return elementos.getFirst();
    }

    // verifica se está vazia
    public boolean isEmpty() {
        return elementos.isEmpty();
    }

    // retorna o tamanho
    public int size() {
        return elementos.size();
    }

    public static void info() {
        System.out.println("Fila → Primeiro que entra, primeiro que sai (FIFO).");
        System.out.println("Vantagem: ideal para filas de espera.");
        System.out.println("Limitação: acesso apenas ao primeiro elemento.\n");
    }
}
