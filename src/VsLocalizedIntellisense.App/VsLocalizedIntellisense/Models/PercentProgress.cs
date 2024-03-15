using System;

namespace VsLocalizedIntellisense.Models.Data
{
    public class PercentProgress
    {
        public PercentProgress(int denominator, IProgress<double> progress)
        {
            Denominator = denominator;
            Progress = progress;
        }

        #region property

        private IProgress<double> Progress { get; }

        public int Current { get; private set; }
        public int Denominator { get; }

        public double Percent => (double)Current / Denominator;

        #endregion

        #region function

        public void Increment()
        {
            Current += 1;
            Progress.Report(Percent);
        }

        #endregion
    }
}
