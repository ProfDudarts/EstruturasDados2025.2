class Pilha<T> {
    private java.util.LinkedList<T> elementos = new java.util.LinkedList<>();

    // adiciona no topo
    public void push(T valor) {
        elementos.addFirst(valor);
    }

    // remove do topo
    public T pop() {
        if (isEmpty()) throw new RuntimeException("Pilha vazia!");
        return elementos.removeFirst();
    }

    // olha o topo
    public T peek() {
        if (isEmpty()) throw new RuntimeException("Pilha vazia!");
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
}
