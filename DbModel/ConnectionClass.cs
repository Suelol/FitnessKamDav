using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessOsnova_Kam_Dav.DbModel;

namespace FitnessOsnova_Kam_Dav.DbModel
{
     internal class ConnectionClass
    {
        public static FitnessClub_Kam_DavEntities connect = new FitnessClub_Kam_DavEntities();
    }
}
