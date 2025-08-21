namespace GCLab;

using System;
using System.Collections.Generic;

static class BigBufferHolder
{
    // Cache fraco: não impede o GC de coletar buffers grandes
    private static readonly List<WeakReference<byte[]>> _cache = new();

    public static byte[] Run()
    {
        var data = new byte[200_000]; // ~200 KB → LOH
        _cache.Add(new WeakReference<byte[]>(data)); // referência fraca
        return data; // Program controla vida; cache não "gruda"
    }
}
