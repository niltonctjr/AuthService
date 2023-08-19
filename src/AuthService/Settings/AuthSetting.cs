namespace AuthService.Settings
{
    public class AuthSetting
    {
        public string SecretKey { get; set; }
        public string SecretKeyEmailToken { get; set; }
        public int ExpireInHours { get; set; }
    }
}
