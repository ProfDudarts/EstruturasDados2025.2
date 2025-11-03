import { Fila } from './Models/fila.js';
import {Pilha} from './Models/pilha.js';
import { ListaEncadeada } from "./Models/ListaEncadeada"
import { ListaDuplamenteEncadeada } from "./Models/ListaDuplamenteEncadeada"


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


function main() {
  console.log("===== Lista Encadeada =====")
  const lista = new ListaEncadeada<number>()
  lista.adicionar(10)
  lista.adicionar(20)
  lista.adicionar_inicio(5)
  lista.imprimir()
  lista.remover(20)
  lista.imprimir()

  console.log("\n===== Lista Duplamente Encadeada =====")
  const listaDupla = new ListaDuplamenteEncadeada<string>()
  listaDupla.adicionar("A")
  listaDupla.adicionar("B")
  listaDupla.adicionarInicio("Início")
  listaDupla.imprimirFrente()
  listaDupla.imprimirTras()
  listaDupla.remover("B")
  listaDupla.imprimirFrente()
}

main()


function exemploUsoListaEncadeada() {
    console.log("=====================================================================");
    console.log("                DEMONSTRAÇÃO DA LISTA ENCADEDADA DE NÚMEROS");
    console.log("=====================================================================");

    // Criando uma lista de números
    const listaNumeros = new ListaEncadeada<number>();

    // Teste de Adição
    listaNumeros.adicionar(10);
    listaNumeros.adicionar(40);
    listaNumeros.adicionar_inicio(5);
    listaNumeros.imprimir(); 

    // Teste de Inserção na Posição
    listaNumeros.inserir_na_posicao(20, 2); // Deve ir após o 10
    listaNumeros.inserir_na_posicao(50, 4); // Deve ir no final
    listaNumeros.imprimir();

    // Teste de Remoção
    listaNumeros.remover(5); // Remove 5 (cabeça)
    listaNumeros.imprimir(); 
    listaNumeros.remover_na_posicao(2); // Remove 40
    listaNumeros.imprimir();
    listaNumeros.remover(99); // Não encontrado

    console.log("\n--- ORDENAÇÃO DEMONSTRAÇÃO ---\n");

    const listaParaOrdenar = new ListaEncadeada<number>();
    listaParaOrdenar.adicionar(50);
    listaParaOrdenar.adicionar(20);
    listaParaOrdenar.adicionar(40);
    listaParaOrdenar.adicionar(10);
    listaParaOrdenar.adicionar(30);
    listaParaOrdenar.adicionar(60);
    listaParaOrdenar.imprimir(); 
    
    // Testando Merge Sort (In-Place)
    console.log("\n>>> Testando Merge Sort (Otimizado para Listas Encadeadas)");
    listaParaOrdenar.ordenar_por_merge_sort();
    listaParaOrdenar.imprimir(); 
    
    // Testando Heap Sort (Auxiliar)
    console.log("\n>>> Reiniciando lista para Heap Sort");
    const listaHeap = new ListaEncadeada<number>();
    listaHeap.adicionar(5); listaHeap.adicionar(2); listaHeap.adicionar(8); listaHeap.adicionar(1);
    listaHeap.imprimir();
    listaHeap.ordenar_por_heap_sort_auxiliar();
    listaHeap.imprimir();


    console.log("\n=====================================================================");
    console.log("               DEMONSTRAÇÃO DA LISTA ENCADEDADA DE STRINGS");
    console.log("=====================================================================");
    
    // Testando lista com tipo String (demonstração de genéricos)
    const listaStrings = new ListaEncadeada<string>();
    listaStrings.adicionar("Banana");
    listaStrings.adicionar_inicio("Maçã");
    listaStrings.adicionar("Pêra");
    listaStrings.adicionar("Abacaxi");
    listaStrings.imprimir();

    // Ordenação com strings (Selection Sort)
    console.log("\n>>> Testando Selection Sort com Strings");
    listaStrings.ordenar_por_selection_sort_in_place();
    listaStrings.imprimir(); 
}

// Inicia a demonstração
exemploUsoListaEncadeada();

