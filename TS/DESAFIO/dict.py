import json
from TS.DESAFIO.no import ItemNode, HistoricoNode
from Lista_de_compras import ListaDeCompras

class PersistenceManager:

    @staticmethod
    def _history_to_list(historico_head):
        history_list = []
        atual = historico_head
        while atual:
            history_list.append({
                "tipo_alteracao": atual.tipo_alteracao,
                "valor_antigo": atual.valor_antigo,
                "valor_novo": atual.valor_novo,
                "data_registro": atual.data_registro
            })
            atual = atual.next
        return history_list

    @staticmethod
    def list_to_json_dict(lista_compras):

        data_list = []
        atual = lista_compras.head
        
        while atual:
            item_data = {
                "nome": atual.nome,
                "quantidade": atual.quantidade,
                "categoria": atual.categoria,
                "prioridade": atual.prioridade,
                "status": atual.status,
                "data_inclusao": atual.data_inclusao,
                "historico": PersistenceManager._history_to_list(atual.historico_head)
            }
            data_list.append(item_data)
            atual = atual.next
            
        print("Lista convertida para formato JSON Dictionary.")
        return data_list

    @staticmethod
    def load_from_json_dict(data_list):

        nova_lista = ListaDeCompras()
        
        for item_data in data_list:

            novo_item = ItemNode(
                nome=item_data["nome"],
                quantidade=item_data["quantidade"],
                categoria=item_data["categoria"],
                prioridade=item_data["prioridade"],
                status=item_data["status"],
                data_inclusao=item_data["data_inclusao"] 
            )

            nova_lista._adicionar_node(novo_item, 'fim')

            historico = item_data.get("historico", [])

            for hist_entry in reversed(historico):

                 hist_node = HistoricoNode(
                    tipo_alteracao=hist_entry["tipo_alteracao"],
                    valor_antigo=hist_entry["valor_antigo"],
                    valor_novo=hist_entry["valor_novo"],
                    data_registro=hist_entry["data_registro"]
                 )

                 hist_node.next = novo_item.historico_head
                 novo_item.historico_head = hist_node

        print("✅ Lista de Compras carregada e Lista Duplamente Encadeada reconstruída.")
        return nova_lista
    
    @staticmethod
    def save_list_to_file(lista_compras, filename="lista_compras.json"):
        """Salva a lista em um arquivo JSON."""
        data_to_save = PersistenceManager.list_to_json_dict(lista_compras)
        try:
            with open(filename, 'w', encoding='utf-8') as f:
                json.dump(data_to_save, f, indent=4, ensure_ascii=False)
            print(f"💾 Lista salva com sucesso em '{filename}'.")
        except IOError as e:
            print(f"❌ Erro ao salvar o arquivo: {e}")

    @staticmethod
    def load_list_from_file(filename="lista_compras.json"):
        """Carrega a lista de um arquivo JSON."""
        try:
            with open(filename, 'r', encoding='utf-8') as f:
                data_list = json.load(f)
            return PersistenceManager.load_from_json_dict(data_list)
        except FileNotFoundError:
            print(f"⚠️ Arquivo '{filename}' não encontrado. Iniciando nova lista vazia.")
            return ListaDeCompras()
        except json.JSONDecodeError as e:
            print(f"❌ Erro de decodificação JSON: {e}. Iniciando nova lista vazia.")
            return ListaDeCompras()
        except Exception as e:
            print(f"❌ Ocorreu um erro inesperado ao carregar: {e}. Iniciando nova lista vazia.")
            return ListaDeCompras()