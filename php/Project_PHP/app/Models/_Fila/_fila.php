<?php
declare(strict_types=1);

namespace Estruturas;

class _fila {
    
    private ?No $frente = null;
    private ?No $fim = null;
    private int $tamanho = 0;

    public function taVazio(): bool {
        return $this->tamanho === 0;
    }

    public function enfileirar(No $no) : void { 
        if ($this->taVazio()) {
            $this->frente = $no;
            $this->fim = $no;
        } else {
            $this->fim->setProximo($no); 
            $this->fim = $no;            
        }
        $this->tamanho++;
    }

    public function desenfileirar() : ?No {
        if ($this->taVazio()){
            return null;
        }

        $removido = $this->frente;
        
        $this->frente = $removido->getProximo();
        $removido->setProximo(null); 

        if ($this->frente === null) {
            $this->fim = null;
        }
        
        $this->tamanho--;
        return $removido;
    }

    public function listar() : void {
        $atual = $this->frente;
        while ($atual !== null) {
            $atual->imprimir();
            $atual = $atual->getProximo();
        }
    }
}