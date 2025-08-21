namespace GCLab;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

class IssueTracker
{
    private readonly Dictionary<string, WeakReference> _refs = new();
    public bool HasSurvivors { get; private set; }

    public void Track(string name, object obj)
    {
        // WeakReference: o tracker não mantém ninguém vivo
        _refs[name] = new WeakReference(obj);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Report()
    {
        // Coleta extra antes do relatório
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);

        Console.WriteLine("\n--- Verificação de sobreviventes (WeakReference) ---");
        HasSurvivors = false;

        foreach (var kv in _refs)
        {
            bool alive = kv.Value.IsAlive;
            Console.WriteLine($"{kv.Key}: {(alive ? "vivo" : "coletado")}");
            if (alive) HasSurvivors = true;
        }
        Console.WriteLine("-----------------------------------------------");

        Console.WriteLine($"Gen0: {GC.CollectionCount(0)} | Gen1: {GC.CollectionCount(1)} | Gen2: {GC.CollectionCount(2)}");
    }
}
