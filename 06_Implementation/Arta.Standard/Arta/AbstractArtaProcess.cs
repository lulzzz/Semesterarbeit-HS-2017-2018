namespace Arta
{
    public abstract class AbstractArtaProcess : IArtaProcess
    {
        private readonly ArProcess ar;
        /*
         * Generates an Arta-Process with the underlying Ar-Process.
         */
        public AbstractArtaProcess(ArProcess ar)
        {
            this.ar = ar;
        }

        abstract protected double Transform(double value);

        public ArProcess GetArProcess()
        {
            return ar;
        }

        public double Next()
        {
            return Transform(ar.Next());
        }
    }
}
