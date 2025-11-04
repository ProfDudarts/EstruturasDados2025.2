<?php
declare (strict_types=1);

namespace App\Estruturas;

class ComparerCallback
{
    // ... métodos compare e dump ...
    public function compare($a, $b): int
    {
        return $a <=> $b;
    }

    public function dump($data): string
    {
        return is_scalar($data) ? (string)$data : 'Obj';
    }
    
    // Método que deve ser implementado para logs/diagnóstico (usado em setStatus)
    public function diagnosticMessage(int $severity, int $id, string $text, array $params, string $qText, string $className)
    {
        // Exemplo simples: apenas loga mensagens de warning ou superiores
        if ($severity >= ConstantesAVL::WARNING) {
            error_log("[AVL WARNING $id] $qText");
        }
    }
    
    // Método de tratamento de erro (usado em setStatus)
    public function errorHandler(int $id, string $text, array $params, string $qText, string $className)
    {
        // Tratamento de erro fatal, se necessário
    }
}