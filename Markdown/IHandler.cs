namespace Markdown
{
    public interface IHandler
    {
        void Handle(char c);
        void Exclude(IHandler handler);
    }
}