using SV18T1021246.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021246.DataLayer
{
    public interface ICountryDAL
    {
        /// <summary>
        /// Lay ds quoc gia
        /// </summary>
        /// <returns></returns>
        IList<Country> List();
    }
}
