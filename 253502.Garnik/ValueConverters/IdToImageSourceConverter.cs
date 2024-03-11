using System.Globalization;

namespace _253502.Garnik.ValueConverters
{
    internal class IdToImageSourceConverter : IValueConverter
    {
       

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            if (path == null)
            {
                return ImageSource.FromFile("dotnet_bot.png");
            }


            string fname = $"{id}.png";
            string imagePath = Path.Combine(path, fname);

            if (Path.Exists(imagePath))
            {
                return ImageSource.FromFile(imagePath);
            }

            return ImageSource.FromFile("dotnet_bot.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
