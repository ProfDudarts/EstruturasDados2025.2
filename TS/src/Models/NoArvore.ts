export class NoArvore<T> {
  public dado: T;
  public esquerda: NoArvore<T> | null;
  public direita: NoArvore<T> | null;

  constructor(dado: T) {
    this.dado = dado;
    this.esquerda = null;
    this.direita = null;
  }
}