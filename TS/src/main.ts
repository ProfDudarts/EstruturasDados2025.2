// 1. IMPORTAÇÕES (Adicionei a ListaEncadeada que faltava)
import { Fila } from './Models/Fila';
import { No } from './Models/no';
import { Pilha } from './Models/pilha';
import { ArvoreBinaria } from './Models/ArvoreBinaria';
import { ListaDuplamenteEncadeada } from './Models/ListaDuplamenteEncadeada';
// Assumindo que este é o caminho correto para a sua ListaEncadeada
import { ListaEncadeada } from './Models/ListaEncadeada'; 

// =====================================================================
// 2. TESTES DA FILA (Queue)
// =====================================================================
function testarFila() {
    console.log("=====================================================================");
    console.log("                       DEMONSTRAÇÃO DA FILA (Queue)");
    console.log("=====================================================================");

    const minhaFila = new Fila<number>();

    console.log('A fila está vazia?', minhaFila.vazio());

    console.log('Inserindo os elementos 10, 20 e 30...');
    minhaFila.inserir(10);
    minhaFila.inserir(20);
    minhaFila.inserir(30);

    console.log('--- Conteúdo da fila ---');
    console.log(minhaFila.listar());

    console.log('Tamanho atual da fila:', minhaFila.tamanho_fila());
    console.log('Elemento no topo (primeiro):', minhaFila.frente());

    console.log('Removendo o primeiro elemento:', minhaFila.apagar());
    console.log('Novo primeiro elemento:', minhaFila.frente());

    console.log('--- Conteúdo da fila após remover ---');
    console.log(minhaFila.listar());

    console.log('Tamanho atual da fila:', minhaFila.tamanho_fila());
}

// =====================================================================
// 3. TESTES DA PILHA (Stack)
// =====================================================================
function testarPilha() {
    console.log("=====================================================================");
    console.log("                      DEMONSTRAÇÃO DA PILHA (Stack)");
    console.log("=====================================================================");
    
    const minhaPilha = new Pilha<string>();

    minhaPilha.inserir('cleive');
    minhaPilha.inserir('rafael');
    minhaPilha.inserir('lucas');

    console.log('Tamanho da pilha:', minhaPilha.tamanho_pilha());
    console.log('Elemento no topo:', minhaPilha.topo_valor());

    console.log('--- Conteúdo da pilha ---');
    console.log(minhaPilha.listar());

    console.log('Apagando o topo:', minhaPilha.apagar());
    console.log('Novo topo:', minhaPilha.topo_valor());

    console.log('A pilha está vazia?', minhaPilha.empty());
}

// =====================================================================
// 4. TESTES DA LISTA ENCADEADA (Singly Linked List) - Vindo da 'main'
// =====================================================================
function testarListaEncadeada() {
    console.log("=====================================================================");
    console.log("               DEMONSTRAÇÃO DA LISTA ENCADEADA (Singly)");
    console.log("=====================================================================");

    // Criando uma lista de números
    const listaNumeros = new ListaEncadeada<number>();

    // Teste de Adição
    listaNumeros.adicionar(10);
    listaNumeros.adicionar(40);
    listaNumeros.adicionar_inicio(5);
    listaNumeros.imprimir(); // Esperado: 5 -> 10 -> 40

    // Teste de Inserção na Posição
    listaNumeros.inserir_na_posicao(20, 2); // Deve ir após o 10
    listaNumeros.inserir_na_posicao(50, 4); // Deve ir no final
    listaNumeros.imprimir(); // Esperado: 5 -> 10 -> 20 -> 40 -> 50

    // Teste de Remoção
    listaNumeros.remover(5); // Remove 5 (cabeça)
    listaNumeros.imprimir(); // Esperado: 10 -> 20 -> 40 -> 50
    listaNumeros.remover_na_posicao(2); // Remove 40
    listaNumeros.imprimir(); // Esperado: 10 -> 20 -> 50
    listaNumeros.remover(99); // Não encontrado

    console.log("\n--- DEMONSTRAÇÃO DE ORDENAÇÃO (LISTA ENCADEADA) ---\n");

    const listaParaOrdenar = new ListaEncadeada<number>();
    listaParaOrdenar.adicionar(50);
    listaParaOrdenar.adicionar(20);
    listaParaOrdenar.adicionar(40);
    listaParaOrdenar.adicionar(10);
    listaParaOrdenar.adicionar(30);
    listaParaOrdenar.adicionar(60);
    console.log("Lista original:");
    listaParaOrdenar.imprimir(); 

    // Testando Merge Sort (In-Place)
    console.log("\n>>> Testando Merge Sort (Otimizado para Listas Encadeadas)");
    listaParaOrdenar.ordenar_por_merge_sort();
    listaParaOrdenar.imprimir(); 

    // Testando Heap Sort (Auxiliar)
    console.log("\n>>> Reiniciando lista para Heap Sort");
    const listaHeap = new ListaEncadeada<number>();
    listaHeap.adicionar(5); listaHeap.adicionar(2); listaHeap.adicionar(8); listaHeap.adicionar(1);
    console.log("Lista original:");
    listaHeap.imprimir();
    listaHeap.ordenar_por_heap_sort_auxiliar();
    listaHeap.imprimir();

    console.log("\n--- LISTA ENCADEADA DE STRINGS ---");

    // Testando lista com tipo String (demonstração de genéricos)
    const listaStrings = new ListaEncadeada<string>();
    listaStrings.adicionar("Banana");
    listaStrings.adicionar_inicio("Maçã");
    listaStrings.adicionar("Pêra");
    listaStrings.adicionar("Abacaxi");
    console.log("Lista original:");
    listaStrings.imprimir();

    // Ordenação com strings (Selection Sort)
    console.log("\n>>> Testando Selection Sort com Strings");
    listaStrings.ordenar_por_selection_sort_in_place();
    listaStrings.imprimir(); 
}

