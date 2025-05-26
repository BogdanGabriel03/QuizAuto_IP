namespace Model
{
    public class Chestionar
    {
        private Intrebare[] intrebari;
        private int corect,gresit;

        public Chestionar(Intrebare[] intrebari)
        {
            this.intrebari = intrebari;
            this.corect = 0;
            this.gresit = 0;
        }
        public void IncrementCorect()
        {
            corect++;
        }
       public void IncrementGresit()
        {
            gresit++;
        }

        public Intrebare[] Intrebari
        {
            get { return intrebari; }
        }

        public int Corect
        {
            get { return corect; }
        }

        public int Gresit
        { get { return gresit; } }

    }
}

