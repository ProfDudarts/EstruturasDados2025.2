<?php
namespace Estruturas;

class Fila{
    private $itens = [];

    public function enfileirar($item){ // adiciona 
        array_push($this->itens, $item);
    }

    public function desenfileirar(){
        if($this->estaVazia()){
            return null;
        }
        return array_shift($this->itens); // primeiro elemento da fila vai de base 
    }

    public function listar(){  // um for i e print em cada elemento
        foreach($this->itens as $item){
            $item->imprimir();
        }
    }

    public function estaVazia(){ // retorna true ou false p desenfileirar
        return empty($this->itens);
    }
}
