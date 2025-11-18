type Comparable = number | string;


/**
 * Representa um nó (elemento) individual na Lista Encadeada, suportando tipagem genérica <T>.
 */
class No<T> {
    public valor: T;
    // 'No<T> | null' substitui 'Optional['No[T]']' do Python
    public proximo: No<T> | null; 

    constructor(valor: T) {
        this.valor = valor;
        this.proximo = null;
    }
}


/**
 * Implementa a estrutura de dados de Lista Encadeada Simples, com tipagem genérica <T>.
 */
export class ListaEncadeada<T extends Comparable> { // T é restrito a Comparable para fins de ordenação
    private cabeca: No<T> | null;
    private tamanho: number;

    constructor() {
        this.cabeca = null;
        this.tamanho = 0;
    }

  

    /** 1. Adiciona um novo nó com o valor especificado ao final da lista. */
    public adicionar(valor: T): void {
        const novoNo = new No(valor);

        if (this.cabeca === null) {
            this.cabeca = novoNo;
        } else {
            let atual: No<T> = this.cabeca;
            while (atual.proximo !== null) {
                atual = atual.proximo;
            }
            atual.proximo = novoNo;
        }
        
        this.tamanho++;
        console.log(`[OP] Valor '${valor}' adicionado ao final.`);
    }

    /** 2. Adiciona um novo nó com o valor especificado no início da lista. */
    public adicionar_inicio(valor: T): void {
        const novoNo = new No(valor);
  
        novoNo.proximo = this.cabeca;
    
        this.cabeca = novoNo;
        
        this.tamanho++;
        console.log(`[OP] Valor '${valor}' adicionado no início.`);
    }

    /** 3. Remove a primeira ocorrência do nó que contém o valor especificado. */
    public remover(valor: T): void {
        if (this.cabeca === null) {
            console.log(`[ERRO] A lista está vazia. Não foi possível remover '${valor}'.`);
            return;
        }

        if (this.cabeca.valor === valor) {
            this.cabeca = this.cabeca.proximo;
            this.tamanho--;
            console.log(`[OP] Valor '${valor}' removido do início (era a cabeça).`);
            return;
        }

        let anterior: No<T> | null = this.cabeca;
        
       
        while (anterior.proximo !== null && anterior.proximo.valor !== valor) {
            anterior = anterior.proximo;
        }

       
        if (anterior.proximo !== null) {
            const noRemovidoValor: T = anterior.proximo.valor;
        
            anterior.proximo = anterior.proximo.proximo;
            this.tamanho--;
            console.log(`[OP] Valor '${noRemovidoValor}' removido da lista.`);
        } else {
            console.log(`[AVISO] Valor '${valor}' não encontrado na lista.`);
        }
    }

    /** 4. Exibe todos os valores da lista encadeada em ordem. */
    public imprimir(): void {
        if (this.cabeca === null) {
            console.log(">>> Lista Encadeada: [Vazia]");
            return;
        }
            
        let atual: No<T> | null = this.cabeca;
        const valores: T[] = [];
        while (atual !== null) {
            valores.push(atual.valor);
            atual = atual.proximo;
        }
        
        
        console.log(`>>> Lista Encadeada (Tamanho: ${this.tamanho}): ${valores.map(String).join(' -> ')}`);
    }

    /** 5. Insere um novo nó com o valor especificado em uma posição (índice) específica. */
    public inserir_na_posicao(valor: T, indice: number): void {
    
        if (indice < 0 || indice > this.tamanho) {
            console.log(`[AVISO] Índice ${indice} fora dos limites válidos (0 a ${this.tamanho}). Inserção cancelada.`);
            return;
        }

      
        if (indice === 0) {
            this.adicionar_inicio(valor);
          
            return;
        }

    
        const novoNo = new No(valor);
        let atual: No<T> | null = this.cabeca;
        let contador: number = 0;

      
        while (atual !== null && contador < indice - 1) {
            atual = atual.proximo;
            contador++;
        }
            
        if (atual !== null) {
          
            novoNo.proximo = atual.proximo;
            
            atual.proximo = novoNo;
        
            this.tamanho++;
            console.log(`[OP] Valor '${valor}' inserido na posição ${indice}.`);
        }
    }

