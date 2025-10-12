

public class Main {
    public static void main(String[] args) {
        //Testando a Pilha
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



// Lista Encadeada 
        ListaEncadeada lista = new ListaEncadeada();
        lista.inserir(10);
        lista.inserir(20);
        lista.inserir(30);
        lista.listar();

        lista.remover(20);
        lista.listar();



// Lista Duplamente Encadeada
        ListaDuplamenteEncadeada listaDupla = new ListaDuplamenteEncadeada();
        listaDupla.inserir(1);
        listaDupla.inserir(2);
        listaDupla.inserir(3);
        listaDupla.listarFrente();
    
        listaDupla.remover(2);
        listaDupla.listarFrente();
        listaDupla.listarTras();
    }

    ArvoreBinaria arvore = new ArvoreBinaria();

        arvore.inserir(50);
        arvore.inserir(30);
        arvore.inserir(70);
        arvore.inserir(20);
        arvore.inserir(40);
        arvore.inserir(60);
        arvore.inserir(80);

        System.out.print("Percorrendo em ordem: ");
        arvore.emOrdem();
        System.out.println();

        System.out.println("Buscar 40: " + arvore.buscar(40)); // true
        System.out.println("Buscar 90: " + arvore.buscar(90)); // false
}