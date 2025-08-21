using System.IO;
using System.Text;

namespace GCLab;

class Logger
{
    private StreamWriter? _writer;

    public Logger(string path)
    {
        _writer = new StreamWriter(path, append: true, Encoding.UTF8);
    }

    public void WriteLines(int count)
    {
        if (_writer is null) return;
        for (int i = 0; i < count; i++)
            _writer.WriteLine($"linha {i}");
        _writer.Flush();
    }

    // Finalizer: garante liberação do recurso externo
    ~Logger()
    {
        try { _writer?.Dispose(); }
        catch { /* não lançar do finalizer */ }
        _writer = null;
    }
}
