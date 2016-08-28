using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo
{
    class Program
    {
        static void Main(string[] args)
        {
            LuceneSearch.AddUpdateLuceneIndex(SampleDataRepository.GetAll());

            var x=LuceneSearch.GetAllIndexRecords();

            var y = LuceneSearch.Search("India");
        }
    }
}
