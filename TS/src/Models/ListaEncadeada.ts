// Implementação de Lista Encadeada (simples)

class No<T> {
  valor: T
  proximo: No<T> | null

  constructor(valor: T) {
    this.valor = valor
    this.proximo = null
  }
}

export class ListaEncadeada<T> {
  private cabeca: No<T> | null = null
  private tamanho: number = 0

  // Inserir no final
  adicionar(valor: T): void {
    const novoNo = new No(valor)
    if (!this.cabeca) {
      this.cabeca = novoNo
    } else {
      let atual = this.cabeca
      while (atual.proximo) {
        atual = atual.proximo
      }
      atual.proximo = novoNo
    }
    this.tamanho++
  }

  // Inserir no início
  adicionarInicio(valor: T): void {
    const novoNo = new No(valor)
    novoNo.proximo = this.cabeca
    this.cabeca = novoNo
    this.tamanho++
  }

  // Remover por valor
  remover(valor: T): void {
    if (!this.cabeca) return

    if (this.cabeca.valor === valor) {
      this.cabeca = this.cabeca.proximo
      this.tamanho--
      return
    }

    let atual = this.cabeca
    while (atual.proximo && atual.proximo.valor !== valor) {
      atual = atual.proximo
    }

    if (atual.proximo) {
      atual.proximo = atual.proximo.proximo
      this.tamanho--
    }
  }

  // Mostrar lista
  imprimir(): void {
    let atual = this.cabeca
    let valores: T[] = []
    while (atual) {
      valores.push(atual.valor)
      atual = atual.proximo
    }
    console.log("Lista Encadeada:", valores.join(" -> "))
  }
}
