namespace Markdown
{
    public interface IHandler
    {
        void Handle(char c);
        void ExcludeProcessIn(IHandler handler);
    }
}