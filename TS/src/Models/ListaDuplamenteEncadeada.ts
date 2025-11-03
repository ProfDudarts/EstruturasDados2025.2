class NoDuplo<T> {
    public valor: T;
    public proximo: NoDuplo<T> | null;
    public anterior: NoDuplo<T> | null;

    constructor(valor: T) {
        this.valor = valor;
        this.proximo = null;
        this.anterior = null;
    }
}


export class ListaDuplamenteEncadeada<T> {
 
    public cabeca: NoDuplo<T> | null = null;
    public cauda: NoDuplo<T> | null = null;  
    public tamanho: number = 0; 
    constructor() {}

    public ordenar_por_selection_sort(): void {
      
        if (this.tamanho <= 1) {
            console.log("AVISO: Lista muito pequena para ordenar.");
            return;
        }

        let atual: NoDuplo<T> | null = this.cabeca;

        while (atual) {
            let min_no: NoDuplo<T> = atual;

            let proximo_no: NoDuplo<T> | null = atual.proximo;
            while (proximo_no) {
                if (proximo_no.valor < min_no.valor) {
                    min_no = proximo_no;
                }
                proximo_no = proximo_no.proximo;
            }

            if (min_no !== atual) {
                [atual.valor, min_no.valor] = [min_no.valor, atual.valor];
            }

            atual = atual.proximo;
        }

        console.log("SUCESSO: Lista ordenada por Selection Sort.");
    }

    public ordenar_por_bubble_sort(): void {

        if (this.tamanho <= 1) {
            console.log("AVISO: Lista muito pequena para ordenar.");
            return;
        }

        const n: number = this.tamanho;

        for (let i = 0; i < n - 1; i++) {
            let troca_feita: boolean = false; 
            let atual: NoDuplo<T> | null = this.cabeca;
            const limite_pass: number = n - 1 - i;
            let j: number = 0;

            while (atual && atual.proximo && j < limite_pass) {
                const proximo: NoDuplo<T> = atual.proximo;

                if (atual.valor > proximo.valor) {
                    [atual.valor, proximo.valor] = [proximo.valor, atual.valor];
                    troca_feita = true;
                }

                atual = atual.proximo;
                j++;
            }

            if (!troca_feita) {
                break;
            }
        }

        console.log("SUCESSO: Lista ordenada por Bubble Sort.");
    }

    public ordenar_por_quick_sort(): void {

        if (this.tamanho <= 1) {
            console.log("AVISO: Lista muito pequena para ordenar.");
            return;
        }

        this._quick_sort_recursive(this.cabeca, this.cauda);

        console.log("SUCESSO: Lista ordenada por Quick Sort.");
    }

    private _partition(low: NoDuplo<T> | null, high: NoDuplo<T> | null): NoDuplo<T> | null {

        if (high === null || low === null) {
            return null;
        }

        const pivot_valor: T = high.valor;

        let i: NoDuplo<T> | null = low.anterior;

        let j: NoDuplo<T> | null = low; 

        while (j && j !== high) { 
            if (j.valor <= pivot_valor) {
                if (i === null) {
                    i = low;
                } else {
                    i = i.proximo;
                }

                if (i) {
                    [i.valor, j.valor] = [j.valor, i.valor];
                }
            }

            j = j.proximo;
        }

        if (i === null) {
            i = low;
        } else {
            i = i.proximo;
        }

        if (i) {

            [i.valor, high.valor] = [high.valor, i.valor];
        }


        return i;
    }

    private _quick_sort_recursive(cabeca: NoDuplo<T> | null, cauda: NoDuplo<T> | null): void {

        if (cabeca === null || cauda === null || cabeca === cauda || cabeca.anterior === cauda) {
            return;
        }


        const pivot_node: NoDuplo<T> | null = this._partition(cabeca, cauda);

        if (pivot_node === null) {
            return;
        }

        if (pivot_node.anterior && pivot_node.anterior !== cabeca.anterior) {
            this._quick_sort_recursive(cabeca, pivot_node.anterior);
        }

        if (pivot_node.proximo && pivot_node.proximo !== cauda.proximo) {
            this._quick_sort_recursive(pivot_node.proximo, cauda);
        }
    }

