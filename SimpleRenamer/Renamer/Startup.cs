namespace Renamer
{
    public class Startup
    {
        public static void Main()
        {
            CyrillicRenamer cyrillicRenamer = new CyrillicRenamer();

            cyrillicRenamer.Rename("D:\\Drive\\Music");
        }
    }
}