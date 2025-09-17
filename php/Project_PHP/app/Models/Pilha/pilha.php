<?php
namespace Estruturas;

class Pilha {
    private $itens = [];

    public function inserir($item) {
        array_push($this->itens, $item);
    }

    public function remover() {
        if ($this->estaVazia()) {
            return null;
        }
        return array_pop($this->itens);
    }

    public function listar() {
        foreach ($this->itens as $item) {
            $item->imprimir();
        }
    }

    public function estaVazia() {
        return empty($this->itens);
    }
}