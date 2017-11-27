namespace Arta.Math
{
    public class Context
    {
        private State _state;
        public Context(State state)
        {
            State = state;
        }

        public State State
        {
            get { return _state; }
            set
            {
                _state = value;
            }
        }

        public void Request()
        {
            _state.Handle(this);
        }
    }
}
