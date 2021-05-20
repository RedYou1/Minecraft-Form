using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public interface Cuisable
    {
        /// <summary>
        /// combien de carburant il coute</br>
        /// 10 par defaut (un item)
        /// </summary>
        /// <returns></returns>
        int TempsDeCuisson();

        /// <summary>
        /// le resultat
        /// </summary>
        /// <returns></returns>
        Item CuitEn();
    }
}
