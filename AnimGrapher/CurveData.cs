using System;
using System.Globalization;

namespace AnimGrapher
{
    class CurveData
    {
        private String name_ = "";

        private CurveType curveType_ = CurveType.PARAMETRIC;

        private string xtEquation_ = "";
        private string ytEquation_ = "";
        private string rtEquation_ = "";
        private string yxEquation_ = "";

        public double XVMin = -2;
        public double XVMax =  2;
        public double YVMin = -2;
        public double YVMax =  2;

        public double PMin = 0;
        public double PMax = 6.283;     // 2*Math.PI;
        public double PStep = 0.01;

        public double Thickness = 1;

        #region Accessors

        public string Name
        {
            get { return name_; }
            set { name_ = value; }
        }

        public CurveType Type
        {
            get { return curveType_; }
            set { curveType_ = value; }
        }

        public string XtEquation
        {
            get { return xtEquation_; }
            set { xtEquation_ = value; }
        }

        public string YtEquation
        {
            get { return ytEquation_; }
            set { ytEquation_ = value; }
        }

        public string RtEquation
        {
            get { return rtEquation_; }
            set { rtEquation_ = value; }
        }

        public string YxEquation
        {
            get { return yxEquation_; }
            set { yxEquation_ = value; }
        }

        #endregion

        public CurveData()
        {
            // create default parametric curve data
        }

        public CurveData(string name, CurveType curveType)
        {
            name_ = name;
            curveType_ = curveType;
        }

        public CurveData(string name, CurveType curveType, string equation1, string equation2)
        {
            name_ = name;
            curveType_ = curveType;

            switch (curveType)
            {
                case CurveType.PARAMETRIC:
                    xtEquation_ = equation1;
                    ytEquation_ = equation2;
                    break;

                case CurveType.POLAR:
                    rtEquation_ = equation1;
                    break;

                case CurveType.CARTESIAN:
                    yxEquation_ = equation1;
                    break;
            }
        }

        public void Clear()
        {
            CurveData cdDefault = new CurveData();

            name_ = cdDefault.name_;
            curveType_ = cdDefault.curveType_;

            xtEquation_ = cdDefault.xtEquation_;
            ytEquation_ = cdDefault.ytEquation_;
            rtEquation_ = cdDefault.rtEquation_;
            yxEquation_ = cdDefault.yxEquation_;

            XVMin = cdDefault.XVMin;
            XVMax = cdDefault.XVMax;
            YVMin = cdDefault.YVMin;
            YVMax = cdDefault.YVMax;

            PMin = cdDefault.PMin;
            PMax = cdDefault.PMax;
            PStep = cdDefault.PStep;

            Thickness = cdDefault.Thickness;
        }

        public CurveData Copy()
        {
            CurveData cdCopy = new CurveData();

            cdCopy.Name = this.Name;
            cdCopy.Type = this.Type;

            cdCopy.XtEquation = this.XtEquation;
            cdCopy.YtEquation = this.YtEquation;
            cdCopy.RtEquation = this.RtEquation;
            cdCopy.YxEquation = this.YxEquation;

            cdCopy.XVMin = this.XVMin;
            cdCopy.XVMax = this.XVMax;
            cdCopy.YVMin = this.YVMin;
            cdCopy.YVMax = this.YVMax;

            cdCopy.PMin = this.PMin;
            cdCopy.PMax = this.PMax;
            cdCopy.PStep = this.PStep;

            cdCopy.Thickness = this.Thickness;

            return cdCopy;
        }

        public string GetDescription()
        {
            string desc = "";
            CurveData cdDefault = new CurveData();

            // name mandatory
            if (String.IsNullOrEmpty(name_))
                return "";
            desc += "[" + name_ + "]" + Environment.NewLine;

            desc += "type=" + curveType_.ToString().ToLower() + Environment.NewLine;

            // equations
            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                    desc += "x(t)=" + xtEquation_ + Environment.NewLine;
                    desc += "y(t)=" + ytEquation_ + Environment.NewLine;
                    break;

                case CurveType.POLAR:
                    desc += "r(t)=" + rtEquation_ + Environment.NewLine;
                    break;

                case CurveType.CARTESIAN:
                    desc += "y(x)=" + yxEquation_ + Environment.NewLine;
                    break;
            }

            // write parameters values if different from default values

            CultureInfo culture = new CultureInfo("en-US");

            if (XVMin != cdDefault.XVMin)
                desc += "xv_min=" + XVMin.ToString(culture) + Environment.NewLine;
            if (XVMax != cdDefault.XVMax)
                desc += "xv_max=" + XVMax.ToString(culture) + Environment.NewLine;
            if (YVMin != cdDefault.YVMin)
                desc += "yv_min=" + YVMin.ToString(culture) + Environment.NewLine;
            if (YVMax != cdDefault.YVMax)
                desc += "yv_max=" + YVMax.ToString(culture) + Environment.NewLine;

            switch (curveType_)
            {
                case CurveType.PARAMETRIC:
                case CurveType.POLAR:
                    if (PMin != cdDefault.PMin)
                        desc += "t_min=" + PMin.ToString(culture) + Environment.NewLine;
                    if (PMax != cdDefault.PMax)
                        desc += "t_max=" + PMax.ToString(culture) + Environment.NewLine;
                    if (PStep != cdDefault.PStep)
                        desc += "t_step=" + PStep.ToString(culture) + Environment.NewLine;
                    break;

                case CurveType.CARTESIAN:
                    if (PMin != cdDefault.PMin)
                        desc += "x_min=" + PMin.ToString(culture) + Environment.NewLine;
                    if (PMax != cdDefault.PMax)
                        desc += "x_max=" + PMax.ToString(culture) + Environment.NewLine;
                    if (PStep != cdDefault.PStep)
                        desc += "x_step=" + PStep.ToString(culture) + Environment.NewLine;
                    break;
            }

            if (Thickness != cdDefault.Thickness)
                desc += "thickness=" + Thickness + Environment.NewLine;

            desc += Environment.NewLine;

            return desc;
        }
    }
}
