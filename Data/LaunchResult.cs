using HorusUITest.PageObjects;

namespace HorusUITest.Data
{
    public class LaunchResult<T> where T : BasePage
    {
        public T Page { get; }
        public LaunchLog Log { get; }

        public LaunchResult(T page, LaunchLog log)
        {
            Page = page;
            Log = log;
        }

        public void Deconstruct(out T page, out LaunchLog log)
        {
            page = Page;
            log = Log;
        }
    }
}