    /** 6. Remove o nó na posição (índice) específica da lista. */
    public remover_na_posicao(indice: number): void {
       
        if (indice < 0 || indice >= this.tamanho) {
            console.log(`[AVISO] Índice ${indice} fora dos limites válidos (0 a ${this.tamanho - 1}). Remoção cancelada.`);
            return;
        }
        
        let noRemovidoValor: T | null = null;

        if (indice === 0) {
            if (this.cabeca !== null) {
                noRemovidoValor = this.cabeca.valor;
                this.cabeca = this.cabeca.proximo;
            }
        
     
        } else {
            let anterior: No<T> | null = this.cabeca;
            let contador: number = 0;

            while (anterior !== null && contador < indice - 1) {
                anterior = anterior.proximo;
                contador++;
            }
            
            if (anterior !== null && anterior.proximo !== null) {
             
                noRemovidoValor = anterior.proximo.valor;
                
        
                anterior.proximo = anterior.proximo.proximo;
            }
        }
        
        this.tamanho--;
        console.log(`[OP] Valor '${noRemovidoValor}' removido da posição ${indice}.`);
    }

    /** 7. Ordena a lista atual usando Insertion Sort, criando uma nova lista completamente ordenada. */
    public ordenar_por_insercao_novo_lista(): ListaEncadeada<T> {
        if (this.cabeca === null) {
            console.log(">>> A lista está vazia, nada para ordenar.");
            return new ListaEncadeada<T>();
        }

        console.log("--- INICIANDO ORDENAÇÃO POR INSERÇÃO (Nova Lista) ---");
        
        const novaListaOrdenada: ListaEncadeada<T> = new ListaEncadeada<T>();
        let atualOriginal: No<T> | null = this.cabeca;

        while (atualOriginal !== null) {
            const valorAInserir: T = atualOriginal.valor;
            
            let atualOrdenado: No<T> | null = novaListaOrdenada.cabeca;
            let indiceInsercao: number = 0;
            
          
            while (atualOrdenado !== null && valorAInserir > atualOrdenado.valor) {
                atualOrdenado = atualOrdenado.proximo;
                indiceInsercao++;
            }
            
          
            novaListaOrdenada.inserir_na_posicao(valorAInserir, indiceInsercao);
            
            atualOriginal = atualOriginal.proximo;
        }
            
        console.log("--- ORDENAÇÃO CONCLUÍDA ---");
        return novaListaOrdenada;
    }

    /** 8. Ordena a lista encadeada diretamente (in-place) usando Insertion Sort (manipulando ponteiros). */
    public ordenar_por_insercao_in_place(): void {
        if (this.tamanho < 2 || this.cabeca === null) {
            console.log(">>> Lista já está ordenada (0 ou 1 elemento).");
            return;
        }
        
        console.log("--- INICIANDO ORDENAÇÃO POR INSERÇÃO (IN-PLACE) ---");
        
        let fimOrdenado: No<T> | null = this.cabeca;
        
        while (fimOrdenado !== null && fimOrdenado.proximo !== null) {
            
            let noAInserir: No<T> = fimOrdenado.proximo;
            
           
            if (noAInserir.valor >= fimOrdenado.valor) {
                fimOrdenado = fimOrdenado.proximo;
                continue; 
            }
            
            
            fimOrdenado.proximo = noAInserir.proximo;
            
        
            if (this.cabeca !== null && noAInserir.valor < this.cabeca.valor) {
                noAInserir.proximo = this.cabeca;
                this.cabeca = noAInserir;
            
         
            } else {
                let anteriorInsercao: No<T> | null = this.cabeca;
                
             
                while (anteriorInsercao !== null && anteriorInsercao.proximo !== null &&
                       noAInserir !== null && anteriorInsercao.proximo.valor < noAInserir.valor) {
                    anteriorInsercao = anteriorInsercao.proximo;
                }
            
                if (anteriorInsercao !== null && noAInserir !== null) {
                    noAInserir.proximo = anteriorInsercao.proximo;
                    anteriorInsercao.proximo = noAInserir;
                }
            }
        }
        console.log("--- ORDENAÇÃO IN-PLACE CONCLUÍDA ---");
    }

