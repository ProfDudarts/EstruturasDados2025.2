<?php
declare(strict_types=1);

namespace Estruturas;

class _pilha {
    private array $itens = [];

    public function inserir(object $item) : void {
        $this->itens[] = $item;
    }

    public function remover() : mixed {
        if ($this->estaVazia()) {
            return null;
        }
        return array_pop($this->itens); 
    }

    public function topo() : mixed {
        if ($this->estaVazia()) {
            return null;
        }
        return $this->itens[count($this->itens) - 1]; 
    }

    public function listar() : void {
        foreach (array_reverse($this->itens) as $item) {
            $item->imprimir();
        }
    }

    public function estaVazia() : bool {
        return empty($this->itens);
    }
}