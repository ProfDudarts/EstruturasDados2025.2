<?php

namespace App\Http\Controllers;

use App\Estruturas\AVLArvore;
use App\Estruturas\ComparerCallback;
use Illuminate\Http\Request;

class AvlDemoController extends Controller
{
    public function index(Request $request)
    {
        // Trata inserção via POST (mesmo na rota GET, pode receber POST)
        if ($request->isMethod('post') && $request->has('numbers')) {
            $request->validate(['numbers' => 'required|string']);
            $input = preg_replace('/[^0-9,\s\-]/', ' ', $request->numbers);
            $newNumbers = array_filter(array_map('intval', preg_split('/[,\s]+/', $input)));
            
            $existing = session('avl_numbers', []);
            $allNumbers = array_unique(array_merge($existing, $newNumbers));
            session(['avl_numbers' => $allNumbers]);
            
            return redirect()->back();
        }

        // Trata reset
        if ($request->isMethod('post') && $request->has('reset')) {
            session()->forget('avl_numbers');
            return redirect()->back();
        }

        // Carrega árvore com números da sessão
        $numbers = session('avl_numbers', []);
        $avlTree = new AVLArvore(ComparerCallback::class, 1, false);

        foreach ($numbers as $num) {
            $avlTree->insert($num);
        }

        $treeStructure = $avlTree->toArray();
        $stats = $avlTree->getStatistics();
        $isValid = is_null($avlTree->debugValidate());

        return view('avl_demo', compact('numbers', 'treeStructure', 'stats', 'isValid'));
    }
}