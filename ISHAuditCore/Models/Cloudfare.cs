namespace ISHAuditCore.Models;

public class Cloudfare
{
    // 定義 Cloudflare 回應模型
    public class captchaVerificationResponse
    {
        public bool Success { get; set; }
        public string ChallengeTs { get; set; }
        public string Hostname { get; set; }
        public string[] ErrorCodes { get; set; }
    }
    
    
    
}