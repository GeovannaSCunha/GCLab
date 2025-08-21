namespace GCLab;

using System.Text;

static class ConcatWork
{
    // Mantemos o nome "Bad" para não mudar o Program, mas implementamos certo.
    public static string Bad(int lines = 50_000)
    {
        var sb = new StringBuilder(capacity: lines * 2);
        for (int i = 0; i < lines; i++)
            sb.Append(i);
        return sb.ToString();
    }
}
