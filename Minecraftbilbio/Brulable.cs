using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftbilbio
{
    public interface Brulable
    {
        /// <summary>
        /// la quantiter d'energie que produit cet item</br>
        /// 10 par item (par defaut)
        /// </summary>
        /// <returns></returns>
        int ProduitTemperature();
    }
}
