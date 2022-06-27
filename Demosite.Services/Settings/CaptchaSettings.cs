using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demosite.Services.Settings
{
    public class CaptchaSettings
    {
        public string DefaultKey { get; set; }
        public FontWarpFactor FontWarpFactor { get; set; }
        public BackgroundNoiseLevel BackgroundNoiseLevel { get; set; }
        public LineNoiseLevel LineNoiseLevel { get; set; }
        public string[] Fonts { get; set; }
        public string[] Colors { get; set; }
        public string Letters { get; set; }
        public string Referrer { get; set; }
        public int TextLength { get; set; }
        public int MaxShift { get; set; }
        public int MinShift { get; set; }
        public int MaxAngle { get; set; }
        public int MinAngle { get; set; }
        public int FontSizeMax { get; set; }
        public int FontSizeMin { get; set; }
        public int CaptchaWidth { get; set; }
        public int CaptchaHeight { get; set; }
    }
    /// <summary>
    /// Amount of random font warping to apply to rendered text
    /// </summary>
    public enum FontWarpFactor
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Low,
        /// <summary>
        /// 
        /// </summary>
        Medium,
        /// <summary>
        /// 
        /// </summary>
        High,
        /// <summary>
        /// 
        /// </summary>
        Extreme
    }

    /// <summary>
    /// Amount of background noise to add to rendered image
    /// </summary>
    public enum BackgroundNoiseLevel
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Low,
        /// <summary>
        /// 
        /// </summary>
        Medium,
        /// <summary>
        /// 
        /// </summary>
        High,
        /// <summary>
        /// 
        /// </summary>
        Extreme
    }

    /// <summary>
    /// Amount of curved line noise to add to rendered image
    /// </summary>
    public enum LineNoiseLevel
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Low,
        /// <summary>
        /// 
        /// </summary>
        Medium,
        /// <summary>
        /// 
        /// </summary>
        High,
        /// <summary>
        /// 
        /// </summary>
        Extreme
    }
}