// =====================================================================
// 5. TESTES DA ÁRVORE BINÁRIA (Binary Search Tree) - Vindo da 'dev'
// =====================================================================
function testarArvoreBinaria() {
    console.log("=====================================================================");
    console.log("                DEMONSTRAÇÃO DA ÁRVORE BINÁRIA DE BUSCA");
    console.log("=====================================================================");
    
    const minhaArvore = new ArvoreBinaria<number>();
    console.log('A árvore está vazia?', minhaArvore.vazio());

    console.log('Inserindo os elementos 20, 10, 30, 5, 15...');
    minhaArvore.inserir(20);
    minhaArvore.inserir(10);
    minhaArvore.inserir(30);
    minhaArvore.inserir(5);
    minhaArvore.inserir(15);

    console.log('--- Conteúdo da árvore (In-Order) ---');
    console.log(minhaArvore.listarInOrder()); // Esperado: [5, 10, 15, 20, 30]

    console.log('Removendo 20 (nó com dois filhos):', minhaArvore.remover(20));
    console.log('Novo conteúdo (In-Order):', minhaArvore.listarInOrder()); // Pode variar (ex: [5, 10, 15, 30])

    console.log('Tentando remover 50 (não existe):', minhaArvore.remover(50));
}

// =====================================================================
// 6. TESTES DA LISTA DUPLAMENTE ENCADEADA (Doubly) - Vindo da 'dev'
// =====================================================================
function testarListaDuplamenteEncadeada() {
    console.log("=====================================================================");
    console.log("            DEMONSTRAÇÃO DA LISTA DUPLAMENTE ENCADEADA (Doubly)");
    console.log("=====================================================================");
    
    console.log("--- Teste de Funcionalidades Básicas ---");

    const lista = new ListaDuplamenteEncadeada<number>();

    lista.adicionar(50);
    lista.adicionar(20);
    lista.adicionar_inicio(80); // [80, 50, 20]
    lista.inserir_na_posicao(10, 3); // [80, 50, 20, 10]
    lista.inserir_na_posicao(90, 2); // [80, 50, 90, 20, 10]
    
    console.log("Lista original:");
    lista.imprimir_frente();

    console.log("\n--- Teste de Quick Sort ---");
    lista.ordenar_por_quick_sort();
    console.log("Lista ordenada:");
    lista.imprimir_frente(); // Deve ser: [10, 20, 50, 80, 90]

    console.log("\n--- Teste de Bubble Sort ---");
    lista.adicionar(5);
    lista.adicionar_inicio(100); // [100, 10, 20, 50, 80, 90, 5]
    console.log("Lista desordenada:");
    lista.imprimir_frente();
    lista.ordenar_por_bubble_sort();
    console.log("Lista ordenada:");
    lista.imprimir_frente(); // Deve ser: [5, 10, 20, 50, 80, 90, 100]

    console.log("\n--- Teste de Insertion Sort ---");
    lista.adicionar(1);
    lista.adicionar(75); // [5, 10, 20, 50, 80, 90, 100, 1, 75]
    console.log("Lista desordenada:");
    lista.imprimir_frente();
    lista.ordenar_por_insertion_sort();
    console.log("Lista ordenada:");
    lista.imprimir_frente(); // Deve ser: [1, 5, 10, 20, 50, 75, 80, 90, 100]

    console.log("\n--- Teste de Merge Sort ---");
    lista.adicionar(45);
    lista.adicionar_inicio(99);
    lista.remover(1); // Remove 1
    lista.remover_na_posicao(5); // Remove 75
    console.log("Lista desordenada (com remoções):");
    lista.imprimir_frente(); // [99, 5, 10, 20, 50, 80, 90, 100, 45]
    lista.ordenar_por_merge_sort();
    console.log("Lista ordenada:");
    lista.imprimir_frente(); // Deve ser: [5, 10, 20, 45, 50, 80, 90, 99, 100]

    console.log("\n--- Teste de Selection Sort ---");
    lista.adicionar_inicio(1000); // Adiciona um elemento grande
    console.log("Lista desordenada:");
    lista.imprimir_frente();
    lista.ordenar_por_selection_sort();
    console.log("Lista ordenada:");
    lista.imprimir_frente(); // Deve ser: [5, 10, 20, 45, 50, 80, 90, 99, 100, 1000]

    console.log("\n--- Impressão da Lista Final (de Trás para Frente) ---");
    lista.imprimir_tras();
}


// =====================================================================
// 7. EXECUÇÃO PRINCIPAL
// =====================================================================
function main() {
    testarFila();
    console.log("\n\n");
    testarPilha();
    console.log("\n\n");
    testarListaEncadeada();
    console.log("\n\n");
    testarArvoreBinaria();
    console.log("\n\n");
    testarListaDuplamenteEncadeada();
}

// Inicia todos os testes
main();

// Testes

const arvore = new ArvoreBinaria<number>();

const dados = [15, 3, 17, 10, 84, 19, 6, 22, 9];

console.log("HeapSort Máximo:", arvore.heapSortMax([...dados]));
console.log("HeapSort Mínimo:", arvore.heapSortMin([...dados]));