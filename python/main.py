from src.No.no import No
from src.Pilha.pilha import Pilha
from src.Fila.fila import Fila
from src.Models.cliente import Cliente

def atender(f: Fila):
    atendido = f.remover()
    print("Atendido: ", end="\t")
    atendido.imprimir()
    print("----------------------")

def posicao(f: Fila, id: int):
    print("----------------------")
    pos = f.obterPosicao(id)
    if pos > 0:
        print(f"Id {id} é o {pos}º da fila.")
    else:
        print(f"Id {id} não encontrado")
    print("----------------------")


c1 = Cliente(123, "Dudarts")
c2 = Cliente(456, "Lucas Eduardo")
c3 = Cliente(789, "Pedro Eduardo")
c4 = Cliente(135, "Dalila Chaves")
c5 = Cliente(246, "Sansão Eduardo")
c6 = Cliente(357, "Eduardo Mendes")

fila = Fila()
fila.inserir(No(c1))
fila.inserir(No(c2))
fila.inserir(No(c3))
fila.listar()
posicao(fila, 789)

fila.listar()
atender(fila)
fila.inserir(No(c4))
atender(fila)
fila.inserir(No(c5))
posicao(fila, 135)
fila.inserir(No(c6))
fila.listar()
