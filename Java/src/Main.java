import Models.*;

public class Main {
    public static void main(String[] args) {
        System.out.println("==== Demonstração de Estruturas de Dados ====\n");

        // Fila
        Fila.info();
        Fila<Integer> fila = new Fila<>();
        System.out.println("Adicionando o valor 1");
        fila.enqueue(1);
        System.out.println("Adicionando o valor 2");
        fila.enqueue(2);
        System.out.println("Adicionando o valor 3");
        fila.enqueue(3);
        System.out.println("Primeiro elemento: " + fila.peek().toString());
        System.out.println("Desenfileirando...");
        fila.dequeue();
        System.out.println("Primeiro elemento: " + fila.peek().toString());
        System.out.println();

        // Pilha
        Pilha.info();
        Pilha<String> pilha = new Pilha<>();
        System.out.println("Adicionando o valor A");
        pilha.push("A");
        System.out.println("Adicionando o valor B");
        pilha.push("B");
        System.out.println("Adicionando o valor C");
        pilha.push("C");
        System.out.println("Elemento que está no topo: " + pilha.peek());
        System.out.println("Desempilhando...");
        pilha.pop();
        System.out.println("Elemento que está no topo: " + pilha.peek());
        System.out.println();

        // Lista Encadeada
        ListaEncadeada.info();
        ListaEncadeada<Integer> lista = new ListaEncadeada<>();
        System.out.println("Adicionando o valor 10");
        lista.inserir(10);
        System.out.println("Adicionando o valor 20");
        lista.inserir(20);
        System.out.println("Adicionando o valor 30");
        lista.inserir(30);
        System.out.println("Resultado:");
        lista.listar();
        System.out.println();

        // Lista Duplamente Encadeada
        ListaDuplamenteEncadeada.info();
        ListaDuplamenteEncadeada<String> lista2 = new ListaDuplamenteEncadeada<>();
        System.out.println("Adicionando o valor A");
        lista2.inserir("A");
        System.out.println("Adicionando o valor B");
        lista2.inserir("B");
        System.out.println("Adicionando o valor C");
        lista2.inserir("C");
        System.out.println("Resultado:");
        lista2.listarFrente();
        System.out.println("Resultado reverso:");
        lista2.listarTras();
        System.out.println();

        // Árvore Binária
        ArvoreBinaria.info();
        ArvoreBinaria arvore = new ArvoreBinaria();
        System.out.println("Adicionando o valor 5");
        arvore.inserir(5);
        System.out.println("Adicionando o valor 2");
        arvore.inserir(2);
        System.out.println("Adicionando o valor 8");
        arvore.inserir(8);
        System.out.println("Adicionando o valor 1");
        arvore.inserir(1);
        System.out.println("Adicionando o valor 3");
        arvore.inserir(3);
        System.out.println("Resultado:");
        arvore.emOrdem();
        System.out.println();

        // Tabela Hash
        TabelaHash.info();
        System.out.println("Criando uma tabela hash com 5 índices...");
        TabelaHash<String, Integer> tabela = new TabelaHash<>(5);
        System.out.println("Adicionando o elemento Alice com o valor 25");
        tabela.inserir("Alice", 25);
        System.out.println("Adicionando o elemento Bruno com o valor 30");
        tabela.inserir("Bruno", 30);
        System.out.println("Adicionando o elemento Carla com o valor 28");
        tabela.inserir("Carla", 28);
        System.out.println("Adicionando o elemento Daniel com o valor 22");
        tabela.inserir("Daniel", 22);
        System.out.println("Resultado:");
        tabela.mostrar();
        System.out.println();
    }
}
