using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2Lib
{
    public interface ITaxRepository
    {

        TaxRate Create(TaxRate taxRate);

        TaxRate ReadAll();

        TaxRate? Read(double id);

        void Update(double oldId, TaxRate taxRate);
    }
}
