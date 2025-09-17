export class No<T> {
  public dado: T;
  public proximo: No<T> | null;

  constructor(dado: T) {
    this.dado = dado;
    this.proximo = null;
  }
}