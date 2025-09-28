<?php

declare(strict_types=1);

namespace Estruturas;

class ListaEncadeada {
    private ?No $inicio = null; // ->Nó inicial da lista
    private int $tamanho = 0;   // ->Quantidade de elementos

    
    public function taVazia(): bool // -> Verifica se a lista está vazia
    {
        return $this->tamanho === 0;
    }

        public function obterTamanho(): int // ->Retorna o tamanho da lista

    {
        return $this->tamanho;
    }

    
    public function buscar(mixed $valor): ?No // ->Busca um nó pelo valor
    {
        $atual = $this->inicio;
        while ($atual !== null) {
            if ($atual->getValor() === $valor) return $atual;
            $atual = $atual->getProximo();
        }
        return null;
    }

    
    public function inserirNoInicio(No $novoNo): void // ->Insere um nó no início
    {
        $novoNo->setProximo($this->inicio);
        $this->inicio = $novoNo;
        $this->tamanho++;
    }

    
    public function inserirNoFim(No $novoNo): void // ->Insere um nó no final
    {
        if ($this->taVazia()) {
            $this->inicio = $novoNo;
        } else {
            $atual = $this->inicio;
            while ($atual->getProximo() !== null) {
                $atual = $atual->getProximo();
            }
            $atual->setProximo($novoNo);
        }
        $this->tamanho++;
    }

    
    public function removerDoInicio(): ?No // ->Remove o nó do início
    {
        if ($this->taVazia()) return null;

        $removido = $this->inicio;
        $this->inicio = $removido->getProximo();
        $removido->setProximo(null);
        $this->tamanho--;
        return $removido;
    }

    
    public function removerPorValor(mixed $valor): ?No // ->Remove o nó que contém o valor indicado
    {
        if ($this->taVazia()) return null;

        $atual = $this->inicio;
        $anterior = null;

        while ($atual !== null) {
            if ($atual->getValor() === $valor) {
                if ($anterior === null){ 
                    $this->inicio = $atual->getProximo();
                } else { 
                    $anterior->setProximo($atual->getProximo());
                }
                $atual->setProximo(null);
                $this->tamanho--;
                return $atual;
            }
            $anterior = $atual;
            $atual = $atual->getProximo();
        }
        return null;
    }

    
    public function listar(): void // ->Lista todos os nós chamando o método imprimir()
    {
        $atual = $this->inicio;
        while ($atual !== null) {
            $atual->imprimir();
            $atual = $atual->getProximo();
        }
    }

}

