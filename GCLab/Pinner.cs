using System;
using System.Runtime.InteropServices;

namespace GCLab;

class Pinner
{
    // Antes: mantinha o buffer pinned por muito tempo.
    // Agora: pin curtíssimo e retorna array NÃO pinned.
    public byte[] PinLongTime()
    {
        var buf = new byte[64 * 1024];

        var handle = GCHandle.Alloc(buf, GCHandleType.Pinned);
        try
        {
            IntPtr ptr = handle.AddrOfPinnedObject();
            FakeNative(ptr, buf.Length);
        }
        finally
        {
            if (handle.IsAllocated) handle.Free(); // solta o pin imediatamente
        }

        return buf; // já não está mais pinned
    }

    private static void FakeNative(IntPtr ptr, int len)
    {
        // noop (simulação de chamada nativa)
    }
}
