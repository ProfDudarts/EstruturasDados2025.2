// Implementação de Lista Duplamente Encadeada

class NoDuplo<T> {
  valor: T
  proximo: NoDuplo<T> | null
  anterior: NoDuplo<T> | null

  constructor(valor: T) {
    this.valor = valor
    this.proximo = null
    this.anterior = null
  }
}

export class ListaDuplamenteEncadeada<T> {
  private cabeca: NoDuplo<T> | null = null
  private cauda: NoDuplo<T> | null = null
  private tamanho: number = 0

  // Inserir no final
  adicionar(valor: T): void {
    const novoNo = new NoDuplo(valor)

    if (!this.cabeca) {
      this.cabeca = novoNo
      this.cauda = novoNo
    } else {
      novoNo.anterior = this.cauda
      if (this.cauda) this.cauda.proximo = novoNo
      this.cauda = novoNo
    }
    this.tamanho++
  }

  // Inserir no início
  adicionarInicio(valor: T): void {
    const novoNo = new NoDuplo(valor)

    if (!this.cabeca) {
      this.cabeca = novoNo
      this.cauda = novoNo
    } else {
      novoNo.proximo = this.cabeca
      this.cabeca.anterior = novoNo
      this.cabeca = novoNo
    }
    this.tamanho++
  }

  // Remover por valor
  remover(valor: T): void {
    if (!this.cabeca) return

    let atual = this.cabeca

    while (atual && atual.valor !== valor) {
      atual = atual.proximo
    }

    if (!atual) return

    if (atual.anterior) {
      atual.anterior.proximo = atual.proximo
    } else {
      this.cabeca = atual.proximo
    }

    if (atual.proximo) {
      atual.proximo.anterior = atual.anterior
    } else {
      this.cauda = atual.anterior
    }

    this.tamanho--
  }

  // Mostrar lista para frente
  imprimirFrente(): void {
    let atual = this.cabeca
    let valores: T[] = []
    while (atual) {
      valores.push(atual.valor)
      atual = atual.proximo
    }
    console.log("Lista Duplamente Encadeada (->):", valores.join(" <-> "))
  }

  // Mostrar lista para trás
  imprimirTras(): void {
    let atual = this.cauda
    let valores: T[] = []
    while (atual) {
      valores.push(atual.valor)
      atual = atual.anterior
    }
    console.log("Lista Duplamente Encadeada (<-):", valores.join(" <-> "))
  }
}
