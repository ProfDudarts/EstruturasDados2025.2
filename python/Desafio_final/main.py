from lista_produtos import ListaCompras
import os

def limpar():
    os.system('cls' if os.name == 'nt' else 'clear')

def menu():
    print("\n🛒 --- SISTEMA INTELIGENTE DE COMPRAS (HouseShare) ---")
    print("1. Adicionar Item")
    print("2. Listar Compras (Ordenado por Prioridade)")
    print("3. Marcar como Comprado")
    print("4. Alterar Prioridade (Reordenar)")
    print("5. Ver Histórico de um Item")
    print("6. Remover Item")
    print("7. Buscar Item")
    print("0. Sair")
    return input("👉 Opção: ")

def main():
    lista = ListaCompras()


    lista.adicionar("Arroz", "5kg", "Alimentos", "Alta")
    lista.adicionar("Detergente", "2un", "Limpeza", "Baixa")
    lista.adicionar("Shampoo", "1un", "Higiene", "Média")
    limpar()

    while True:
        opcao = menu()
        print()
        
        if opcao == '1':
            nome = input("Nome do produto: ")
            qtd = input("Quantidade: ")
            cat = input("Categoria (Alimentos/Limpeza/Higiene/Outros): ")
            print("Prioridade: [1] Baixa | [2] Média | [3] Alta")
            prio_op = input("Escolha: ")
            prio_dict = {'1': 'Baixa', '2': 'Média', '3': 'Alta'}
            prioridade = prio_dict.get(prio_op, 'Baixa')
            
            lista.adicionar(nome, qtd, cat, prioridade)

        elif opcao == '2':
            limpar()
            lista.listar()
            input("Pressione ENTER para voltar...")

        elif opcao == '3':
            lista.listar()
            nome = input("Qual item foi comprado? (Digite o nome): ")
            lista.marcar_comprado(nome)

        elif opcao == '4':
            lista.listar()
            nome = input("Qual item quer alterar a prioridade? ")
            print("Nova Prioridade: [1] Baixa | [2] Média | [3] Alta")
            prio_op = input("Escolha: ")
            prio_dict = {'1': 'Baixa', '2': 'Média', '3': 'Alta'}
            if prio_op in prio_dict:
                lista.alterar_prioridade(nome, prio_dict[prio_op])
            else:
                print("Opção inválida.")

        elif opcao == '5':
            termo = input("Nome do item para ver histórico: ")
            itens = lista.buscar(termo)
            if itens:
                for item in itens:
                    print(f"\n📜 Histórico de '{item.nome}':")
                    print(item.historico.exibir_historico())
            else:
                print("Item não encontrado.")
            input("\nENTER para continuar...")

        elif opcao == '6':
            nome = input("Nome do item para remover: ")
            lista.remover(nome)

        elif opcao == '7':
            termo = input("Palavra-chave: ")
            itens = lista.buscar(termo)
            print(f"\n🔍 Encontrados {len(itens)} itens:")
            for item in itens:
                print(f"- {item.nome} ({item.prioridade}) - {item.status}")
            input("\nENTER para continuar...")

        elif opcao == '0':
            print("Saindo... Boas compras! 🛍️")
            break
        else:
            print("Opção inválida!")

if __name__ == "__main__":
    main()