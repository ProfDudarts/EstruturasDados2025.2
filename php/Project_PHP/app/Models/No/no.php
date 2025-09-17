<?php
namespace Estruturas;

class No {
    public $valor;
    public $proximo;

    public function __construct($valor) {
        $this->valor = $valor;
        $this->proximo = null;
    }

    public function imprimir() {
        echo $this->valor . PHP_EOL;
    }
}