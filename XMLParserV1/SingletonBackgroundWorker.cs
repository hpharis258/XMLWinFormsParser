using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1
{
    public class SingletonBackgroundWorker
    {
        private static BackgroundWorker? Worker = null;
        private static readonly object _lock = new object();
        //
        SingletonBackgroundWorker()
        {
        }
        //
        public static BackgroundWorker GetWorker()
        { 
            lock( _lock)
            {
                if (Worker == null)
                {
                    Worker = new BackgroundWorker();
                }
            }
            return Worker;
        }


    }
}
