namespace Safranek.Commands;

public interface ITableBase {
    public static abstract string Name { get; }
    public static abstract void Init();
}