    public ordenar_por_insertion_sort(): void {

        if (this.tamanho <= 1) {
            console.log("AVISO: Lista muito pequena para ordenar.");
            return;
        }

        let atual: NoDuplo<T> | null = this.cabeca ? this.cabeca.proximo : null;

        while (atual) {

            let compara: NoDuplo<T> | null = atual;

            while (compara.anterior && compara.valor < compara.anterior.valor) {
                [compara.valor, compara.anterior.valor] = [compara.anterior.valor, compara.valor];

                compara = compara.anterior;
            }

            atual = atual.proximo;
        }

        console.log("SUCESSO: Lista ordenada por Insertion Sort.");
    }

    private _split_list(cabeca: NoDuplo<T> | null): [NoDuplo<T> | null, NoDuplo<T> | null] {

        if (cabeca === null || cabeca.proximo === null) {
            return [cabeca, null];
        }

        let lento: NoDuplo<T> = cabeca;
        let rapido: NoDuplo<T> | null = cabeca.proximo;

        while (rapido && rapido.proximo) {
            lento = lento.proximo as NoDuplo<T>; 
            rapido = rapido.proximo.proximo;
        }

        const segunda_cabeca: NoDuplo<T> | null = lento.proximo;

        lento.proximo = null;

        if (segunda_cabeca) {
            segunda_cabeca.anterior = null;
        }

        return [cabeca, segunda_cabeca];
    }

    private _merge_sorted_lists(left: NoDuplo<T> | null, right: NoDuplo<T> | null): NoDuplo<T> | null {

        const dummy: NoDuplo<T | undefined> = new NoDuplo(undefined);
        let cauda_mesclada: NoDuplo<T | undefined> | NoDuplo<T> = dummy;

        while (left && right) {
            let proximo_no: NoDuplo<T>;

            if (left.valor <= right.valor) {
                proximo_no = left;
                left = left.proximo;
            } else {
                proximo_no = right;
                right = right.proximo;
            }

            cauda_mesclada.proximo = proximo_no;

            proximo_no.anterior = cauda_mesclada as NoDuplo<T>;

            cauda_mesclada = proximo_no;
        }

        const restante: NoDuplo<T> | null = left ? left : right;
        if (restante) {
            cauda_mesclada.proximo = restante;
            restante.anterior = cauda_mesclada as NoDuplo<T>;
        }

        const cabeca_mesclada: NoDuplo<T> | null = dummy.proximo as NoDuplo<T> | null;
        if (cabeca_mesclada) {
            cabeca_mesclada.anterior = null; 
        }

        return cabeca_mesclada;
    }

    private _merge_sort_recursive(cabeca: NoDuplo<T> | null): NoDuplo<T> | null {

        if (cabeca === null || cabeca.proximo === null) {
            return cabeca;
        }

        const [left, right] = this._split_list(cabeca);

        const left_sorted: NoDuplo<T> | null = this._merge_sort_recursive(left);
        const right_sorted: NoDuplo<T> | null = this._merge_sort_recursive(right);

        return this._merge_sorted_lists(left_sorted, right_sorted);
    }

    public ordenar_por_merge_sort(): void {

        if (this.tamanho <= 1) {
            console.log("AVISO: Lista muito pequena para ordenar.");
            return;
        }

        const nova_cabeca: NoDuplo<T> | null = this._merge_sort_recursive(this.cabeca);

        this.cabeca = nova_cabeca;

        let atual: NoDuplo<T> | null = this.cabeca;
        while (atual && atual.proximo) {
            atual = atual.proximo;
        }
        this.cauda = atual;

        console.log("SUCESSO: Lista ordenada por Merge Sort.");
    }

    public adicionar(valor: T): void {

        const novo_no = new NoDuplo<T>(valor);

        if (!this.cabeca) {
            // Lista vazia
            this.cabeca = novo_no;
            this.cauda = novo_no;
        } else {
            novo_no.anterior = this.cauda;
            if (this.cauda) {
                this.cauda.proximo = novo_no;
            }
            this.cauda = novo_no;
        }

        this.tamanho++;
        console.log(`SUCESSO: Valor '${valor}' adicionado ao final.`);
    }


    public adicionar_inicio(valor: T): void {

        const novo_no = new NoDuplo<T>(valor);

        if (!this.cabeca) {
            // Lista vazia
            this.cabeca = novo_no;
            this.cauda = novo_no;
        } else {
            novo_no.proximo = this.cabeca;
            if (this.cabeca) {
                this.cabeca.anterior = novo_no;
            }
            this.cabeca = novo_no;
        }

        this.tamanho++;
        console.log(`SUCESSO: Valor '${valor}' adicionado ao início.`);
    }


