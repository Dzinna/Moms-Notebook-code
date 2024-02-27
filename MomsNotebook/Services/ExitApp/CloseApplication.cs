namespace MomsNotebook.Services.ExitApp
{
    public class CloseApplication : ICloseApplication
    {
        public void ExitApp()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
