<?php
declare(strict_types=1);

namespace Estruturas;

class ListaDuplamenteEncadeada {
    
    private ?NoDuplo $cima = null;
    private ?NoDuplo $baixo = null;
    private int $tamanho = 0;

    public function obterTamanho(): int {
        return $this->tamanho;
    }
    
    public function taVazia(): bool {
        return $this->tamanho === 0;
    }

    public function inserirNoInicio(NoDuplo $novoNo): void {
        if ($this->taVazia()) {
            $this->cima = $novoNo;
            $this->baixo = $novoNo;
        } else {
            $novoNo->setProximo($this->cima); 
            $this->cima->setAnterior($novoNo); 
            $this->cima = $novoNo;
        }
        $this->tamanho++;
    }

    public function inserirNoFim(NoDuplo $novoNo): void {
        if ($this->taVazia()) {
            $this->inserirNoInicio($novoNo);
        } else {
            $novoNo->setAnterior($this->baixo); 
            $this->baixo->setProximo($novoNo); 
            $this->baixo = $novoNo;
        }
        $this->tamanho++;
    }

    private function removerNo(NoDuplo $noParaRemover) : void {
        if ($noParaRemover === $this->cima) {
            $this->removerDoInicio();
            return;
        } elseif ($noParaRemover === $this->baixo) {
            $this->removerDoFim();
            return;
        }

        $anterior = $noParaRemover->getAnterior();
        $proximo = $noParaRemover->getProximo();

        $anterior->setProximo($proximo);
        $proximo->setAnterior($anterior);

        $this->tamanho--;

        $noParaRemover->setProximo(null);
        $noParaRemover->setAnterior(null);
    }
    
    public function removerPorValor(mixed $valor) : ?NoDuplo {
        $noParaRemover = $this->buscar($valor);

        if ($noParaRemover === null) {
            return null;
        }
        $this->removerNo($noParaRemover);
        return $noParaRemover;
    }

    public function removerPorIndice(int $indice) : ?NoDuplo {
        if ($this->taVazia() || $indice < 0 || $indice >= $this->tamanho) {
            return null;
        }

        if ($indice === 0) {
            return $this->removerDoInicio();
        }

        $contador =0;
        $atual = $this->cima;

        while ($contador < $indice) {
            $atual = $atual->getProximo();
            $contador++;
        }

        $removido = $atual;
        $this->removerNo($removido);

        return $removido;
    }

    public function removerDoInicio(): ?NoDuplo {
        if ($this->taVazia()) {
            return null;
        }
        
        $removido = $this->cima;
        
        if ($this->cima === $this->baixo) {
            $this->cima = null;
            $this->baixo = null;
        } else {
            $this->cima = $removido->getProximo(); 
            $this->cima->setAnterior(null); 
            $removido->setProximo(null); 
        }

        $this->tamanho--;
        return $removido;
    }
    
    public function removerDoFim(): ?NoDuplo {
        if ($this->taVazia()) {
            return null;
        }
        
        $removido = $this->baixo;

        if ($this->cima === $this->baixo) {
            $this->cima = null;
            $this->baixo = null;
        } else {
            $this->baixo = $removido->getAnterior();
            $this->baixo->setProximo(null); 
            $removido->setAnterior(null); 
        }

        $this->tamanho--;
        return $removido;
    }
    
    public function listarCimaBaixo(): void {
        $atual = $this->cima;
        while ($atual !== null) {
            $atual->imprimir();
            $atual = $atual->getProximo();
        }
    }

    public function listarBaixoCima(): void {
        $atual = $this->baixo;
        while ($atual !== null) {
            $atual->imprimir();
            $atual = $atual->getAnterior();
        }
    }

    public function buscar(mixed $valorBuscado): ?NoDuplo {
        $atual = $this->cima;

        while ($atual !== null) {
            if ($atual->getValor() === $valorBuscado) {
                return $atual;
            }
            $atual = $atual->getProximo();
        }
        return null;
    }
}