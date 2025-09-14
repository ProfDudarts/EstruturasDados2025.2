<?php
require_once 'No/No.php';
require_once 'Pilha/Pilha.php';

$pilha = new Pilha();
$pilha->inserir(item: new No(valor: "Philipe"));
$pilha->inserir(item: new No(valor: "Luis"));
$pilha->inserir(item: new No(valor: "Gustavo"));

$pilha->listar();
echo PHP_EOL;

$removido = $pilha->remover();
$removido->imprimir();
echo PHP_EOL;

$pilha->listar();
echo PHP_EOL;

$pilha->inserir(item: new No(valor: "Jeniffer"));
$pilha->listar();
?>