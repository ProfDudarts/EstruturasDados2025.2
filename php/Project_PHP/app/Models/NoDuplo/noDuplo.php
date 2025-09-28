<?php
namespace Estruturas;

class NoDuplo {
    
    private mixed $valor;
    private ?self $proximo;
    private ?self $anterior;

    public function __construct(mixed $valor) {
        $this->valor = $valor;
        $this->proximo = null;
        $this->anterior = null;
    }

    public function getValor() : mixed { 
        return $this->valor; 
    }
    
    public function getProximo() : ?self { 
        return $this->proximo; 
    }

    public function setProximo(?self $no) : void { 
        $this->proximo = $no; 
    }

    public function getAnterior() : ?self { 
        return $this->anterior; 
    }

    public function setAnterior(?self $no) : void { 
        $this->anterior = $no; 
    }
    
    public function imprimir() : void {
        if (is_scalar($this->valor)) {
            echo $this->valor . PHP_EOL;
        } else {
            echo "[Objeto NoDuplo]" . PHP_EOL; 
        }
    }
}