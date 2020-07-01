using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public  class CrateCollection : List<Crate>
    {
        private static CrateCollection _instance = null;

        public static CrateCollection Instance
        {
            get
            {
                if (_instance == null) _instance = new CrateCollection();
                return _instance;
            }
        }


    }
}
