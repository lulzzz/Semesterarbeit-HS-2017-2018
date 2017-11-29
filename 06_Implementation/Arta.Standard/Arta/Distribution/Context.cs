namespace Arta.Math
{
    public class Context
    {
        private DistributionState _state;
        public Context(DistributionState state)
        {
            State = state;
        }

        public DistributionState State
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
