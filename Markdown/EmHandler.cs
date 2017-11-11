namespace Markdown
{
    public class EmHandler : IHandler
    {
        public State State { get; private set; } = State.Out;
        private char StartSymbol { get; set; } = '_';
        private char StopSymbol { get; set; } = '_';

        public void Handle(char c)
        {
            
        }

        public void ExcludeProcessIn(IHandler handler)
        {
            
        }
    }
}