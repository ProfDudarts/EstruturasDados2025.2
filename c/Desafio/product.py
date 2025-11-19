from __future__ import annotations
from dataclasses import dataclass, field
from datetime import datetime
from typing import List

@dataclass
class HistoryEntry:
    timestamp: datetime
    field: str
    old: str
    new: str

@dataclass
class Product:
    name: str
    quantity: int
    category: str
    priority: str  # 'alta', 'media', 'baixa'
    status: bool = False  # False -> pendente, True -> comprado
    date_added: datetime = field(default_factory=datetime.now)
    history: List[HistoryEntry] = field(default_factory=list)

    def record_history(self, field: str, old: str, new: str):
        self.history.append(HistoryEntry(datetime.now(), field, old, new))

    def set_quantity(self, quantity: int):
        if quantity != self.quantity:
            self.record_history('quantity', str(self.quantity), str(quantity))
            self.quantity = quantity

    def set_priority(self, priority: str):
        if priority != self.priority:
            self.record_history('priority', self.priority, priority)
            self.priority = priority

    def set_status(self, status: bool):
        if status != self.status:
            self.record_history('status', str(self.status), str(status))
            self.status = status

    def __repr__(self):
        return (f"Product(name={self.name!r}, qty={self.quantity}, cat={self.category}, "
                f"prio={self.priority}, status={'comprado' if self.status else 'pendente'}, "
                f"date={self.date_added.strftime('%Y-%m-%d %H:%M:%S')})")
