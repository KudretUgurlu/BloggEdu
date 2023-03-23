using Microsoft.AspNetCore.Mvc;

namespace BloggEdu.ViewComponents.Writer
{
    public class WriterMessageNotification:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
