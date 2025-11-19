from src.listaEncadeada.listaEncadeada import ListaEncadeada
from src.models.produtos import Produto
import datetime

def gerar_menu(placeholder : ListaEncadeada):
    while True:
        print('''
            1 - Adicionar itens a lista
            2 - Remover itens da lista
            3 - Mudar prioridade
            4 - Exibir lista
              ''')
        
        acao_menu = input("Escolha a opção")

        if acao_menu == '1':
            return adicionarItens(placeholder)
        
        elif acao_menu == '2':
            return removerItens(placeholder)
        
        elif acao_menu == '3':
            return mudarPrioridade(placeholder)
        
        elif acao_menu == '4':
            return placeholder.imprimir_lista()
        
def adicionarItens(placeholder : ListaEncadeada):
    produto = Produto(
        nome = input(),
        quantidade = int(input()),
        prioridade = str(input()),
        status = str(input()),
        categoria = str(input()),
        data_inclusao = datetime.datetime.now()
    )
    return placeholder.inserir(produto)

def removerItens(placeholder : ListaEncadeada, produto : Produto):
    return placeholder.remover(produto)

def mudarPrioridade(placeholder : ListaEncadeada, produto : Produto):
    return placeholder.buscar(produto.alterar_prioridade())