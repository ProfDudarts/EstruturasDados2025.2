public class Main {
    public static void main(String[] args) {
        // Testando a Pilha
        Pilha<Integer> pilha = new Pilha<>();
        pilha.push(10);
        pilha.push(20);
        pilha.push(30);
        System.out.println("Topo da pilha: " + pilha.peek()); // 30
        System.out.println("Pop: " + pilha.pop()); // 30
        System.out.println("Novo topo: " + pilha.peek()); // 20

        // Testando a Fila
        Fila<String> fila = new Fila<>();
        fila.enqueue("A");
        fila.enqueue("B");
        fila.enqueue("C");
        System.out.println("Primeiro da fila: " + fila.peek()); // A
        System.out.println("Dequeue: " + fila.dequeue()); // A
        System.out.println("Novo primeiro: " + fila.peek()); // B
    }
}
