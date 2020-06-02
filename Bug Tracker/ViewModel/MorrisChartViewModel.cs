using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace Bug_Tracker.ViewModel
{
    public class MorrisChartViewModel
    {
        public ArrayList Data = new ArrayList();
        public List<string> YKeys = new List<string>();
        public List<string> Labels = new List<string>();
        public List<string> BarColors = new List<string>();
    }
}