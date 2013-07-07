using System;
using System.Windows.Forms;

namespace SSEPS_II.Synchronisation
{
    public class WaitCursor : IDisposable
    {
        protected Cursor _previousCursor;

        public WaitCursor()
        {
            _previousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = _previousCursor;
        }
    }
}
