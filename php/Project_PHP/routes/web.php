<?php

use App\Http\Controllers\AvlDemoController;
use Illuminate\Support\Facades\Route;

Route::get('/', [AvlDemoController::class, 'index']);
// Aceitamos POST na mesma URL
Route::post('/', [AvlDemoController::class, 'index']);