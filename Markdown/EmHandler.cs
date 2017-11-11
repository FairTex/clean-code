namespace Markdown
{
    public class EmHandler : IHandler
    {
        private State State { get; set; } = State.Out;
        private char StartSymbol { get; set; } = '_';
        private char StopSymbol { get; set; } = '_';

        public void Handle(char c)
        {
            
        }

        public void Exclude(IHandler handler)
        {
            
        }
    }
}