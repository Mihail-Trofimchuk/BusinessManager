

namespace BusinessManager.Model
{
    public static class SaveUser
    {
        private static User user;
        public static User CurrentUser
        {
            get { return user; }
            set { user = value; }
        }

       
    }
}
