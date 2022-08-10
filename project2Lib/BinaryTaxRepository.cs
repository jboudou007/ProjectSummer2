using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project2Lib
{
    public class BinaryTaxRepository : ITaxRepository
    {
        private string _filePath;

        public BinaryTaxRepository(string filePath)
        {
            _filePath = filePath;
        }

        public TaxRate Create(TaxRate taxRate)      // Will create the file
        {
            try 
            {
                FileStream stream = new(_filePath, FileMode.Append);
                BinaryWriter writer = new(stream);
                writer.Write(taxRate.Rate);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return taxRate;
        }

        public TaxRate ReadAll()
        {
            TaxRate taxRate = new();
            try
            {
                FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);

                BinaryReader reader = new(stream);

                /* will Open File
                 * Check for end-of-file
                 * Read data While not end of file process data then end
                 */

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    TaxRate taxRate2 = new()
                    {
                        Rate = reader.ReadDouble()
                    };

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return taxRate;
        }

        public TaxRate? Read(double id)
        {
            /* Open File
                 * Read data while not end of file process data then end
                 */
            TaxRate? taxRate = null;
            try 
            {
                FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new(stream);
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    double rate = reader.ReadDouble();
                    Console.WriteLine($"{rate}");
                    if(rate == id)
                    {
                        taxRate = new()
                        {
                            Rate = rate
                        };
                        break;  // No need to check any other records

                    }
                }

                reader.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return taxRate;
        }

       

        public void Update(double oldId, TaxRate taxRate)
        {
            //Open temp file for writing
            string tempFilename = _filePath + ".temp";
            FileStream tempstream = new(tempFilename, FileMode.Append);
            BinaryWriter writer = new(tempstream);
            //Open file for reading

            FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new(stream);
            //Read a record not necessary here
            //While not EOF
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                double rate = reader.ReadDouble();
                Console.WriteLine($"{rate}");
                if (rate == oldId)
                {
                    writer.Write(rate);
                }
                else 
                {
                    writer.Write(taxRate.Rate);
                }
            }

            reader.Close();
            writer.Close();

            File.Delete(_filePath);
            File.Move(tempFilename, _filePath); //Rename
        }
    }
}
