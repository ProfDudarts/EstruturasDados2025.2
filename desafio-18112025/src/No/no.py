class No():
    def __init__(self, valor):
        self.valor = valor
        self.proximo: No = None

    def __str__(self):
        return str(self.valor)
    
    def imprimir(self):
        self.valor.exibir()

    def temProximo(self):
        return self.proximo != None