from src.No.no import No
from src.Pilha.pilha import Pilha
from src.Fila.fila import Fila

fila = Fila()
fila.inserir(No("Dudarts"))
fila.inserir(No("Lucas Eduardo"))
fila.inserir(No("Pedro Eduardo"))

fila.listar()
removido = fila.remover()
print()
removido.imprimir()
print()
fila.listar()
print()

fila.inserir(No("Dalila Eduardo"))
fila.listar()
