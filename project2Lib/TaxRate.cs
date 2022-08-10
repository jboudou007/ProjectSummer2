using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2Lib
{
    public class TaxRate
    {
        


        private double _rate;
        public double Rate
        {
            
            get { return _rate; }
            set
            {
                if (value <= 0 || value >= 1)
                {
                    throw new InvalidTaxException("Rate must be from 0 to 1 inclusive");
                }
                
                _rate = value;

            } 
        }

        public TaxRate() { }

        public void UpdateRate(double rate) //Will call this method to change Tax Rate
        {
            Rate = rate;
        }



    }
}
