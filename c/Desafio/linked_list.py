from typing import Optional, Callable, List
from product import Product


class Node:
    """Nó da lista duplamente encadeada"""
    def __init__(self, value: Product):
        self.value = value
        self.next: Optional[Node] = None
        self.prev: Optional[Node] = None

    def __repr__(self):
        return f"Node({self.value})"


class DoublyLinkedList:
    """Lista duplamente encadeada para gerenciar produtos"""
    def __init__(self):
        self.head: Optional[Node] = None
        self.tail: Optional[Node] = None
        self._size = 0

    def is_empty(self) -> bool:
        """Verifica se a lista está vazia"""
        return self.head is None

    def size(self) -> int:
        """Retorna o tamanho da lista"""
        return self._size

    def insert_at_beginning(self, product: Product) -> Node:
        """Insere um produto no início da lista"""
        new_node = Node(product)
        if self.is_empty():
            self.head = self.tail = new_node
        else:
            new_node.next = self.head
            self.head.prev = new_node
            self.head = new_node
        self._size += 1
        return new_node

    def insert_at_end(self, product: Product) -> Node:
        """Insere um produto no final da lista"""
        new_node = Node(product)
        if self.is_empty():
            self.head = self.tail = new_node
        else:
            new_node.prev = self.tail
            self.tail.next = new_node
            self.tail = new_node
        self._size += 1
        return new_node

    def insert_after(self, node: Node, product: Product) -> Optional[Node]:
        """Insere um produto após um nó específico"""
        if not node:
            return None
        
        new_node = Node(product)
        new_node.next = node.next
        new_node.prev = node
        
        if node.next:
            node.next.prev = new_node
        else:
            self.tail = new_node
        
        node.next = new_node
        self._size += 1
        return new_node

    def remove_node(self, node: Node) -> bool:
        """Remove um nó específico da lista"""
        if not node:
            return False

        if node.prev:
            node.prev.next = node.next
        else:
            self.head = node.next

        if node.next:
            node.next.prev = node.prev
        else:
            self.tail = node.prev

        self._size -= 1
        return True

    def remove_by_predicate(self, predicate: Callable[[Product], bool]) -> bool:
        """Remove o primeiro produto que atende ao predicado"""
        node = self.find(predicate)
        if node:
            return self.remove_node(node)
        return False

    def find(self, predicate: Callable[[Product], bool]) -> Optional[Node]:
        """Encontra o primeiro nó que atende ao predicado"""
        current = self.head
        while current:
            if predicate(current.value):
                return current
            current = current.next
        return None

    def iter_nodes(self):
        """Itera sobre todos os nós da lista"""
        current = self.head
        while current:
            yield current
            current = current.next

    def to_list(self) -> List[Product]:
        """Converte a lista encadeada para uma lista Python"""
        return [node.value for node in self.iter_nodes()]

    def clear(self):
        """Remove todos os elementos da lista"""
        self.head = None
        self.tail = None
        self._size = 0

    def __len__(self):
        return self._size

    def __repr__(self):
        products = self.to_list()
        return f"DoublyLinkedList({len(products)} items)"