    /** 9. Ordena a lista encadeada diretamente (in-place) usando Bubble Sort. */
    public ordenar_por_bubble_sort_in_place(): void {
        if (this.tamanho < 2) {
            console.log(">>> Lista já está ordenada (0 ou 1 elemento).");
            return;
        }

        console.log("--- INICIANDO ORDENAÇÃO POR BUBBLE SORT (IN-PLACE) ---");
        
        let troca: boolean = true;
        
        while (troca) {
            troca = false;
            let anterior: No<T> | null = null;
            let atual: No<T> | null = this.cabeca;
            
            while (atual !== null && atual.proximo !== null) {
                let noProximo: No<T> = atual.proximo;
                
                if (atual.valor > noProximo.valor) {
                    troca = true;
                   
                    atual.proximo = noProximo.proximo;
                    noProximo.proximo = atual;
                    
                   
                    if (anterior === null) {
                        this.cabeca = noProximo;
                    } else {
                        anterior.proximo = noProximo;
                    }
                        
                    anterior = noProximo; 
                    
                } else {
                    
                    anterior = atual;
                    atual = atual.proximo;
                }
            }
        }
        
        console.log("--- BUBBLE SORT IN-PLACE CONCLUÍDO ---");
    }

    /** 10. Ordena a lista encadeada diretamente (in-place) usando Selection Sort (trocando apenas valores). */
    public ordenar_por_selection_sort_in_place(): void {
        if (this.tamanho < 2) {
            console.log(">>> Lista já está ordenada (0 ou 1 elemento).");
            return;
        }

        console.log("--- INICIANDO ORDENAÇÃO POR SELECTION SORT (IN-PLACE por valor) ---");
        
        let atualI: No<T> | null = this.cabeca;
        
        while (atualI !== null && atualI.proximo !== null) {
            let noMinimo: No<T> = atualI;
            let atualJ: No<T> | null = atualI.proximo;
            
            while (atualJ !== null) {
                if (atualJ.valor < noMinimo.valor) { 
                    noMinimo = atualJ;
                }
                atualJ = atualJ.proximo;
            }
            
            if (noMinimo !== atualI) {
                
                [atualI.valor, noMinimo.valor] = [noMinimo.valor, atualI.valor];
            }
                
            atualI = atualI.proximo;
        }

        console.log("--- SELECTION SORT IN-PLACE CONCLUÍDO ---");
    }


    /** Auxiliar para particionar o array em torno do pivô (para Quick Sort). */
    private _particionar(arr: T[], inicio: number, fim: number): number {
        const pivo: T = arr[fim];
        let i: number = inicio - 1;
        
        for (let j = inicio; j < fim; j++) {
            if (arr[j] <= pivo) {
                i++;
              
                [arr[i], arr[j]] = [arr[j], arr[i]]; 
            }
        }
            
     
        [arr[i + 1], arr[fim]] = [arr[fim], arr[i + 1]];
        return i + 1;
    }

    /** Função recursiva que implementa o algoritmo Quick Sort no array auxiliar. */
    private _quick_sort_recursivo(arr: T[], inicio: number, fim: number): void {
        if (inicio < fim) {
            const pi = this._particionar(arr, inicio, fim);
            this._quick_sort_recursivo(arr, inicio, pi - 1);
            this._quick_sort_recursivo(arr, pi + 1, fim);
        }
    }

    /** 11. Ordena a lista encadeada usando o Quick Sort (através de um Array Auxiliar). */
    public ordenar_por_quick_sort_auxiliar(): void {
        if (this.tamanho < 2) {
            console.log(">>> Lista já está ordenada (0 ou 1 elemento).");
            return;
        }

        console.log("--- INICIANDO ORDENAÇÃO POR QUICK SORT (COM ARRAY AUXILIAR) ---");

       
        const arrayAuxiliar: T[] = [];
        let atual: No<T> | null = this.cabeca;
        while (atual !== null) {
            arrayAuxiliar.push(atual.valor);
            atual = atual.proximo;
        }
     
        this._quick_sort_recursivo(arrayAuxiliar, 0, arrayAuxiliar.length - 1);
       
        atual = this.cabeca;
        for (const valorOrdenado of arrayAuxiliar) {
            if (atual !== null) {
                atual.valor = valorOrdenado;
                atual = atual.proximo;
            }
        }
        
        console.log("--- QUICK SORT CONCLUÍDO ---");
    }

   
    /** Auxiliar que transforma a sub-árvore com raiz no índice 'i' num Max Heap (Max Heapify). */
    private _heapify(arr: T[], n: number, i: number): void {
        let maior: number = i; 
        const filhoEsq: number = 2 * i + 1;
        const filhoDir: number = 2 * i + 2;

        if (filhoEsq < n && arr[i] < arr[filhoEsq]) {
            maior = filhoEsq;
        }

        if (filhoDir < n && arr[maior] < arr[filhoDir]) {
            maior = filhoDir;
        }

        if (maior !== i) {
        
            [arr[i], arr[maior]] = [arr[maior], arr[i]]; 
            this._heapify(arr, n, maior);
        }
    }
            
