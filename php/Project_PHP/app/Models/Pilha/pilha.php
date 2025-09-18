<?php
declare(strict_types=1);

namespace Estruturas;

class Pilha {
    private $itens = [];

    public function inserir(object $item) : void {
        array_push($this->itens, $item);
    }

    public function remover() : ?object {
        if ($this->estaVazia()) {
            return null;
        }
        return array_pop($this->itens);
    }

    public function listar() : void {
        foreach ($this->itens as $item) {
            $item->imprimir();
        }
    }

    public function estaVazia() : bool {
        return empty($this->itens);
    }
}