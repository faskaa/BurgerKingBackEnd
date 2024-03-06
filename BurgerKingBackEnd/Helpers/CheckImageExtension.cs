namespace BurgerKingBackEnd.Helpers
{
    public static class CheckImageExtension
    {

        public static bool IsImage(this IFormFile file)
        {
             return file !=  null &&  file.ContentType.StartsWith("image");
        }
    }
}
