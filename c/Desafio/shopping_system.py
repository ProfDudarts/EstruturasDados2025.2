from typing import Optional
from linked_list import DoublyLinkedList, Node
from product import Product
from history_list import HistoryLinkedList

PRIORITY_ORDER = {'alta': 0, 'media': 1, 'baixa': 2}

class ShoppingSystem:
    def __init__(self):
        self.list = DoublyLinkedList()
        self.history = HistoryLinkedList()

    def add_item_beginning(self, product: Product):
        node = self.list.insert_at_beginning(product)
        self._reorder_by_priority(node)
        self.history.add_entry(product.name, 'adicionado', f'Produto adicionado com prioridade {product.priority}')
        return node

    def add_item_end(self, product: Product):
        node = self.list.insert_at_end(product)
        self._reorder_by_priority(node)
        self.history.add_entry(product.name, 'adicionado', f'Produto adicionado com prioridade {product.priority}')
        return node

    def add_item_after(self, existing_name: str, product: Product) -> Optional[Node]:
        node = self.list.find(lambda p: p.name == existing_name)
        if not node:
            return None
        return self.list.insert_after(node, product)

    def remove_item_by_name(self, name: str) -> bool:
        result = self.list.remove_by_predicate(lambda p: p.name == name)
        if result:
            self.history.add_entry(name, 'removido', 'Produto removido da lista')
        return result

    def find_items(self, keyword: str):
        return [n.value for n in self.list.iter_nodes() if keyword.lower() in n.value.name.lower()]

    def mark_as_bought(self, name: str) -> bool:
        node = self.list.find(lambda p: p.name == name)
        if not node:
            return False
        node.value.set_status(True)
        self.history.add_entry(name, 'comprado', 'Produto marcado como comprado')
        return True

    def change_priority(self, name: str, priority: str) -> bool:
        node = self.list.find(lambda p: p.name == name)
        if not node:
            return False
        old_priority = node.value.priority
        node.value.set_priority(priority)
        self._reorder_by_priority(node)
        self.history.add_entry(name, 'alterado', f'Prioridade alterada de {old_priority} para {priority}')
        return True

    def _reorder_by_priority(self, node: Node):
        # Move node forward until it reaches correct position based on priority
        if not node:
            return
        while node.prev and PRIORITY_ORDER[node.value.priority] < PRIORITY_ORDER[node.prev.value.priority]:
            prev = node.prev
            # swap nodes by moving node before prev
            # remove node and insert before prev
            self.list.remove_node(node)
            # insert node before prev (which is now possibly updated)
            node.prev = prev.prev
            node.next = prev
            prev.prev = node
            if node.prev:
                node.prev.next = node
            else:
                self.list.head = node
            if node.next is None:
                self.list.tail = node
            self.list._size += 1

    def list_all(self):
        # return products ordered by priority
        return sorted(self.list.to_list(), key=lambda p: PRIORITY_ORDER[p.priority])

    def show(self):
        for p in self.list_all():
            print(p)
