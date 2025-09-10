
class No():
    def __init__(self, valor):
        self.valor = valor
        self.proximo: No = None

    def __str__(self):
        return str(self.valor)
    
    def imprimir(self):
        print(f"Ola, {self.valor}")

    def temProximo(self):
        return self.proximo != None
    
