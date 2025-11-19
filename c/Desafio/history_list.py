from typing import Optional
from datetime import datetime


class HistoryNode:
    """Nó da lista de histórico"""
    def __init__(self, product_name: str, action: str, details: str, timestamp: datetime):
        self.product_name = product_name
        self.action = action  # 'adicionado', 'removido', 'alterado', 'comprado'
        self.details = details
        self.timestamp = timestamp
        self.next: Optional[HistoryNode] = None
        self.prev: Optional[HistoryNode] = None

    def __repr__(self):
        return f"HistoryNode({self.product_name}, {self.action}, {self.timestamp})"


class HistoryLinkedList:
    """Lista encadeada para armazenar histórico de alterações"""
    def __init__(self):
        self.head: Optional[HistoryNode] = None
        self.tail: Optional[HistoryNode] = None
        self._size = 0

    def add_entry(self, product_name: str, action: str, details: str):
        """Adiciona uma nova entrada no histórico"""
        new_node = HistoryNode(product_name, action, details, datetime.now())
        
        if not self.head:
            self.head = self.tail = new_node
        else:
            new_node.prev = self.tail
            self.tail.next = new_node
            self.tail = new_node
        
        self._size += 1

    def get_all_entries(self):
        """Retorna todas as entradas do histórico"""
        entries = []
        current = self.head
        while current:
            entries.append(current)
            current = current.next
        return entries

    def get_product_history(self, product_name: str):
        """Retorna o histórico de um produto específico"""
        entries = []
        current = self.head
        while current:
            if current.product_name.lower() == product_name.lower():
                entries.append(current)
            current = current.next
        return entries

    def clear(self):
        """Limpa todo o histórico"""
        self.head = None
        self.tail = None
        self._size = 0

    def size(self):
        """Retorna o tamanho do histórico"""
        return self._size

    def __len__(self):
        return self._size
