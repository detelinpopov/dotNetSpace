using System.Web;

namespace WebProject.Helpers
{
    public class SessionUtils
    {
        public static T Get<T>(string key)
        {
            var valueFromSession = HttpContext.Current.Session[key];
            if (valueFromSession is T)
            {
                return (T) valueFromSession;
            }
            return default(T);
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public static void Set(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}