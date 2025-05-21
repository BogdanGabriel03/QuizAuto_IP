namespace Model
{
    public class Chestionar
    {
        private Intrebare[] intrebari;
        private int scor;

        public Chestionar(Intrebare[] intrebari)
        {
            this.intrebari = intrebari;
            this.scor = 0;
        }
        public void IncrementScor()
        {
            scor++;
        }

        public Intrebare[] Intrebari
        {
            get { return intrebari; }
        }

        public int Scor
        {
            get { return scor; }
        }

    }
}

