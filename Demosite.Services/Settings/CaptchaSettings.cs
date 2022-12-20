using SixLabors.ImageSharp;

namespace Demosite.Services.Settings
{
    public class CaptchaSettings
    {
        public bool IsActive { get; set; }
        public string DefaultKey { get; set; }
        public BackgroundNoiseLevel BackgroundNoiseLevel { get; set; }
        public byte DrawLineNoise { get; set; }
        public string[] Colors { get; set; }
        public string Referrer { get; set; }
        public int TextLength { get; set; }
        public byte MaxAngle { get; set; }
        public byte FontSize { get; set; }
        public ushort CaptchaWidth { get; set; }
        public ushort CaptchaHeight { get; set; }
        public Color[] GetColors()
        {
            if (Colors?.Length == 0)
            {
                return new Color[0];
            }
            Color[] result = new Color[Colors.Length];
            for (int i = 0; i < Colors.Length; i++)
            {
                result[i] = Color.TryParse(Colors[i], out Color newColor) ? newColor : Color.Black;
            }
            return result;
        }
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
        Low = 200,
        /// <summary>
        /// 
        /// </summary>
        Medium = 400,
        /// <summary>
        /// 
        /// </summary>
        High = 800,
        /// <summary>
        /// 
        /// </summary>
        Extreme = 1600
    }
}
