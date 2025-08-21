namespace GCLab;

using System;
using System.Collections.Generic;

class Publisher
{
    private readonly object _lock = new();
    private readonly List<WeakReference<Action>> _subs = new();

    // Evento "fraco": evita que o Publisher mantenha o subscriber vivo
    public event Action? OnSomething
    {
        add
        {
            if (value is null) return;
            lock (_lock) _subs.Add(new WeakReference<Action>(value));
        }
        remove
        {
            if (value is null) return;
            lock (_lock)
            {
                for (int i = _subs.Count - 1; i >= 0; i--)
                {
                    if (_subs[i].TryGetTarget(out var t) && t == value)
                        _subs.RemoveAt(i);
                }
            }
        }
    }

    public void Raise()
    {
        Action[] toInvoke;
        lock (_lock)
        {
            // Limpa handlers já coletados
            for (int i = _subs.Count - 1; i >= 0; i--)
                if (!_subs[i].TryGetTarget(out _)) _subs.RemoveAt(i);

            var tmp = new List<Action>(_subs.Count);
            foreach (var wr in _subs)
                if (wr.TryGetTarget(out var a)) tmp.Add(a);
            toInvoke = tmp.ToArray();
        }

        foreach (var a in toInvoke)
        {
            try { a(); } catch { /* não falhar o lab */ }
        }
    }
}
