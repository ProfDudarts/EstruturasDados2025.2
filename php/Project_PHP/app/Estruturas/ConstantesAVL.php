<?php
namespace App\Estruturas;

/**
 * Armazena as constantes numéricas para logs e validação da Árvore AVL.
 */
class ConstantesAVL
{
    const DEBUG = 10;
    const INFO = 20;
    const NOTICE = 30;
    const WARNING = 40;
    const ERROR = 50;
    const TEXT = 60;
    const VERSION_NUMBER = '1.0';
    const VERSION_STATE = 'STABLE';
    const FIND_EXACT_MATCH = 1;
    const FIND_PREV_MATCH = 2;
    const FIND_NEXT_MATCH = 3;
    const VALIDATION_OK = 0;
    const VALIDATION_HEIGHT_FAILURE = 1;
    const VALIDATION_BALANCE_FAILURE = 2;
}
