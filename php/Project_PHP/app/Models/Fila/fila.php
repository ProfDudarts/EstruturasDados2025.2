<?php
declare(strict_types=1);

namespace Estruturas;

class Fila{
    private $itens = [];

    public function enfileirar(object $item) : void { // adiciona 
        array_push($this->itens, $item);
    }

    public function desenfileirar() : ?object {
        if($this->estaVazia()){
            return null;
        }
        return array_shift($this->itens); // primeiro elemento da fila vai de base 
    }

    public function listar() : void {  // um for i e print em cada elemento
        foreach($this->itens as $item){
            $item->imprimir();
        }
    }

    public function estaVazia() : bool { // retorna true ou false p desenfileirar
        return empty($this->itens);
    }
}
