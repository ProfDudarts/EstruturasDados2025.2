<?php
class Fila{
    private $itens = [];

    public funcion enfileirar($item){ // adiciona 
        array_push($item->itens,$item);
    }

    public funcion desenfileirar(){
        if($this->estaVazia()){
            return null;
        }
        return array_shift($this->itens); // primeiro elemento da fila vai de base 
    }

    public funcion listar(){  // um for i e print em cada elemento
        foreach($this->itens as $item){
            $item->imprimir();
        }
    }

    public funcion taVazia(){ // retorna true ou false p desenfileirar
        return empty($this->itens);
    }
}