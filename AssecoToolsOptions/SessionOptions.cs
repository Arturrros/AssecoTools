using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace AssecoToolsOptions
{
    
    /// <summary>
    /// Options for Session 
    /// </summary>
    public partial class SessionOptions 
    {
        /// <summary>
        /// User defined Connetction Name 
        /// </summary>
        public string userConnectionName { get; set; }

        /// <summary>
        /// Color for modal windows for one connection
        /// </summary>
        public Color SessionColor { get; set; }

        /// <summary>
        /// Is color active?
        /// </summary>
        public bool isActiveSessionColor { get; set; } = false;
        //private SessionOptions opts;
        public SessionOptions() { }
        //public SessionOptions Opts
        //{
        //    get
        //    {
        //        if (opts == null)
        //        {
        //            opts = new SessionOptions();
        //        }
        //        return opts;
        //    }
        //}

    }
}
