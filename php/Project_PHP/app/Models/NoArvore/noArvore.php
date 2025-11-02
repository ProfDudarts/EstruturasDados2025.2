<?php
namespace Estruturas;

class NoArvore {
    private int $valor; 
    private ?self $esquerdo = null; 
    private ?self $direito = null;

    public function __construct(int $valor) {
        $this->valor = $valor;
    }

    public function getValor() : int {
        return $this->valor;
    }

    public function getEsquerdo() : ?self {
        return $this->esquerdo;
    }

    public function getDireito() : ?self {
        return $this->direito;
    }
    
    public function setEsquerdo(?self $no) : self {
        $this->esquerdo = $no;
        return $this;
    }

    public function setDireito(?self $no) : self {
        $this->direito = $no;
        return $this;
    }

    public function imprimir() : void {
        echo $this->valor . PHP_EOL; 
    }
    
    public function __toString(): string
    {
        return (string)$this->valor;
    }
}