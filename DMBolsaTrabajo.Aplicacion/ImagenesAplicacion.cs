using iText.IO.Image;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace DMBolsaTrabajo.Aplicacion
{
    public static class ImagenesAplicacion
    {
        public static byte[] getLogoArrayByte()
        {
            try
            {
                var logoDM = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images", "logo.png");
                return File.ReadAllBytes(logoDM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Image createImagePDFFromPath(byte[] byteImage, float widthPercentValue = 100f)
        {
            return new Image(ImageDataFactory.Create(byteImage)).SetWidth(UnitValue.CreatePercentValue(widthPercentValue));
        }

    }
}
