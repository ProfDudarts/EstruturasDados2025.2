import { No } from './no.js';

export class Fila<T> {
  private primeiro: No<T> | null;
  private ultimo: No<T> | null;
  private tamanho: number;

  constructor() {
    this.primeiro = null;
    this.ultimo = null;
    this.tamanho = 0;
  }

  tamanho_fila(): number {
    return this.tamanho;
  }

  vazio(): boolean {
    return this.tamanho === 0;
  }

  inserir(elemento: T): void {
    const novoNo = new No(elemento);

    if (this.ultimo === null) {
      this.primeiro = novoNo;
      this.ultimo = novoNo;
    } else {
      this.ultimo.proximo = novoNo;
      this.ultimo = novoNo;
    }
    this.tamanho++;
  }

  apagar(): T | string {
    if (this.vazio()) {
      return 'Fila vazia';
    }

    const noRemovido = this.primeiro;
    if (noRemovido) {
      this.primeiro = noRemovido.proximo;
      this.tamanho--;

      if (this.primeiro === null) {
        this.ultimo = null;
      }
      return noRemovido.dado;
    }
    return 'Erro inesperado na fila';
  }

  frente(): T | string {
    if (this.vazio()) {
      return 'Fila vazia';
    }
    return this.primeiro!.dado;
  }

  listar(): string {
    if (this.vazio()) {
      return 'Fila vazia';
    }

    const elementos: string[] = [];
    let p = this.primeiro;
    while (p) {
      elementos.push(String(p.dado));
      p = p.proximo;
    }
    return elementos.join(' -> ');
  }
}