using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SsepsII.Synchronisation.Services
{
    public class ApprovalNodeBO
    {
        private ApprovalNodeBO() { }
        public ApprovalNodeBO(string name, string oldValue, string newValue)
        {
            Name = name;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public string Name { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public string TextToDisplay
        {
            get
            {
                if(OldValue == string.Empty)
                    return string.Format("{0} :- new value: <b>{1}</b>", Name, NewValue);
                else
                    return string.Format("{0} :- old value: <b>{1}</b>, new value: <b>{2}</b>", Name, OldValue, NewValue);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} :- old value: {1}, new value: {2}");
        }
    }
}
