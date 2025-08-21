namespace GCLab;

using System;
using System.Runtime.CompilerServices;

static class GCHelpers
{
    // Força coletas completas, aguardando finalizers e compactando LOH
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void FullCollect()
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
        GC.WaitForPendingFinalizers();
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking: true, compacting: true);
    }
}