    /** Implementa o algoritmo Heap Sort (Ordenação por Heap) in-place no array. */
    private _heap_sort_auxiliar(arr: T[]): void {
        const n = arr.length;

      
        for (let i = Math.floor(n / 2) - 1; i >= 0; i--) {
            this._heapify(arr, n, i);
        }
        
     
        for (let i = n - 1; i > 0; i--) {
           
            [arr[i], arr[0]] = [arr[0], arr[i]];
            this._heapify(arr, i, 0);
        }
    }

    /** 12. Ordena a lista encadeada usando o Heap Sort (através de um Array Auxiliar). */
    public ordenar_por_heap_sort_auxiliar(): void {
        if (this.tamanho < 2) {
            console.log(">>> Lista já está ordenada (0 ou 1 elemento).");
            return;
        }

        console.log("--- INICIANDO ORDENAÇÃO POR HEAP SORT (COM ARRAY AUXILIAR) ---");

     
        const arrayAuxiliar: T[] = [];
        let atual: No<T> | null = this.cabeca;
        while (atual !== null) {
            arrayAuxiliar.push(atual.valor);
            atual = atual.proximo;
        }
        
        this._heap_sort_auxiliar(arrayAuxiliar);
        
        atual = this.cabeca;
        for (const valorOrdenado of arrayAuxiliar) {
            if (atual !== null) {
                atual.valor = valorOrdenado;
                atual = atual.proximo;
            }
        }
        
        console.log("--- HEAP SORT CONCLUÍDO ---");
    }


    /** Encontra o nó anterior ao início da segunda metade (Slow-Fast Pointer). */
    private _encontrar_meio(cabeca: No<T> | null): No<T> | null {
        if (cabeca === null || cabeca.proximo === null) {
            return cabeca; 
        }

        let lento: No<T> | null = cabeca;
        let rapido: No<T> | null = cabeca.proximo.proximo;

        while (rapido !== null && rapido.proximo !== null) {
            if (lento === null) break; 
            lento = lento.proximo;
            rapido = rapido.proximo.proximo;
        }
            
        return lento;
    }

    /** Combina duas sub-listas ordenadas. Retorna a cabeça da nova lista combinada. */
    private _merge(lista1: No<T> | null, lista2: No<T> | null): No<T> | null {
   
        const tempCabeca: No<T> = new No(null as any); 
        let cauda: No<T> = tempCabeca;

        while (lista1 !== null && lista2 !== null) {
            if (lista1.valor <= lista2.valor) {
                cauda.proximo = lista1;
                lista1 = lista1.proximo;
            } else {
                cauda.proximo = lista2;
                lista2 = lista2.proximo;
            }
            cauda = cauda.proximo;
        }

   
        if (lista1 !== null) {
            cauda.proximo = lista1;
        } else if (lista2 !== null) {
            cauda.proximo = lista2;
        }

        return tempCabeca.proximo;
    }

    private _merge_sort_recursivo(cabeca: No<T> | null): No<T> | null {
        if (cabeca === null || cabeca.proximo === null) {
            return cabeca;
        }

        const meioAnterior: No<T> | null = this._encontrar_meio(cabeca);
        
        if (meioAnterior === null || meioAnterior.proximo === null) {
            return cabeca; 
        }
        
      
        const cabecaLista2: No<T> | null = meioAnterior.proximo; 
     
        meioAnterior.proximo = null; 
        const cabecaLista1: No<T> | null = cabeca;
        
        const lista1Ordenada = this._merge_sort_recursivo(cabecaLista1);
        const lista2Ordenada = this._merge_sort_recursivo(cabecaLista2);

        return this._merge(lista1Ordenada, lista2Ordenada);
    }

    /** 13. Ordena a lista encadeada in-place usando o algoritmo Merge Sort (O(N log N)). */
    public ordenar_por_merge_sort(): void {
        if (this.tamanho < 2) {
            console.log(">>> Lista já está ordenada (0 ou 1 elemento).");
            return;
        }

        console.log("--- INICIANDO ORDENAÇÃO POR MERGE SORT (IN-PLACE por ponteiro) ---");
        
        this.cabeca = this._merge_sort_recursivo(this.cabeca);
        
        console.log("--- MERGE SORT CONCLUÍDO ---");
    }


    public obterTamanho(): number {
        return this.tamanho;
    }
}