    public inserir_na_posicao(valor: T, posicao: number): void {

        if (posicao < 0 || posicao > this.tamanho) {
            console.log(`ERRO: Posição ${posicao} é inválida. Deve estar entre 0 e ${this.tamanho}.`);
            return;
        }

        if (posicao === 0) {
            this.adicionar_inicio(valor);
            return;
        }

        if (posicao === this.tamanho) {
            this.adicionar(valor);
            return;
        }

        let atual: NoDuplo<T> | null = this.cabeca;
        for (let i = 0; i < posicao; i++) {
            if (atual) {
                atual = atual.proximo;
            }
        }

        if (atual === null || atual.anterior === null) {

            console.log("ERRO INTERNO: Falha ao encontrar ponteiros adjacentes para inserção.");
            return;
        }

        const no_depois: NoDuplo<T> = atual;
        const no_antes: NoDuplo<T> = atual.anterior;

        const novo_no = new NoDuplo<T>(valor);

        novo_no.proximo = no_depois;
        novo_no.anterior = no_antes;

        no_antes.proximo = novo_no;
        no_depois.anterior = novo_no;

        this.tamanho++;
        console.log(`SUCESSO: Valor '${valor}' inserido na posição ${posicao}.`);
    }


    public remover_na_posicao(posicao: number): void {

        if (!this.cabeca) {
            console.log("ERRO: A lista está vazia. Não é possível remover.");
            return;
        }

        if (posicao < 0 || posicao >= this.tamanho) {
            console.log(`ERRO: Posição ${posicao} é inválida. Deve estar entre 0 e ${this.tamanho - 1}.`);
            return;
        }

        let atual: NoDuplo<T> | null = this.cabeca;

        for (let i = 0; i < posicao; i++) {
            if (atual) {
                atual = atual.proximo;
            }
        }

        if (atual === null) {
            console.log("ERRO INTERNO: Nó na posição especificada não foi encontrado.");
            return;
        }

        const valor_removido: T | null = atual.valor;

        if (atual.anterior === null) {
            this.cabeca = atual.proximo;
            if (this.cabeca) {
                this.cabeca.anterior = null;
            } else {
                this.cauda = null;
            }

        } else if (atual.proximo === null) {
            this.cauda = atual.anterior;
            if (this.cauda) {
                this.cauda.proximo = null;
            }

        } else {
            atual.anterior.proximo = atual.proximo;
            atual.proximo.anterior = atual.anterior;
        }

        this.tamanho--;
        console.log(`SUCESSO: Valor '${valor_removido}' removido da posição ${posicao}.`);
    }

    public remover(valor: T): void {

        if (!this.cabeca) {
            console.log("ERRO: A lista está vazia. Não é possível remover.");
            return;
        }

        let atual: NoDuplo<T> | null = this.cabeca;

        while (atual && atual.valor !== valor) {
            atual = atual.proximo;
        }

        if (!atual) {
            console.log(`AVISO: Valor '${valor}' não encontrado na lista.`);
            return; 
        }

        if (atual.anterior) {
            atual.anterior.proximo = atual.proximo;
        } else {
            this.cabeca = atual.proximo;
        }

        if (atual.proximo) {
            atual.proximo.anterior = atual.anterior;
        } else {
            this.cauda = atual.anterior;
        }

        this.tamanho--;
        console.log(`SUCESSO: Valor '${valor}' removido.`);
    }

    public imprimir_frente(): void {

        let atual: NoDuplo<T> | null = this.cabeca;
        const valores: T[] = []; 
        while (atual) {
            valores.push(atual.valor);
            atual = atual.proximo;
        }

        if (valores.length > 0) {
            console.log("Lista Duplamente Encadeada (->):", valores.join(" <-> "), `(Tamanho: ${this.tamanho})`);
        } else {
            console.log("Lista Duplamente Encadeada (->): [Vazia]");
        }
    }


    public imprimir_tras(): void {

        let atual: NoDuplo<T> | null = this.cauda;
        const valores: T[] = [];
        while (atual) {
            valores.push(atual.valor);
            atual = atual.anterior;
        }

        if (valores.length > 0) {

            console.log("Lista Duplamente Encadeada (<-):", valores.join(" <-> "), `(Tamanho: ${this.tamanho})`);
        } else {
            console.log("Lista Duplamente Encadeada (<-): [Vazia]");
        }
    }
}

