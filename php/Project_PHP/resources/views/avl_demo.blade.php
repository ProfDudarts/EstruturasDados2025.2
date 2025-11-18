<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Árvore AVL Visual</title>
    <style>
        body {
            font-family: system-ui, sans-serif;
            margin: 0;
            padding: 20px;
            background: #f5f7fa;
        }
        .container {
            max-width: 1200px;
            margin: 0 auto;
        }
        h1 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 20px;
        }
        .controls {
            text-align: center;
            margin: 20px 0;
        }
        input[type="text"] {
            padding: 10px;
            width: 300px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        button {
            padding: 10px 16px;
            margin: 0 5px;
            font-size: 16px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .btn-add { background: #28a745; color: white; }
        .btn-add:hover { background: #218838; }
        .btn-reset { background: #dc3545; color: white; }
        .btn-reset:hover { background: #c82333; }

        /* === Estilo da Árvore === */
        .tree {
            display: flex;
            justify-content: center;
            margin: 30px 0;
        }

        .tree-node {
            display: flex;
            flex-direction: column;
            align-items: center;
            position: relative;
        }

        .node {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            background: #4CAF50;
            color: white;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            box-shadow: 0 2px 6px rgba(0,0,0,0.2);
            position: relative;
            z-index: 2;
        }

        /* Linha vertical do nó pai até o conector */
        .node-line {
            height: 20px;
            width: 2px;
            background: #777;
            margin: 0 auto;
            z-index: 1;
        }

        .children {
            display: flex;
            gap: 40px;
            margin-top: 10px;
        }

        .connector {
            display: flex;
            gap: 40px;
            position: relative;
            align-items: flex-start;
        }

        /* Linha horizontal centralizada entre os dois filhos */
        .connector::before {
            content: '';
            position: absolute;
            top: 0;
            left: 25px; /* metade da largura do nó (50px / 2) */
            right: 25px;
            height: 2px;
            background: #777;
            z-index: 1;
        }

        /* Linha vertical de cada filho até a linha horizontal */
        .connector > *:not(.placeholder) {
            position: relative;
        }
        .connector > *:not(.placeholder)::before {
            content: '';
            position: absolute;
            top: 0;
            bottom: 100%;
            left: 50%;
            width: 2px;
            background: #777;
            transform: translateX(-50%);
            z-index: 1;
        }

        .placeholder {
            width: 50px;
            height: 0;
            opacity: 0;
            pointer-events: none;
        }
    </style>
</head>
<body>
<div class="container">
    <h1>Árvore AVL Visual</h1>

    <div class="controls">
        <form method="POST" style="display:inline-block;">
            @csrf
            <input type="text" name="numbers" placeholder="Ex: 50, 25, 75 ou 10 20 30" required>
            <button type="submit" class="btn-add">Adicionar</button>
        </form>
        <form method="POST" style="display:inline-block;">
            @csrf
            <input type="hidden" name="reset" value="1">
            <button type="submit" class="btn-reset">Resetar Árvore</button>
        </form>
    </div>

    @if(!empty($numbers))
        <p style="text-align:center;"><strong>Números na árvore:</strong> {{ implode(', ', $numbers) }}</p>
    @endif

    @if(isset($isValid))
        <div class="status {{ $isValid ? 'valid' : 'invalid' }}">
            <strong>{{ $isValid ? '✅ Árvore válida e balanceada!' : '❌ Árvore inválida!' }}</strong>
        </div>
    @endif

    <div class="tree">
        @if($treeStructure)
            @include('components.tree-node', ['node' => $treeStructure])
        @else
            <p style="text-align:center; color:#777;">Árvore vazia</p>
        @endif
    </div>

    @if(isset($stats))
        <div style="text-align:center; margin-top:30px; color:#555;">
            <small>
                Nós: {{ $stats['count'] ?? 0 }} |
                Altura: {{ $stats['height'] ?? 0 }} |
                Rotações: {{ ($stats['ll'] ?? 0) + ($stats['lr'] ?? 0) + ($stats['rr'] ?? 0) + ($stats['rl'] ?? 0) }}
            </small>
        </div>
    @endif
</div>
</body>
</html>