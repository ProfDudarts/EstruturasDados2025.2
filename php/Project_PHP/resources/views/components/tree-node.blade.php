{{-- resources/views/components/tree-node.blade.php --}}
<div class="tree-node">
    <div class="node">{{ $node['value'] }}</div>

    @if($node['left'] || $node['right'])
        <div class="node-line"></div>
        <div class="children">
            <div class="connector">
                @if($node['left'])
                    @include('components.tree-node', ['node' => $node['left']])
                @else
                    <div class="placeholder"></div>
                @endif

                @if($node['right'])
                    @include('components.tree-node', ['node' => $node['right']])
                @else
                    <div class="placeholder"></div>
                @endif
            </div>
        </div>
    @endif
</div>