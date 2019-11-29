using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimGrapher
{
    public class View
    {
        // display axis variables

        public double Xmin;

        public double Xmax;

        public double Ymin;

        public double Ymax;

        public double Xunit;

        public double Yunit;

        public bool Isometric;

        public View()
        {
            Xmin = -2;
            Xmax = +2;
            Ymin = -2;
            Ymax = +2;

            Xunit = 0.5;
            Yunit = 0.5;

            Isometric = true;
        }

        public void UpdateGivenX(int width, int height)
        {
            if (!Isometric)
                return;

            // maintain y center
            double yCenter = 0.5 * (Ymin + Ymax);

            // compute new y range
            double xRange = Xmax - Xmin;
            double yRangeNew = xRange * (double)height / (double)width;

            // compute new y min and max
            Ymin = yCenter - 0.5 * yRangeNew;
            Ymax = yCenter + 0.5 * yRangeNew;
        }

        public void UpdateGivenY(int width, int height)
        {
            if (!Isometric)
                return;

            // maintain x center
            double xCenter = 0.5 * (Xmin + Xmax);

            // compute new x range
            double yRange = Ymax - Ymin;
            double xRangeNew = yRange * (double)width / (double)height;

            // compute new x min and max
            Xmin = xCenter - 0.5 * xRangeNew;
            Xmax = xCenter + 0.5 * xRangeNew;
        }
    }
}
