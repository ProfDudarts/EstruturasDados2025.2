import os
from Lista_de_compras import ListaDeCompras
from dict import PersistenceManager

class InteractiveMenu:

    def __init__(self, filename="lista_compras_compartilhada.json"):
        self.filename = filename
        self.lista = PersistenceManager.load_list_from_file(self.filename)
        self.running = True

    def _show_menu(self):
        """Exibe o menu de opções e solicita a escolha do usuário."""
        print("\n==============================================")
        print("🛒 MENU DA LISTA DE COMPRAS INTERATIVA")
        print("==============================================")
        print("1. Adicionar Novo Item")
        print("2. Remover Item")
        print("3. Marcar Item como Comprado")
        print("4. Alterar Prioridade e Reordenar")
        print("5. Buscar Item por Nome/Categoria")
        print("6. Visualizar Lista Completa")
        print("7. Visualizar Histórico de um Item")
        print("8. Salvar e Sair")
        print("==============================================")
        return input("Escolha uma opção (1-8): ").strip()

    def _handle_choice(self, choice):
        """Processa a opção escolhida pelo usuário."""
        if choice == '1':
            print("\n--- Adicionar Item ---")
            nome = input("Nome do Item: ").strip()
            if not nome: print("Nome não pode ser vazio."); return
            quantidade = input("Quantidade (ex: 3, 1kg, 2L): ").strip()
            categoria = input("Categoria (ex: Alimentos, Limpeza): ").strip()
            # Pede a prioridade, aceitando 'Média' como padrão
            prioridade = input("Prioridade (Alta/Média/Baixa) [Média]: ") or "Média"
            self.lista.adicionar_item(nome, quantidade, categoria, prioridade)
            
        elif choice == '2':
            print("\n--- Remover Item ---")
            nome = input("Nome do Item a ser removido: ").strip()
            if nome:
                self.lista.remover_item(nome)
            
        elif choice == '3':
            print("\n--- Marcar Item como Comprado ---")
            nome = input("Nome do Item a ser marcado como Comprado: ").strip()
            if nome:
                self.lista.marcar_comprado(nome)

        elif choice == '4':
            print("\n--- Alterar Prioridade ---")
            nome = input("Nome do Item para alterar prioridade: ").strip()
            if not nome: return
            nova_prioridade = input("Nova Prioridade (Alta/Média/Baixa): ").strip()
            self.lista.alterar_prioridade_e_reordenar(nome, nova_prioridade)

        elif choice == '5':
            print("\n--- Busca ---")
            palavra_chave = input("Buscar por Nome ou Categoria: ").strip()
            if palavra_chave:
                self.lista.buscar_item(palavra_chave)
            
        elif choice == '6':
            self.lista.exibir_lista()
            
        elif choice == '7':
            print("\n--- Histórico de Item ---")
            nome = input("Nome do Item para visualizar o Histórico: ").strip()
            item = self.lista._encontrar_item(nome)
            if item:
                print(f"\n--- Histórico de Alterações para '{nome}' ---")

                print(item.exibir_historico())
                print("---------------------------------------------")
            else:
                print(f"Item '{nome}' não encontrado.")
            
        elif choice == '8':
            PersistenceManager.save_list_to_file(self.lista, self.filename)
            print("Lista salva. Saindo do programa. Até mais!")
            self.running = False
            
        else:
            print("Opção inválida. Por favor, escolha um número entre 1 e 8.")

    def run(self):
        """Inicia o loop principal do programa."""
        print("--- Sistema de Gerenciamento de Lista de Compras ---")
        while self.running:
            choice = self._show_menu()
            self._handle_choice(choice)
            if self.running and choice in ['1', '2', '3', '4', '5', '6', '7']:
                input("\nPressione ENTER para continuar...")