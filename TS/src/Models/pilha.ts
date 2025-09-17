import { No } from './no.js';

export class Pilha<T> {
  private topo: No<T> | null;
  private tamanho: number;

  constructor() {
    this.topo = null;
    this.tamanho = 0;
  }

  inserir(elemento: T): void {
    const novoNo = new No(elemento);
    novoNo.proximo = this.topo;
    this.topo = novoNo;
    this.tamanho++;
  }

  apagar(): T | string {
    if (this.empty()) {
      return 'pilha vazia';
    }
    const noApagado = this.topo;
    if (noApagado){
      this.topo = noApagado.proximo;
    this.tamanho--;
    return noApagado.dado;
    }
    return "Erro"
  }

  topo_valor(): T | string {
    if (this.topo === null) {
      return 'pilha vazia';
    }
    return this.topo.dado;
  }

  tamanho_pilha(): number {
    return this.tamanho;
  }

  empty(): boolean {
    return this.tamanho === 0;
  }

  listar(): string {
    if (this.tamanho === 0) {
      return 'Pilha vazia';
    }
    let s = '';
    let p = this.topo;
    while (p) {
      s += `${p.dado}\n`;
      p = p.proximo;
    }
    return s;
  }
}