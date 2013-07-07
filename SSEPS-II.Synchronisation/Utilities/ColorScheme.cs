using System;
using System.Drawing;

namespace SSEPS_II.Synchronisation
{
    public static class ColorScheme
    {
        public static Color FormBackGroundColor
        {
            get
            {
                return Color.FromArgb(242, 242, 242);
            }
        }

        public static Color OKColor
        {
            get
            {
                return Color.FromArgb(10, 160, 10);
            }
        }

        public static Color CancelColor
        {
            get
            {
                return Color.FromArgb(255, 5, 5);
            }
        }

        public static Color Alternating
        {
            get
            {
                return Color.FromArgb(224, 224, 224);
            }
        }

        public static Color NeutralDataGridViewColor
        {
            get
            {
                return Color.FromArgb(196, 189, 151);
            }
        }

        public static Color NeutralButtonColor
        {
            get
            {
                return Color.FromArgb(255, 255, 255);
            }
        }

        public static Color FormHeaderColor
        {
            get
            {
                return Color.FromArgb(0, 102, 204);
            }
        }
    }
}
