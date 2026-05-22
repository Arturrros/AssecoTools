using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassWaiters
{
    class ClassWaiters
    {
    }
    public class Blockers
    {
        public Blockers(){}
        public int sid { get; set; }
        public int serial { get; set; }
        public string sadr { get; set; }

        private List<Waiters> wl = new List<Waiters>();
        public void AddWaiter(string sadr, int sid, int serial)
        {
            Waiters w = new Waiters(sadr, sid, serial);
            
            wl.Add(w);
        }
        
    }

    class Waiters
    {
        private string parentSadr { get; set; }
        public int sid { get; set; }
        public int serial { get; set; }
        public string sadr { get; set; }

        public Waiters()
        { 
        }

        public Waiters(string pSadr, int sid, int serial) 
        {
            this.parentSadr = pSadr;
            this.sid = sid;
            this.serial = serial;
            this.sadr = "2";

        }
        
    }
}
